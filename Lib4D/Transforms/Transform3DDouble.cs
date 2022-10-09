using System;

namespace Lib4D
{
	public class Transform3DDouble
	{
		private double[,] _matrix = _CreateIdentityMatrix();


		public void Translate (Vector3DDouble t)
		{
			Translate (t.X, t.Y, t.Z);
		}


		public void Translate(double tx, double ty, double tz)
		{
			double[,] transformMatrix = _CreateIdentityMatrix();
			transformMatrix[3, 0] = tx;
			transformMatrix[3, 1] = ty;
			transformMatrix[3, 2] = tz;
			_matrix = MatrixMath.Mul(_matrix, transformMatrix);
		}


		public void Rotate(Vector3DDouble axis, double angle) {
			Rotate(axis.X, axis.Y, axis.Z, angle);
		}


		public void Rotate(double x, double y, double z, double angle)
		{
			double cos = Math.Cos(angle);
			double sin = Math.Sin(angle);
			double oneMinusCos = 1 - cos;
			double oneMinusCosXY = oneMinusCos * x * y;
			double oneMinusCosYZ = oneMinusCos * y * z;
			double oneMinusCosXZ = oneMinusCos * x * z;
			double sinX = sin * x;
			double sinY = sin * y;
			double sinZ = sin * z;

			double[,] rotateMatrix = new double[4, 4]
			{
				{ cos + oneMinusCos * x * x			, oneMinusCosXY + sinZ					, oneMinusCosXZ - sinY					, 0 },
				{ oneMinusCosXY - sinZ					, cos + oneMinusCos * y * y			, oneMinusCosYZ + sinX					, 0 },
				{ oneMinusCosXZ + sinY					, oneMinusCosYZ - sinX					, cos + oneMinusCos * z * z			, 0 },
				{ 0															, 0															, 0															, 1 }
			};

			_matrix = MatrixMath.Mul(_matrix, rotateMatrix);
		}


		public void Rotate(Quaternion q) {
			double w = q.R;
			double x = q.I;
			double y = q.J;
			double z = q.K;
			double twoXY = 2 * x * y;
			double twoXZ = 2 * x * z;
			double twoYZ = 2 * y * z;
			double twoWX = 2 * w * x;
			double twoWY = 2 * w * y;
			double twoWZ = 2 * w * z;
			double twoXX = 2 * x * x;
			double twoYY = 2 * y * y;
			double twoZZ = 2 * z * z;

			double[,] rotateMatrix = new double[4, 4] {
				{ 1 - twoYY - twoZZ	,		twoXY + twoWZ			,		twoXZ - twoWY			, 0 },
				{ twoXY - twoWZ			,		1 - twoXX - twoZZ	,		twoYZ + twoWX			, 0 },
				{ twoXZ + twoWY			, 	twoYZ - twoWX			,		1 - twoXX - twoYY , 0 },
				{ 0									,		0									,		0									,	1	}
			};

			_matrix = MatrixMath.Mul(_matrix, rotateMatrix);
		}


		public void Scale(Vector3DDouble k)
		{
			Scale(k.X, k.Y, k.Z);
		}


		public void Scale(double kx, double ky, double kz)
		{
			double[,] scaleMatrix = new double[4, 4]
			{
				{ kx,  0,  0, 0 },
				{  0, ky,  0, 0 },
				{  0,  0, kz, 0 },
				{  0,  0,  0, 1 },
			};
			_matrix = MatrixMath.Mul(_matrix, scaleMatrix);
		}

		#region Static Constructors
		public static Transform3DDouble GetTranslate(Vector3DDouble t)
		{
			return GetTranslate(t.X, t.Y, t.Z);
		}


		public static Transform3DDouble GetTranslate(double tx, double ty, double tz)
		{
			Transform3DDouble t = new Transform3DDouble();
			t.Translate(tx, ty, tz);
			return t;
		}


		public static Transform3DDouble GetScale(Vector3DDouble k)
		{
			return GetScale(k.X, k.Y, k.Z);
		}


		public static Transform3DDouble GetScale(double kx, double ky, double kz)
		{
			Transform3DDouble t = new Transform3DDouble();
			t.Scale(kx, ky, kz);
			return t;
		}


		public static Transform3DDouble GetRotate(Vector3DDouble axis, double angle)
		{
			return GetRotate(axis.X, axis.Y, axis.Z, angle);
		}


		public static Transform3DDouble GetRotate(double x, double y, double z, double angle)
		{
			Transform3DDouble t = new Transform3DDouble();
			t.Rotate(x, y, z, angle);
			return t;
		}


		public static Transform3DDouble GetRotate(Quaternion q)
		{
			Transform3DDouble t = new Transform3DDouble();
			t.Rotate(q);
			return t;
		}
		#endregion


		#region Operators
		public static Vector3DDouble operator *(Transform3DDouble t, Vector3DDouble v)
		{
			double[,] column = new double[1, 4];
			column[0, 0] = v.X;
			column[0, 1] = v.Y;
			column[0, 2] = v.Z;
			column[0, 3] = 1;

			column = MatrixMath.Mul(t._matrix, column);

			return new Vector3DDouble(column[0, 0], column[0, 1], column[0, 2]);
		}


		public static Transform3DDouble operator *(Transform3DDouble a, Transform3DDouble b)
		{
			return new Transform3DDouble()
			{
				_matrix = MatrixMath.Mul(a._matrix, b._matrix)
			};
		}
		#endregion

		private static double[,] _CreateIdentityMatrix()
		{
			return new double[4, 4]
			{
				{ 1, 0, 0, 0 },
				{ 0, 1, 0, 0 },
				{ 0, 0, 1, 0 },
				{ 0, 0, 0, 1 },
			};
		}
	}
}
