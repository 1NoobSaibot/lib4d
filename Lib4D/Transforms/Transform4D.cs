using Lib4D.Mathematic;
using Lib4D.Mathematic.Matrix;
using System.Numerics;

namespace Lib4D
{
	public class Transform4D<TNumber> where TNumber : INumber<TNumber>
	{
		private TNumber[,] _matrix;
		private readonly TNumber[,] _buffer = CreateIdentityMatrix();
		private TNumber[,] _bufferRes = CreateIdentityMatrix();

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


		public void Rotate(Vector4D<TNumber> a1, Vector4D<TNumber> a2, double angle)
		{
			Rotate(a1, a2, Math<TNumber>.Double2Number!(angle));
		}

		public void RotateOriginal(Vector4D<TNumber> a1, Vector4D<TNumber> a2, TNumber angle)
		{
			Bivector4D<TNumber> b = new(a1, a2);
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


		public void Rotate(Vector4D<TNumber> axis1, Vector4D<TNumber> axis2, TNumber angle)
		{
			TNumber c = Math<TNumber>.Cos!(angle);
			TNumber s = Math<TNumber>.Sin!(angle);
			TNumber a = TNumber.One - c;

			TNumber xy = axis1[0] * axis2[1] - axis1[1] * axis2[0];
			TNumber xz = axis1[0] * axis2[2] - axis1[2] * axis2[0];
			TNumber xq = axis1[0] * axis2[3] - axis1[3] * axis2[0];
			TNumber yz = axis1[1] * axis2[2] - axis1[2] * axis2[1];
			TNumber yq = axis1[1] * axis2[3] - axis1[3] * axis2[1];
			TNumber zq = axis1[2] * axis2[3] - axis1[3] * axis2[2];

			TNumber xy2 = xy * xy;
			TNumber xz2 = xz * xz;
			TNumber xq2 = xq * xq;
			TNumber yz2 = yz * yz;
			TNumber yq2 = yq * yq;
			TNumber zq2 = zq * zq;

			TNumber p01 = a * (xz * yz + xq * yq);
			TNumber p02 = a * (xq * zq - xy * yz);
			TNumber p03 = a * (xy * yq + xz * zq);
			TNumber p12 = a * (xy * xz + yq * zq);
			TNumber p13 = a * (xy * xq - yz * zq);
			TNumber p23 = a * (xz * xq + yz * yq);

			TNumber s01 = s * zq;
			TNumber s02 = s * yq;
			TNumber s03 = s * yz;
			TNumber s12 = s * xq;
			TNumber s13 = s * xz;
			TNumber s23 = s * xy;

			_buffer[0, 0] = c + a * (xy2 + xz2 + xq2);
			_buffer[1, 1] = c + a * (xy2 + yz2 + yq2);
			_buffer[2, 2] = c + a * (xz2 + yz2 + zq2);
			_buffer[3, 3] = c + a * (xq2 + yq2 + zq2);

			_buffer[0, 1] = s01 + p01;
			_buffer[1, 0] = p01 - s01;
			_buffer[0, 2] = p02 - s02;
			_buffer[2, 0] = s02 + p02;
			_buffer[0, 3] = -s03 - p03;
			_buffer[3, 0] = s03 - p03;
			_buffer[1, 2] = s12 + p12;
			_buffer[2, 1] = p12 - s12;
			_buffer[1, 3] = p13 - s13;
			_buffer[3, 1] = s13 + p13;
			_buffer[2, 3] = s23 + p23;
			_buffer[3, 2] = p23 - s23;

			_buffer[4, 0] = TNumber.Zero;
			_buffer[4, 1] = TNumber.Zero;
			_buffer[4, 2] = TNumber.Zero;
			_buffer[4, 3] = TNumber.Zero;


			_matrix.Mul(_buffer, _bufferRes);
			var temp = _matrix;
			_matrix = _bufferRes;
			_bufferRes = temp;
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
