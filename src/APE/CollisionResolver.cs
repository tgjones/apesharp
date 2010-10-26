/*
Copyright (c) 2006, 2007 Alec Cove

Permission is hereby granted, free of charge, to any person obtaining a copy of this 
software and associated documentation files (the "Software"), to deal in the Software 
without restriction, including without limitation the rights to use, copy, modify, 
merge, publish, distribute, sublicense, and/or sell copies of the Software, and to 
permit persons to whom the Software is furnished to do so, subject to the following 
conditions:

The above copyright notice and this permission notice shall be included in all copies 
or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE 
OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

Converted to C# by Tim Jones tim@jones.name
*/

using System;

namespace APE
{
	public static class CollisionResolver
	{
		public static void resolveParticleParticle(AbstractParticle pa, AbstractParticle pb, Vector normal, float depth)
		{
			// a collision has occured. set the current positions to sample locations
			pa.curr = pa.samp;
			pb.curr = pb.samp;

			Vector mtd = normal * depth;
			float te = pa.Elasticity + pb.Elasticity;
			float sumInvMass = pa.InvMass + pb.InvMass;

			// the total friction in a collision is combined but clamped to [0,1]
			float tf = MathUtil.Clamp(1 - (pa.Friction + pb.Friction), 0, 1);

			// get the collision components, vn and vt
			Collision ca = pa.getComponents(normal);
			Collision cb = pb.getComponents(normal);

			// calculate the coefficient of restitution based on the mass, as the normal component
			Vector cbvn = cb.vn * ((te + 1) * pa.InvMass);
			Vector cavn = ca.vn * (pb.InvMass - te * pa.InvMass);
			Vector vnA = (cbvn + cavn) / sumInvMass;

			cavn = ca.vn * ((te + 1) * pb.InvMass);
			cbvn = cb.vn * (pa.InvMass - te * pb.InvMass);

			Vector vnB = (cavn + cbvn) / sumInvMass;

			// apply friction to the tangental component
			ca.vt *= tf;
			cb.vt *= tf;

			// scale the mtd by the ratio of the masses. heavier particles move less 
			Vector mtdA = mtd * (pa.InvMass / sumInvMass);
			Vector mtdB = mtd * (-pb.InvMass / sumInvMass);

			// add the tangental component to the normal component for the new velocity 
			vnA += ca.vt;
			vnB += cb.vt;

			if (!pa.Fixed) pa.resolveCollision(mtdA, vnA, normal, depth, -1, pb);
			if (!pb.Fixed) pb.resolveCollision(mtdB, vnB, normal, depth, 1, pa);
		}
	}
}