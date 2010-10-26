using System;
using APE;

namespace Demo
{
	public class Capsule : Group
	{
		public Capsule(int colC)
			: base(false)
		{
			CircleParticle capsuleP1 = new CircleParticle(300, 10, 14, false, 1.3f, 0.4f, 0);
			capsuleP1.SetStyle(0, colC, colC);
			this.Particles.Add(capsuleP1);

			CircleParticle capsuleP2 = new CircleParticle(325, 35, 14, false, 1.3f, 0.4f, 0);
			capsuleP2.SetStyle(0, colC, colC);
			this.Particles.Add(capsuleP2);

			SpringConstraint capsule = new SpringConstraint(capsuleP1, capsuleP2, 1, true, 24, 1, false);
			capsule.SetStyle(0, colC, colC);
			this.Constraints.Add(capsule);
		}
	}
}
