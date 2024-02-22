
using CefSharp;
using CefSharp.DevTools.CacheStorage;
using CefSharp.DevTools.Debugger;
using CefSharp.DevTools.Runtime;
using CefSharp.WinForms;
using Common;
using Common.Uci;
using Common.Chess;
using Common.Interop;
using UI;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Xml;
using WindowsInput;
using WindowsInput.Native;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using Microsoft.VisualBasic.Devices;
using pax.chess;
using System.Windows.Forms.VisualStyles;

namespace UI
{
    public partial class MainForm : Form
    {
        System.Timers.Timer updateInfoTimer;

        long whiteTime;
        long blackTime;

        KeyboardHook kbHook;

        InputSimulator inputSimulator;

        Player player;
        Player opponent;

        SettingsForm settingsForm;
        LoadEngineForm loadEngineForm;

        UciEngineWrapper engineWrapper;

        string engineMove;
        string engineName;

        ChessSiteJsInvoker jsInvoker;

        bool autoplayerEnabled;


        List<string> moves;

        (
            bool switchSidesVisible,
            bool endVisible,
            bool ourTurn,
            bool oppTurn,
            bool inProgress,
            bool started,
            string move,
            string msgText,
            string whiteUser,
            string blackUser,
            string result,
            DateTime date
            ) gameInfo;

        (bool switchedSides, bool ended, bool ourTurn, bool oppTurn) gameFlags;
        private bool loaded;

        public MainForm()
        {
            inputSimulator = new InputSimulator();
            engineWrapper = new UciEngineWrapper();

            player = Player.White;
            opponent = Player.Black;

            moves = new List<string>();

            CenterToScreen();

            initForms();
            initBrowser();
            InitializeComponent();

            Debug.WriteLine(Globals.AppPath);
        }

        #region private methods

        #region misc
        private async void Initialize()
        {
            initKbHook();

            initUpdateInfoTimer();
            initTasks();

            if (UI.Properties.Settings.Default.LoadEngineOnStartup)
                await initAndLoadEngine();
        }

        private void FreeResources()
        {
            loadEngineForm.Dispose();
            settingsForm.Dispose();
            this.Dispose();
            kbHook.Uninstall();
            TaskManager.StopAll();
            engineWrapper.Exit();
        }

        private void initKbHook()
        {
            var handler = new KeyHandler();
            handler.WindowHandle = (int)this.Handle;
            handler.KeyPressed += Handler_KeyPressed;
            handler.KeyReleased += Handler_KeyReleased; 

            kbHook = new KeyboardHook(handler);
            kbHook.Install();
        }

        private void initForms()
        {
            initAndShowSettingsForm(false);
            initAndShowLoadEngineForm(false);
        }

        private void initAndShowSettingsForm(bool show = true)
        {
            if (settingsForm != null)
            {
                settingsForm.Dispose();
                settingsForm = null;
            }

            settingsForm = new SettingsForm();
            settingsForm.Initialize(this);
            settingsForm.EngineSettingsChanged += SettingsForm_EngineSettingsChanged;
            settingsForm.EngineButtonClicked += SettingsForm_EngineButtonClicked;

            settingsForm.PopulateControls(engineWrapper.Options);

            if (show)
                settingsForm.ShowAndCenter();
        }

        private void initAndShowLoadEngineForm(bool show = true)
        {
            if (loadEngineForm != null)
            {
                loadEngineForm.Dispose();
                loadEngineForm = null;
            }

            loadEngineForm = new LoadEngineForm();
            loadEngineForm.Initialize(this);
            loadEngineForm.EngineChosen += LoadEngineForm_EngineChosen;
            if (show)
                loadEngineForm.ShowAndCenter();
        }

        private void initBrowser()
        {
            CefSettings settings = new CefSettings();
            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            settings.CachePath = path;

            Cef.Initialize(settings);

        }

