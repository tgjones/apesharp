using System;
using APE;

namespace Demo
{
	public class Surfaces : Group
	{
		private RectangleParticle floor, ceil, rampRight, rampLeft, rampLeft2, bouncePad, leftWall, leftWallChannelInner, leftWallChannel, leftWallChannelAng, topLeftAng, rightWall, bridgeStart, bridgeEnd;
		private CircleParticle rampCircle, floorBump;

		public Surfaces(int colA, int colB, int colC, int colD, int colE, int colBouncePad)
			: base(false)
		{
			floor = new RectangleParticle(340, 327, 550, 50, 0, true, 1.0f, 0.3f, 0);
			floor.SetStyle(0, colD, colD);
			this.Particles.Add(floor);

			ceil = new RectangleParticle(325, -33, 649, 80, 0, true, 1.0f, 0.3f, 0);
			ceil.SetStyle(0, colD, colD);
			this.Particles.Add(ceil);

			rampRight = new RectangleParticle(375, 220, 390, 20, 0.405f, true, 1.0f, 0.3f, 0);
			rampRight.SetStyle(0, colD, colD);
			this.Particles.Add(rampRight);

			rampLeft = new RectangleParticle(90, 200, 102, 20, -.7f, true, 1.0f, 0.3f, 0);
			rampLeft.SetStyle(0, colD, colD);
			this.Particles.Add(rampLeft);

			rampLeft2 = new RectangleParticle(96, 129, 102, 20, -.7f, true, 1.0f, 0.3f, 0);
			rampLeft2.SetStyle(0, colD, colD);
			this.Particles.Add(rampLeft2);

			rampCircle = new CircleParticle(175, 190, 60, true, 1.0f, 0.3f, 0);
			rampCircle.SetStyle(1, colD, colB);
			this.Particles.Add(rampCircle);

			floorBump = new CircleParticle(600, 660, 400, true, 1.0f, 0.3f, 0);
			floorBump.SetStyle(1, colD, colB);
			this.Particles.Add(floorBump);

			bouncePad = new RectangleParticle(30, 370, 32, 60, 0, true, 1.0f, 0.3f, 0);
			bouncePad.SetStyle(1, colD, colBouncePad);
			bouncePad.Elasticity = 4;
			this.Particles.Add(bouncePad);

			leftWall = new RectangleParticle(1, 99, 30, 500, 0, true, 1.0f, 0.3f, 0);
			leftWall.SetStyle(0, colD, colD);
			this.Particles.Add(leftWall);

			leftWallChannelInner = new RectangleParticle(54, 300, 20, 150, 0, true, 1.0f, 0.3f, 0);
			leftWallChannelInner.SetStyle(0, colD, colD);
			this.Particles.Add(leftWallChannelInner);

			leftWallChannel = new RectangleParticle(54, 122, 20, 94, 0, true, 1.0f, 0.3f, 0);
			leftWallChannel.SetStyle(0, colD, colD);
			this.Particles.Add(leftWallChannel);

			leftWallChannelAng = new RectangleParticle(75, 65, 60, 25, -0.7f, true, 1.0f, 0.3f, 0);
			leftWallChannelAng.SetStyle(0, colD, colD);
			this.Particles.Add(leftWallChannelAng);

			topLeftAng = new RectangleParticle(23, 11, 65, 40, -0.7f, true, 1.0f, 0.3f, 0);
			topLeftAng.SetStyle(0, colD, colD);
			this.Particles.Add(topLeftAng);

			rightWall = new RectangleParticle(654, 230, 50, 500, 0, true, 1.0f, 0.3f, 0);
			rightWall.SetStyle(0, colD, colD);
			this.Particles.Add(rightWall);

			bridgeStart = new RectangleParticle(127, 49, 75, 25, 0, true, 1.0f, 0.3f, 0);
			bridgeStart.SetStyle(0, colD, colD);
			this.Particles.Add(bridgeStart);

			bridgeEnd = new RectangleParticle(483, 55, 100, 15, 0, true, 1.0f, 0.3f, 0);
			bridgeEnd.SetStyle(0, colD, colD);
			this.Particles.Add(bridgeEnd);
		}
	}
}