using Lib4D.Mathematic;
using Lib4D.Mathematic.Matrix;
using System.Numerics;

namespace Lib4D
{
	public struct Vector4D<TNumber> where TNumber : INumber<TNumber>
	{
		public TNumber X, Y, Z, Q;


		public TNumber AbsQuad
		{
			get => X * X + Y * Y + Z * Z + Q * Q;
		}


		public TNumber Abs => Math<TNumber>.Sqrt!(AbsQuad);


		#region Constructors
		public Vector4D()
		{
			X = TNumber.Zero;
			Y = TNumber.Zero;
			Z = TNumber.Zero;
			Q = TNumber.Zero;
		}

		public Vector4D(TNumber x)
		{
			X = x;
			Y = TNumber.Zero;
			Z = TNumber.Zero;
			Q = TNumber.Zero;
		}

		public Vector4D(TNumber x, TNumber y)
		{
			X = x;
			Y = y;
			Z = TNumber.Zero;
			Q = TNumber.Zero;
		}

		public Vector4D(TNumber x, TNumber y, TNumber z)
		{
			X = x;
			Y = y;
			Z = z;
			Q = TNumber.Zero;
		}

		public Vector4D (TNumber x, TNumber y, TNumber z, TNumber q)
		{
			X = x;
			Y = y;
			Z = z;
			Q = q;
		}

		public Vector4D(double x)
		{
			X = Math<TNumber>.Double2Number!(x);
			Y = TNumber.Zero;
			Z = TNumber.Zero;
			Q = TNumber.Zero;
		}

		public Vector4D(double x, double y)
		{
			X = Math<TNumber>.Double2Number!(x);
			Y = Math<TNumber>.Double2Number!(y);
			Z = TNumber.Zero;
			Q = TNumber.Zero;
		}

		public Vector4D(double x, double y, double z)
		{
			X = Math<TNumber>.Double2Number!(x);
			Y = Math<TNumber>.Double2Number!(y);
			Z = Math<TNumber>.Double2Number!(z);
			Q = TNumber.Zero;
		}

		public Vector4D(double x, double y, double z, double q)
		{
			X = Math<TNumber>.Double2Number!(x);
			Y = Math<TNumber>.Double2Number!(y);
			Z = Math<TNumber>.Double2Number!(z);
			Q = Math<TNumber>.Double2Number!(q);
		}
		#endregion


		public void Normalize()
		{
			TNumber k = TNumber.One / Abs;
			X *= k;
			Y *= k;
			Z *= k;
			Q *= k;
		}

		public Vector4D<TNumber> GetNormalized()
		{
			TNumber k = TNumber.One / Abs;
			return k * this;
		}


		public TNumber[,] ToMatrixRow()
		{
			return new TNumber[4, 1]
			{
				{ X }, { Y }, { Z }, { Q }
			};
		}


		public static Vector4D<TNumber> operator +(Vector4D<TNumber> a, Vector4D<TNumber> b)
		{
			return new(
				a.X + b.X,
				a.Y + b.Y,
				a.Z + b.Z,
				a.Q + b.Q
			);
		}


		public static Vector4D<TNumber> operator -(Vector4D<TNumber> a, Vector4D<TNumber> b)
		{
			return new(
				a.X - b.X,
				a.Y - b.Y,
				a.Z - b.Z,
				a.Q - b.Q
			);
		}


		public static Vector4D<TNumber> operator *(Vector4D<TNumber> a, TNumber b)
		{
			return new(
				a.X * b,
				a.Y * b,
				a.Z * b,
				a.Q * b
			);
		}


		public static Vector4D<TNumber> operator *(TNumber b, Vector4D<TNumber> a)
		{
			return new(
				a.X * b,
				a.Y * b,
				a.Z * b,
				a.Q * b
			);
		}


		public static Vector4D<TNumber> operator *(Vector4D<TNumber> v, TNumber[,] m)
		{
			TNumber[,] row = v.ToMatrixRow();
			row = MatrixMath.Mul(row, m);
			return new(row[0, 0], row[1, 0], row[2, 0], row[3, 0]);
		}


		public static Vector4D<TNumber> operator /(Vector4D<TNumber> a, TNumber b)
		{
			return new(
				a.X / b,
				a.Y / b,
				a.Z / b,
				a.Q / b
			);
		}


		public static bool operator ==(Vector4D<TNumber> a, Vector4D<TNumber> b)
		{
			return a.X == b.X && a.Y == b.Y && a.Z == b.Z && a.Q == b.Q;
		}


		public static bool operator !=(Vector4D<TNumber> a, Vector4D<TNumber> b)
		{
			return a.X != b.X || a.Y != b.Y || a.Z != b.Z || a.Q != b.Q;
		}


		public unsafe TNumber this[int i] {
#pragma warning disable CS8500 // Это принимает адрес, получает размер или объявляет указатель на управляемый тип
			get
			{
				if (i < 0 || i >= 4)
				{
					throw new Exception("Index is out of range 4D Vector: " + i);
				}
				fixed (Vector4D<TNumber>* thisPtr = &this)
				{
					return ((TNumber*)thisPtr)[i];
				}

			}
			set
			{
				if (i < 0 || i >= 4)
				{
					throw new Exception("Index is out of range 4D Vector: " + i);
				}
				fixed (Vector4D<TNumber>* thisPtr = &this)
				{
					((TNumber*)thisPtr)[i] = value;
				}
			}
#pragma warning restore CS8500 // Это принимает адрес, получает размер или объявляет указатель на управляемый тип
		}


		public override string ToString()
		{
			string sX = string.Format("{0:f2}", X);
			string sY = string.Format("{0:f2}", Y);
			string sZ = string.Format("{0:f2}", Z);
			string sQ = string.Format("{0:f2}", Q);
			return "(" + sX + "; " + sY + "; " + sZ + "; " + sQ + ")";
		}


		public override bool Equals(object? obj)
		{
			return obj is Vector4D<TNumber> v && this == v;
		}

		public override int GetHashCode()
		{
			return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode() ^ Q.GetHashCode();
		}
	}
}
