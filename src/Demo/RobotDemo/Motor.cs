using System;
using System.Drawing;
using APE;

namespace Demo.RobotDemo
{
	public class Motor : Composite
	{
		private static float ONE_THIRD = (float) ((Math.PI * 2) / 3);
		
		private WheelParticle wheel;
		private float radius;
		private CircleParticle _rimA;
		private CircleParticle _rimB;
		private CircleParticle _rimC;
		private int color;

		public float Power
		{
			get { return wheel.Speed; }
			set { wheel.Speed = value; }
		}

		public AbstractParticle RimA
		{
			get { return _rimA; }
		}

		public AbstractParticle RimB
		{
			get { return _rimB; }
		}

		public AbstractParticle RimC
		{
			get { return _rimC; }
		}
		
		public Motor(AbstractParticle attach, float radius, int color)
		{
			wheel = new WheelParticle(attach.PX, attach.PY - .01f, radius);
			wheel.SetStyle(0, 0, Color.FromArgb(255 / 2, 255, 0, 0).ToArgb());
			SpringConstraint axle = new SpringConstraint(wheel, attach);

			_rimA = new CircleParticle(0, 0, 2, true);
			_rimB = new CircleParticle(0, 0, 2, true);
			_rimC = new CircleParticle(0, 0, 2, true);
			
			wheel.Collidable = false;
			_rimA.Collidable = false;
			_rimB.Collidable = false;
			_rimC.Collidable = false;
			
			this.Particles.Add(_rimA);
			this.Particles.Add(_rimB);
			this.Particles.Add(_rimC);
			this.Particles.Add(wheel);
			this.Constraints.Add(axle);
			
			this.color = color;	
			this.radius = radius;
			
			// run it once to make sure the rim particles are in the right place
			run();
		}
	
		public void run()
		{	
			// align the rim particle based on the wheel rotation
			float theta = wheel.Radian;
			_rimA.PX = (float) (-radius * Math.Sin(theta) + wheel.PX);
			_rimA.PY = (float) (radius * Math.Cos(theta) + wheel.PY);
			
			theta += ONE_THIRD;
			_rimB.PX = (float) (-radius * Math.Sin(theta) + wheel.PX);
			_rimB.PY = (float) (radius * Math.Cos(theta) + wheel.PY);
			
			theta += ONE_THIRD;
			_rimC.PX = (float) (-radius * Math.Sin(theta) + wheel.PX);
			_rimC.PY = (float) (radius * Math.Cos(theta) + wheel.PY);
		}
		
		// doing some custom painting here. contrast this with the custom painting of the
		// legs. in this case we draw the shape in the init method, and then just move/rotate
		// it in the paint method. one important thing here - the initial drawing happens in
		// object space (i.e., x = 0, y = 0) not world space. Another option would be to
		// just draw everything dynamically using the wheel and rim point locations.
		public override void Init()
		{	
			/*sg.clear();
			sg.lineStyle(0, color, 1);
			sg.beginFill(color);
			sg.drawCircle(0, 0, 3);
			sg.endFill();
			
			var theta:Number = 0;
			var cx:Number = -radius * Math.sin(theta);
			var cy:Number =  radius * Math.cos(theta); 
			sg.moveTo(0,0);
			sg.lineTo(cx,cy);
			sg.drawCircle(cx, cy, 2);
		
			theta += ONE_THIRD;
			cx = -radius * Math.sin(theta);
			cy =  radius * Math.cos(theta); 
			sg.moveTo(0,0);
			sg.lineTo(cx,cy);
			sg.drawCircle(cx, cy, 2);
			
			theta += ONE_THIRD;
			cx = -radius * Math.sin(theta);
			cy =  radius * Math.cos(theta); 
			sg.moveTo(0,0);
			sg.lineTo(cx,cy);
			sg.drawCircle(cx, cy, 2);*/	
		}
		
		public override void Paint(IRenderer renderer)
		{
			/*sprite.x = wheel.px;
			sprite.y = wheel.py;
			sprite.rotation = wheel.angle;*/
		}
	}
}
