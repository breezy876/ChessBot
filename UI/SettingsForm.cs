using Common.Chess;
using UI;
using pax.uciChessEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace UI
{
    public partial class SettingsForm : Form
    {

        enum ControlType {  Combo, Text, Spin, Check, Button };

        public SettingsForm()
        {
            InitializeComponent();
            LoadSettings();
        }

        public event EventHandler<List<Common.Uci.EngineOption>> EngineSettingsChanged;
        public event EventHandler<string> EngineButtonClicked;

        List<Common.Uci.EngineOption> engineOptions;

        public void Initialize(MainForm parent)
        {
            Owner = parent;
        }

        public void ShowAndCenter()
        {
            this.CenterToParent();
            this.Show();
        }

        #region controls
        public void PopulateControls(List<Common.Uci.EngineOption> engineOpts)
        {
            if (engineOpts.IsNullOrEmpty())
                return;

            engineOptions = engineOpts;

            engineTable.ColumnStyles.Clear();
            engineTable.RowStyles.Clear();

            engineTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,450));
            engineTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 450));

            foreach (var option in engineOpts)
            {
                var control = CreateControl(option);
                var panel = CreateControlPanel(control.Item2, control.Item1.Value);

                engineTable.RowStyles.Add(new RowStyle(SizeType.AutoSize, 30));
                engineTable.Controls.Add(panel);
            }

            engineTable.RowCount = engineOptions.Count;

            //tabControl1.Height = engineTable.Height+40;
        }

        #region private methods
        private Control CreateControlPanel(Control control, ControlType type)
        {
            var panel = new FlowLayoutPanel();

            panel.WrapContents = false;
            panel.Width = 500;
            panel.Height = 30;
            //panel.AutoSizeMode = AutoSizeMode.GrowOnly;
            panel.FlowDirection = FlowDirection.LeftToRight;
        
            if (type != ControlType.Button)
            {
                panel.Controls.Add(new Label() { Text = control.Name, Margin = new Padding(0,5,0,0), Width = 150 });
                panel.Controls.Add(control);
            }
            else
                panel.Controls.Add(control);

            panel.PerformLayout();
            return panel;
        }


        private (ControlType?, Control) CreateSpinControl(Common.Uci.EngineOption option)
        {
            var control = new NumericUpDown();
            control.Width = 60;
            control.Name = option.Name;
            control.Minimum = option.Min;
            control.Maximum = option.Max;
            control.Value = Convert.ToInt32(option.Value);
            control.ValueChanged += SpinControl_ValueChanged;
            return (ControlType.Spin, control);
        }

        private (ControlType?, Control) CreateTextControl(Common.Uci.EngineOption option)
        {
            var control = new TextBox();
            control.Width =250;
            control.Name = option.Name;
            control.Text = (string)option.Value;
            control.TextChanged += TextControl_TextChanged;
            return (ControlType.Text, control);
        }

        private (ControlType?, Control) CreateCheckControl(Common.Uci.EngineOption option)
        {
            var control = new CheckBox();
            control.Name = option.Name;
            control.Checked = (bool)option.Value;
            control.CheckedChanged += CheckControl_Checked;
            return (ControlType.Check, control);
        }

        private (ControlType?, Control) CreateButtonControl(Common.Uci.EngineOption option)
        {
            var control = new Button();
            control.Name = option.Name;
            control.Text = option.Name;
            control.Click += ButtonControl_Clicked;
            return (ControlType.Button, control);
        }

        private (ControlType?, Control) CreateComboControl(Common.Uci.EngineOption option)
        {
            var control = new ComboBox();
            control.Name = option.Name;

            foreach (var val in option.Vars)
            {
                control.Items.Add(new Label() { Text = val });
            }

            control.SelectedValueChanged += ComboControl_Selected;
           return (ControlType.Combo, control);
        }


        private (ControlType?, Control) CreateControl(Common.Uci.EngineOption option)
        {
            switch(option.Type)
            {
                case "spin":
                    return CreateSpinControl(option);
                case "string":
                    return CreateTextControl(option);
                case "check":
                    return CreateCheckControl(option);
                case "button":
                    return CreateButtonControl(option);
                case "combo":
                    return CreateComboControl(option);
            }
            return (null, null);
        }
        #endregion

        #region control events
        private void SpinControl_ValueChanged(object? sender, EventArgs e)
        {
            var control = (NumericUpDown)sender;
            var option = this.engineOptions.FirstOrDefault(o => o.Name == control.Name);
            option.Value = control.Value;
        }

        private void TextControl_TextChanged(object? sender, EventArgs e)
        {
            var control = (TextBox)sender;
            var option = this.engineOptions.FirstOrDefault(o => o.Name == control.Name);
            option.Value = control.Text;
        }

        private void CheckControl_Checked(object? sender, EventArgs e)
        {
            var control = (CheckBox)sender;
            var option = this.engineOptions.FirstOrDefault(o => o.Name == control.Name);
            option.Value = control.Checked;
        }

        private void ComboControl_Selected(object? sender, EventArgs e)
        {
            var control = (ComboBox)sender;
            var option = this.engineOptions.FirstOrDefault(o => o.Name == control.Name);
            option.Value = ((Label)control.SelectedItem).Text;
        }

        private void ButtonControl_Clicked(object? sender, EventArgs e)
        {
            var control = (Button)sender;
            EngineButtonClicked.Invoke(this, control.Name);
        }

        #endregion

        #region UI events
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            SaveSettings();
            EngineSettingsChanged.Invoke(this, engineOptions);//send to engine
            this.Hide();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {


            CenterToParent();
        }
        #endregion

        #region private methods
        private void LoadSettings()
        {
            if (string.IsNullOrEmpty(Properties.Settings.Default.PgnPath))
                Properties.Settings.Default.PgnPath = Path.Combine(Globals.AppPath, "games");

            chkAutodetectTimeControls.Checked = Properties.Settings.Default.UseAutodetectTimeControl;
            chkFixedDepth.Checked = Properties.Settings.Default.UseFixedDepth;
            fixedDepth.Value = Properties.Settings.Default.FixedDepth;
            chkLoadEngineOnStartup.Checked = Properties.Settings.Default.LoadEngineOnStartup;
            chkPonder.Checked = Properties.Settings.Default.EnginePonder;
            chkAutosave.Checked = Properties.Settings.Default.AutoSaveGames;
            txtPgnPath.Text = Properties.Settings.Default.PgnPath;
        }

        public void SaveSettings()
        {
            Properties.Settings.Default.UseAutodetectTimeControl = chkAutodetectTimeControls.Checked;
            Properties.Settings.Default.UseFixedDepth = chkFixedDepth.Checked;
            Properties.Settings.Default.FixedDepth = (int)fixedDepth.Value;
            Properties.Settings.Default.LoadEngineOnStartup = chkLoadEngineOnStartup.Checked;
            Properties.Settings.Default.EnginePonder = chkPonder.Checked;
            Properties.Settings.Default.AutoSaveGames = chkAutosave.Checked;
            Properties.Settings.Default.PgnPath = txtPgnPath.Text;

            UI.Properties.Settings.Default.Save();
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true; 
        }
        #endregion

        #endregion

        private void openFolderButton_Click(object sender, EventArgs e)
        {
            var result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
                txtPgnPath.Text = openFileDialog.FileName;
        }
    }
}
