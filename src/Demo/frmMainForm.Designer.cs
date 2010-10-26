namespace Demo
{
	partial class frmMain
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
			this.components = new System.ComponentModel.Container();
			this.tmrTimer = new System.Windows.Forms.Timer(this.components);
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mniFileExit = new System.Windows.Forms.ToolStripMenuItem();
			this.demoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mniDemoCar = new System.Windows.Forms.ToolStripMenuItem();
			this.mniDemoRobot = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mniHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.nfpWorkArea = new Demo.NonFlickerPanel();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tmrTimer
			// 
			this.tmrTimer.Interval = 30;
			this.tmrTimer.Tick += new System.EventHandler(this.tmrTimer_Tick);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.demoToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(632, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniFileExit});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// mniFileExit
			// 
			this.mniFileExit.Name = "mniFileExit";
			this.mniFileExit.Size = new System.Drawing.Size(152, 22);
			this.mniFileExit.Text = "Exit";
			this.mniFileExit.Click += new System.EventHandler(this.mniFileExit_Click);
			// 
			// demoToolStripMenuItem
			// 
			this.demoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniDemoCar,
            this.mniDemoRobot});
			this.demoToolStripMenuItem.Name = "demoToolStripMenuItem";
			this.demoToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
			this.demoToolStripMenuItem.Text = "Demo";
			// 
			// mniDemoCar
			// 
			this.mniDemoCar.Name = "mniDemoCar";
			this.mniDemoCar.Size = new System.Drawing.Size(152, 22);
			this.mniDemoCar.Text = "Car";
			// 
			// mniDemoRobot
			// 
			this.mniDemoRobot.Name = "mniDemoRobot";
			this.mniDemoRobot.Size = new System.Drawing.Size(152, 22);
			this.mniDemoRobot.Text = "Robot";
			this.mniDemoRobot.Click += new System.EventHandler(this.mniDemoRobot_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniHelpAbout});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// mniHelpAbout
			// 
			this.mniHelpAbout.Name = "mniHelpAbout";
			this.mniHelpAbout.Size = new System.Drawing.Size(107, 22);
			this.mniHelpAbout.Text = "About";
			this.mniHelpAbout.Click += new System.EventHandler(this.mniHelpAbout_Click);
			// 
			// nfpWorkArea
			// 
			this.nfpWorkArea.BackColor = System.Drawing.Color.FromArgb(((int) (((byte) (51)))), ((int) (((byte) (68)))), ((int) (((byte) (51)))));
			this.nfpWorkArea.Dock = System.Windows.Forms.DockStyle.Fill;
			this.nfpWorkArea.Location = new System.Drawing.Point(0, 24);
			this.nfpWorkArea.Name = "nfpWorkArea";
			this.nfpWorkArea.Size = new System.Drawing.Size(632, 322);
			this.nfpWorkArea.TabIndex = 1;
			this.nfpWorkArea.Paint += new System.Windows.Forms.PaintEventHandler(this.nfpWorkArea_Paint);
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.ClientSize = new System.Drawing.Size(632, 346);
			this.Controls.Add(this.nfpWorkArea);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "frmMain";
			this.Text = "APE Demo";
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyUp);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Timer tmrTimer;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem mniFileExit;
		private System.Windows.Forms.ToolStripMenuItem demoToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem mniDemoCar;
		private System.Windows.Forms.ToolStripMenuItem mniDemoRobot;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem mniHelpAbout;
		private NonFlickerPanel nfpWorkArea;
	}
}