        private async System.Threading.Tasks.Task initAndLoadEngine()
        {
            engineWrapper.ComputeFinished += EngineWrapper_ComputeFinished;
            engineWrapper.EngineStarted += EngineWrapper_EngineStarted;

            if (!string.IsNullOrEmpty(UI.Properties.Settings.Default.EnginePath))
            {
                await engineWrapper.Start(UI.Properties.Settings.Default.EnginePath);
                UI.Properties.Settings.Default.EngineName = engineWrapper.EngineName;
                if (settingsForm != null)
                    settingsForm.PopulateControls(engineWrapper.Options);
            }

            engineName = UI.Properties.Settings.Default.EngineName;
        }

        private void initUpdateInfoTimer()
        {
            updateInfoTimer = new System.Timers.Timer();
            updateInfoTimer.Interval = 50;
            updateInfoTimer.Elapsed += UpdateInfoTimer_Elapsed;
            updateInfoTimer.Enabled = true;
        }


        private void initTasks()
        {
            jsInvoker = new ChessSiteJsInvoker(webBrowser);

            TaskManager.Add(TaskFactory.Create(BrowserJsTask,10
                , "browser"));
        }

        private void ActivateBrowser()
        {
            this.Activate();
            webBrowser.Focus();
        }

        private void SendBrowserMove(string moveLong)
        {
            this.Invoke(new Action(() =>
            {
                ActivateBrowser();
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
                inputSimulator.Keyboard.TextEntry(moveLong);
            }));

        }


        private async void Compute()
        {
            engineMove = "Calculating...";

            this.Invoke(
        new Action(() =>
        {
            computeButton.Enabled = false;
            interruptButton.Visible = true;
        }));

            ResetFlags();

            await engineWrapper.UpdatePosition(moves);
            await engineWrapper.Go(whiteTime, blackTime, UI.Properties.Settings.Default.FixedDepth, UI.Properties.Settings.Default.UseFixedDepth);
        }

        private void ToggleAutoplayer()
        {
            autoplayerEnabled ^= true;

            ResetFlags();

            this.Invoke(
           new Action(() => {
               toggleAutoplayerButton.Checked = autoplayerEnabled;
           }));
           
        }

        private async void Interrupt()
        {
            await engineWrapper.Stop();
        }

        private void ResetFlags()
        {
            gameFlags.ourTurn = false;
            gameFlags.oppTurn = false;
            gameFlags.ended = false;
            gameFlags.switchedSides = false;
        }

        private void showEngineControls()
        {
            this.Invoke(
                new Action(() => {
                    computeButton.Visible = true;
                    //moveButton.Visible = true;
                }));
        }

        private void hideEngineControls()
        {
            this.Invoke(
                new Action(() => {
                    computeButton.Visible = false;
                    interruptButton.Visible = false;
                    //moveButton.Visible = false;
                }));
        }
        #endregion


        #region tasks
        private string GetOpponentUser()
        {
            return (opponent == Player.White) ? gameInfo.whiteUser : gameInfo.blackUser;
        }

        private bool IsOurTurn()
        {
            return (player == Player.White && moves.Count % 2 == 0) || (player == Player.Black && moves.Count % 2 == 1);
        }

        private bool IsOpponentTurn()
        {
            return (opponent == Player.White && moves.Count % 2 == 0) || (opponent == Player.Black && moves.Count % 2 == 1);
        }

        private async void SaveGame()
        {
            var pgnInfo = new PgnInfo()
            {
                Result = gameInfo.result,
                WhitePlayer = gameInfo.whiteUser,
                BlackPLayer = gameInfo.blackUser,
                Site = "lichess",
                Moves = moves.ToArray(),
                Date = DateTime.Now
            };

            string oppUser = GetOpponentUser();
            oppUser = string.IsNullOrEmpty(oppUser) ? "Opponent" : oppUser;

            await PgnWriter.Write(pgnInfo, Path.Combine(Globals.AppPath, "games", oppUser, DateTime.Now.ToString("dd-MM-yyyy"), $"{DateTime.Now.ToString("hh-mm")}.pgn"));
         }

