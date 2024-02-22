namespace UI
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.openFolderButton = new System.Windows.Forms.Button();
            this.chkAutosave = new System.Windows.Forms.CheckBox();
            this.txtPgnPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkPonder = new System.Windows.Forms.CheckBox();
            this.chkLoadEngineOnStartup = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.fixedDepth = new System.Windows.Forms.NumericUpDown();
            this.chkFixedDepth = new System.Windows.Forms.CheckBox();
            this.chkAutodetectTimeControls = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.engineTable = new System.Windows.Forms.TableLayoutPanel();
            this.applyButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fixedDepth)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1302, 707);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1294, 669);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.openFolderButton);
            this.groupBox3.Controls.Add(this.chkAutosave);
            this.groupBox3.Controls.Add(this.txtPgnPath);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(6, 172);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(728, 82);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Games";
            // 
            // openFolderButton
            // 
            this.openFolderButton.Location = new System.Drawing.Point(665, 26);
            this.openFolderButton.Name = "openFolderButton";
            this.openFolderButton.Size = new System.Drawing.Size(44, 38);
            this.openFolderButton.TabIndex = 5;
            this.openFolderButton.Text = "...";
            this.openFolderButton.UseVisualStyleBackColor = true;
            this.openFolderButton.Click += new System.EventHandler(this.openFolderButton_Click);
            // 
            // chkAutosave
            // 
            this.chkAutosave.Location = new System.Drawing.Point(17, 30);
            this.chkAutosave.Name = "chkAutosave";
            this.chkAutosave.Size = new System.Drawing.Size(137, 29);
            this.chkAutosave.TabIndex = 0;
            this.chkAutosave.Text = "Save to PGN";
            this.chkAutosave.UseVisualStyleBackColor = true;
            // 
            // txtPgnPath
            // 
            this.txtPgnPath.Location = new System.Drawing.Point(230, 30);
            this.txtPgnPath.Name = "txtPgnPath";
            this.txtPgnPath.Size = new System.Drawing.Size(429, 31);
            this.txtPgnPath.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(178, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Path";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkPonder);
            this.groupBox2.Controls.Add(this.chkLoadEngineOnStartup);
            this.groupBox2.Location = new System.Drawing.Point(6, 89);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(728, 77);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Engine";
            // 
            // chkPonder
            // 
            this.chkPonder.Location = new System.Drawing.Point(187, 30);
            this.chkPonder.Name = "chkPonder";
            this.chkPonder.Size = new System.Drawing.Size(133, 29);
            this.chkPonder.TabIndex = 2;
            this.chkPonder.Text = "Ponder";
            this.chkPonder.UseVisualStyleBackColor = true;
            // 
            // chkLoadEngineOnStartup
            // 
            this.chkLoadEngineOnStartup.Location = new System.Drawing.Point(17, 30);
            this.chkLoadEngineOnStartup.Name = "chkLoadEngineOnStartup";
            this.chkLoadEngineOnStartup.Size = new System.Drawing.Size(164, 29);
            this.chkLoadEngineOnStartup.TabIndex = 0;
            this.chkLoadEngineOnStartup.Text = "Load on startup";
            this.chkLoadEngineOnStartup.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.fixedDepth);
            this.groupBox1.Controls.Add(this.chkFixedDepth);
            this.groupBox1.Controls.Add(this.chkAutodetectTimeControls);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(728, 77);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Time Controls";
            // 
            // fixedDepth
            // 
            this.fixedDepth.Location = new System.Drawing.Point(365, 32);
            this.fixedDepth.Name = "fixedDepth";
            this.fixedDepth.Size = new System.Drawing.Size(100, 31);
            this.fixedDepth.TabIndex = 2;
            // 
            // chkFixedDepth
            // 
            this.chkFixedDepth.AutoSize = true;
            this.chkFixedDepth.Location = new System.Drawing.Point(187, 32);
            this.chkFixedDepth.Name = "chkFixedDepth";
            this.chkFixedDepth.Size = new System.Drawing.Size(133, 29);
            this.chkFixedDepth.TabIndex = 1;
            this.chkFixedDepth.Text = "Fixed Depth";
            this.chkFixedDepth.UseVisualStyleBackColor = true;
            // 
            // chkAutodetectTimeControls
            // 
            this.chkAutodetectTimeControls.AutoSize = true;
            this.chkAutodetectTimeControls.Location = new System.Drawing.Point(17, 30);
            this.chkAutodetectTimeControls.Name = "chkAutodetectTimeControls";
            this.chkAutodetectTimeControls.Size = new System.Drawing.Size(126, 29);
            this.chkAutodetectTimeControls.TabIndex = 0;
            this.chkAutodetectTimeControls.Text = "Autodetect";
            this.chkAutodetectTimeControls.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.engineTable);
            this.tabPage2.Location = new System.Drawing.Point(4, 34);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1294, 669);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Engine";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // engineTable
            // 
            this.engineTable.AutoSize = true;
            this.engineTable.ColumnCount = 2;
            this.engineTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.engineTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.engineTable.Location = new System.Drawing.Point(6, 6);
            this.engineTable.Name = "engineTable";
            this.engineTable.RowCount = 1;
            this.engineTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.engineTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.engineTable.Size = new System.Drawing.Size(0, 0);
            this.engineTable.TabIndex = 0;
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(1084, 725);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(112, 34);
            this.applyButton.TabIndex = 1;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(1202, 725);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(112, 34);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1324, 768);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";

            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fixedDepth)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TableLayoutPanel engineTable;
        private Button applyButton;
        private Button cancelButton;
        private GroupBox groupBox1;
        private CheckBox chkAutodetectTimeControls;
        private CheckBox chkFixedDepth;
        private NumericUpDown fixedDepth;
        private GroupBox groupBox3;
        private CheckBox chkAutosave;
        private GroupBox groupBox2;
        private CheckBox chkPonder;
        private CheckBox chkLoadEngineOnStartup;
        private Button openFolderButton;
        private TextBox txtPgnPath;
        private Label label1;
        private OpenFileDialog openFileDialog;
    }
}