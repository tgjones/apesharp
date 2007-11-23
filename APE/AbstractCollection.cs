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
	public abstract class AbstractCollection
	{
		#region Fields

		private bool _isParented;
		private AbstractItemCollection<AbstractParticle> _particles;
		private AbstractItemCollection<AbstractConstraint> _constraints;

		#endregion

		#region Properties

		public bool IsParented
		{
			get { return _isParented; }
			set { _isParented = value; }
		}

		public AbstractItemCollection<AbstractParticle> Particles
		{
			get { return _particles; }
		}

		public AbstractItemCollection<AbstractConstraint> Constraints
		{
			get { return _constraints; }
		}

		#endregion

		#region Constructor

		public AbstractCollection()
		{
			_isParented = false;

			_particles = new AbstractItemCollection<AbstractParticle>(this);
			_constraints = new AbstractItemCollection<AbstractConstraint>(this);
		}

		#endregion

		#region Methods

		public virtual void Init()
		{
			foreach (AbstractParticle p in _particles)
				p.Init();

			foreach (AbstractConstraint c in _constraints)
				c.Init();
		}

		public virtual void Paint(IRenderer renderer)
		{
			foreach (AbstractParticle p in _particles)
				if ((!p.Fixed) || p.AlwaysRepaint) p.Paint(renderer);

			foreach (SpringConstraint c in _constraints)
				if ((!c.Fixed) || c.AlwaysRepaint) c.Paint(renderer);
		}

		public virtual void Integrate(float dt2, Vector force, Vector masslessForce, float damping)
		{
			foreach (AbstractParticle p in _particles)
				p.update(dt2, force, masslessForce, damping);
		}

		public virtual void SatisfyConstraints()
		{
			foreach (AbstractConstraint c in _constraints)
				c.Resolve();
		}

		public void CheckCollisionsVsCollection(AbstractCollection ac)
		{
			// every particle in this collection...
			foreach (AbstractParticle pga in _particles)
			{
				if (!pga.Collidable) continue;

				// ...vs every particle in the other collection
				foreach (AbstractParticle pgb in ac.Particles)
					if (pgb.Collidable) CollisionDetector.test(pga, pgb);

				// ...vs every constraint in the other collection
				foreach (SpringConstraint cgb in ac.Constraints)
				{
					if (cgb.Collidable && !cgb.isConnectedTo(pga))
					{
						cgb.Scp.updatePosition();
						CollisionDetector.test(pga, cgb.Scp);
					}
				}
			}

			// every constraint in this collection...
			foreach (SpringConstraint cga in _constraints)
			{
				if (!cga.Collidable) continue;

				// ...vs every particle in the other collection
				foreach (AbstractParticle pgb in ac.Particles)
				{
					if (pgb.Collidable && !cga.isConnectedTo(pgb))
					{
						cga.Scp.updatePosition();
						CollisionDetector.test(pgb, cga.Scp);
					}
				}
			}
		}

		public void CheckInternalCollisions()
		{
			// every particle in this AbstractCollection
			int plen = _particles.Count;
			for (int j = 0; j < plen; j++)
			{
				AbstractParticle pa = _particles[j];
				if (!pa.Collidable) continue;

				// ...vs every other particle in this AbstractCollection
				for (int i = j + 1; i < plen; i++)
				{
					AbstractParticle pb = _particles[i];
					if (pb.Collidable) CollisionDetector.test(pa, pb);
				}

				// ...vs every other constraint in this AbstractCollection
				foreach (SpringConstraint c in _constraints)
				{
					if (c.Collidable && !c.isConnectedTo(pa))
					{
						c.Scp.updatePosition();
						CollisionDetector.test(pa, c.Scp);
					}
				}
			}
		}

		#endregion
	}
}