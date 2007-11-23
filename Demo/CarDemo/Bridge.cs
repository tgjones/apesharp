using System;
using APE;

namespace Demo
{
	public class Bridge : Group
	{
		private SpringConstraint bridgeConnA, bridgeConnB, bridgeConnC, bridgeConnD, bridgeConnE;
		private CircleParticle bridgePAA, bridgePA, bridgePB, bridgePC, bridgePD, bridgePDD;

		public Bridge(int colB, int colC, int colD)
			: base(false)
		{
			float bx = 170;
			float by = 40;
			float bsize = 51.5f;
			float yslope = 2.4f;
			float particleSize = 4;

			bridgePAA = new CircleParticle(bx, by, particleSize, true, 1, 0.3f, 0);
			bridgePAA.SetStyle(1, colC, colB);
			this.Particles.Add(bridgePAA);

			bx += bsize;
			by += yslope;
			bridgePA = new CircleParticle(bx, by, particleSize, false, 1, 0.3f, 0);
			bridgePA.SetStyle(1, colC, colB);
			this.Particles.Add(bridgePA);

			bx += bsize;
			by += yslope;
			bridgePB = new CircleParticle(bx, by, particleSize, false, 1, 0.3f, 0);
			bridgePB.SetStyle(1, colC, colB);
			this.Particles.Add(bridgePB);

			bx += bsize;
			by += yslope;
			bridgePC = new CircleParticle(bx, by, particleSize, false, 1, 0.3f, 0);
			bridgePC.SetStyle(1, colC, colB);
			this.Particles.Add(bridgePC);

			bx += bsize;
			by += yslope;
			bridgePD = new CircleParticle(bx, by, particleSize, false, 1, 0.3f, 0);
			bridgePD.SetStyle(1, colC, colB);
			this.Particles.Add(bridgePD);

			bx += bsize;
			by += yslope;
			bridgePDD = new CircleParticle(bx, by, particleSize, true, 1, 0.3f, 0);
			bridgePDD.SetStyle(1, colC, colB);
			this.Particles.Add(bridgePDD);

			bridgeConnA = new SpringConstraint(bridgePAA, bridgePA,
					0.9f, true, 10, 0.8f, false);

			// collision response on the bridgeConnA will be ignored on
			// on the first 1/4 of the constraint. this avoids blow ups
			// particular to springcontraints that have 1 fixed particle.
			bridgeConnA.FixedEndLimit = 0.25f;
			bridgeConnA.SetStyle(1, colC, colB);
			this.Constraints.Add(bridgeConnA);

			bridgeConnB = new SpringConstraint(bridgePA, bridgePB,
					0.9f, true, 10, 0.8f, false);
			bridgeConnB.SetStyle(1, colC, colB);
			this.Constraints.Add(bridgeConnB);

			bridgeConnC = new SpringConstraint(bridgePB, bridgePC,
					0.9f, true, 10, 0.8f, false);
			bridgeConnC.SetStyle(1, colC, colB);
			this.Constraints.Add(bridgeConnC);

			bridgeConnD = new SpringConstraint(bridgePC, bridgePD,
					0.9f, true, 10, 0.8f, false);
			bridgeConnD.SetStyle(1, colC, colB);
			this.Constraints.Add(bridgeConnD);

			bridgeConnE = new SpringConstraint(bridgePD, bridgePDD,
					0.9f, true, 10, 0.8f, false);
			bridgeConnE.FixedEndLimit = 0.25f;
			bridgeConnE.SetStyle(1, colC, colB);
			this.Constraints.Add(bridgeConnE);
		}
	}
}