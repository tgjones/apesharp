using System;
using APE;

namespace Demo
{
	public class Car : Group
	{
		private WheelParticle wheelParticleA, wheelParticleB;

		public float Speed
		{
			set
			{
				wheelParticleA.AngularVelocity = value;
				wheelParticleB.AngularVelocity = value;
			}
		}

		public Car(int colC, int colE)
			: base(false)
		{
			wheelParticleA = new WheelParticle(140, 10, 14, false, 2, 0.3f, 0, 1);
			wheelParticleA.SetStyle(1, colC, colE);
			this.Particles.Add(wheelParticleA);

			wheelParticleB = new WheelParticle(200, 10, 14, false, 2, 0.3f, 0, 1);
			wheelParticleB.SetStyle(1, colC, colE);
			this.Particles.Add(wheelParticleB);

			SpringConstraint wheelConnector = new SpringConstraint(wheelParticleA, wheelParticleB,
					0.5f, true, 8, 1, false);
			wheelConnector.SetStyle(1, colC, colE);
			this.Constraints.Add(wheelConnector);
		}
	}
}