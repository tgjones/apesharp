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
	public struct Vector
	{
		public float X, Y;

		public Vector(float x, float y)
		{
			X = x;
			Y = y;
		}

		public float Dot(Vector v)
		{
			return X * v.X + Y * v.Y;
		}

		public float Cross(Vector v)
		{
			return X * v.Y - Y * v.X;
		}

		#region Operators

		public static Vector operator -(Vector v1, Vector v2)
		{
			return new Vector(v1.X - v2.X, v1.Y - v2.Y);
		}

		public static Vector operator +(Vector v1, Vector v2)
		{
			return new Vector(v1.X + v2.X, v1.Y + v2.Y);
		}

		public static Vector operator *(Vector v, float s)
		{
			return new Vector(v.X * s, v.Y * s);
		}

		public static Vector operator /(Vector v, float s)
		{
			if (s == 0) s = 0.0001f;
			return new Vector(v.X / s, v.Y / s);
		}

		#endregion

		public float magnitude()
		{
			return (float) Math.Sqrt(X * X + Y * Y);
		}

		public float distance(Vector v)
		{
			Vector temp = this - v;
			float delta = temp.magnitude();
			return delta;
		}

		public Vector normalize()
		{
			float m = magnitude();
			if (m == 0) m = 0.0001f;
			return this * (1 / m);
		}
	}
}
