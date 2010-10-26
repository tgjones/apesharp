using System;
using APE;

namespace Demo.RobotDemo
{
	public class Leg : Composite
	{
		private CircleParticle pa;
		private CircleParticle pb;
		private CircleParticle pc;
		private CircleParticle pd;
		private CircleParticle pe;
		private CircleParticle pf;
		private CircleParticle ph;
	
		private int lineColor;
		private float lineWeight;
		
		private int fillColor;
		private bool _visible;

		public CircleParticle Cam
		{
			get { return ph; }
		}

		public CircleParticle Fix
		{
			get { return pa; }
		}

		public bool Visible
		{
			set { _visible = value; }
		}
		
		public Leg (
			float px,
			float py,
			int orientation,
			float scale,
			float lineWeight,
			int lineColor,
			int fillColor)
		{
			this.lineColor = lineColor;
			this.lineWeight = lineWeight;
			
			this.fillColor = fillColor;
			
			// top triangle -- pa is the attach point to the body
			float os = orientation * scale;
			pa = new CircleParticle(px + 31 * os, py - 8 * scale, 1);
			pb = new CircleParticle(px + 25 * os, py - 37 * scale, 1);
			pc = new CircleParticle(px + 60 * os, py - 15 * scale, 1);
			
			// bottom triangle particles -- pf is the foot
			pd = new CircleParticle(px + 72 * os, py + 12 * scale,  1);
			pe = new CircleParticle(px + 43 * os, py + 19 * scale,  1);
			pf = new CircleParticle(px + 54 * os, py + 61 * scale,  2);
			
			// strut attach point particle
			ph = new CircleParticle(px, py, 3);
			
			// top triangle constraints
			SpringConstraint cAB = new SpringConstraint(pa,pb,1);
			SpringConstraint cBC = new SpringConstraint(pb,pc,1);
			SpringConstraint cCA = new SpringConstraint(pc,pa,1);
			
			// middle leg constraints
			SpringConstraint cCD = new SpringConstraint(pc,pd,1);
			SpringConstraint cAE = new SpringConstraint(pa,pe,1);
			
			// bottom leg constraints
			SpringConstraint cDE = new SpringConstraint(pd,pe,1);
			SpringConstraint cDF = new SpringConstraint(pd,pf,1);
			SpringConstraint cEF = new SpringConstraint(pe,pf,1);
			
			// cam constraints
			SpringConstraint cBH = new SpringConstraint(pb,ph,1);
			SpringConstraint cEH = new SpringConstraint(pe,ph,1);
			
			this.Particles.Add(pa);
			this.Particles.Add(pb);
			this.Particles.Add(pc);
			this.Particles.Add(pd);
			this.Particles.Add(pe);
			this.Particles.Add(pf);
			this.Particles.Add(ph);	
			
			this.Constraints.Add(cAB);
			this.Constraints.Add(cBC);
			this.Constraints.Add(cCA);
			this.Constraints.Add(cCD);
			this.Constraints.Add(cAE);
			this.Constraints.Add(cDE);
			this.Constraints.Add(cDF);
			this.Constraints.Add(cEF);
			this.Constraints.Add(cBH);
			this.Constraints.Add(cEH);	
			
			// for added efficiency, only test the feet (pf) for collision. these
			// selective tweaks should always be considered for best performance.
			pa.Collidable = false;
			pb.Collidable = false;
			pc.Collidable = false;
			pd.Collidable = false;
			pe.Collidable = false;
			ph.Collidable = false;
			
			_visible = true;
		}
	
		// in most cases when you want to do custom painting youll need to override init because
		// it sets up the sprites with vector drawings that get moved around in the default paint
		// method. in this case were dynamically drawing the legs so we dont need to do anything
		// with the init override, eg draw the leg first and then move it around in the paint method.
		// by doing nothing here we prevent the init from being called on all the particles and 
		// constraints of the leg, which is what we want.
		public override void Init()
		{
		}
		
		
		public override void Paint(IRenderer renderer)
		{
			/*
			sg.clear();
			if (! _visible) return;
			
			sg.lineStyle(lineWeight, lineColor, lineAlpha);
			sg.beginFill(fillColor, fillAlpha);
			
			sg.moveTo(pa.px, pa.py);
			sg.lineTo(pb.px, pb.py);
			sg.lineTo(pc.px, pc.py);
			sg.lineTo(pa.px, pa.py);
			
			sg.moveTo(pd.px, pd.py);
			sg.lineTo(pe.px, pe.py);
			sg.lineTo(pf.px, pf.py);
			sg.lineTo(pd.px, pd.py);
			sg.endFill();
			
			// triangle to triangle
			sg.moveTo(pa.px, pa.py);
			sg.lineTo(pe.px, pe.py);
			sg.moveTo(pc.px, pc.py);
			sg.lineTo(pd.px, pd.py);
			
			// leg motor attachments
			sg.moveTo(pb.px, pb.py);
			sg.lineTo(ph.px, ph.py);
			sg.moveTo(pe.px, pe.py);
			sg.lineTo(ph.px, ph.py);
			
			sg.drawCircle(pf.px, pf.py, pf.radius);*/

			renderer.DrawCircle(pf.PX, pf.PY, pf.Radius, lineWeight, lineColor);
		}
	}
}