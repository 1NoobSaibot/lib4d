using System;

namespace Lib4D
{
	public struct Vector3D
	{
		public double X, Y, Z;


		public double AbsQuad
		{
			get => X * X + Y * Y + Z * Z;
		}


		public double Abs => Math.Sqrt(AbsQuad);


		public Vector3D (double x, double y, double z)
		{
			X = x;
			Y = y;
			Z = z;
		}


		public Vector3D Normalize()
		{
			double k = 1 / Abs;
			return new Vector3D(k * X, k * Y, k * Z);
		}


		public static Vector3D operator +(Vector3D a, Vector3D b)
		{
			return new Vector3D (a.X + b.X, a.Y + b.Y, a.Z + b.Z);
		}


		public static Vector3D operator -(Vector3D a, Vector3D b)
		{
			return new Vector3D(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
		}


		public override string ToString()
		{
			return "(" + X + "; " + Y + "; " + Z + ")";
		}
	}
}
