using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using APE;

namespace Demo
{
	public partial class frmMain : Form
	{
		private AbstractDemo _demo;

		public frmMain()
		{
			InitializeComponent();

			_demo = new CarDemo.CarDemo();
		}

		private void tmrTimer_Tick(object sender, EventArgs e)
		{
			_demo.Run();
			nfpWorkArea.Invalidate();
		}

		private void frmMain_KeyDown(object sender, KeyEventArgs e)
		{
			_demo.HandleKeyDown(e);
		}

		private void frmMain_KeyUp(object sender, KeyEventArgs e)
		{
			_demo.HandleKeyUp(e);
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			tmrTimer.Enabled = true;
		}

		private void mniHelpAbout_Click(object sender, EventArgs e)
		{
			MessageBox.Show(this,
				"APE Engine 0.45a\nOriginal ActionScript version by Alec Cove\nC# port by Tim Jones",
				"APE Engine Demo",
				MessageBoxButtons.OK,
				MessageBoxIcon.Information);
		}

		private void nfpWorkArea_Paint(object sender, PaintEventArgs e)
		{
			GdiPlusRenderer renderer = new GdiPlusRenderer();
			renderer.InitialiseDeviceContext(e.Graphics);
			_demo.Paint(renderer);
		}

		private void mniDemoRobot_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Not yet completed");
			return;

			tmrTimer.Enabled = false;
			_demo = new RobotDemo.RobotDemo();
			tmrTimer.Enabled = true;
		}

		private void mniFileExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
	}
}