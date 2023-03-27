using Lib4D.Math.Matrix;
using System.Numerics;

namespace Lib4D
{
	public class Transform3D<TNumber> where TNumber : INumber<TNumber>
	{
		private TNumber[,] _matrix = CreateIdentityMatrix();


		public void Translate (Vector3D<TNumber> t)
		{
			Translate (t.X, t.Y, t.Z);
		}


		public void Translate(TNumber tx, TNumber ty, TNumber tz)
		{
			TNumber[,] transformMatrix = CreateIdentityMatrix();
			transformMatrix[3, 0] = tx;
			transformMatrix[3, 1] = ty;
			transformMatrix[3, 2] = tz;
			_matrix = MatrixMath.Mul(_matrix, transformMatrix);
		}


		public void Rotate(Quaternion<TNumber> q)
		{
			TNumber two = TNumber.One + TNumber.One;
			TNumber w = q.R;
			TNumber x = q.I;
			TNumber y = q.J;
			TNumber z = q.K;
			TNumber twoXY = two * x * y;
			TNumber twoXZ = two * x * z;
			TNumber twoYZ = two * y * z;
			TNumber twoWX = two * w * x;
			TNumber twoWY = two * w * y;
			TNumber twoWZ = two * w * z;
			TNumber twoXX = two * x * x;
			TNumber twoYY = two * y * y;
			TNumber twoZZ = two * z * z;

			TNumber[,] rotateMatrix = new TNumber[4, 4] {
				{ TNumber.One - twoYY - twoZZ, twoXY + twoWZ              , twoXZ - twoWY              , TNumber.Zero },
				{ twoXY - twoWZ              , TNumber.One - twoXX - twoZZ, twoYZ + twoWX              , TNumber.Zero },
				{ twoXZ + twoWY              , twoYZ - twoWX              , TNumber.One - twoXX - twoYY, TNumber.Zero },
				{ TNumber.Zero               , TNumber.Zero               , TNumber.Zero               , TNumber.One  }
			};

			_matrix = MatrixMath.Mul(_matrix, rotateMatrix);
		}


		public void Scale(Vector3D<TNumber> k)
		{
			Scale(k.X, k.Y, k.Z);
		}


		public void Scale(TNumber kx, TNumber ky, TNumber kz)
		{
			TNumber[,] scaleMatrix = new TNumber[4, 4]
			{
				{ kx          , TNumber.Zero, TNumber.Zero, TNumber.Zero },
				{ TNumber.Zero, ky          , TNumber.Zero, TNumber.Zero },
				{ TNumber.Zero, TNumber.Zero, kz          , TNumber.Zero },
				{ TNumber.Zero, TNumber.Zero, TNumber.Zero, TNumber.One  },
			};
			_matrix = MatrixMath.Mul(_matrix, scaleMatrix);
		}

		#region Static Constructors
		public static Transform3D<TNumber> GetTranslate(Vector3D<TNumber> t)
		{
			return GetTranslate(t.X, t.Y, t.Z);
		}


		public static Transform3D<TNumber> GetTranslate(TNumber tx, TNumber ty, TNumber tz)
		{
			Transform3D<TNumber> t = new Transform3D<TNumber>();
			t.Translate(tx, ty, tz);
			return t;
		}


		public static Transform3D<TNumber> GetScale(Vector3D<TNumber> k)
		{
			return GetScale(k.X, k.Y, k.Z);
		}


		public static Transform3D<TNumber> GetScale(TNumber kx, TNumber ky, TNumber kz)
		{
			Transform3D<TNumber> t = new Transform3D<TNumber>();
			t.Scale(kx, ky, kz);
			return t;
		}


		public static Transform3D<float> GetRotate(Vector3D<float> axis, float angle)
		{
			return GetRotate(axis.X, axis.Y, axis.Z, angle);
		}

		public static Transform3D<double> GetRotate(Vector3D<double> axis, double angle)
		{
			return GetRotate(axis.X, axis.Y, axis.Z, angle);
		}

