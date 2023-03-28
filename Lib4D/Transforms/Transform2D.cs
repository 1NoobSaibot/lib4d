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


		public static Transform2D<TNumber> GetScale(double kx, double ky)
		{
			var t = new Transform2D<TNumber>();
			t.Scale(kx, ky);
			return t;
		}

		public static Transform2D<TNumber> GetScale(TNumber kx, TNumber ky)
		{
			var t = new Transform2D<TNumber>();
			t.Scale(kx, ky);
			return t;
		}


		public static Transform2D<TNumber> GetRotate(double alpha)
		{
			return GetRotate(Math<TNumber>.Double2Number!(alpha));
		}

		public static Transform2D<TNumber> GetRotate(TNumber alpha)
		{
			var t = new Transform2D<TNumber>();
			t.Rotate(alpha);
			return t;
		}


		public static Transform2D<TNumber> GetTranslate(double tx, double ty)
		{
			return GetTranslate(
				Math<TNumber>.Double2Number!(tx),
				Math<TNumber>.Double2Number!(ty)
			);
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
			TNumber[,] translateMatrix = CreateIdentity();
			translateMatrix[2, 0] = tx;
			translateMatrix[2, 1] = ty;
			_matrix = MatrixMath.Mul(_matrix, translateMatrix);
		}

		public void Translate(double tx, double ty)
		{
			TNumber[,] translateMatrix = CreateIdentity();
			translateMatrix[2, 0] = Math<TNumber>.Double2Number!(tx);
			translateMatrix[2, 1] = Math<TNumber>.Double2Number!(ty);
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


		public void Scale(double kx, double ky)
		{
			Scale(Math<TNumber>.Double2Number!(kx), Math<TNumber>.Double2Number!(ky));
		}

		// TODO: It can be optimized, because it's only diagonal matrix
		public void Scale(TNumber kx, TNumber ky)
		{
			TNumber[,] scaleMatrix = CreateIdentity();
			scaleMatrix[0, 0] = kx;
			scaleMatrix[1, 1] = ky;
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


		private static TNumber[,] CreateIdentity()
		{
			var res = new TNumber[3, 3];
			res[0, 0] = TNumber.One;
			res[1, 1] = TNumber.One;
			res[2, 2] = TNumber.One;
			return res;
		}
	}
}
