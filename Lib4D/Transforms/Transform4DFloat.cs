﻿using System;

namespace Lib4D
{
	public class Transform4DFloat
	{
		private float[,] _matrix;

		public Transform4DFloat (float[,] matrix)
		{
			if (matrix.GetWidth() != 5 || matrix.GetHeight() != 5)
			{
				throw new ArgumentException("Matrix should be size 5*5");
			}

			_matrix = matrix;
		}

		public Transform4DFloat ()
		{
			_matrix = _CreateIdentityMatrix();
		}

		public void Translate (Vector4DFloat t)
		{
			Translate(t.X, t.Y, t.Z, t.Q);
		}


		public void Translate(float tx, float ty, float tz, float tq)
		{
			float[,] transformMatrix = _CreateIdentityMatrix();
			transformMatrix[4, 0] = tx;
			transformMatrix[4, 1] = ty;
			transformMatrix[4, 2] = tz;
			transformMatrix[4, 3] = tq;
			_matrix = MatrixMathF.Mul(_matrix, transformMatrix);
		}


		public void Scale(Vector4DFloat k)
		{
			Scale(k.X, k.Y, k.Z, k.Q);
		}


		public void Scale(float kx, float ky, float kz, float kq)
		{
			float[,] scaleMatrix = new float[5, 5]
			{
				{ kx,  0,  0,  0, 0 },
				{  0, ky,  0,  0, 0 },
				{  0,  0, kz,  0, 0 },
				{  0,  0,  0, kq, 0 },
				{  0,  0,  0,  0, 1 },
			};
			_matrix = MatrixMathF.Mul(_matrix, scaleMatrix);
		}


		public void Rotate(Bivector4DFloat b, float angle)
		{
			float c = (float)Math.Cos(angle);
			float s = (float)Math.Sin(angle);
			float xy = b.XY;
			float xz = b.XZ;
			float xq = b.XQ;
			float yz = b.YZ;
			float yq = b.YQ;
			float zq = b.ZQ;

			float[,] uut = MatrixMathF.Mul(b.Matrix, MatrixMathF.Transpose(b.Matrix));
			uut = uut.Mul(1.0f - c);
			
			float[,] sinAndCos = new float[4, 4]
			{
				{	 c			,  s * zq	, -s * yq	, -s * yz },
				{ -s * zq	,	 c			,	 s * xq	, -s * xz },
				{	 s * yq	,	-s * xq	,	 c			,  s * xy },
				{  s * yz	,	 s * xz	,	-s * xy	,  c			},
			};

			float[,] R = MatrixMathF.Add(sinAndCos, uut);
			R = MatrixMathF.Extend(R, 5, 5);
			R[4, 4] = 1;

			_matrix = _matrix.Mul(R);
		}

		
		#region Static Constructors
		public static Transform4DFloat GetTranslate(Vector4DFloat t)
		{
			return GetTranslate(t.X, t.Y, t.Z, t.Q);
		}


		public static Transform4DFloat GetTranslate(float tx, float ty, float tz, float tq)
		{
			Transform4DFloat t = new Transform4DFloat();
			t.Translate(tx, ty, tz, tq);
			return t;
		}


		public static Transform4DFloat GetScale(Vector4DFloat k)
		{
			return GetScale(k.X, k.Y, k.Z, k.Q);
		}


		public static Transform4DFloat GetScale(float kx, float ky, float kz, float kq)
		{
			Transform4DFloat t = new Transform4DFloat();
			t.Scale(kx, ky, kz, kq);
			return t;
		}
		#endregion


		#region Operators
		public static Vector4DFloat operator *(Transform4DFloat t, Vector4DFloat v)
		{
			float[,] column = new float[1, 5];
			column[0, 0] = v.X;
			column[0, 1] = v.Y;
			column[0, 2] = v.Z;
			column[0, 3] = v.Q;
			column[0, 4] = 1;

			column = MatrixMathF.Mul(t._matrix, column);

			return new Vector4DFloat(column[0, 0], column[0, 1], column[0, 2], column[0, 3]);
		}


		public static Transform4DFloat operator *(Transform4DFloat a, Transform4DFloat b)
		{
			return new Transform4DFloat()
			{
				_matrix = MatrixMathF.Mul(a._matrix, b._matrix)
			};
		}
		#endregion

		private static float[,] _CreateIdentityMatrix()
		{
			return new float[5, 5]
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