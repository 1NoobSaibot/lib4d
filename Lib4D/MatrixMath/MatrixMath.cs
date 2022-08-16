using System;

namespace Lib4D
{
	public static class MatrixMath {
		public static double GetDeterminant(this double[,] matrix) {
			QuadMatrixDeterminantCalculator calc = new QuadMatrixDeterminantCalculator(matrix);
			return calc.Determinant;
		}


		public static double[,] Transpose (this double[,] matrixA)
		{
			int width = matrixA.GetLength(0);
			int height = matrixA.GetLength(1);
			double[,] res = new double[height, width];

			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					res[y, x] = matrixA[x, y];
				}
			}

			return res;
		}

		public static bool EqualsTo(this double[,] a, double[,] b)
		{
			if (a.GetLength(0) != b.GetLength(0) || a.GetLength(1) != b.GetLength(1))
			{
				return false;
			}

			int width = a.GetLength(0);
			int height = a.GetLength(1);

			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					if (a[x, y] != b[x, y])
					{
						return false;
					}
				}
			}

			return true;
		}


		/// <summary>
		/// Returns identity matrix width*width
		/// </summary>
		/// <param name="width"></param>
		/// <returns>Quad Identity Matrix</returns>
		public static double[,] CreateIdentity(int width)
		{
			double[,] m = new double[width, width];
			for (int i = 0; i < width; i++)
			{
				m[i, i] = 1;
			}
			return m;
		}


		public static double[,] Mul (this double[,] a, double[,] b)
		{
			// Ширина матрицы A равна высоте B
			if (a.GetWidth() != b.GetHeight()) {
				throw new Exception("Cannot execute multiplication of matrixes, because their sizes are not suitable");
			}

			int commonWidth = a.GetWidth();
			int width = b.GetWidth();
			int height = a.GetHeight();
			double[,] c = new double[width, height];

			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					double sum = 0.0;
					for (int i = 0; i < commonWidth; i++)
					{
						sum += a[i, y] * b[x, i];
					}
					c[x, y] = sum;
				}
			}

			return c;
		}

		public static int GetWidth (this double[,] a)
		{
			return a.GetLength(0);
		}

		public static int GetHeight(this double[,] a)
		{
			return a.GetLength(1);
		}
	}
}
