using System;
using APE;

namespace Demo.RobotDemo
{
	public class Body : Composite
	{
		private AbstractParticle top;
		private AbstractParticle rgt;
		private AbstractParticle lft;
		private AbstractParticle bot;
		private AbstractParticle ctr;

		public AbstractParticle Left
		{
			get { return lft; }
		}

		public AbstractParticle Center
		{
			get { return ctr; }
		}

		public AbstractParticle Right
		{
			get { return rgt; }
		}
			
		public Body(
				AbstractParticle left,
				AbstractParticle right,
				int height,
				float lineWeight,
				int lineColor)
		{

			float cpx = (right.PX + left.PX) / 2;
			float cpy = right.PY;

			rgt = new CircleParticle(right.PX, right.PY, 1);
			rgt.SetStyle(3, lineColor, lineColor);
			lft = new CircleParticle(left.PX, left.PY, 1);
			lft.SetStyle(3, lineColor, lineColor);

			ctr = new CircleParticle(cpx, cpy, 1);
			ctr.Visible = false;
			top = new CircleParticle(cpx, cpy - height / 2, 1);
			top.Visible = false;
			bot = new CircleParticle(cpx, cpy + height / 2, 1);
			bot.Visible = false;

			// outer constraints
			SpringConstraint tr = new SpringConstraint(top, rgt, 1);
			tr.Visible = false;
			SpringConstraint rb = new SpringConstraint(rgt, bot, 1);
			rb.Visible = false;
			SpringConstraint bl = new SpringConstraint(bot, lft, 1);
			bl.Visible = false;
			SpringConstraint lt = new SpringConstraint(lft, top, 1);
			lt.Visible = false;

			// inner constrainst
			SpringConstraint ct = new SpringConstraint(top, this.Center, 1);
			ct.Visible = false;
			SpringConstraint cr = new SpringConstraint(rgt, this.Center, 1);
			cr.SetLine(lineWeight, lineColor);
			SpringConstraint cb = new SpringConstraint(bot, this.Center, 1);
			cb.Visible = false;
			SpringConstraint cl = new SpringConstraint(lft, this.Center, 1);
			cl.SetLine(lineWeight, lineColor);

			ctr.Collidable = false;
			top.Collidable = false;
			rgt.Collidable = false;
			bot.Collidable = false;
			lft.Collidable = false;

			this.Particles.Add(ctr);
			this.Particles.Add(top);
			this.Particles.Add(rgt);
			this.Particles.Add(bot);
			this.Particles.Add(lft);

			this.Constraints.Add(tr);
			this.Constraints.Add(rb);
			this.Constraints.Add(bl);
			this.Constraints.Add(lt);

			this.Constraints.Add(ct);
			this.Constraints.Add(cr);
			this.Constraints.Add(cb);
			this.Constraints.Add(cl);
		}
	}
}