using Lib4D.Mathematic;
using Lib4D.Mathematic.Matrix;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace Lib4D
{
	public struct Vector3D<TNumber>
		: IEquatable<Vector3D<TNumber>>,
		IEqualityOperators<Vector3D<TNumber>, Vector3D<TNumber>, bool>
		where TNumber : INumber<TNumber>
	{
		public TNumber X = TNumber.Zero;
		public TNumber Y = TNumber.Zero;
		public TNumber Z = TNumber.Zero;


		public TNumber AbsQuad
		{
			get => X * X + Y * Y + Z * Z;
		}

		public TNumber Abs => Math<TNumber>.Sqrt!(AbsQuad);


		#region Constructors
		public Vector3D(TNumber x)
		{
			X = x;
			Y = TNumber.Zero;
			Z = TNumber.Zero;
		}
		public Vector3D(TNumber x, TNumber y)
		{
			X = x;
			Y = y;
			Z = TNumber.Zero;
		}
		public Vector3D (TNumber x, TNumber y, TNumber z)
		{
			X = x;
			Y = y;
			Z = z;
		}
		public Vector3D(double x)
		{
			X = Math<TNumber>.Double2Number!(x);
			Y = TNumber.Zero;
			Z = TNumber.Zero;
		}
		public Vector3D(double x, double y)
		{
			X = Math<TNumber>.Double2Number!(x);
			Y = Math<TNumber>.Double2Number!(y);
			Z = TNumber.Zero;
		}
		public Vector3D(double x, double y, double z)
		{
			X = Math<TNumber>.Double2Number!(x);
			Y = Math<TNumber>.Double2Number!(y);
			Z = Math<TNumber>.Double2Number!(z);
		}
		#endregion


		public void Normalize()
		{
			var absQuad = AbsQuad;
			if (absQuad == TNumber.Zero)
			{
				X = TNumber.One;
			}

			var abs = Math<TNumber>.Sqrt!(AbsQuad);
			var k = TNumber.One / abs;
			X *= k;
			Y *= k;
			Z *= k;
		}


		public Vector3D<TNumber> GetNormalized()
		{
			var absQuad = AbsQuad;
			if (absQuad == TNumber.Zero)
			{
				return new(TNumber.One, TNumber.Zero, TNumber.Zero);
			}

			var abs = Math<TNumber>.Sqrt!(AbsQuad);
			var k = TNumber.One / abs;
			return new(k * X, k * Y, k * Z);
		}


		public static Vector3D<TNumber> operator +(Vector3D<TNumber> a, Vector3D<TNumber> b)
		{
			return new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
		}


		public static Vector3D<TNumber> operator -(Vector3D<TNumber> a, Vector3D<TNumber> b)
		{
			return new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
		}


		#region Multiplication By A Number
		public static Vector3D<TNumber> operator *(Vector3D<TNumber> a, TNumber b)
		{
			return new Vector3D<TNumber>(a.X * b, a.Y * b, a.Z * b);
		}
		public static Vector3D<TNumber> operator *(TNumber a, Vector3D<TNumber> b)
		{
			return new Vector3D<TNumber>(b.X * a, b.Y * a, b.Z * a);
		}
		public static Vector3D<TNumber> operator -(Vector3D<TNumber> a)
		{
			return new(-a.X, -a.Y, -a.Z);
		}
		#endregion


		#region Cross Vector Multiplication
		public static Vector3D<TNumber> operator *(Vector3D<TNumber> a, Vector3D<TNumber> b)
		{
			return new Vector3D<TNumber>(
				a.Y * b.Z - a.Z * b.Y,
				a.Z * b.X - a.X * b.Z,
				a.X * b.Y - a.Y * b.X
			);
		}
		#endregion


		// TODO: It must be inside MatrixMath, not here
		#region Multiplication with Matrix
		public static Vector3D<TNumber> operator *(TNumber[,] m, Vector3D<TNumber> v)
		{
			TNumber[,] column = new TNumber[1, 3]
			{
				{ v.X, v.Y, v.Z },
			};
			TNumber[,] r = MatrixMath.Mul(m, column);
			return new Vector3D<TNumber>(r[0, 0], r[0, 1], r[0, 3]);
		}

		public static Vector3D<TNumber> operator *(Vector3D<TNumber> a, TNumber[,] b)
		{
			TNumber[,] row = new TNumber[3, 1]
			{
				{ a.X }, { a.Y }, { a.Z }
			};
			TNumber[,] r = MatrixMath.Mul(row, b);
			return new Vector3D<TNumber>(r[0, 0], r[1, 0], r[2, 0]);
		}
		#endregion


		#region Equality
		public static bool operator ==(Vector3D<TNumber> a, Vector3D<TNumber> b)
		{
			return
				a.X == b.X &&
				a.Y == b.Y &&
				a.Z == b.Z;
		}

		public static bool operator !=(Vector3D<TNumber> a, Vector3D<TNumber> b)
		{
			return
				a.X != b.X ||
				a.Y != b.Y ||
				a.Z != b.Z;
		}

		public bool Equals(Vector3D<TNumber> b)
		{
			return
				X == b.X &&
				Y == b.Y &&
				Z == b.Z;
		}

		public override bool Equals(object? obj)
		{
			return obj is Vector3D<TNumber> v
				&& Equals(v);
		}
		#endregion


		public override string ToString()
		{
			return $"({X}; {Y}; {Z})";
		}

		public override int GetHashCode()
		{
			return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
		}
	}
}
