
using Chess;
using Common.Chess;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using pax.chess;
using pax.uciChessEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Common.Uci
{
    public class UciEngineWrapper
    {
        public enum EngineStatus { NotStarted, Starting, Started, Stopped }

        Engine engine;

        List<string> moves;

        string engineConfigFilePath;


        public event EventHandler<string> ComputeFinished;
        public event EventHandler EngineStarted;


        public UciEngineWrapper()
        {
            moves = new List<string>();
        }


        public List<Uci.EngineOption> Options { get; private set; }
        public EngineStatus Status { get; private set; }

        public bool Started => Status == EngineStatus.Started;

        public string EngineName { get; private set; }

        public string EnginePath { get; private set; }

        public Player Player { get; set; }
        public Player Opponent { get; set; }


        #region public methods

        public async Task Start(string enginePath)
        {
            EnginePath = enginePath;
            EngineName = Path.GetFileNameWithoutExtension(enginePath);

            engineConfigFilePath = Path.Combine(Path.GetDirectoryName(enginePath), $"{EngineName}.json");

            Status = EngineStatus.Starting;
            engine = new Engine(EngineName, enginePath);
            engine.Status.MoveReady += Status_MoveReady;

            await engine.Start();
            await engine.IsReady();

            var options = await engine.GetOptions();
          

            Status = EngineStatus.Started;
            Options = CreateOptions(await engine.GetOptions());//populate with default options

            if (File.Exists(engineConfigFilePath))//load engine options json
            {
                await LoadOptions();
                await UpdateOptions();
            }

            EngineStarted.Invoke(this, EventArgs.Empty);
        }

        public async Task Stop()
        {
            //engine.Status.MoveReady += (o, e) =>
            //{
            //    Status_MoveReady(o, e);
            //};

            if (!Started)
                return;

            var info = await engine.GetStopInfo();

        }

        public void Exit()
        {
            if (!Started)
                return;

            ProcessManager.Exit(EngineName);

            Status = EngineStatus.Stopped;
        }

        public async Task SetOption(string name, object value)
        {
            if (!Started)
                return;

            await engine.SetOption(name, value);
        }

        public async Task UpdateOptions()
        {
            if (!Started)
                return;

            foreach (var option in Options)
            {
                await engine.SetOption(option.Name, option.Value);
            }
        }

        public async Task LoadOptions()
        {
            try
            {
                string json = await File.ReadAllTextAsync(engineConfigFilePath);
                Options = JsonConvert.DeserializeObject<List<Uci.EngineOption>>(json);
            }
            catch (Exception ex)
            {

            }
        }

        public async Task SaveOptions(List<Uci.EngineOption> options)
        {
            try
            {
                Options = options;
                string json = JsonConvert.SerializeObject(Options);
                await File.WriteAllTextAsync(engineConfigFilePath, json);
            }
            catch (Exception ex)
            {

            }
        }

        public async Task NewGame()
        {
            if (!Started)
                return;

            ChessHelpers.ResetBoard();
            moves.Clear();
            await engine.Send("ucinewgame");
            await engine.Send("position startpos");
        }

        public async Task Go(
            long whiteTime,
            long blackTime,
            int depth,
            bool useFixedDepth = false)
        {
            if (!Started)
                return;

            if (useFixedDepth)
                await engine.Send($"go depth {depth}");
            else
                await engine.Send($"go wtime {whiteTime} btime {blackTime}");
        }

        public async Task SendMove(
            string moveSAN,
            long whiteTime,
            long blackTime,
            int depth,
            bool useFixedDepth = false)
        {
            if (!Started)
                return;

            string movelong = ChessHelpers.GetLong(moveSAN, Opponent);
            if (string.IsNullOrEmpty(movelong))
                return;

            bool isValid = _MakeMoveIfValid(moveSAN);

            if (!isValid)
                return;

            moves.Add(movelong);

            string moveStr = moves.ToArray().ToSpaced();

            //await Stop();
            await engine.Send($"position startpos moves {moveStr}");
            await Go(whiteTime, blackTime, depth, useFixedDepth);

        }

        public async Task<List<string>> UpdatePosition(List<string> sanMoves)
        {
            if (!Started)
                return null;

            try
            {
                ChessHelpers.ResetBoard();
                moves = ChessHelpers.MakeAllMoves(sanMoves);

                string moveStr = moves.Where(m => !string.IsNullOrEmpty(m)).ToArray().ToSpaced();
                await engine.Send($"position startpos moves {moveStr.Replace("=", "")}");
                return moves;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region private methods
        private void Status_MoveReady(object? sender, MoveEventArgs e)
        {
            if (e.Move == null)
            {
                ComputeFinished.Invoke(this, "");
                return;
            }

            string move = GetMove(e.Move);

            if (!ChessHelpers.IsValidMove(move))
            {
                ComputeFinished.Invoke(this, "");
                return;
            }

            moves.Add(move);
            ChessHelpers.MakeMove(move);
            ComputeFinished.Invoke(this, move);
        }

        private string FixPromote(string move)
        {
            if (move.Length < 5)
                return move;
            return $"{move.Substring(0, move.Length - 1)}={move.Substring(move.Length - 1, 1).ToUpper()}";
        }

        private string GetMove(EngineMove move)
        {
            if (move == null)
                return "";

            return FixPromote(move.ToString());
        }


        private bool _MakeMoveIfValid(string san)
        {
            var move = ChessHelpers.ParseMove(san, Opponent);
            if (move == null)
                return false;

            if (ChessHelpers.IsValidMove(san))
            {
                ChessHelpers.MakeMove(san);
                return true;
            }

            return false;
        }

        private List<Uci.EngineOption> CreateOptions(List<pax.uciChessEngine.EngineOption> engineOptions)
        {
            return engineOptions.Select(o => new Uci.EngineOption()
            {
                Min = o.Min,
                Max = o.Max,
                Default = o.Default,
                Value = o.Value,
                Name = o.Name,
                Vars = o.Vars,
                Type = o.Type
            }).ToList();
        }

        #endregion
    }
}

