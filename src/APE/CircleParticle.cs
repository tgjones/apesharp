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
	public class CircleParticle : AbstractParticle
	{
		private float _radius;

		public override int ParticleType
		{
			get { return 2; }
		}

		public float Radius
		{
			get { return _radius; }
			set { _radius = value; }
		}

		public CircleParticle(float x, float y, float radius, bool fixed_,
				float mass, float elasticity, float friction)
			: base(x, y, fixed_, mass, elasticity, friction)
		{
			_radius = radius;
		}

		public CircleParticle(float x, float y, float radius, bool fixed_)
			: this(x, y, radius, fixed_, 1, 0.3f, 0)
		{

		}

		public CircleParticle(float x, float y, float radius)
			: this(x, y, radius, false)
		{
			
		}

		public override void Paint(IRenderer renderer)
		{
			if (!this.Visible)
				return;
			renderer.DrawFillCircle(curr.X, curr.Y, this.Radius, _lineThickness, _lineColor, _fillColor);
		}

		public Interval getIntervalX()
		{
			interval.min = curr.X - _radius;
			interval.max = curr.X + _radius;
			return interval;
		}

		public Interval getIntervalY()
		{
			interval.min = curr.Y - _radius;
			interval.max = curr.Y + _radius;
			return interval;
		}

		public Interval getProjection(Vector axis)
		{
			float c = samp.Dot(axis);
			interval.min = c - _radius;
			interval.max = c + _radius;
			return interval;
		}
	}
}