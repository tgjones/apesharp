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
using System.Collections.Generic;

namespace APE
{
	public class Group : AbstractCollection
	{
		private bool _collideInternal;
		private List<Composite> _composites;
		private List<Group> _collisionList;

		public bool CollideInternal
		{
			get { return _collideInternal; }
			set { _collideInternal = value; }
		}

		public List<Composite> Composites
		{
			get { return _composites; }
		}

		public List<Group> CollisionList
		{
			get { return _collisionList; }
		}

		public Group(bool collideInternal)
			: base()
		{
			this.CollideInternal = collideInternal;

			_composites = new List<Composite>();
			_collisionList = new List<Group>();
		}

		public Group()
			: this(false)
		{

		}

		public void addComposite(Composite c)
		{
			this.Composites.Add(c);
			c.IsParented = true;
			if (this.IsParented) c.Init();
		}

		public void addCollidable(Group g)
		{
			this.CollisionList.Add(g);
		}

		public override void Paint(IRenderer renderer)
		{
			base.Paint(renderer);

			foreach (Composite c in _composites)
				c.Paint(renderer);
		}

		public override void Integrate(float dt2, Vector force, Vector masslessForce, float damping)
		{
			base.Integrate(dt2, force, masslessForce, damping);

			foreach (Composite c in _composites)
				c.Integrate(dt2, force, masslessForce, damping);
		}

		public override void SatisfyConstraints()
		{
			base.SatisfyConstraints();

			foreach (Composite c in _composites)
				c.SatisfyConstraints();
		}

		public void checkCollisions()
		{
			if (this.CollideInternal) checkCollisionGroupInternal();

			foreach (Group g in _collisionList)
				checkCollisionVsGroup(g);
		}

		private void checkCollisionVsGroup(Group g)
		{
			// check particles and constraints not in composites of either group
			CheckCollisionsVsCollection(g);

			// for every composite in this group..
			foreach (Composite c in _composites)
			{
				// check vs the particles and constraints of g
				c.CheckCollisionsVsCollection(g);

				// check vs composites of g
				foreach (Composite gc in g.Composites)
					c.CheckCollisionsVsCollection(gc);
			}

			// check particles and constraints of this group vs the composites of g
			foreach (Composite gc in g.Composites)
				CheckCollisionsVsCollection(gc);
		}

		public override void Init()
		{
			base.Init();

			foreach (Composite c in _composites)
				c.Init();
		}

		private void checkCollisionGroupInternal()
		{
			// check collisions not in composites
			CheckInternalCollisions();

			// for every composite in this Group..
			int clen = _composites.Count;
			for (int j = 0; j < clen; j++)
			{
				Composite ca = _composites[j];

				// .. vs non composite particles and constraints in this group
				ca.CheckCollisionsVsCollection(this);

				// ...vs every other composite in this Group
				for (int i = j + 1; i < clen; i++)
				{
					Composite cb = _composites[i];
					ca.CheckCollisionsVsCollection(cb);
				}
			}
		}
	}
}