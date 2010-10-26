using System;
using System.Drawing;
using APE;

namespace Demo
{
	public class GdiPlusRenderer : IRenderer
	{
		private Graphics _g;

		public void InitialiseDeviceContext(object deviceContext)
		{
			_g = (Graphics) deviceContext;
		}

		public void DrawFillRectangle(float x, float y, float w, float h, float angle, float lineThickness, int lineColor, int fillColor)
		{
			_g.TranslateTransform((float) x, (float) y);
			_g.RotateTransform((float) MathUtil.ToDegrees(angle));

			Brush fillBrush = new SolidBrush(Color.FromArgb(fillColor));
			_g.FillRectangle(fillBrush, (float) (-w / 2), (float) (-h / 2), (float) w, (float) h);

			if (lineThickness > 0)
			{
				Pen borderPen = new Pen(Color.FromArgb(lineColor), lineThickness);
				_g.DrawRectangle(borderPen, (float) (-w / 2), (float) (-h / 2), (float) w, (float) h);
			}

			_g.ResetTransform();
		}

		public void DrawFillCircle(float x, float y, float radius, float lineThickness, int lineColor, int fillColor)
		{
			Brush fillBrush = new SolidBrush(Color.FromArgb(fillColor));
			_g.FillEllipse(fillBrush, (float) (x - radius), (float) (y - radius), (float) (radius * 2), (float) (radius * 2));

			DrawCircle(x, y, radius, lineThickness, lineColor);
		}

		public void DrawCircle(float x, float y, float radius, float lineThickness, int lineColor)
		{
			if (lineThickness > 0)
			{
				Pen borderPen = new Pen(Color.FromArgb(lineColor), lineThickness);
				_g.DrawEllipse(borderPen, (float) (x - radius), (float) (y - radius), (float) (radius * 2), (float) (radius * 2));
			}
		}

		public void DrawLine(float angle, float x1, float y1, float x2, float y2, float lineThickness, int lineColor)
		{
			SizeF centre = new SizeF((x2 + x1) / 2, (y2 + y1) / 2);
			_g.TranslateTransform(centre.Width, centre.Height);
			_g.RotateTransform((float) MathUtil.ToDegrees(angle));

			Pen borderPen = new Pen(Color.FromArgb(lineColor), lineThickness);
			_g.DrawLine(
				borderPen,
				(new SizeF(x1, y1) - centre).ToPointF(),
				(new SizeF(x2, y2) - centre).ToPointF());

			_g.ResetTransform();
		}
	}
}
