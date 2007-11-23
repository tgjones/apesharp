using System;
using APE;

namespace Demo
{
	public class RectComposite : Composite
	{
		private CircleParticle cpA, cpC, cpB, cpD;
		private SpringConstraint sprA, sprB, sprC, sprD;

		public CircleParticle PA
		{
			get { return cpA; }
		}

		public CircleParticle PC
		{
			get { return cpC; }
		}

		public RectComposite(Vector ctr, int colA, int colB)
			: base()
		{
			float rw = 75;
			float rh = 18;
			float rad = 4;

			// going clockwise from left top..
			cpA = new CircleParticle(ctr.X - rw / 2, ctr.Y - rh / 2, rad, true, 1, 0.3f, 0);
			cpB = new CircleParticle(ctr.X + rw / 2, ctr.Y - rh / 2, rad, true, 1, 0.3f, 0);
			cpC = new CircleParticle(ctr.X + rw / 2, ctr.Y + rh / 2, rad, true, 1, 0.3f, 0);
			cpD = new CircleParticle(ctr.X - rw / 2, ctr.Y + rh / 2, rad, true, 1, 0.3f, 0);

			cpA.SetStyle(0, 0, colA);
			cpB.SetStyle(0, 0, colA);
			cpC.SetStyle(0, 0, colA);
			cpD.SetStyle(0, 0, colA);

			sprA = new SpringConstraint(cpA, cpB, 0.5f, true, rad * 2, 1, false);
			sprB = new SpringConstraint(cpB, cpC, 0.5f, true, rad * 2, 1, false);
			sprC = new SpringConstraint(cpC, cpD, 0.5f, true, rad * 2, 1, false);
			sprD = new SpringConstraint(cpD, cpA, 0.5f, true, rad * 2, 1, false);

			sprA.SetStyle(0, 0, colA);
			sprB.SetStyle(0, 0, colA);
			sprC.SetStyle(0, 0, colA);
			sprD.SetStyle(0, 0, colA);

			this.Particles.Add(cpA);
			this.Particles.Add(cpB);
			this.Particles.Add(cpC);
			this.Particles.Add(cpD);

			this.Constraints.Add(sprA);
			this.Constraints.Add(sprB);
			this.Constraints.Add(sprC);
			this.Constraints.Add(sprD);
		}
	}
}