        private async void GameTask()
        {
            if (gameInfo.endVisible && !gameFlags.ended)
            {
                engineMove = "";
                gameInfo.move = "";

                gameFlags.ended = true;
                gameFlags.switchedSides = false;

                await ExecGetResultJs();

                if (UI.Properties.Settings.Default.AutoSaveGames)
                    SaveGame();

                this.Invoke(new Action(() => { statusStrip.Items[1].Text = "Game ended"; }));
                System.Diagnostics.Debug.WriteLine("LichBot- game ended");

                hideEngineControls();
            }

            else if (gameInfo.switchSidesVisible && !gameFlags.switchedSides)
            {
                await ExecGetUserJs();

                if (engineWrapper.Started)
                this.Invoke(
                  new Action(() =>
                  {
                      computeButton.Visible = true;
                      interruptButton.Visible = false;
                  }));

                moves = new List<string>();

                //this.Invoke(new Action(() => { statusStrip.Items[1].Text = "Switched sides: " + player.ToString(); }));
                System.Diagnostics.Debug.WriteLine("LichBot- switched sides");

                engineWrapper.Player = player;
                engineWrapper.Opponent = opponent;
     
                if (autoplayerEnabled && engineWrapper.Started)
                {
                    await engineWrapper.NewGame();
                    if (player == Player.White)
                       await engineWrapper.Go(whiteTime, blackTime, UI.Properties.Settings.Default.FixedDepth, UI.Properties.Settings.Default.UseFixedDepth);
                }

                gameFlags.switchedSides = true;
                gameFlags.ended = false;
            }
    

            if (gameInfo.ourTurn && !gameFlags.ourTurn)
            {

                System.Diagnostics.Debug.WriteLine("LichBot- your turn");

                if (!string.IsNullOrEmpty(gameInfo.move) && autoplayerEnabled && engineWrapper.Started)
                {
                    System.Diagnostics.Debug.WriteLine("LichBot- Sending move to engine: " + gameInfo.move);
                    engineMove = "Calculating...";
                    await engineWrapper.UpdatePosition(moves);
                    await engineWrapper.Go(whiteTime, blackTime, UI.Properties.Settings.Default.FixedDepth, UI.Properties.Settings.Default.UseFixedDepth);
                    //await engineWrapper.SendMove(gameInfo.move, whiteTime, blackTime);
                }
                gameFlags.ourTurn = true;
                gameFlags.oppTurn = false;
            }

            else if (gameInfo.oppTurn && !gameFlags.oppTurn)
            {
                //string lastMove = "";

                //if (moves.Count > 0)
                //    lastMove = moves.Last();

                System.Diagnostics.Debug.WriteLine("LichBot- opponent's turn");

                gameFlags.oppTurn = true;
                gameFlags.ourTurn = false;
            }
        }

        #region info
        private string GetEngineStatusText()
        {
            switch (engineWrapper.Status)
            {
                case UciEngineWrapper.EngineStatus.NotStarted:
                    return "No engine      ";
                case UciEngineWrapper.EngineStatus.Starting:
                    return "Starting...      ";
                case UciEngineWrapper.EngineStatus.Started:
                    return $"Started   {engineWrapper.EngineName}   ";
                case UciEngineWrapper.EngineStatus.Stopped:
                    return "Stopped      ";
            }
            return "";
        }

        private void UpdateInfoTask()
        {
            string engineMoveText = !string.IsNullOrEmpty(engineMove) ? $"Move: {engineMove}" : "";

            string engineStatusText = $"{GetEngineStatusText()}   {engineMoveText}   ";
            string autoplayerStatusText = $"{(autoplayerEnabled ? $"Running      " : "Not running      ")}";

            string turnText = gameInfo.started ? $"{(gameInfo.ourTurn ? player.ToString() : opponent.ToString())}'s turn      " : "";

            string browserMoveText = !string.IsNullOrEmpty(gameInfo.move) ? $"Move: {gameInfo.move}" : "";

            string browserStatusText = gameInfo.inProgress ? $"{(gameInfo.started ? "Game in progress      " : "Game ended      ")}      {turnText}      {browserMoveText}" : "No game      ";

            this.Invoke(new Action(() =>
            {
                statusStrip.Items[0].Text = engineStatusText;
                statusStrip.Items[1].Text = autoplayerStatusText;
                statusStrip.Items[2].Text = browserStatusText;
            }));
        }
        #endregion

