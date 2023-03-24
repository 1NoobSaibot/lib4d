using System;

namespace Lib4D
{
	public class Transform3DFloat
	{
		private float[,] _matrix = _CreateIdentityMatrix();


		public void Translate (Vector3DFloat t)
		{
			Translate (t.X, t.Y, t.Z);
		}


		public void Translate(float tx, float ty, float tz)
		{
			float[,] transformMatrix = _CreateIdentityMatrix();
			transformMatrix[3, 0] = tx;
			transformMatrix[3, 1] = ty;
			transformMatrix[3, 2] = tz;
			_matrix = MatrixMath.Mul(_matrix, transformMatrix);
		}


		public void Rotate(Vector3DFloat axis, float angle) {
			Rotate(axis.X, axis.Y, axis.Z, angle);
		}


		public void Rotate(float x, float y, float z, float angle)
		{
			float cos = (float)Math.Cos(angle);
			float sin = (float)Math.Sin(angle);
			float oneMinusCos = 1 - cos;
			float oneMinusCosXY = oneMinusCos * x * y;
			float oneMinusCosYZ = oneMinusCos * y * z;
			float oneMinusCosXZ = oneMinusCos * x * z;
			float sinX = sin * x;
			float sinY = sin * y;
			float sinZ = sin * z;

			float[,] rotateMatrix = new float[4, 4]
			{
				{ cos + oneMinusCos * x * x			, oneMinusCosXY + sinZ					, oneMinusCosXZ - sinY					, 0 },
				{ oneMinusCosXY - sinZ					, cos + oneMinusCos * y * y			, oneMinusCosYZ + sinX					, 0 },
				{ oneMinusCosXZ + sinY					, oneMinusCosYZ - sinX					, cos + oneMinusCos * z * z			, 0 },
				{ 0															, 0															, 0															, 1 }
			};

			_matrix = MatrixMath.Mul(_matrix, rotateMatrix);
		}


		public void Rotate(Quaternion q)
		{
			float w = (float)q.R;
			float x = (float)q.I;
			float y = (float)q.J;
			float z = (float)q.K;
			float twoXY = 2 * x * y;
			float twoXZ = 2 * x * z;
			float twoYZ = 2 * y * z;
			float twoWX = 2 * w * x;
			float twoWY = 2 * w * y;
			float twoWZ = 2 * w * z;
			float twoXX = 2 * x * x;
			float twoYY = 2 * y * y;
			float twoZZ = 2 * z * z;

			float[,] rotateMatrix = new float[4, 4] {
				{ 1 - twoYY - twoZZ	,		twoXY + twoWZ			,		twoXZ - twoWY			, 0 },
				{ twoXY - twoWZ			,		1 - twoXX - twoZZ	,		twoYZ + twoWX			, 0 },
				{ twoXZ + twoWY			, 	twoYZ - twoWX			,		1 - twoXX - twoYY , 0 },
				{ 0									,		0									,		0									,	1	}
			};

			_matrix = MatrixMath.Mul(_matrix, rotateMatrix);
		}


		public void Scale(Vector3DFloat k)
		{
			Scale(k.X, k.Y, k.Z);
		}


		public void Scale(float kx, float ky, float kz)
		{
			float[,] scaleMatrix = new float[4, 4]
			{
				{ kx,  0,  0, 0 },
				{  0, ky,  0, 0 },
				{  0,  0, kz, 0 },
				{  0,  0,  0, 1 },
			};
			_matrix = MatrixMath.Mul(_matrix, scaleMatrix);
		}

		#region Static Constructors
		public static Transform3DFloat GetTranslate(Vector3DFloat t)
		{
			return GetTranslate(t.X, t.Y, t.Z);
		}


		public static Transform3DFloat GetTranslate(float tx, float ty, float tz)
		{
			Transform3DFloat t = new Transform3DFloat();
			t.Translate(tx, ty, tz);
			return t;
		}


		public static Transform3DFloat GetScale(Vector3DFloat k)
		{
			return GetScale(k.X, k.Y, k.Z);
		}


		public static Transform3DFloat GetScale(float kx, float ky, float kz)
		{
			Transform3DFloat t = new Transform3DFloat();
			t.Scale(kx, ky, kz);
			return t;
		}


		public static Transform3DFloat GetRotate(Vector3DFloat axis, float angle)
		{
			return GetRotate(axis.X, axis.Y, axis.Z, angle);
		}


		public static Transform3DFloat GetRotate(float x, float y, float z, float angle)
		{
			Transform3DFloat t = new Transform3DFloat();
			t.Rotate(x, y, z, angle);
			return t;
		}


		public static Transform3DFloat GetRotate(Quaternion q)
		{
			Transform3DFloat t = new Transform3DFloat();
			t.Rotate(q);
			return t;
		}
		#endregion


		#region Operators
		public static Vector3DFloat operator *(Transform3DFloat t, Vector3DFloat v)
		{
			float[,] column = new float[1, 4];
			column[0, 0] = v.X;
			column[0, 1] = v.Y;
			column[0, 2] = v.Z;
			column[0, 3] = 1;

			column = MatrixMath.Mul(t._matrix, column);

			return new Vector3DFloat(column[0, 0], column[0, 1], column[0, 2]);
		}


		public static Transform3DFloat operator *(Transform3DFloat a, Transform3DFloat b)
		{
			return new Transform3DFloat()
			{
				_matrix = MatrixMath.Mul(a._matrix, b._matrix)
			};
		}
		#endregion

		private static float[,] _CreateIdentityMatrix()
		{
			return new float[4, 4]
			{
				{ 1, 0, 0, 0 },
				{ 0, 1, 0, 0 },
				{ 0, 0, 1, 0 },
				{ 0, 0, 0, 1 },
			};
		}
	}
}
