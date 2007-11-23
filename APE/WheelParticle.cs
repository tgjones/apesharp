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
	public class WheelParticle : CircleParticle
	{
		private RimParticle rp;
		private Vector tan, normSlip;
		private float _traction;

		public float Speed
		{
			get { return rp.Speed; }
			set { rp.Speed = value; }
		}

		public float AngularVelocity
		{
			get { return rp.AngularVelocity; }
			set { rp.AngularVelocity = value; }
		}

		public float Traction
		{
			get { return 1 - _traction; }
			set { _traction = 1 - value; }
		}

		public float Radian
		{
			get
			{
				return (float) (Math.Atan2(rp.curr.Y, rp.curr.X) + Math.PI);
			}
		}

		public float Angle
		{
			get { return MathUtil.ToDegrees(this.Radian); }
		}

		public WheelParticle(float x, float y, float radius, bool fixed_, float mass,
				float elasticity, float friction, float traction)
			: base(x, y, radius, fixed_, mass, elasticity, friction)
		{
			tan = new Vector(0, 0);
			normSlip = new Vector(0, 0);
			rp = new RimParticle(radius, 2);

			this.Traction = traction;
		}

		public WheelParticle(float x, float y, float radius)
			: this(x, y, radius, false, 1, 0.3f, 0, 1)
		{

		}

		public override void Paint(IRenderer renderer)
		{
			base.Paint(renderer);

			// spokes
			renderer.DrawLine(
				this.Radian,
				curr.X - this.Radius,
				curr.Y,
				curr.X + this.Radius,
				curr.Y,
				_lineThickness,
				_lineColor);
			renderer.DrawLine(
				this.Radian + (float) Math.PI,
				curr.X,
				curr.Y - this.Radius,
				curr.X,
				curr.Y + this.Radius,
				_lineThickness,
				_lineColor);
		}

		public override void update(float dt, Vector force, Vector masslessForce, float damping)
		{
			base.update(dt, force, masslessForce, damping);
			rp.update(dt);
		}

		public override void resolveCollision(Vector mtd, Vector vel, Vector n, float d, int o, AbstractParticle p)
		{
			// review the o (order) need here - its a hack fix
			base.resolveCollision(mtd, vel, n, d, o, p);
			resolve(n * Math.Sign(d * o));
		}

		private void resolve(Vector n)
		{
			// this is the tangent vector at the rim particle
			tan = new Vector(-rp.curr.Y, rp.curr.X);

			// normalize so we can scale by the rotational speed
			tan = tan.normalize();

			// velocity of the wheel's surface 
			Vector wheelSurfaceVelocity = tan * rp.Speed;

			// the velocity of the wheel's surface relative to the ground
			this.Velocity += wheelSurfaceVelocity;
			Vector combinedVelocity = this.Velocity;

			// the wheel's comb velocity projected onto the contact normal
			float cp = combinedVelocity.Cross(n);

			// set the wheel's spinspeed to track the ground
			tan *= cp;
			rp.prev = rp.curr - tan;

			// some of the wheel's torque is removed and converted into linear displacement
			float slipSpeed = (1 - _traction) * rp.Speed;
			normSlip = new Vector(slipSpeed * n.Y, slipSpeed * n.X);
			curr += normSlip;
			rp.Speed = rp.Speed * _traction;
		}
	}
}