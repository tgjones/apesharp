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
	public class RectangleParticle : AbstractParticle
	{
		private float[] _extents = new float[2];
		private float _radian;

		public Vector[] axes = new Vector[2];

		public float Radian
		{
			get { return _radian; }
			set
			{
				_radian = value;
				setAxes(value);
			}
		}

		public float Angle
		{
			get { return MathUtil.ToDegrees(this.Radian); }
			set { this.Radian = MathUtil.ToRadians(value); }
		}

		public float Width
		{
			get { return _extents[0] * 2; }
			set { _extents[0] = value / 2; }
		}

		public float Height
		{
			get { return _extents[1] * 2; }
			set { _extents[1] = value / 2; }
		}

		public RectangleParticle(float x, float y, float width, float height, float rotation,
									 bool fixed_, float mass, float elasticity, float friction)
			: base(x, y, fixed_, mass, elasticity, friction)
		{
			_extents[0] = width / 2;
			_extents[1] = height / 2;

			axes[0] = new Vector(0, 0);
			axes[1] = new Vector(0, 0);

			this.Radian = rotation;
		}

		public RectangleParticle(float x, float y, float width, float height, float rotation, bool fixed_)
			: this(x, y, width, height, rotation, fixed_, 1, 0.3f, 0)
		{

		}

		public RectangleParticle(float x, float y, float width, float height, float rotation)
			: this(x, y, width, height, rotation, false)
		{

		}

		public override void Paint(IRenderer renderer)
		{
			if (!this.Visible)
				return;
			int w = (int) _extents[0] * 2;
			int h = (int) _extents[1] * 2;

			renderer.DrawFillRectangle(curr.X, curr.Y, w, h, this.Radian,
				_lineThickness, _lineColor, _fillColor);
		}

		public override int ParticleType
		{
			get { return 1; }
		}

		public float extents(int index) { return this._extents[index]; }

		public Interval getProjection(Vector axis)
		{
			float radius =
				_extents[0] * Math.Abs(axis.Dot(axes[0])) +
				_extents[1] * Math.Abs(axis.Dot(axes[1]));

			float c = samp.Dot(axis);

			interval.min = c - radius;
			interval.max = c + radius;
			return interval;
		}

		public void setAxes(float t)
		{
			float s = (float) Math.Sin(t);
			float c = (float) Math.Cos(t);

			axes[0].X = c;
			axes[0].Y = s;
			axes[1].X = -s;
			axes[1].Y = c;
		}
	}
}