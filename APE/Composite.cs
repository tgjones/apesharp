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
	public class Composite : AbstractCollection
	{
		private Vector delta;

		public bool Fixed
		{
			get
			{
				foreach (AbstractParticle p in this.Particles)
					if (!p.Fixed) return false;
				return true;
			}

			set
			{
				foreach (AbstractParticle p in this.Particles)
					p.Fixed = value;
			}
		}

		public Composite()
			: base()
		{
			delta = new Vector(0, 0);
		}

		public void rotateByRadian(float angleRadians, Vector center)
		{
			foreach (AbstractParticle p in this.Particles)
			{
				float radius = p.center().distance(center);
				float angle = getRelativeAngle(center, p.center()) + angleRadians;

				p.PX = ((float) Math.Cos(angle) * radius) + center.X;
				p.PY = ((float) Math.Sin(angle) * radius) + center.Y;
			}
		}

		public void rotateByAngle(float angleDegrees, Vector center)
		{
			float angleRadians = MathUtil.ToRadians(angleDegrees);
			rotateByRadian(angleRadians, center);
		}

		private float getRelativeAngle(Vector center, Vector p)
		{
			delta = new Vector(p.X - center.X, p.Y - center.Y);
			return (float) Math.Atan2(delta.Y, delta.X);
		}
	}
}
