using CefSharp.WinForms.Internals;
using UI;
using pax.uciChessEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace UI
{
    public partial class LoadEngineForm : Form
    {
        public LoadEngineForm()
        {
            InitializeComponent();

            enginePath.Text = Properties.Settings.Default.EnginePath;

           
        }

        public event EventHandler<string> EngineChosen;

        #region public methods
        public void Initialize(MainForm parent)
        {
            Owner = parent;
        }

        public void ShowAndCenter()
        {
            this.CenterToParent();
            this.Show();
        }
        #endregion

        #region private methods

        #region UI events
        private void okButton_Click(object sender, EventArgs e)
        {
            EngineChosen.Invoke(this, enginePath.Text);
            this.Hide();
        }


        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void openFolderButton_Click(object sender, EventArgs e)
        {
            var result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
                enginePath.Text = openFileDialog.FileName;
        }

        private void LoadEngineForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void LoadEngineForm_Load(object sender, EventArgs e)
        {
 
        }
        #endregion

        #endregion
    }
}
