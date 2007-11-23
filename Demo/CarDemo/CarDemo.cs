using System;
using System.Drawing;
using System.Windows.Forms;
using APE;

namespace Demo.CarDemo
{
	public class CarDemo : AbstractDemo
	{
		// APE Objects
		private Rotator rotator;
		private Car car;

		public CarDemo()
		{
			int alpha = 255;
			int colA = Color.FromArgb(alpha, 51, 58, 51).ToArgb();
			int colB = Color.FromArgb(alpha, 51, 102, 170).ToArgb();
			int colC = Color.FromArgb(alpha, 170, 187, 187).ToArgb();
			int colD = Color.FromArgb(alpha, 102, 153, 170).ToArgb();
			int colE = Color.FromArgb(alpha, 119, 136, 119).ToArgb();
			int colPad = Color.FromArgb(alpha, 153, 102, 51).ToArgb();

			APEngine.init((float) 1 / 4);

			Vector massLessForces = new Vector(0, 3);
			APEngine.addMasslessForce(massLessForces);

			Surfaces surfaces = new Surfaces(colA, colB, colC, colD, colE, colPad);
			APEngine.addGroup(surfaces);

			Bridge bridge = new Bridge(colB, colC, colD);
			APEngine.addGroup(bridge);

			Capsule capsule = new Capsule(colC);
			APEngine.addGroup(capsule);

			rotator = new Rotator(colB, colE);
			APEngine.addGroup(rotator);

			SwingDoor swingDoor = new SwingDoor(colC);
			APEngine.addGroup(swingDoor);

			car = new Car(colC, colE);
			APEngine.addGroup(car);

			car.addCollidable(surfaces);
			car.addCollidable(bridge);
			car.addCollidable(rotator);
			car.addCollidable(swingDoor);
			car.addCollidable(capsule);

			capsule.addCollidable(surfaces);
			capsule.addCollidable(bridge);
			capsule.addCollidable(rotator);
			capsule.addCollidable(swingDoor);
		}

		public override void Run()
		{
			APEngine.step();
			rotator.rotateByRadian(.02f);
		}

		public override void HandleKeyDown(KeyEventArgs e)
		{
			// 276 - left 
			// 275 - right
			float keySpeed = 0.2f;

			if (e.KeyCode == Keys.Left)
			{
				car.Speed = -keySpeed;
			}
			else if (e.KeyCode == Keys.Right)
			{
				car.Speed = keySpeed;
			}
		}

		public override void HandleKeyUp(KeyEventArgs e)
		{
			car.Speed = 0;
		}

		public override void Paint(IRenderer renderer)
		{
			APEngine.paint(renderer);
		}
	}
}
