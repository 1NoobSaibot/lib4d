using Lib4D.Mathematic;
using System.Numerics;

namespace Lib4D
{
	public struct Vector2D<TNumber> where TNumber : INumber<TNumber>
	{
		public TNumber X, Y;


		#region Constructors
		public Vector2D(double x)
		{
			X = Math<TNumber>.Double2Number!(x);
			Y = TNumber.Zero;
		}

		public Vector2D (double x, double y)
		{
			X = Math<TNumber>.Double2Number!(x);
			Y = Math<TNumber>.Double2Number!(y);
		}

		public Vector2D(TNumber x)
		{
			X = x;
			Y = TNumber.Zero;
		}

		public Vector2D(TNumber x, TNumber y)
		{
			X = x;
			Y = y;
		}
		#endregion

		public static Vector2D<TNumber> operator +(Vector2D<TNumber> a, Vector2D<TNumber> b)
		{
			return new(a.X + b.X, a.Y + b.Y);
		}

		public static Vector2D<TNumber> operator -(Vector2D<TNumber> a, Vector2D<TNumber> b)
		{
			return new(a.X - b.X, a.Y - b.Y);
		}


		public static bool operator ==(Vector2D<TNumber> v1, Vector2D<TNumber> v2)
		{
			return
				v1.X == v2.X &&
				v1.Y == v2.Y;
		}


		public static bool operator !=(Vector2D<TNumber> v1, Vector2D<TNumber> v2)
		{
			return
				v1.X != v2.X ||
				v1.Y != v2.Y;
		}


		public override string ToString()
		{
			return $"({X}; {Y})";
		}


		public override bool Equals(object? obj)
		{
			return obj is Vector2D<TNumber> v && this == v;
		}


		public override int GetHashCode()
		{
			return X.GetHashCode() ^ Y.GetHashCode();
		}
	}
}
