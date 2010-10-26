using System;
using APE;

namespace Demo
{
	public class Rotator : Group
	{
		private Vector ctr;
		private RectComposite rectComposite;

		private CircleParticle circA;
		private SpringConstraint connectorA, connectorB;
		private RectangleParticle rectA, rectB;

		public Rotator(int colA, int colB)
			: base(false)
		{
			this.CollideInternal = true;

			ctr = new Vector(555, 175);
			rectComposite = new RectComposite(ctr, colA, colB);
			addComposite(rectComposite);

			circA = new CircleParticle(ctr.X, ctr.Y, 5, false, 1, 0.3f, 0);
			circA.SetStyle(1, colA, colB);
			this.Particles.Add(circA);

			rectA = new RectangleParticle(555, 160, 10, 10, 0, false, 3, 0.3f, 0);
			rectA.SetStyle(1, colA, colB);
			this.Particles.Add(rectA);

			connectorA = new SpringConstraint(rectComposite.PA, rectA, 1, false, 1, 1, false);
			connectorA.SetStyle(2, colB);
			this.Constraints.Add(connectorA);

			rectB = new RectangleParticle(555, 190, 10, 10, 0, false, 3, 0.3f, 0);
			rectB.SetStyle(1, colA, colB);
			this.Particles.Add(rectB);

			connectorB = new SpringConstraint(rectComposite.PC, rectB, 1, false, 1, 1, false);
			connectorB.SetStyle(2, colB);
			this.Constraints.Add(connectorB);
		}

		public void rotateByRadian(float a)
		{
			rectComposite.rotateByRadian(a, ctr);
		}
	}
}
