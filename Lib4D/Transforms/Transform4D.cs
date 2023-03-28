using Lib4D.Mathematic;
using Lib4D.Mathematic.Matrix;
using System.Numerics;

namespace Lib4D
{
	public class Transform4D<TNumber> where TNumber : INumber<TNumber>
	{
		private TNumber[,] _matrix;

		public Transform4D (TNumber[,] matrix)
		{
			if (matrix.GetWidth() != 5 || matrix.GetHeight() != 5)
			{
				throw new ArgumentException("Matrix should be size 5*5");
			}

			_matrix = matrix;
		}

		public Transform4D ()
		{
			_matrix = CreateIdentityMatrix();
		}

		public void Translate (Vector4D<TNumber> t)
		{
			Translate(t.X, t.Y, t.Z, t.Q);
		}

		public void Translate(TNumber tx, TNumber ty, TNumber tz, TNumber tq)
		{
			TNumber[,] transformMatrix = CreateIdentityMatrix();
			transformMatrix[4, 0] = tx;
			transformMatrix[4, 1] = ty;
			transformMatrix[4, 2] = tz;
			transformMatrix[4, 3] = tq;
			_matrix = MatrixMath.Mul(_matrix, transformMatrix);
		}

		public void Translate(double tx, double ty, double tz, double tq)
		{
			TNumber[,] transformMatrix = CreateIdentityMatrix();
			transformMatrix[4, 0] = Math<TNumber>.Double2Number!(tx);
			transformMatrix[4, 1] = Math<TNumber>.Double2Number!(ty);
			transformMatrix[4, 2] = Math<TNumber>.Double2Number!(tz);
			transformMatrix[4, 3] = Math<TNumber>.Double2Number!(tq);
			_matrix = MatrixMath.Mul(_matrix, transformMatrix);
		}


		public void Scale(Vector4D<TNumber> k)
		{
			Scale(k.X, k.Y, k.Z, k.Q);
		}

		public void Scale(double kx, double ky, double kz, double kq)
		{
			Scale(
				Math<TNumber>.Double2Number!(kx),
				Math<TNumber>.Double2Number!(ky),
				Math<TNumber>.Double2Number!(kz),
				Math<TNumber>.Double2Number!(kq)
			);
		}

		public void Scale(TNumber kx, TNumber ky, TNumber kz, TNumber kq)
		{
			TNumber[,] scaleMatrix = new TNumber[5, 5];
			scaleMatrix[0, 0] = kx;
			scaleMatrix[1, 1] = ky;
			scaleMatrix[2, 2] = kz;
			scaleMatrix[3, 3] = kq;
			scaleMatrix[4, 4] = TNumber.One;
			_matrix = MatrixMath.Mul(_matrix, scaleMatrix);
		}


		public void Rotate(Bivector4D<TNumber> b, double angle)
		{
			Rotate(b, Math<TNumber>.Double2Number!(angle));
		}

		public void Rotate(Bivector4D<TNumber> b, TNumber angle)
		{
			TNumber c = Math<TNumber>.Cos!(angle);
			TNumber s = Math<TNumber>.Sin!(angle);
			TNumber xy = b.XY;
			TNumber xz = b.XZ;
			TNumber xq = b.XQ;
			TNumber yz = b.YZ;
			TNumber yq = b.YQ;
			TNumber zq = b.ZQ;

			TNumber[,] uut = MatrixMath.Mul(b.Matrix, MatrixMath.Transpose(b.Matrix));
			uut = uut.Mul(TNumber.One - c);
			
			TNumber[,] sinAndCos = new TNumber[4, 4]
			{
				{	 c			,  s * zq	, -s * yq	, -s * yz },
				{ -s * zq	,	 c			,	 s * xq	, -s * xz },
				{	 s * yq	,	-s * xq	,	 c			,  s * xy },
				{  s * yz	,	 s * xz	,	-s * xy	,  c			},
			};

			TNumber[,] R = MatrixMath.Add(sinAndCos, uut);
			R = MatrixMath.Extend(R, 5, 5);
			R[4, 4] = TNumber.One;

			_matrix = _matrix.Mul(R);
		}

		
		#region Static Constructors
		public static Transform4D<TNumber> GetTranslate(Vector4D<TNumber> t)
		{
			return GetTranslate(t.X, t.Y, t.Z, t.Q);
		}

		public static Transform4D<TNumber> GetTranslate(TNumber tx, TNumber ty, TNumber tz, TNumber tq)
		{
			Transform4D<TNumber> t = new();
			t.Translate(tx, ty, tz, tq);
			return t;
		}

		public static Transform4D<TNumber> GetTranslate(double tx, double ty, double tz, double tq)
		{
			Transform4D<TNumber> t = new();
			t.Translate(tx, ty, tz, tq);
			return t;
		}


		public static Transform4D<TNumber> GetScale(Vector4D<TNumber> k)
		{
			return GetScale(k.X, k.Y, k.Z, k.Q);
		}

		public static Transform4D<TNumber> GetScale(double kx, double ky, double kz, double kq)
		{
			Transform4D<TNumber> t = new();
			t.Scale(kx, ky, kz, kq);
			return t;
		}

		public static Transform4D<TNumber> GetScale(TNumber kx, TNumber ky, TNumber kz, TNumber kq)
		{
			Transform4D<TNumber> t = new();
			t.Scale(kx, ky, kz, kq);
			return t;
		}
		#endregion


		#region Operators
		public static Vector4D<TNumber> operator *(Transform4D<TNumber> t, Vector4D<TNumber> v)
		{
			TNumber[,] column = new TNumber[1, 5];
			column[0, 0] = v.X;
			column[0, 1] = v.Y;
			column[0, 2] = v.Z;
			column[0, 3] = v.Q;
			column[0, 4] = TNumber.One;

			column = MatrixMath.Mul(t._matrix, column);

			return new Vector4D<TNumber>(column[0, 0], column[0, 1], column[0, 2], column[0, 3]);
		}


		public static Transform4D<TNumber> operator *(Transform4D<TNumber> a, Transform4D<TNumber> b)
		{
			return new()
			{
				_matrix = MatrixMath.Mul(a._matrix, b._matrix)
			};
		}
		#endregion

		private static TNumber[,] CreateIdentityMatrix()
		{
			var res = new TNumber[5, 5];
			res[0, 0] = TNumber.One;
			res[1, 1] = TNumber.One;
			res[2, 2] = TNumber.One;
			res[3, 3] = TNumber.One;
			res[4, 4] = TNumber.One;
			return res;
		}
	}
}
