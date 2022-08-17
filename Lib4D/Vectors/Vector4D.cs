using System;

namespace Lib4D
{
	public struct Vector4D
	{
		public double X, Y, Z, Q;


		public double AbsQuad
		{
			get => X * X + Y * Y + Z * Z + Q * Q;
		}


		public double Abs => Math.Sqrt(AbsQuad);


		public Vector4D (double x = 0, double y = 0, double z = 0, double q = 0)
		{
			X = x;
			Y = y;
			Z = z;
			Q = q;
		}


		public Vector4D Normalize()
		{
			double k = 1 / Abs;
			return k * this;
		}


		public static Vector4D operator +(Vector4D a, Vector4D b)
		{
			return new Vector4D (
				a.X + b.X,
				a.Y + b.Y,
				a.Z + b.Z,
				a.Q + b.Q
			);
		}


		public static Vector4D operator -(Vector4D a, Vector4D b)
		{
			return new Vector4D(
				a.X - b.X,
				a.Y - b.Y,
				a.Z - b.Z,
				a.Q - b.Q
			);
		}


		public static Vector4D operator *(Vector4D a, double b)
		{
			return new Vector4D(
				a.X * b,
				a.Y * b,
				a.Z * b,
				a.Q * b
			);
		}


		public static Vector4D operator *(double b, Vector4D a)
		{
			return new Vector4D(
				a.X * b,
				a.Y * b,
				a.Z * b,
				a.Q * b
			);
		}


		public static bool operator ==(Vector4D a, Vector4D b)
		{
			return a.X == b.X && a.Y == b.Y && a.Z == b.Z && a.Q == b.Q;
		}


		public static bool operator !=(Vector4D a, Vector4D b)
		{
			return !(a == b);
		}


		public unsafe double this[int i] {
			get
			{
				if (i < 0 || i >= 4)
				{
					throw new Exception("Index is out of range 4D Vector: " + i);
				}
				fixed (Vector4D* thisPtr = &this)
				{
					return ((double*)thisPtr)[i];
				}
			}
			set
			{
				if (i < 0 || i >= 4)
				{
					throw new Exception("Index is out of range 4D Vector: " + i);
				}
				fixed (Vector4D* thisPtr = &this)
				{
					((double*)thisPtr)[i] = value;
				}
			}
		}


		public override string ToString()
		{
			return "(" + X + "; " + Y + "; " + Z + "; " + Q + ")";
		}
	}
}
