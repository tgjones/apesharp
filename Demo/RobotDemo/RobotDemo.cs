using System;
using System.Windows.Forms;
using APE;

namespace Demo.RobotDemo
{
	public class RobotDemo : AbstractDemo
	{
		private static int halfStageWidth = 325;
		private Robot robot;

		public RobotDemo()
		{
			APEngine.init(1 / 4);
			//APEngine.container = this;
			APEngine.addMasslessForce(new Vector(0, 4));

			APEngine.Damping = .99f;
			APEngine.ConstraintCollisionCycles = 10;

			robot = new Robot(1250, 260, 1.6f, 0.02f);

			Group terrainA = new Group();
			Group terrainB = new Group(true);
			Group terrainC = new Group();

			RectangleParticle floor = new RectangleParticle(600, 390, 1700, 100, 0, true, 1, 0, 1);
			floor.SetStyle(0, 0, 0x999999);
			terrainA.Particles.Add(floor);

			// pyramid of boxes
			RectangleParticle box0 = new RectangleParticle(600, 337, 600, 7, 0, true, 10, 0, 1);
			box0.SetStyle(1, 0x999999, 0x336699);
			terrainA.Particles.Add(box0);

			RectangleParticle box1 = new RectangleParticle(600, 330, 500, 7, 0, true, 10, 0, 1);
			box1.SetStyle(1, 0x999999, 0x336699);
			terrainA.Particles.Add(box1);

			RectangleParticle box2 = new RectangleParticle(600, 323, 400, 7, 0, true, 10, 0, 1);
			box2.SetStyle(1, 0x999999, 0x336699);
			terrainA.Particles.Add(box2);

			RectangleParticle box3 = new RectangleParticle(600, 316, 300, 7, 0, true, 10, 0, 1);
			box3.SetStyle(1, 0x999999, 0x336699);
			terrainA.Particles.Add(box3);

			RectangleParticle box4 = new RectangleParticle(600, 309, 200, 7, 0, true, 10, 0, 1);
			box4.SetStyle(1, 0x999999, 0x336699);
			terrainA.Particles.Add(box4);

			RectangleParticle box5 = new RectangleParticle(600, 302, 100, 7, 0, true, 10, 0, 1);
			box5.SetStyle(1, 0x999999, 0x336699);
			terrainA.Particles.Add(box5);


			// left side floor
			RectangleParticle floor2 = new RectangleParticle(-100, 390, 1100, 100, 0.3f, true, 1, 0, 1);
			floor2.SetStyle(0, 0, 0x999999);
			terrainB.Particles.Add(floor2);

			RectangleParticle floor3 = new RectangleParticle(-959, 232, 700, 100, 0, true, 1, 0, 1);
			floor3.SetStyle(0, 0, 0x999999);
			terrainB.Particles.Add(floor3);

			RectangleParticle box6 = new RectangleParticle(-990, 12, 50, 25, 0);
			box6.SetStyle(1, 0x999999, 0x336699);
			terrainB.Particles.Add(box6);

			RectangleParticle floor5 = new RectangleParticle(-1284, 170, 50, 100, 0, true);
			floor5.SetStyle(0, 0, 0x999999);
			terrainB.Particles.Add(floor5);


			// right side floor
			RectangleParticle floor6 = new RectangleParticle(1430, 320, 50, 60, 0, true);
			floor6.SetStyle(0, 0, 0x00999999);
			terrainC.Particles.Add(floor6);

			APEngine.addGroup(robot);
			APEngine.addGroup(terrainA);
			APEngine.addGroup(terrainB);
			APEngine.addGroup(terrainC);

			robot.addCollidable(terrainA);
			robot.addCollidable(terrainB);
			robot.addCollidable(terrainC);

			robot.togglePower();
		}

		public override void Run()
		{
			APEngine.step();
			robot.run();

			//APEngine.container.x = -robot.px + halfStageWidth;
		}

		public override void HandleKeyUp(KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.P :
					robot.togglePower();
					break;
				case Keys.R:
					robot.toggleDirection();
					break;
				case Keys.H:
					robot.toggleLegs();
					break;
			}
		}

		public override void Paint(IRenderer renderer)
		{
			APEngine.paint(renderer);
		}
	}
}
