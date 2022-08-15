using System;

namespace Lib4D
{
	public class Transform3D
	{
		private double[,] _matrix = _CreateIdentityMatrix();


		public void Translate (Vector3D t)
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


		public void Rotate(Vector3D axis, double angle) {
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


		public void Scale(Vector3D k)
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
		public static Transform3D GetTranslate(Vector3D t)
		{
			return GetTranslate(t.X, t.Y, t.Z);
		}


		public static Transform3D GetTranslate(double tx, double ty, double tz)
		{
			Transform3D t = new Transform3D();
			t.Translate(tx, ty, tz);
			return t;
		}


		public static Transform3D GetScale(Vector3D k)
		{
			return GetScale(k.X, k.Y, k.Z);
		}


		public static Transform3D GetScale(double kx, double ky, double kz)
		{
			Transform3D t = new Transform3D();
			t.Scale(kx, ky, kz);
			return t;
		}


		public static Transform3D GetRotate(Vector3D axis, double angle)
		{
			return GetRotate(axis.X, axis.Y, axis.Z, angle);
		}


		public static Transform3D GetRotate(double x, double y, double z, double angle)
		{
			Transform3D t = new Transform3D();
			t.Rotate(x, y, z, angle);
			return t;
		}
		#endregion


		#region Operators
		public static Vector3D operator *(Transform3D t, Vector3D v)
		{
			double[,] column = new double[1, 4];
			column[0, 0] = v.X;
			column[0, 1] = v.Y;
			column[0, 2] = v.Z;
			column[0, 3] = 1;

			column = MatrixMath.Mul(t._matrix, column);

			return new Vector3D(column[0, 0], column[0, 1], column[0, 2]);
		}


		public static Transform3D operator *(Transform3D a, Transform3D b)
		{
			return new Transform3D()
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
