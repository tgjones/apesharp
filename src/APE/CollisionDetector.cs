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
	public static class CollisionDetector
	{
		public static void test(AbstractParticle objA, AbstractParticle objB)
		{
			if (objA.Fixed && objB.Fixed) return;

			if (objA.MultiSample == 0 && objB.MultiSample == 0)
			{
				normVsNorm(objA, objB);

			}
			else if (objA.MultiSample > 0 && objB.MultiSample == 0)
			{
				sampVsNorm(objA, objB);

			}
			else if (objB.MultiSample > 0 && objA.MultiSample == 0)
			{
				sampVsNorm(objB, objA);

			}
			else if (objA.MultiSample == objB.MultiSample)
			{
				sampVsSamp(objA, objB);

			}
			else
			{
				normVsNorm(objA, objB);
			}
		}

		private static void normVsNorm(AbstractParticle objA, AbstractParticle objB)
		{
			objA.samp = objA.curr;
			objB.samp = objB.curr;
			testTypes(objA, objB);
		}

		private static void sampVsNorm(AbstractParticle objA, AbstractParticle objB)
		{
			float s = 1 / (objA.MultiSample + 1);
			float t = s;

			objB.samp = objB.curr;

			for (int i = 0; i <= objA.MultiSample; i++)
			{
				objA.samp = new Vector(objA.prev.X + t * (objA.curr.X - objA.prev.X),
								objA.prev.Y + t * (objA.curr.Y - objA.prev.Y));

				if (testTypes(objA, objB)) return;
				t += s;
			} // for (var i:int = 0; i <= objA.multisample; i++)
		}

		private static void sampVsSamp(AbstractParticle objA, AbstractParticle objB)
		{
			float s = 1 / (objA.MultiSample + 1);
			float t = s;

			for (int i = 0; i <= objA.MultiSample; i++)
			{
				objA.samp = new Vector(objA.prev.X + t * (objA.curr.X - objA.prev.X),
								objA.prev.Y + t * (objA.curr.Y - objA.prev.Y));

				objB.samp = new Vector(objB.prev.X + t * (objB.curr.X - objB.prev.X),
								objB.prev.Y + t * (objB.curr.Y - objB.prev.Y));

				if (testTypes(objA, objB)) return;
				t += s;
			}
		}

		private static bool testTypes(AbstractParticle objA, AbstractParticle objB)
		{
			if (objA.ParticleType == 1 && objB.ParticleType == 1)
				return testOBBvsOBB((RectangleParticle) objA, (RectangleParticle) objB);
			else if (objA.ParticleType == 2 && objB.ParticleType == 2)
				return testCirclevsCircle((CircleParticle) objA, (CircleParticle) objB);
			else if (objA.ParticleType == 1 && objB.ParticleType == 2)
				return testOBBvsCircle((RectangleParticle) objA, (CircleParticle) objB);
			else if (objA.ParticleType == 2 && objB.ParticleType == 1)
				return testOBBvsCircle((RectangleParticle) objB, (CircleParticle) objA);

			return false;
		}

		private static bool testOBBvsOBB(RectangleParticle ra, RectangleParticle rb)
		{
			Vector collisionNormal = new Vector();
			float collisionDepth = float.PositiveInfinity;

			for (int i = 0; i < 2; i++)
			{
				Vector axisA = ra.axes[i];
				float depthA = testIntervals(ra.getProjection(axisA), rb.getProjection(axisA));
				if (depthA == 0) return false;

				Vector axisB = rb.axes[i];
				float depthB = testIntervals(ra.getProjection(axisB), rb.getProjection(axisB));
				if (depthB == 0) return false;

				float absA = Math.Abs(depthA);
				float absB = Math.Abs(depthB);

				if (absA < Math.Abs(collisionDepth) || absB < Math.Abs(collisionDepth))
				{
					bool altb = absA < absB;
					collisionNormal = altb ? axisA : axisB;
					collisionDepth = altb ? depthA : depthB;
				}
			}
			CollisionResolver.resolveParticleParticle(ra, rb, collisionNormal, collisionDepth);
			return true;
		}

		private static bool testOBBvsCircle(RectangleParticle ra, CircleParticle ca)
		{
			Vector collisionNormal = new Vector();
			float collisionDepth = float.PositiveInfinity;
			float[] depths = new float[2];

			// first go through the axes of the rectangle
			for (int i = 0; i < 2; i++)
			{
				Vector boxAxis = ra.axes[i];
				float depth = testIntervals(ra.getProjection(boxAxis), ca.getProjection(boxAxis));
				if (depth == 0) return false;

				if (Math.Abs(depth) < Math.Abs(collisionDepth))
				{
					collisionNormal = boxAxis;
					collisionDepth = depth;
				}
				depths[i] = depth;
			}

			// determine if the circle's center is in a vertex region
			float r = ca.Radius;
			if (Math.Abs(depths[0]) < r && Math.Abs(depths[1]) < r)
			{
				Vector vertex = closestVertexOnOBB(ca.samp, ra);

				// get the distance from the closest vertex on rect to circle center
				collisionNormal = vertex - ca.samp;
				float mag = collisionNormal.magnitude();
				collisionDepth = r - mag;

				if (collisionDepth > 0)
				{
					// there is a collision in one of the vertex regions
					collisionNormal /= mag;
				}
				else
				{
					// ra is in vertex region, but is not colliding
					return false;
				}
			}
			CollisionResolver.resolveParticleParticle(ra, ca, collisionNormal, collisionDepth);
			return true;
		}

		private static bool testCirclevsCircle(CircleParticle ca, CircleParticle cb)
		{
			float depthX = testIntervals(ca.getIntervalX(), cb.getIntervalX());
			if (depthX == 0) return false;

			float depthY = testIntervals(ca.getIntervalY(), cb.getIntervalY());
			if (depthY == 0) return false;

			Vector collisionNormal = ca.samp - cb.samp;
			float mag = collisionNormal.magnitude();
			float collisionDepth = (ca.Radius + cb.Radius) - mag;

			if (collisionDepth > 0)
			{
				collisionNormal /= mag;
				CollisionResolver.resolveParticleParticle(ca, cb, collisionNormal, collisionDepth);
				return true;
			}
			return false;
		}


		private static float testIntervals(Interval intervalA, Interval intervalB)
		{
			if (intervalA.max < intervalB.min) return 0;
			if (intervalB.max < intervalA.min) return 0;

			float lenA = intervalB.max - intervalA.min;
			float lenB = intervalB.min - intervalA.max;

			return (Math.Abs(lenA) < Math.Abs(lenB)) ? lenA : lenB;
		}

		private static Vector closestVertexOnOBB(Vector p, RectangleParticle r)
		{
			Vector d = p - r.samp;
			Vector q = new Vector(r.samp.X, r.samp.Y);

			for (int i = 0; i < 2; i++)
			{
				float dist = d.Dot(r.axes[i]);

				if (dist >= 0) dist = r.extents(i);
				else if (dist < 0) dist = -r.extents(i);

				q += (r.axes[i] * dist);
			}
			return q;
		}
	}
}