using System;
using System.Windows.Forms;

namespace Demo
{
	public class NonFlickerPanel : System.Windows.Forms.Panel
	{
		public NonFlickerPanel()
		{
			SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
			UpdateStyles();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

			base.OnPaint(e);
		}
	}
}
