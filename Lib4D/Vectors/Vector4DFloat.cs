using System;

namespace Lib4D
{
	public struct Vector4DFloat
	{
		public float X, Y, Z, Q;


		public float AbsQuad
		{
			get => X * X + Y * Y + Z * Z + Q * Q;
		}


		public float Abs => (float)Math.Sqrt(AbsQuad);


		public Vector4DFloat (float x = 0, float y = 0, float z = 0, float q = 0)
		{
			X = x;
			Y = y;
			Z = z;
			Q = q;
		}


		public Vector4DFloat Normalize()
		{
			float k = 1 / Abs;
			return k * this;
		}


		public float[,] ToMatrixRow()
		{
			return new float[4, 1]
			{
				{ X }, { Y }, { Z }, { Q }
			};
		}


		public static Vector4DFloat operator +(Vector4DFloat a, Vector4DFloat b)
		{
			return new Vector4DFloat(
				a.X + b.X,
				a.Y + b.Y,
				a.Z + b.Z,
				a.Q + b.Q
			);
		}


		public static Vector4DFloat operator -(Vector4DFloat a, Vector4DFloat b)
		{
			return new Vector4DFloat(
				a.X - b.X,
				a.Y - b.Y,
				a.Z - b.Z,
				a.Q - b.Q
			);
		}


		public static Vector4DFloat operator *(Vector4DFloat a, float b)
		{
			return new Vector4DFloat(
				a.X * b,
				a.Y * b,
				a.Z * b,
				a.Q * b
			);
		}


		public static Vector4DFloat operator *(float b, Vector4DFloat a)
		{
			return new Vector4DFloat(
				a.X * b,
				a.Y * b,
				a.Z * b,
				a.Q * b
			);
		}


		public static Vector4DFloat operator *(Vector4DFloat v, float[,] m)
		{
			float[,] row = v.ToMatrixRow();
			row = MatrixMath.Mul(row, m);
			return new Vector4DFloat(row[0, 0], row[1, 0], row[2, 0], row[3, 0]);
		}


		public static Vector4DFloat operator /(Vector4DFloat a, float b)
		{
			return new Vector4DFloat(
				a.X / b,
				a.Y / b,
				a.Z / b,
				a.Q / b
			);
		}


		public static bool operator ==(Vector4DFloat a, Vector4DFloat b)
		{
			return a.X == b.X && a.Y == b.Y && a.Z == b.Z && a.Q == b.Q;
		}


		public static bool operator !=(Vector4DFloat a, Vector4DFloat b)
		{
			return !(a == b);
		}


		public unsafe float this[int i] {
			get
			{
				if (i < 0 || i >= 4)
				{
					throw new Exception("Index is out of range 4D Vector: " + i);
				}
				fixed (Vector4DFloat* thisPtr = &this)
				{
					return ((float*)thisPtr)[i];
				}
			}
			set
			{
				if (i < 0 || i >= 4)
				{
					throw new Exception("Index is out of range 4D Vector: " + i);
				}
				fixed (Vector4DFloat* thisPtr = &this)
				{
					((float*)thisPtr)[i] = value;
				}
			}
		}


		public override string ToString()
		{
			string sX = string.Format("{0:f2}", X);
			string sY = string.Format("{0:f2}", Y);
			string sZ = string.Format("{0:f2}", Z);
			string sQ = string.Format("{0:f2}", Q);
			return "(" + sX + "; " + sY + "; " + sZ + "; " + sQ + ")";
		}
	}
}
