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
	public class SpringConstraint : AbstractConstraint
	{
		private AbstractParticle p1, p2;
		private SpringConstraintParticle _scp;
		private bool _collidable;
		private float _restLength;

		public SpringConstraintParticle Scp
		{
			get { return _scp; }
		}

		public float FixedEndLimit
		{
			get { return Scp.FixedEndLimit; }
			set
			{
				if (this.Scp == null) return;
				this.Scp.FixedEndLimit = value;
			}
		}

		public float CurrLength
		{
			get { return p1.curr.distance(p2.curr); }
		}

		public float RestLength
		{
			get { return _restLength; }
			set { _restLength = value; }
		}

		public bool Fixed
		{
			get { return (p1.Fixed && p2.Fixed); }
		}

		public bool Collidable
		{
			get { return _collidable; }
		}

		public Vector Center
		{
			get { return (p1.curr + p2.curr) / 2; }
		}

		public Vector Delta
		{
			get { return p1.curr - p2.curr; }
		}

		public float Radian
		{
			get
			{
				Vector d = this.Delta;
				float retval = (float) Math.Atan2(d.Y, d.X);
				return retval;
			}
		}

		public float Angle
		{
			get { return MathUtil.ToDegrees(this.Radian); }
		}

		public SpringConstraint(AbstractParticle p1, AbstractParticle p2, float stiffness, bool collidable,
				float rectHeight, float rectScale, bool scaleToLength)
			: base(stiffness)
		{
			_scp = null;

			this.p1 = p1;
			this.p2 = p2;
			checkParticlesLocation();

			_restLength = this.CurrLength;
			setCollidable(collidable, rectHeight, rectScale, scaleToLength);
		}

		public SpringConstraint(AbstractParticle p1, AbstractParticle p2, float stiffness)
			: this(p1, p2, stiffness, false, 1, 1, false)
		{

		}

		public SpringConstraint(AbstractParticle p1, AbstractParticle p2)
			: this(p1, p2, 0.5f)
		{

		}

		public void setCollidable(bool b, float rectHeight, float rectScale, bool scaleToLength)
		{
			_collidable = b;

			_scp = null;

			if (_collidable)
			{
				_scp = new SpringConstraintParticle(p1, p2, this, rectHeight, rectScale, scaleToLength);
			}
		}

		private void checkParticlesLocation()
		{
			if (p1.curr.X == p2.curr.X && p1.curr.Y == p2.curr.Y)
				p2.curr.X += 0.0001f;
		}

		public override void Paint(IRenderer renderer)
		{
			if (this.Collidable)
			{
				this.Scp.Paint(renderer);
			}
			else
			{
				if (!this.Visible)
					return;

				Vector c = this.Center;

				int w = (int) (p2.Position - p1.Position).magnitude();
				int h = 1 /*abs(p2.py()-p1.py())*/;

				renderer.DrawLine(
					this.Radian,
					c.X - w / 2, c.Y - h / 2,
					c.X + w / 2, c.Y + h / 2,
					_lineThickness, _lineColor);
			}
		}

		public override void Init()
		{
			//cleanup();
			if (this.Collidable)
			{
				this.Scp.Init();
				//} else if (displayObject != null) {
				//	initDisplay();
			}
			//paint();

		}

		public override void Resolve()
		{
			if (p1.Fixed && p2.Fixed) return;

			float deltaLength = this.CurrLength;
			float diff = (deltaLength - this.RestLength) / (deltaLength * (p1.InvMass + p2.InvMass));
			Vector dmds = this.Delta * (diff * this.Stiffness);

			p1.curr -= dmds * p1.InvMass;
			p2.curr += dmds * p2.InvMass;
		}

		public bool isConnectedTo(AbstractParticle p)
		{
			return (p == p1 || p == p2);
		}
	}
}