        #region browser js
        private async System.Threading.Tasks.Task ExecGetResultJs()
        {
            var result = await jsInvoker.InvokeAsync(ChessSiteJsInvoker.ChessSiteScriptType.Result);
            if (result == null)
                return;

            gameInfo.result = (string)result;
        }

        private async System.Threading.Tasks.Task ExecGetUserJs()
        {
            string topUser = "", bottomUser = "";

            var result = await jsInvoker.InvokeAsync(ChessSiteJsInvoker.ChessSiteScriptType.Users);
            if (result == null)
                return;

            var items = (List<object>)result;

            topUser = (string)items[0];
            bottomUser = (string)items[1];

            if (player == Player.White)
                gameInfo.whiteUser = bottomUser;
            else
                gameInfo.blackUser = bottomUser;

            if (opponent == Player.White)
                gameInfo.whiteUser = topUser;
            else
                gameInfo.blackUser = topUser;

        }



        private string GetLastOpponentMove()
        {
            if (moves.Count > 0)
                return moves.Last();
            return "";
        }

        private async System.Threading.Tasks.Task ExecMoveHistoryJs()
        {
            var result = await jsInvoker.InvokeAsync(ChessSiteJsInvoker.ChessSiteScriptType.Moves);
            if (result == null)
                return;

            dynamic items = result;
            moves = new List<string>();
            foreach (var item in items)
            {
                moves.Add(item as string);
            }

            if (moves.Count > 0)
                gameInfo.move = GetLastOpponentMove();


            //our turn or opponent?
            gameInfo.ourTurn = IsOurTurn();
            gameInfo.oppTurn = IsOpponentTurn();

        }


        private async System.Threading.Tasks.Task ExecGameProgressJs()
        {
            var result = await jsInvoker.InvokeAsync(ChessSiteJsInvoker.ChessSiteScriptType.GameInProgress);
            if (result == null)
                return;

            gameInfo.inProgress = (bool)result;
        }

        private async System.Threading.Tasks.Task ExecGameEndedJs()
        {
            var result = await jsInvoker.InvokeAsync(ChessSiteJsInvoker.ChessSiteScriptType.GameEnded);
            if (result == null)
                return;

            if ((bool)result)
            {
                gameInfo.endVisible = true;
                gameInfo.started = false;
                gameInfo.move = "";
            }
            else
                gameInfo.endVisible = false;
        }

        private async System.Threading.Tasks.Task ExecGameStartedJs()
        {

            //switching sides?
            var result = (await jsInvoker.InvokeAsync(ChessSiteJsInvoker.ChessSiteScriptType.GameStarted));
            if (result == null)
                return;

            var items = (List<object>)result;

            gameInfo.started = (bool)items[0];
            gameInfo.switchSidesVisible = (bool)items[0];


            string color = (string)(await jsInvoker.InvokeAsync(ChessSiteJsInvoker.ChessSiteScriptType.Player));


            if (color == "white")
            {
                player = Player.White;
                opponent = Player.Black;
            }
            else
            {
                player = Player.Black;
                opponent = Player.White;
            }

            if ((bool)items[1])
            {
                gameInfo.ourTurn = true;
                gameInfo.oppTurn = false;
            }
            else
            {
                gameInfo.ourTurn = false;
                gameInfo.oppTurn = true;
            }

        }

        private async System.Threading.Tasks.Task ExecClockJs()
        {
            var result = (List<object>)(await jsInvoker.InvokeAsync(ChessSiteJsInvoker.ChessSiteScriptType.Clocks));
            if (result == null)
                return;

            whiteTime = LichessUtilities.GetMilliSeconds(result[0] as string);
            blackTime = LichessUtilities.GetMilliSeconds(result[1] as string);
        }

        private async void BrowserJsTask()
        {
            if (!TaskManager.IsRunning("browser") || 
                webBrowser == null 
                    || webBrowser.IsDisposed 
                    || !webBrowser.CanExecuteJavascriptInMainFrame)
                return;

            await ExecGameProgressJs();

            if (!gameInfo.inProgress)
                return;

            if (engineWrapper.Started)
                showEngineControls();

            //if (!TaskManager.IsRunning("game"))
            //    TaskManager.Start("game");

            await ExecMoveHistoryJs();
            await ExecGameStartedJs();
            await ExecGameEndedJs();
            await ExecClockJs();

            GameTask();
        }
        #endregion

