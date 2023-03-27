using Lib4D.Mathematic.Matrix;
using System;

namespace Lib4D
{
	public class Transform4DDouble
	{
		private double[,] _matrix;

		public Transform4DDouble (double[,] matrix)
		{
			if (matrix.GetWidth() != 5 || matrix.GetHeight() != 5)
			{
				throw new ArgumentException("Matrix should be size 5*5");
			}

			_matrix = matrix;
		}

		public Transform4DDouble ()
		{
			_matrix = CreateIdentityMatrix();
		}

		public void Translate (Vector4D<double> t)
		{
			Translate(t.X, t.Y, t.Z, t.Q);
		}


		public void Translate(double tx, double ty, double tz, double tq)
		{
			double[,] transformMatrix = CreateIdentityMatrix();
			transformMatrix[4, 0] = tx;
			transformMatrix[4, 1] = ty;
			transformMatrix[4, 2] = tz;
			transformMatrix[4, 3] = tq;
			_matrix = MatrixMath.Mul(_matrix, transformMatrix);
		}


		public void Scale(Vector4D<double> k)
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


		public void Rotate(Bivector4D<double> b, double angle)
		{
			double c = System.Math.Cos(angle);
			double s = System.Math.Sin(angle);
			double xy = b.XY;
			double xz = b.XZ;
			double xq = b.XQ;
			double yz = b.YZ;
			double yq = b.YQ;
			double zq = b.ZQ;

			double[,] uut = MatrixMath.Mul(b.Matrix, MatrixMath.Transpose(b.Matrix));
			uut = uut.Mul(1.0 - c);
			
			double[,] sinAndCos = new double[4, 4]
			{
				{	 c			,  s * zq	, -s * yq	, -s * yz },
				{ -s * zq	,	 c			,	 s * xq	, -s * xz },
				{	 s * yq	,	-s * xq	,	 c			,  s * xy },
				{  s * yz	,	 s * xz	,	-s * xy	,  c			},
			};

			double[,] R = MatrixMath.Add(sinAndCos, uut);
			R = MatrixMath.Extend(R, 5, 5);
			R[4, 4] = 1;

			_matrix = _matrix.Mul(R);
		}

		
		#region Static Constructors
		public static Transform4DDouble GetTranslate(Vector4D<double> t)
		{
			return GetTranslate(t.X, t.Y, t.Z, t.Q);
		}


		public static Transform4DDouble GetTranslate(double tx, double ty, double tz, double tq)
		{
			Transform4DDouble t = new();
			t.Translate(tx, ty, tz, tq);
			return t;
		}


		public static Transform4DDouble GetScale(Vector4D<double> k)
		{
			return GetScale(k.X, k.Y, k.Z, k.Q);
		}


		public static Transform4DDouble GetScale(double kx, double ky, double kz, double kq)
		{
			Transform4DDouble t = new();
			t.Scale(kx, ky, kz, kq);
			return t;
		}
		#endregion


		#region Operators
		public static Vector4D<double> operator *(Transform4DDouble t, Vector4D<double> v)
		{
			double[,] column = new double[1, 5];
			column[0, 0] = v.X;
			column[0, 1] = v.Y;
			column[0, 2] = v.Z;
			column[0, 3] = v.Q;
			column[0, 4] = 1;

			column = MatrixMath.Mul(t._matrix, column);

			return new Vector4D<double>(column[0, 0], column[0, 1], column[0, 2], column[0, 3]);
		}


		public static Transform4DDouble operator *(Transform4DDouble a, Transform4DDouble b)
		{
			return new Transform4DDouble()
			{
				_matrix = MatrixMath.Mul(a._matrix, b._matrix)
			};
		}
		#endregion

		private static double[,] CreateIdentityMatrix()
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
