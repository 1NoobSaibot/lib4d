using System;

namespace Lib4D
{
	public class Transform2D
	{
		private double[,] _matrix = new double[3, 3];


		#region Constructors
		public Transform2D()
		{
			_matrix = new double[3, 3];
			_matrix[0, 0] = 1;
			_matrix[1, 1] = 1;
			_matrix[2, 2] = 1;
		}

		
		public static Transform2D GetScale(double kx, double ky)
		{
			Transform2D t = new Transform2D();
			t.Scale(kx, ky);
			return t;
		}


		public static Transform2D GetRotate(double alpha)
		{
			Transform2D t = new Transform2D();
			t.Rotate(alpha);
			return t;
		}


		public static Transform2D GetTranslate(double tx, double ty)
		{
			Transform2D t = new Transform2D();
			t.Translate(tx, ty);
			return t;
		}
		#endregion


		public void Translate(double tx, double ty)
		{
			double[,] translateMatrix = new double[3, 3]
			{
				{  1,  0,  0 },
				{  0,  1,  0 },
				{ tx, ty,  1 }
			};
			_matrix = MatrixMath.Mul(_matrix, translateMatrix);
		}


		public void Rotate(double alpha)
		{
			double c = Math.Cos(alpha);
			double s = Math.Sin(alpha);
			double[,] rotateMatrix = new double[3, 3]
			{
				{  c,  s,  0 },
				{ -s,  c,  0 },
				{  0,  0,  1 }
			};

			_matrix = MatrixMath.Mul(_matrix, rotateMatrix);
		}


		// TODO: It can be optimized, because it's only diagonal matrix
		public void Scale(double kx, double ky)
		{
			double[,] scaleMatrix = new double[3, 3] {
				{ kx,  0,  0 },
				{  0, ky,  0 },
				{  0,  0,  1 }
			};
			_matrix = MatrixMath.Mul(_matrix, scaleMatrix);
		}


		public static Vector2D operator *(Transform2D t, Vector2D v) {
			double[,] column = new double[1, 3];
			column[0, 0] = v.X;
			column[0, 1] = v.Y;
			column[0, 2] = 1;

			column = MatrixMath.Mul(t._matrix, column);

			return new Vector2D(column[0, 0], column[0, 1]);
		}


		public static Transform2D operator *(Transform2D a, Transform2D b)
		{
			return new Transform2D()
			{
				_matrix = MatrixMath.Mul(a._matrix, b._matrix)
			};
		}
	}
}
