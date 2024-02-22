namespace UI
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toggleAutoplayerButton = new System.Windows.Forms.ToolStripButton();
            this.loadEngineButton = new System.Windows.Forms.ToolStripButton();
            this.computeButton = new System.Windows.Forms.ToolStripButton();
            this.interruptButton = new System.Windows.Forms.ToolStripButton();
            this.settingsButton = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.webBrowser = new CefSharp.WinForms.ChromiumWebBrowser();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.CanOverflow = false;
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(10, 10);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toggleAutoplayerButton,
            this.loadEngineButton,
            this.computeButton,
            this.interruptButton,
            this.settingsButton});
            this.toolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.toolStrip.Size = new System.Drawing.Size(2373, 47);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip1";
            // 
            // toggleAutoplayerButton
            // 
            this.toggleAutoplayerButton.CheckOnClick = true;
            this.toggleAutoplayerButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toggleAutoplayerButton.Image = ((System.Drawing.Image)(resources.GetObject("toggleAutoplayerButton.Image")));
            this.toggleAutoplayerButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toggleAutoplayerButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toggleAutoplayerButton.Name = "toggleAutoplayerButton";
            this.toggleAutoplayerButton.Size = new System.Drawing.Size(42, 42);
            this.toggleAutoplayerButton.ToolTipText = "Toggle autoplayer";
            this.toggleAutoplayerButton.Click += new System.EventHandler(this.toggleAutoplayerButton_Click);
            // 
            // loadEngineButton
            // 
            this.loadEngineButton.AutoToolTip = false;
            this.loadEngineButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.loadEngineButton.Image = ((System.Drawing.Image)(resources.GetObject("loadEngineButton.Image")));
            this.loadEngineButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.loadEngineButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadEngineButton.Name = "loadEngineButton";
            this.loadEngineButton.Size = new System.Drawing.Size(42, 42);
            this.loadEngineButton.ToolTipText = "Load Engine...";
            this.loadEngineButton.Click += new System.EventHandler(this.loadEngineButton_Click);
            // 
            // computeButton
            // 
            this.computeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.computeButton.Image = ((System.Drawing.Image)(resources.GetObject("computeButton.Image")));
            this.computeButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.computeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.computeButton.Name = "computeButton";
            this.computeButton.Size = new System.Drawing.Size(42, 42);
            this.computeButton.ToolTipText = "Engine compute...";
            this.computeButton.Visible = false;
            this.computeButton.Click += new System.EventHandler(this.computeButton_Click);
            // 
            // interruptButton
            // 
            this.interruptButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.interruptButton.Image = ((System.Drawing.Image)(resources.GetObject("interruptButton.Image")));
            this.interruptButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.interruptButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.interruptButton.Name = "interruptButton";
            this.interruptButton.Size = new System.Drawing.Size(42, 42);
            this.interruptButton.ToolTipText = "Interrupt";
            this.interruptButton.Visible = false;
            this.interruptButton.Click += new System.EventHandler(this.interruptButton_Click);
            // 
            // settingsButton
            // 
            this.settingsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.settingsButton.Image = ((System.Drawing.Image)(resources.GetObject("settingsButton.Image")));
            this.settingsButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.settingsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(42, 42);
            this.settingsButton.ToolTipText = "Settings...";
            this.settingsButton.Click += new System.EventHandler(this.settingsButton_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3});
            this.statusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip.Location = new System.Drawing.Point(0, 1248);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 17, 0);
            this.statusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip.Size = new System.Drawing.Size(2373, 39);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 2;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.toolStripStatusLabel1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripStatusLabel1.Image")));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(147, 32);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.Text = "No engine";
            this.toolStripStatusLabel1.ToolTipText = "Autoplayer";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.toolStripStatusLabel2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripStatusLabel2.Image")));
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(154, 32);
            this.toolStripStatusLabel2.Spring = true;
            this.toolStripStatusLabel2.Text = "Not started";
            this.toolStripStatusLabel2.ToolTipText = "Engine";
            this.toolStripStatusLabel2.Click += new System.EventHandler(this.toolStripStatusLabel2_Click);
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.toolStripStatusLabel3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripStatusLabel3.Image")));
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(133, 32);
            this.toolStripStatusLabel3.Text = "No game";
            this.toolStripStatusLabel3.ToolTipText = "Game";
            // 
            // webBrowser
            // 
            this.webBrowser.ActivateBrowserOnCreation = false;
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(0, 47);
            this.webBrowser.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(2373, 1201);
            this.webBrowser.TabIndex = 3;
            this.webBrowser.AddressChanged += new System.EventHandler<CefSharp.AddressChangedEventArgs>(this.webBrowser_AddressChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(2373, 1287);
            this.Controls.Add(this.webBrowser);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LichBot";
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton loadEngineButton;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripButton toggleAutoplayerButton;
        private System.Windows.Forms.ToolStripButton settingsButton;
        private System.Windows.Forms.ToolStripButton computeButton;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private CefSharp.WinForms.ChromiumWebBrowser webBrowser;
        private ToolStripButton interruptButton;
    }
}

