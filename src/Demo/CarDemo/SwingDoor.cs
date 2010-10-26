using System;
using APE;

namespace Demo
{
	public class SwingDoor : Group
	{
		public SwingDoor(int colE)
			: base(false)
		{
			// setting collideInternal allows the arm to hit the hidden stoppers. 
			// you could also make the stoppers its own group and tell it to collide 
			// with the SwingDoor
			this.CollideInternal = true;

			CircleParticle swingDoorP1 = new CircleParticle(543, 55, 7, false, 1, 0.3f, 0);
			swingDoorP1.Mass = 0.001f;
			swingDoorP1.SetStyle(1, colE, colE);
			this.Particles.Add(swingDoorP1);

			CircleParticle swingDoorP2 = new CircleParticle(620, 55, 7, true, 1, 0.3f, 0);
			swingDoorP2.SetStyle(1, colE, colE);
			this.Particles.Add(swingDoorP2);

			SpringConstraint swingDoor = new SpringConstraint(swingDoorP1, swingDoorP2, 1, true, 13, 1, false);
			swingDoor.SetStyle(2, colE, colE);
			this.Constraints.Add(swingDoor);

			CircleParticle swingDoorAnchor = new CircleParticle(543, 5, 2, true, 1, 0.3f, 0);
			swingDoorAnchor.Visible = false;
			swingDoorAnchor.Collidable = false;
			this.Particles.Add(swingDoorAnchor);

			SpringConstraint swingDoorSpring = new SpringConstraint(swingDoorP1, swingDoorAnchor, 0.02f, false, 1, 1, false);
			swingDoorSpring.RestLength = 40;
			swingDoorSpring.Visible = false;
			this.Constraints.Add(swingDoorSpring);

			CircleParticle stopperA = new CircleParticle(550, -60, 70, true, 1, 0.3f, 0);
			stopperA.Visible = false;
			this.Particles.Add(stopperA);

			RectangleParticle stopperB = new RectangleParticle(650, 130, 42, 70, 0, true, 1, 0.3f, 0);
			stopperB.Visible = false;
			this.Particles.Add(stopperB);
		}
	}
}