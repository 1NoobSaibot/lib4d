using Lib4D.Mathematic;
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


		public static Transform2D<TNumber> GetRotate(TNumber alpha)
		{
			var t = new Transform2D<TNumber>();
			t.Rotate(alpha);
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


		public void Rotate(TNumber alpha)
		{
			var c = Math<TNumber>.Cos!(alpha);
			var s = Math<TNumber>.Sin!(alpha);
			TNumber[,] rotateMatrix = new TNumber[3, 3]
			{
				{            c,            s, TNumber.Zero },
				{           -s,            c, TNumber.Zero },
				{ TNumber.Zero, TNumber.Zero, TNumber.One  }
			};

			_matrix = MatrixMath.Mul(_matrix, rotateMatrix);
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
