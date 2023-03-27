using Lib4D.Mathematic.Matrix;
using System.Numerics;

namespace Lib4D
{
	public struct Vector3D<TNumber> where TNumber : INumber<TNumber>
	{
		public TNumber X, Y, Z;


		public TNumber AbsQuad
		{
			get => X * X + Y * Y + Z * Z;
		}


		public Vector3D (TNumber x, TNumber y, TNumber z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public static double Abs(Vector3D<double> v)
		{
			return System.Math.Sqrt(v.AbsQuad);
		}

		public static float Abs(Vector3D<float> v)
		{
			return MathF.Sqrt(v.AbsQuad);
		}

		public static Vector3D<double> Normalize(Vector3D<double> v)
		{
			var k = 1f / Abs(v);
			return new(k * v.X, k * v.Y, k * v.Z);
		}

		public static Vector3D<float> Normalize(Vector3D<float> v)
		{
			var k = 1f / Abs(v);
			return new(k * v.X, k * v.Y, k * v.Z);
		}


		public static Vector3D<TNumber> operator +(Vector3D<TNumber> a, Vector3D<TNumber> b)
		{
			return new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
		}


		public static Vector3D<TNumber> operator -(Vector3D<TNumber> a, Vector3D<TNumber> b)
		{
			return new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
		}


		public static Vector3D<TNumber> operator *(Vector3D<TNumber> a, TNumber b)
		{
			return new Vector3D<TNumber>(a.X * b, a.Y * b, a.Z * b);
		}


		public static Vector3D<TNumber> operator *(Vector3D<TNumber> a, Vector3D<TNumber> b)
		{
			return new Vector3D<TNumber>(
				a.Y * b.Z - a.Z * b.Y,
				a.Z * b.X - a.X * b.Z,
				a.X * b.Y - a.Y * b.X
			);
		}


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


		public override string ToString()
		{
			return $"({X}; {Y}; {Z})";
		}
	}
}
