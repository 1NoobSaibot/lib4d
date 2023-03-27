using System.Numerics;

namespace Lib4D
{
	public struct Vector2D<TNumber> where TNumber : INumber<TNumber>
	{
		public TNumber X, Y;

		public Vector2D (TNumber x, TNumber y)
		{
			X = x;
			Y = y;
		}

		public static Vector2D<TNumber> operator +(Vector2D<TNumber> a, Vector2D<TNumber> b)
		{
			return new(a.X + b.X, a.Y + b.Y);
		}

		public static Vector2D<TNumber> operator -(Vector2D<TNumber> a, Vector2D<TNumber> b)
		{
			return new(a.X - b.X, a.Y - b.Y);
		}


		public override string ToString()
		{
			return $"({X}; {Y})";
		}
	}
}
