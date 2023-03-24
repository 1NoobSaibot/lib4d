using System;

namespace Lib4D
{
	public struct Vector3DFloat
	{
		public float X, Y, Z;


		public float AbsQuad
		{
			get => X * X + Y * Y + Z * Z;
		}


		public float Abs => (float)Math.Sqrt(AbsQuad);


		public Vector3DFloat (float x, float y, float z)
		{
			X = x;
			Y = y;
			Z = z;
		}


		public Vector3DFloat Normalize()
		{
			float k = 1 / Abs;
			return this * k;
		}


		public float[,] ToMatrixRow()
		{
			float[,] res = new float[3, 1];
			res[0, 0] = X;
			res[1, 0] = Y;
			res[2, 0] = Z;
			return res;
		}


		public static Vector3DFloat operator +(Vector3DFloat a, Vector3DFloat b)
		{
			return new Vector3DFloat (a.X + b.X, a.Y + b.Y, a.Z + b.Z);
		}


		public static Vector3DFloat operator -(Vector3DFloat a, Vector3DFloat b)
		{
			return new Vector3DFloat(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
		}


		public override string ToString()
		{
			return "(" + X + "; " + Y + "; " + Z + ")";
		}


		public static Vector3DFloat operator *(Vector3DFloat a, float b)
		{
			return new Vector3DFloat(
				a.X * b,
				a.Y * b,
				a.Z * b
			);
		}


		public static Vector3DFloat operator *(float l, Vector3DFloat r)
		{
			return new Vector3DFloat(
				r.X * l,
				r.Y * l,
				r.Z * l
			);
		}


		public static Vector3DFloat operator *(Vector3DFloat a, Vector3DFloat b)
		{
			return new Vector3DFloat(
				a.Y * b.Z - a.Z * b.Y,
				a.Z * b.X - a.X * b.Z,
				a.X * b.Y - a.Y * b.X
			);
		}


		public static Vector3DFloat operator *(Vector3DFloat v, float[,] m)
		{
			float[,] row = v.ToMatrixRow();
			row = MatrixMath.Mul(row, m);
			return new Vector3DFloat(row[0, 0], row[1, 0], row[2, 0]);
		}
	}
}
