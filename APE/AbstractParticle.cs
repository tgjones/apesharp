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
	public abstract class AbstractParticle : AbstractItem
	{
		#region Fields

		private Vector forces, temp;
		private Collision collision;

		private float _kfr, _mass, _invMass, _friction;

		private bool _fixed, _collidable;

		private Vector _center;
		private Vector _collision_vn, _collision_vt;
		private int _multisample;

		public Vector curr, prev, samp;
		public Interval interval;

		#endregion

		#region Properties

		public virtual float Elasticity
		{
			get { return _kfr; }
			set { _kfr = value; }
		}

		public virtual float Friction
		{
			get { return _friction; }
			set
			{
				if (value < 0 || value > 1)
				{
					throw new Exception("Legal friction must be >= 0 and <=1");
				}
				_friction = value;
			}
		}

		public virtual float Mass
		{
			get { return _mass; }
			set
			{
				if (value <= 0)
				{
					throw new Exception("mass may not be set <= 0");
				}
				_mass = value;
				_invMass = 1 / _mass;

			}
		}

		public bool Collidable
		{
			get { return _collidable; }
			set { _collidable = value; }
		}

		public bool Fixed
		{
			get { return _fixed; }
			set { _fixed = value; }
		}

		public int MultiSample
		{
			get { return _multisample; }
			set { _multisample = value; }
		}

		public float PX
		{
			get { return curr.X; }
			set
			{
				curr.X = value;
				prev.X = value;
			}
		}

		public float PY
		{
			get { return curr.Y; }
			set
			{
				curr.Y = value;
				prev.Y = value;
			}
		}

		public Vector Position
		{
			get { return new Vector(curr.X, curr.Y); }
			set
			{
				curr = value;
				prev = value;
			}
		}

		public virtual Vector Velocity
		{
			get { return curr - prev; }
			set { prev = curr - value; }
		}

		public virtual float InvMass
		{
			get { return (this.Fixed) ? 0 : _invMass; }
		}

		public abstract int ParticleType
		{
			get;
		}

		#endregion

		public AbstractParticle(float x, float y, bool isFixed, float mass, float elasticity, float friction)
			: base()
		{
			interval = new Interval(0, 0);

			curr = new Vector(x, y);
			prev = new Vector(x, y);
			samp = new Vector(0, 0);
			temp = new Vector(0, 0);
			this.Fixed = isFixed;

			forces = new Vector(0, 0);
			_collision_vn = new Vector(0, 0);
			_collision_vt = new Vector(0, 0);
			collision = new Collision(_collision_vn, _collision_vt);
			this.Collidable = true;

			this.Mass = mass;
			this.Elasticity = elasticity;
			this.Friction = friction;

			//setStyle();

			_center = new Vector(0, 0);
			_multisample = 0;
		}

		public Vector center()
		{
			_center = new Vector(this.PX, this.PY);
			return _center;
		}

		public virtual void update(float dt2, Vector force, Vector masslessForce, float damping)
		{
			if (this.Fixed) return;

			// global forces
			addForce(force);
			addMasslessForce(masslessForce);

			// integrate
			temp = curr;

			Vector nv = this.Velocity + (forces *= dt2);
			curr += nv *= damping;
			prev = temp;

			// clear the forces
			forces = new Vector(0, 0);
		}

		public void addForce(Vector f)
		{
			Vector tmp = f * this.InvMass;
			forces += tmp;
		}

		public void addMasslessForce(Vector f)
		{
			forces += f;
		}

		public virtual void resolveCollision(Vector mtd, Vector vel, Vector n, float d, int o, AbstractParticle p)
		{
			curr += mtd;
			this.Velocity = vel;
		}

		public Collision getComponents(Vector collisionNormal)
		{
			Vector vel = this.Velocity;
			float vdotn = collisionNormal.Dot(vel);

			collision.vn = collisionNormal * vdotn;

			collision.vt = vel - collision.vn;

			return collision;
		}
	}
}