		public static Transform3D<float> GetRotate(float x, float y, float z, float angle)
		{
			var t = new Transform3D<float>();
			Rotate(t, x, y, z, angle);
			return t;
		}

		public static Transform3D<double> GetRotate(double x, double y, double z, double angle)
		{
			var t = new Transform3D<double>();
			Transform3D<double>.Rotate(t, x, y, z, angle);
			return t;
		}


		public static Transform3D<TNumber> GetRotate(Quaternion<TNumber> q)
		{
			var t = new Transform3D<TNumber>();
			t.Rotate(q);
			return t;
		}
		#endregion



		public static void Rotate(Transform3D<float> t, float x, float y, float z, float angle)
		{
			var cos = MathF.Cos(angle);
			var sin = MathF.Sin(angle);
			Transform3D<float>.Rotate(t, x, y, z, sin, cos);
		}

		public static void Rotate(Transform3D<double> t, double x, double y, double z, double angle)
		{
			var cos = System.Math.Cos(angle);
			var sin = System.Math.Sin(angle);
			Transform3D<double>.Rotate(t, x, y, z, sin, cos);
		}


		public static void Rotate(Transform3D<float> t, Vector3D<float> axis, float angle)
		{
			var cos = MathF.Cos(angle);
			var sin = MathF.Sin(angle);
			Transform3D<float>.Rotate(t, axis.X, axis.Y, axis.Z, sin, cos);
		}


		public static void Rotate(Transform3D<double> t, Vector3D<double> axis, double angle)
		{
			var cos = System.Math.Cos(angle);
			var sin = System.Math.Sin(angle);
			Transform3D<double>.Rotate(t, axis.X, axis.Y, axis.Z, sin, cos);
		}


		#region Operators
		public static Vector3D<TNumber> operator *(Transform3D<TNumber> t, Vector3D<TNumber> v)
		{
			TNumber[,] column = new TNumber[1, 4];
			column[0, 0] = v.X;
			column[0, 1] = v.Y;
			column[0, 2] = v.Z;
			column[0, 3] = TNumber.One;

			column = MatrixMath.Mul(t._matrix, column);

			return new(column[0, 0], column[0, 1], column[0, 2]);
		}


		public static Transform3D<TNumber> operator *(Transform3D<TNumber> a, Transform3D<TNumber> b)
		{
			return new Transform3D<TNumber>()
			{
				_matrix = MatrixMath.Mul(a._matrix, b._matrix)
			};
		}
		#endregion

		private static TNumber[,] CreateIdentityMatrix()
		{
			var res = new TNumber[4, 4];
			res[0, 0] = TNumber.One;
			res[1, 1] = TNumber.One;
			res[2, 2] = TNumber.One;
			res[3, 3] = TNumber.One;
			return res;
		}


		private static void Rotate(Transform3D<TNumber> t, TNumber x, TNumber y, TNumber z, TNumber sin, TNumber cos)
		{
			TNumber oneMinusCos = TNumber.One - cos;
			TNumber oneMinusCosXY = oneMinusCos * x * y;
			TNumber oneMinusCosYZ = oneMinusCos * y * z;
			TNumber oneMinusCosXZ = oneMinusCos * x * z;
			TNumber sinX = sin * x;
			TNumber sinY = sin * y;
			TNumber sinZ = sin * z;

			TNumber[,] rotateMatrix = new TNumber[4, 4]
			{
				{ cos + oneMinusCos * x * x, oneMinusCosXY + sinZ     , oneMinusCosXZ - sinY     , TNumber.Zero },
				{ oneMinusCosXY - sinZ     , cos + oneMinusCos * y * y, oneMinusCosYZ + sinX     , TNumber.Zero },
				{ oneMinusCosXZ + sinY     , oneMinusCosYZ - sinX     , cos + oneMinusCos * z * z, TNumber.Zero },
				{ TNumber.Zero             , TNumber.Zero             , TNumber.Zero             , TNumber.One  }
			};

			t._matrix = MatrixMath.Mul(t._matrix, rotateMatrix);
		}

	}
}
