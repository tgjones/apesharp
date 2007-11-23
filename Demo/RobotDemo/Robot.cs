using System;
using APE;

namespace Demo.RobotDemo
{
	public class Robot : Group
	{
		private Body body;
		private Motor motor;

		private int direction;
		private float powerLevel;

		private bool powered;
		private bool legsVisible;

		private Leg legLA;
		private Leg legRA;
		private Leg legLB;
		private Leg legRB;
		private Leg legLC;
		private Leg legRC;

		public float PX
		{
			get { return body.Center.PX; }
		}

		public float PY
		{
			get { return body.Center.PY; }
		}

		public float Stiffness
		{
			set
			{
				// top level constraints in the group
				foreach (SpringConstraint sp in this.Constraints)
					sp.Stiffness = value;

				// constraints within this groups composites
				foreach (Composite c in this.Composites)
					foreach (SpringConstraint sp in c.Constraints)
						sp.Stiffness = value;
			}
		}

		public Robot(float px, float py, float scale, float power)
		{
			// legs
			legLA = new Leg(px, py, -1, scale, 2, unchecked((int) 0xFF444444), unchecked((int) 0xFF222222));
			legRA = new Leg(px, py, 1, scale, 2, 0x444444, 0x222222);
			legLB = new Leg(px, py, -1, scale, 2, 0x666666, 0x444444);
			legRB = new Leg(px, py, 1, scale, 2, 0x666666, 0x444444);
			legLC = new Leg(px, py, -1, scale, 2, 0x888888, 0x666666);
			legRC = new Leg(px, py, 1, scale, 2, 0x888888, 0x666666);

			// body
			body = new Body(legLA.Fix, legRA.Fix, (int) (30 * scale), 2, 0x336699);

			// motor
			motor = new Motor(body.Center, 8 * scale, 0x336699);

			// connect the body to the legs
			SpringConstraint connLA = new SpringConstraint(body.Left, legLA.Fix, 1);
			SpringConstraint connRA = new SpringConstraint(body.Right, legRA.Fix, 1);
			SpringConstraint connLB = new SpringConstraint(body.Left, legLB.Fix, 1);
			SpringConstraint connRB = new SpringConstraint(body.Right, legRB.Fix, 1);
			SpringConstraint connLC = new SpringConstraint(body.Left, legLC.Fix, 1);
			SpringConstraint connRC = new SpringConstraint(body.Right, legRC.Fix, 1);

			// connect the legs to the motor
			legLA.Cam.Position = motor.RimA.Position;
			legRA.Cam.Position = motor.RimA.Position;
			SpringConstraint connLAA = new SpringConstraint(legLA.Cam, motor.RimA, 1);
			SpringConstraint connRAA = new SpringConstraint(legRA.Cam, motor.RimA, 1);

			legLB.Cam.Position = motor.RimB.Position;
			legRB.Cam.Position = motor.RimB.Position;
			SpringConstraint connLBB = new SpringConstraint(legLB.Cam, motor.RimB, 1);
			SpringConstraint connRBB = new SpringConstraint(legRB.Cam, motor.RimB, 1);

			legLC.Cam.Position = motor.RimC.Position;
			legRC.Cam.Position = motor.RimC.Position;
			SpringConstraint connLCC = new SpringConstraint(legLC.Cam, motor.RimC, 1);
			SpringConstraint connRCC = new SpringConstraint(legRC.Cam, motor.RimC, 1);

			connLAA.SetLine(2, 0x999999);
			connRAA.SetLine(2, 0x999999);
			connLBB.SetLine(2, 0x999999);
			connRBB.SetLine(2, 0x999999);
			connLCC.SetLine(2, 0x999999);
			connRCC.SetLine(2, 0x999999);

			// add to the engine
			this.Composites.Add(legLA);
			this.Composites.Add(legRA);
			this.Composites.Add(legLB);
			this.Composites.Add(legRB);
			this.Composites.Add(legLC);
			this.Composites.Add(legRC);

			this.Composites.Add(body);
			this.Composites.Add(motor);

			this.Constraints.Add(connLA);
			this.Constraints.Add(connRA);
			this.Constraints.Add(connLB);
			this.Constraints.Add(connRB);
			this.Constraints.Add(connLC);
			this.Constraints.Add(connRC);

			this.Constraints.Add(connLAA);
			this.Constraints.Add(connRAA);
			this.Constraints.Add(connLBB);
			this.Constraints.Add(connRBB);
			this.Constraints.Add(connLCC);
			this.Constraints.Add(connRCC);

			direction = -1;
			powerLevel = power;

			powered = true;
			legsVisible = true;
		}

		public void run()
		{
			motor.run();
		}

		public void togglePower()
		{
			powered = !powered;

			if (powered)
			{
				motor.Power = powerLevel * direction;
				Stiffness = 1;
				APEngine.Damping = 0.99f;
			}
			else
			{
				motor.Power = 0;
				Stiffness = 0.2f;
				APEngine.Damping = 0.35f;
			}
		}


		public void toggleDirection()
		{
			direction *= -1;
			motor.Power = powerLevel * direction;
		}

		public void toggleLegs()
		{
			legsVisible = !legsVisible;
			if (!legsVisible)
			{
				legLA.Visible = false;
				legRA.Visible = false;
				legLB.Visible = false;
				legRB.Visible = false;
			}
			else
			{
				legLA.Visible = true;
				legRA.Visible = true;
				legLB.Visible = true;
				legRB.Visible = true;
			}
		}
	}
}