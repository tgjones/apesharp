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
	public class RimParticle
	{
		private float wr, av, sp, maxTorque;

		public Vector curr, prev;

		public float Speed
		{
			get { return sp; }
			set { sp = value; }
		}

		public float AngularVelocity
		{
			get { return av; }
			set { av = value; }
		}

		public RimParticle(float r, float mt)
		{
			curr = new Vector(r, 0);
			prev = new Vector(0, 0);

			sp = 0;
			av = 0;

			maxTorque = mt;
			wr = r;
		}

		public void update(float dt)
		{
			//clamp torques to valid range
			sp = Math.Max(-maxTorque, Math.Min(maxTorque, sp + av));

			//apply torque
			//this is the tangent vector at the rim particle
			float dx = -curr.Y;
			float dy = curr.X;

			//normalize so we can scale by the rotational speed
			float len = (float) Math.Sqrt(dx * dx + dy * dy);
			dx /= len;
			dy /= len;

			curr.X += sp * dx;
			curr.Y += sp * dy;

			float ox = prev.X;
			float oy = prev.Y;
			float px = prev.X = curr.X;
			float py = prev.Y = curr.Y;

			curr.X += APEngine.Damping * (px - ox);
			curr.Y += APEngine.Damping * (py - oy);

			// hold the rim particle in place
			float clen = (float) Math.Sqrt(curr.X * curr.X + curr.Y * curr.Y);
			float diff = (clen - wr) / clen;

			curr.X -= curr.X * diff;
			curr.Y -= curr.Y * diff;
		}
	}
}
