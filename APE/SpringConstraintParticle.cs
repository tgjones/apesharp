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
	public class SpringConstraintParticle : RectangleParticle
	{
		private AbstractParticle p1, p2;
		private Vector avgVelocity, lambda;
		private SpringConstraint parent;
		private bool scaleToLength;
		private Vector rca, rcb;
		private float s;
		private float _rectScale, _rectHeight, _fixedEndLimit;

		public float RectHeight
		{
			get { return _rectHeight; }
			set { _rectHeight = value; }
		}

		public float RectScale
		{
			get { return _rectScale; }
			set { _rectScale = value; }
		}

		public float FixedEndLimit
		{
			get { return _fixedEndLimit; }
			set { _fixedEndLimit = value; }
		}

		public override float Mass
		{
			get { return (p1.Mass + p2.Mass) / 2; }
		}

		public override float Elasticity
		{
			get { return (p1.Elasticity + p2.Elasticity) / 2; }
		}

		public override float Friction
		{
			get { return (p1.Friction + p2.Friction) / 2; }
		}

		public override Vector Velocity
		{
			get
			{
				Vector p1v = p1.Velocity;
				Vector p2v = p2.Velocity;

				avgVelocity = new Vector(((p1v.X + p2v.X) / 2), ((p1v.Y + p2v.Y) / 2));
				return avgVelocity;
			}
		}

		public override float InvMass
		{
			get
			{
				if (p1.Fixed && p2.Fixed) return 0;
				return 1 / ((p1.Mass + p2.Mass) / 2);
			}
		}

		public SpringConstraintParticle(AbstractParticle p1, AbstractParticle p2, SpringConstraint p,
				float rectHeight, float rectScale, bool scaleToLength)
			: base(0, 0, 0, 0, 0, false, 1, 0.3f, 0)
		{
			this.p1 = p1;
			this.p2 = p2;

			lambda = new Vector(0, 0);
			avgVelocity = new Vector(0, 0);

			parent = p;
			this.RectScale = rectScale;
			this.RectHeight = rectHeight;
			this.scaleToLength = scaleToLength;

			this.FixedEndLimit = 0;
			rca = new Vector(0, 0);
			rcb = new Vector(0, 0);
		}

		public override void Paint(IRenderer renderer)
		{
			Vector c = parent.Center;

			int width = (int) ((scaleToLength) ? (parent.CurrLength * RectScale) : (parent.RestLength * RectScale));

			int w = width;
			int h = (int) this.RectHeight;

			renderer.DrawFillRectangle(
				(int) c.X, (int) c.Y, w, h, parent.Radian,
				parent._lineThickness, parent._lineColor,
				parent._fillColor);
		}

		public void updatePosition()
		{
			Vector c = parent.Center;
			curr = new Vector(c.X, c.Y);

			this.Width = (scaleToLength) ? parent.CurrLength * this.RectScale : parent.RestLength * this.RectScale;
			this.Height = this.RectHeight;
			this.Radian = parent.Radian;
		}


		private float getContactPointParam(AbstractParticle p)
		{
			float t = 0;

			if (p.ParticleType == 2)
			{
				t = closestParamPoint(p.curr);
			}
			else if (p.ParticleType == 1)
			{
				// go through the sides of the colliding rectangle as line segments
				int shortestIndex = 0;
				float[] paramList = new float[4];
				float shortestDistance = float.PositiveInfinity;

				for (int i = 0; i < 4; i++)
				{
					setCorners((RectangleParticle) p, i);

					// check for closest points on SCP to side of rectangle
					float d = closestPtSegmentSegment();
					if (d < shortestDistance)
					{
						shortestDistance = d;
						shortestIndex = i;
						paramList[i] = s;
					}
				}
				t = paramList[shortestIndex];
			}
			return t;
		}

		private void setCorners(RectangleParticle r, int i)
		{
			float rx = r.curr.X;
			float ry = r.curr.Y;


			float ae0_x = r.axes[0].X * r.extents(0);
			float ae0_y = r.axes[0].Y * r.extents(0);
			float ae1_x = r.axes[1].X * r.extents(1);
			float ae1_y = r.axes[1].Y * r.extents(1);

			float emx = ae0_x - ae1_x;
			float emy = ae0_y - ae1_y;
			float epx = ae0_x + ae1_x;
			float epy = ae0_y + ae1_y;


			if (i == 0)
			{
				// 0 and 1
				rca.X = rx - epx;
				rca.Y = ry - epy;
				rcb.X = rx + emx;
				rcb.Y = ry + emy;

			}
			else if (i == 1)
			{
				// 1 and 2
				rca.X = rx + emx;
				rca.Y = ry + emy;
				rcb.X = rx + epx;
				rcb.Y = ry + epy;

			}
			else if (i == 2)
			{
				// 2 and 3
				rca.X = rx + epx;
				rca.Y = ry + epy;
				rcb.X = rx - emx;
				rcb.Y = ry - emy;

			}
			else if (i == 3)
			{
				// 3 and 0
				rca.X = rx - emx;
				rca.Y = ry - emy;
				rcb.X = rx - epx;
				rcb.Y = ry - epy;
			}
		}


		private float closestPtSegmentSegment()
		{
			Vector pp1 = p1.curr;
			Vector pq1 = p2.curr;
			Vector pp2 = rca;
			Vector pq2 = rcb;

			Vector d1 = pq1 - pp1;
			Vector d2 = pq2 - pp2;
			Vector r = pp1 - pp2;

			float t;
			float a = d1.Dot(d1);
			float e = d2.Dot(d2);
			float f = d2.Dot(r);

			float c = d1.Dot(r);
			float b = d1.Dot(d2);
			float denom = a * e - b * b;

			if (denom != 0.0)
			{
				s = MathUtil.Clamp((b * f - c * e) / denom, 0, 1);
			}
			else
			{
				s = 0.5f; // give the midpoint for parallel lines
			}
			t = (b * s + f) / e;

			if (t < 0)
			{
				t = 0;
				s = MathUtil.Clamp(-c / a, 0, 1);
			}
			else if (t > 0)
			{
				t = 1;
				s = MathUtil.Clamp((b - c) / a, 0, 1);
			}

			Vector c1 = pp1 + (d1 * s);
			Vector c2 = pp2 + (d2 * t);
			Vector c1mc2 = c1 - c2;

			return c1mc2.Dot(c1mc2);
		}

		private float closestParamPoint(Vector c)
		{
			Vector ab = p2.curr - p1.curr;
			float t = (ab.Dot(c - p1.curr)) / (ab.Dot(ab));
			return MathUtil.Clamp(t, 0, 1);
		}

		public override void resolveCollision(Vector mtd, Vector vel, Vector n, float d, int o, AbstractParticle p)
		{
			float t = getContactPointParam(p);
			float c1 = (1 - t);
			float c2 = t;

			// if one is fixed then move the other particle the entire way out of collision.
			// also, dispose of collisions at the sides of the scp. The higher the fixedEndLimit
			// value, the more of the scp not be effected by collision. 
			if (p1.Fixed)
			{
				if (c2 <= this.FixedEndLimit) return;
				lambda = new Vector(mtd.X / c2, mtd.Y / c2);
				p2.curr += lambda;
				p2.Velocity = vel;
			}
			else if (p2.Fixed)
			{
				if (c1 <= this.FixedEndLimit) return;
				lambda = new Vector(mtd.X / c1, mtd.Y / c1);
				p1.curr += lambda;
				p1.Velocity = vel;

				// else both non fixed - move proportionally out of collision
			}
			else
			{
				float denom = (c1 * c1 + c2 * c2);
				if (denom == 0) return;
				lambda = new Vector(mtd.X / denom, mtd.Y / denom);

				p1.curr += lambda * c1;
				p2.curr += lambda * c2;

				// if collision is in the middle of SCP set the velocity of both end particles
				if (t == 0.5)
				{
					p1.Velocity = vel;
					p2.Velocity = vel;

					// otherwise change the velocity of the particle closest to contact
				}
				else
				{
					AbstractParticle corrParticle = (t < 0.5) ? p1 : p2;
					corrParticle.Velocity = vel;
				}
			}
		}
	}
}