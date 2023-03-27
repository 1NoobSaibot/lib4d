using Lib4D.Mathematic.Matrix;
using System.Numerics;

namespace Lib4D
{
	public class Transform2D<TNumber> where TNumber : INumber<TNumber>
	{
		private TNumber[,] _matrix = new TNumber[3, 3];


		#region Constructors
		public Transform2D()
		{
			_matrix = new TNumber[3, 3];
			_matrix[0, 0] = TNumber.One;
			_matrix[1, 1] = TNumber.One;
			_matrix[2, 2] = TNumber.One;
		}

		
		public static Transform2D<TNumber> GetScale(TNumber kx, TNumber ky)
		{
			var t = new Transform2D<TNumber>();
			t.Scale(kx, ky);
			return t;
		}


		public static Transform2D<float> GetRotate(float alpha)
		{
			var t = new Transform2D<float>();
			Rotate(t, alpha);
			return t;
		}

		public static Transform2D<double> GetRotate(double alpha)
		{
			var t = new Transform2D<double>();
			Rotate(t, alpha);
			return t;
		}


		public static Transform2D<TNumber> GetTranslate(TNumber tx, TNumber ty)
		{
			var t = new Transform2D<TNumber>();
			t.Translate(tx, ty);
			return t;
		}
		#endregion


		public void Translate(TNumber tx, TNumber ty)
		{
			TNumber[,] translateMatrix = new TNumber[3, 3]
			{
				{ TNumber.One , TNumber.Zero, TNumber.Zero },
				{ TNumber.Zero, TNumber.One , TNumber.One  },
				{ tx          , ty          , TNumber.One  }
			};
			_matrix = MatrixMath.Mul(_matrix, translateMatrix);
		}


		public void Rotate(float alpha, Func<float, TNumber> cast)
		{
			TNumber c = cast(MathF.Cos(alpha));
			TNumber s = cast(MathF.Sin(alpha));
			TNumber[,] rotateMatrix = new TNumber[3, 3]
			{
				{            c,            s, TNumber.Zero },
				{           -s,            c, TNumber.Zero },
				{ TNumber.Zero, TNumber.Zero, TNumber.One  }
			};

			_matrix = MatrixMath.Mul(_matrix, rotateMatrix);
		}


		public static void Rotate(Transform2D<float> t, float alpha)
		{
			var c = MathF.Cos(alpha);
			var s = MathF.Sin(alpha);
			var rotateMatrix = new float[3, 3]
			{
				{  c, s, 0 },
				{ -s, c, 0 },
				{  0, 0, 1 }
			};

			t._matrix = MatrixMath.Mul(t._matrix, rotateMatrix);
		}


		public static void Rotate(Transform2D<double> t, double alpha)
		{
			var c = System.Math.Cos(alpha);
			var s = System.Math.Sin(alpha);
			var rotateMatrix = new double[3, 3]
			{
				{  c, s, 0 },
				{ -s, c, 0 },
				{  0, 0, 1 }
			};

			t._matrix = MatrixMath.Mul(t._matrix, rotateMatrix);
		}




		// TODO: It can be optimized, because it's only diagonal matrix
		public void Scale(TNumber kx, TNumber ky)
		{
			TNumber[,] scaleMatrix = new TNumber[3, 3] {
				{           kx, TNumber.Zero, TNumber.Zero },
				{ TNumber.Zero,           ky, TNumber.Zero },
				{ TNumber.Zero, TNumber.Zero, TNumber.One  }
			};
			_matrix = MatrixMath.Mul(_matrix, scaleMatrix);
		}


		public static Vector2D<TNumber> operator *(Transform2D<TNumber> t, Vector2D<TNumber> v) {
			TNumber[,] column = new TNumber[1, 3];
			column[0, 0] = v.X;
			column[0, 1] = v.Y;
			column[0, 2] = TNumber.One;

			column = MatrixMath.Mul(t._matrix, column);

			return new Vector2D<TNumber>(column[0, 0], column[0, 1]);
		}


		public static Transform2D<TNumber> operator *(Transform2D<TNumber> a, Transform2D<TNumber> b)
		{
			return new()
			{
				_matrix = MatrixMath.Mul(a._matrix, b._matrix)
			};
		}
	}
}
