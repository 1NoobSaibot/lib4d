namespace Lib4D
{
	public struct Vector4DDouble
	{
		public double X, Y, Z, Q;


		public double AbsQuad
		{
			get => X * X + Y * Y + Z * Z + Q * Q;
		}


		public double Abs => System.Math.Sqrt(AbsQuad);


		public Vector4DDouble (double x = 0, double y = 0, double z = 0, double q = 0)
		{
			X = x;
			Y = y;
			Z = z;
			Q = q;
		}


		public Vector4DDouble Normalize()
		{
			double k = 1 / Abs;
			return k * this;
		}


		public static Vector4DDouble operator +(Vector4DDouble a, Vector4DDouble b)
		{
			return new Vector4DDouble (
				a.X + b.X,
				a.Y + b.Y,
				a.Z + b.Z,
				a.Q + b.Q
			);
		}


		public static Vector4DDouble operator -(Vector4DDouble a, Vector4DDouble b)
		{
			return new Vector4DDouble(
				a.X - b.X,
				a.Y - b.Y,
				a.Z - b.Z,
				a.Q - b.Q
			);
		}


		public static Vector4DDouble operator *(Vector4DDouble a, double b)
		{
			return new Vector4DDouble(
				a.X * b,
				a.Y * b,
				a.Z * b,
				a.Q * b
			);
		}


		public static Vector4DDouble operator *(double b, Vector4DDouble a)
		{
			return new Vector4DDouble(
				a.X * b,
				a.Y * b,
				a.Z * b,
				a.Q * b
			);
		}


		public static bool operator ==(Vector4DDouble a, Vector4DDouble b)
		{
			return a.X == b.X && a.Y == b.Y && a.Z == b.Z && a.Q == b.Q;
		}


		public static bool operator !=(Vector4DDouble a, Vector4DDouble b)
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
				fixed (Vector4DDouble* thisPtr = &this)
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
				fixed (Vector4DDouble* thisPtr = &this)
				{
					((double*)thisPtr)[i] = value;
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
