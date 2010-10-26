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
	public static class APEngine
	{
		private static int numGroups;
		private static float timeStep, _damping;
		private static int _constraintCycles, _constraintCollisionCycles;
		private static List<Group> groups;

		public static Vector force, masslessForce;

		/// <summary>
		/// The global damping. Values should be between 0 and 1. Higher numbers
		/// result in less damping. A value of 1 is no damping. A value of 0 will
		/// not allow any particles to move. The default is 1.
		/// 
		/// Damping will slow down your simulation and make it more stable. If you find
		/// that your sim is "blowing up", try applying more damping. 
		/// </summary>
		public static float Damping
		{
			get { return _damping; }
			set { _damping = value; }
		}

		public static int ConstraintCollisionCycles
		{
			get { return _constraintCollisionCycles; }
			set { _constraintCollisionCycles = value; }
		}

		public static void init(float dt)
		{
			timeStep = dt * dt;

			numGroups = 0;

			groups = new List<Group>();

			force = new Vector(0, 0);
			masslessForce = new Vector(0, 0);

			Damping = 1;

			_constraintCycles = 0;
			_constraintCollisionCycles = 1;
		}

		public static void addMasslessForce(Vector v)
		{
			masslessForce += v;
		}

		public static void addGroup(Group g)
		{
			groups.Add(g);
			g.IsParented = true;
			numGroups++;
			g.Init();
		}

		public static void removeGroup(Group g)
		{
			groups.Remove(g);
		}

		public static void step()
		{
			integrate();

			for (int j = 0; j < _constraintCycles; j++)
			{
				satisfyConstraints();
			}
			for (int i = 0; i < _constraintCollisionCycles; i++)
			{
				satisfyConstraints();
				checkCollisions();
			}
		}

		public static void integrate()
		{
			foreach (Group g in groups)
				g.Integrate(timeStep, force, masslessForce, Damping);
		}

		public static void satisfyConstraints()
		{
			foreach (Group g in groups)
				g.SatisfyConstraints();
		}

		public static void checkCollisions()
		{
			foreach (Group g in groups)
				g.checkCollisions();
		}

		public static void paint(IRenderer renderer)
		{
			foreach (Group g in groups)
				g.Paint(renderer);
		}

		public static void addForce(Vector v)
		{
			force += v;
		}
	}
}