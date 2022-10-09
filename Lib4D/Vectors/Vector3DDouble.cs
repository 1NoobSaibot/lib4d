using System;

namespace Lib4D
{
	public struct Vector3DDouble
	{
		public double X, Y, Z;


		public double AbsQuad
		{
			get => X * X + Y * Y + Z * Z;
		}


		public double Abs => Math.Sqrt(AbsQuad);


		public Vector3DDouble (double x, double y, double z)
		{
			X = x;
			Y = y;
			Z = z;
		}


		public Vector3DDouble Normalize()
		{
			double k = 1 / Abs;
			return new Vector3DDouble(k * X, k * Y, k * Z);
		}


		public static Vector3DDouble operator +(Vector3DDouble a, Vector3DDouble b)
		{
			return new Vector3DDouble (a.X + b.X, a.Y + b.Y, a.Z + b.Z);
		}


		public static Vector3DDouble operator -(Vector3DDouble a, Vector3DDouble b)
		{
			return new Vector3DDouble(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
		}


		public override string ToString()
		{
			return "(" + X + "; " + Y + "; " + Z + ")";
		}
	}
}