        #endregion

        #region events

        #region UI
        private void MainForm_Load(object sender, EventArgs e)
        {
  
            webBrowser.LoadUrl("http://lichess.org");
        }


        private async void newGameButton_Click(object sender, EventArgs e)
        {
            await engineWrapper.NewGame();
        }


        private void toggleAutoplayerButton_Click(object sender, EventArgs e)
        {
            ResetFlags();
            autoplayerEnabled = toggleAutoplayerButton.Checked;
        }

        private void computeButton_Click(object sender, EventArgs e)
        {
            Compute();
        }

        private void interruptButton_Click(object sender, EventArgs e)
        {
            Interrupt();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                FreeResources();
                ProcessManager.Exit(Path.GetFileNameWithoutExtension(Application.ExecutablePath));
            }
            catch (Exception ex)
            {

            }
        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            initAndShowSettingsForm();
        }

        private void loadEngineButton_Click(object sender, EventArgs e)
        {
            initAndShowLoadEngineForm();
        }

        private void webBrowser_AddressChanged(object sender, AddressChangedEventArgs e)
        {
            jsInvoker.CurrentHost = new Uri(e.Address).Host.Replace("www.","");

            if (!jsInvoker.IsChessHost && TaskManager.IsRunning("browser"))
                TaskManager.Start("browser");
        }
        #endregion

        #region engine
        private void EngineWrapper_ComputeFinished(object sender, string move)
        {
            //autoplayerEnabled = false;

            if (gameFlags.ended || !gameInfo.inProgress)
                return;

            if (string.IsNullOrEmpty(move))
                engineMove = "Engine error!";
            else
            {
                engineMove = move;
                SendBrowserMove(move);
            }

            this.Invoke(
                    new Action(() =>
                    {
                        computeButton.Enabled = true;
                        interruptButton.Visible = false;

                    }));
        }


        private void EngineWrapper_EngineStarted(object sender, EventArgs e)
        {
           
        }
        #endregion

        private void Handler_KeyPressed(object? sender, int keyCode)
        {
            switch ((Keys)keyCode)
            {
                case Keys.F1:
                    ToggleAutoplayer();
                    break;
                case Keys.F2:
                    Compute();
                    break;
                case Keys.F3:
                    Interrupt();
                    break;
                case Keys.F4:
                    ToggleTimeControl();
                    break;
            }
        }

        private void ToggleTimeControl()
        {
            if (UI.Properties.Settings.Default.UseFixedDepth)
            {
                UI.Properties.Settings.Default.UseFixedDepth = false;
                UI.Properties.Settings.Default.UseAutodetectTimeControl = true;
            }
            else if (UI.Properties.Settings.Default.UseAutodetectTimeControl)
            {
                UI.Properties.Settings.Default.UseFixedDepth = true;
                UI.Properties.Settings.Default.UseAutodetectTimeControl = false;
            }

            if (settingsForm != null)
                settingsForm.SaveSettings();
        }

        private void Handler_KeyReleased(object? sender, int e)
        {

        }

        private async void SettingsForm_EngineButtonClicked(object? sender, string name)
        {
            await engineWrapper.SetOption(name, null);
        }

        private async void SettingsForm_EngineSettingsChanged(object? sender, List<Common.Uci.EngineOption> options)
        {
            await engineWrapper.SaveOptions(options);
            await engineWrapper.UpdateOptions();
        }

        private async void LoadEngineForm_EngineChosen(object? sender, string path)
        {
            UI.Properties.Settings.Default.EnginePath = path;
            await System.Threading.Tasks.Task.Run(() => UI.Properties.Settings.Default.Save());
          await initAndLoadEngine();
        }

        private void UpdateInfoTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            UpdateInfoTask();
        }


        #endregion

        #endregion

        private void MainForm_Activated(object sender, EventArgs e)
        {
            if (loaded)
                return;
            else
                loaded = true;

            Initialize();
        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {

        }
    }
}
