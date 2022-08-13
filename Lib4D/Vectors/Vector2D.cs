namespace Lib4D
{
	public struct Vector2D
	{
		public double X, Y;

		public Vector2D (double x, double y)
		{
			X = x;
			Y = y;
		}

		public static Vector2D operator +(Vector2D a, Vector2D b)
		{
			return new Vector2D (a.X + b.X, a.Y + b.Y);
		}

		public static Vector2D operator -(Vector2D a, Vector2D b)
		{
			return new Vector2D(a.X - b.X, a.Y - b.Y);
		}


		public override string ToString()
		{
			return "(" + X + "; " + Y + ")";
		}
	}
}
