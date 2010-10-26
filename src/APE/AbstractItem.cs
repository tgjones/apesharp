/*
Copyright (c) 2006, 2007 Alec Cove

Permission is hereby granted, free of charge, to any person obtaining a copy of this 
software and associated documentation files (the "Software"), to deal in the Software 
without restriction, including without limitation the rights to use, copy, modify, 
merge, publish, distribute, sublicense, and/or sell copies of the Software, and to 
permit persons to whom the Software is furnished to do so, subject to the following 
conditions:

The above copyright notice and this permission notice shall be included in all copies 
or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE 
OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

Converted to C# by Tim Jones tim@jones.name
*/

using System;

namespace APE
{
	public abstract class AbstractItem
	{
		#region Fields

		private bool _visible;
		private bool _alwaysRepaint;

		internal float _lineThickness;
		internal int _lineColor;
		internal int _fillColor;

		#endregion

		#region Properties

		public bool AlwaysRepaint
		{
			get { return _alwaysRepaint; }
			set { _alwaysRepaint = value; }
		}

		public bool Visible
		{
			get { return _visible; }
			set { _visible = value; }
		}

		#endregion

		#region Constructor

		public AbstractItem()
		{
			_visible = true;

			// TJ - changed to default to true, in C# versione we always need to repaint
			_alwaysRepaint = true;
		}

		#endregion

		#region Methods

		public virtual void Init() { }

		public abstract void Paint(IRenderer renderer);

		public void SetStyle(float lineThickness, int lineColor, int fillColor)
		{
			SetLine(lineThickness, lineColor);
			SetFill(fillColor);
		}

		public void SetStyle(float lineThickness, int lineColor)
		{
			SetStyle(lineThickness, lineColor, unchecked((int) 0xFFFFFFFF));
		}

		public void SetLine(float lineThickness, int lineColor)
		{
			_lineThickness = lineThickness;
			_lineColor = lineColor;
		}

		public void SetFill(int fillColor)
		{
			_fillColor = fillColor;
		}

		#endregion
	}
}
