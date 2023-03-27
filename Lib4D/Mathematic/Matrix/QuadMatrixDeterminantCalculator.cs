using System.Numerics;

namespace Lib4D.Mathematic.Matrix
{
	internal class QuadMatrixDeterminantCalculator<TNumber> where TNumber : INumber<TNumber>
	{
		private readonly TNumber[,] _matrix;
		private readonly bool[] _usedColumns;
		public readonly TNumber Determinant;


		public QuadMatrixDeterminantCalculator(TNumber[,] matrix) {
			int width = matrix.GetWidth();
			if (width != matrix.GetHeight()) {
				throw new Exception("This matrix is not a QuadMatrix");
			}
			if (width == 0) {
				throw new Exception("Matrix doesn't contain any element");
			}

			_matrix = matrix;
			_usedColumns = new bool[width];

			Determinant = GetLocalDeterminant(0);
		}


		private TNumber GetLocalDeterminant(int row) {
			if (row == _matrix.GetHeight()) {
				return TNumber.One;
			}

			TNumber dt = TNumber.Zero;
			int width = _usedColumns.Length;

			int localIndex = 0;
			for (int absoluteIndex = 0; absoluteIndex < width; absoluteIndex++) {
				if (_usedColumns[absoluteIndex]) {
					continue;
				}

				TNumber sign = ((localIndex & 1) == 0) ? +TNumber.One : -TNumber.One;
				_usedColumns[absoluteIndex] = true;
				dt += sign * _matrix[absoluteIndex, row] * GetLocalDeterminant(row + 1);
				_usedColumns[absoluteIndex] = false;

				localIndex++;
			}

			return dt;
		}
	}
}
