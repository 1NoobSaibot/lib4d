using System.Numerics;

namespace Lib4D.Mathematic.Matrix
{
	public static class MatrixMath {
		public static TNumber[,] Add<TNumber>(TNumber[,] a, TNumber[,] b)
			where TNumber : INumber<TNumber>
		{
			int width = a.GetWidth();
			int height = a.GetHeight();
			if (width != b.GetWidth() || height != b.GetHeight())
			{
				throw new ArgumentException("Matrixes have different size");
			}

			TNumber[,] result = new TNumber[width, height];
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					result[i, j] = a[i, j] + b[i, j];
				}
			}

			return result;
		}


		public static TNumber GetDeterminant<TNumber>(this TNumber[,] matrix)
			where TNumber : INumber<TNumber>
		{
			var calc = new QuadMatrixDeterminantCalculator<TNumber>(matrix);
			return calc.Determinant;
		}


		public static TNumber[,] Transpose<TNumber>(this TNumber[,] matrixA)
			where TNumber : INumber<TNumber>
		{
			int width = matrixA.GetLength(0);
			int height = matrixA.GetLength(1);
			TNumber[,] res = new TNumber[height, width];

			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					res[y, x] = matrixA[x, y];
				}
			}

			return res;
		}


		public static bool EqualsTo<TNumber>(this TNumber[,] a, TNumber[,] b)
			where TNumber : INumber<TNumber>
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
		public static TNumber[,] CreateIdentity<TNumber>(int width)
			where TNumber : INumber<TNumber>
		{
			TNumber[,] m = new TNumber[width, width];
			for (int i = 0; i < width; i++)
			{
				m[i, i] = TNumber.One;
			}
			return m;
		}


		public static TNumber[,] Mul<TNumber>(this TNumber[,] a, TNumber b)
			where TNumber : INumber<TNumber>
		{
			int width = a.GetWidth();
			int height = a.GetHeight();
			TNumber[,] c = new TNumber[width, height];

			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					c[x, y] = a[x, y] * b;
				}
			}

			return c;
		}


		public static TNumber[,] Mul<TNumber>(this TNumber[,] a, TNumber[,] b)
			where TNumber : INumber<TNumber>
		{
			// Ширина матрицы A равна высоте B
			if (a.GetWidth() != b.GetHeight()) {
				throw new Exception($"Cannot execute multiplication of matrixes, because their sizes are not suitable: a.Width={a.GetWidth()}, b.Height={b.GetHeight()}");
			}

			int commonWidth = a.GetWidth();
			int width = b.GetWidth();
			int height = a.GetHeight();
			TNumber[,] c = new TNumber[width, height];

			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					TNumber sum = TNumber.Zero;
					for (int i = 0; i < commonWidth; i++)
					{
						sum += a[i, y] * b[x, i];
					}
					c[x, y] = sum;
				}
			}

			return c;
		}


		public static void Mul<TNumber>(this TNumber[,] a, TNumber[,] b, TNumber[,] c)
			where TNumber : INumber<TNumber>
		{
			// Ширина матрицы A равна высоте B
			if (a.GetWidth() != b.GetHeight())
			{
				throw new Exception($"Cannot execute multiplication of matrixes, because their sizes are not suitable: a.Width={a.GetWidth()}, b.Height={b.GetHeight()}");
			}

			int commonWidth = a.GetWidth();
			int width = b.GetWidth();
			int height = a.GetHeight();

			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					TNumber sum = TNumber.Zero;
					for (int i = 0; i < commonWidth; i++)
					{
						sum += a[i, y] * b[x, i];
					}
					c[x, y] = sum;
				}
			}
		}


		public static TNumber[,] Extend<TNumber>(TNumber[,] sourceMatrix, int newWidth, int newHeight)
			where TNumber : INumber<TNumber>
		{
			TNumber[,] res = new TNumber[newWidth, newHeight];
			for (int i = 0; i < sourceMatrix.GetWidth(); i++) {
				for (int j = 0; j < sourceMatrix.GetHeight(); j++) {
					res[i, j] = sourceMatrix[i, j];
				}
			}
			return res;
		}


		public static int GetWidth<T>(this T[,] a)
		{
			return a.GetLength(0);
		}

		public static int GetHeight<T>(this T[,] a)
		{
			return a.GetLength(1);
		}
	}
}
