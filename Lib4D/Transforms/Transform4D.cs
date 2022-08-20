using System;

namespace Lib4D
{
	public class Transform4D
	{
		private double[,] _matrix;

		public Transform4D(double[,] matrix)
		{
			if (matrix.GetWidth() != 5 || matrix.GetHeight() != 5)
			{
				throw new ArgumentException("Matrix should be size 5*5");
			}

			_matrix = matrix;
		}

		public Transform4D()
		{
			_matrix = _CreateIdentityMatrix();
		}

		public void Translate (Vector4D t)
		{
			Translate(t.X, t.Y, t.Z, t.Q);
		}


		public void Translate(double tx, double ty, double tz, double tq)
		{
			double[,] transformMatrix = _CreateIdentityMatrix();
			transformMatrix[4, 0] = tx;
			transformMatrix[4, 1] = ty;
			transformMatrix[4, 2] = tz;
			transformMatrix[4, 3] = tq;
			_matrix = MatrixMath.Mul(_matrix, transformMatrix);
		}


		public void Scale(Vector4D k)
		{
			Scale(k.X, k.Y, k.Z, k.Q);
		}


		public void Scale(double kx, double ky, double kz, double kq)
		{
			double[,] scaleMatrix = new double[5, 5]
			{
				{ kx,  0,  0,  0, 0 },
				{  0, ky,  0,  0, 0 },
				{  0,  0, kz,  0, 0 },
				{  0,  0,  0, kq, 0 },
				{  0,  0,  0,  0, 1 },
			};
			_matrix = MatrixMath.Mul(_matrix, scaleMatrix);
		}


		public void Rotate(Bivector4D b, double angle)
		{
			double c = Math.Cos(angle);
			double s = Math.Sin(angle);
			
			double xy = b.XY;
			double xz = b.XZ;
			double xq = b.XQ;
			double yz = b.YZ;
			double yq = b.YQ;
			double zq = b.ZQ;

			double[,] rotateMatrix = new double[5, 5]
			{
				{		c*(zq + yq + yz) + xq + xz + xy ,	s*zq														, -s*yq											, -s*yz									, 0 },
				{  -s*zq														,	c*(zq + xq + xz) + yq + yz + xy	, s*xq											, -s*xz									, 0 },
				{	  s*yq														,	-s*xq														, c*(yq + xq + xy) + yz + xz,	 s*xy									, 0 },
				{		s*yz														,	s*xz														,	-s*xy											,  c*(yz + xz + xy) + xq, 0 },
				{		0																,	0																, 0													,  0										, 1 }
			};

			_matrix = _matrix.Mul(rotateMatrix);
		}
		#region Static Constructors
		public static Transform4D GetTranslate(Vector4D t)
		{
			return GetTranslate(t.X, t.Y, t.Z, t.Q);
		}


		public static Transform4D GetTranslate(double tx, double ty, double tz, double tq)
		{
			Transform4D t = new Transform4D();
			t.Translate(tx, ty, tz, tq);
			return t;
		}


		public static Transform4D GetScale(Vector4D k)
		{
			return GetScale(k.X, k.Y, k.Z, k.Q);
		}


		public static Transform4D GetScale(double kx, double ky, double kz, double kq)
		{
			Transform4D t = new Transform4D();
			t.Scale(kx, ky, kz, kq);
			return t;
		}
		#endregion


		#region Operators
		public static Vector4D operator *(Transform4D t, Vector4D v)
		{
			double[,] column = new double[1, 5];
			column[0, 0] = v.X;
			column[0, 1] = v.Y;
			column[0, 2] = v.Z;
			column[0, 3] = v.Q;
			column[0, 4] = 1;

			column = MatrixMath.Mul(t._matrix, column);

			return new Vector4D(column[0, 0], column[0, 1], column[0, 2], column[0, 3]);
		}


		public static Transform4D operator *(Transform4D a, Transform4D b)
		{
			return new Transform4D()
			{
				_matrix = MatrixMath.Mul(a._matrix, b._matrix)
			};
		}
		#endregion

		private static double[,] _CreateIdentityMatrix()
		{
			return new double[5, 5]
			{
				{ 1, 0, 0, 0, 0 },
				{ 0, 1, 0, 0, 0 },
				{ 0, 0, 1, 0, 0 },
				{ 0, 0, 0, 1, 0 },
				{ 0, 0, 0, 0, 1 },
			};
		}
	}
}
