using System;

namespace Lib4D
{
	internal class QuadMatrixDeterminantCalculator
	{
		private double[,] _matrix;
		private bool[] _usedColumns;
		public readonly double Determinant;


		public QuadMatrixDeterminantCalculator(double[,] matrix) {
			int width = matrix.GetWidth();
			if (width != matrix.GetHeight()) {
				throw new Exception("This matrix is not a QuadMatrix");
			}
			if (width == 0) {
				throw new Exception("Matrix doesn't contain any element");
			}

			_matrix = matrix;
			_usedColumns = new bool[width];

			Determinant = _GetLocalDeterminant(0);
		}


		private double _GetLocalDeterminant(int row) {
			if (row == _matrix.GetHeight()) {
				return 1;
			}

			double dt = 0;
			int width = _usedColumns.Length;

			int localIndex = 0;
			for (int absoluteIndex = 0; absoluteIndex < width; absoluteIndex++) {
				if (_usedColumns[absoluteIndex]) {
					continue;
				}

				double sign = ((localIndex & 1) == 0) ? +1 : -1;
				_usedColumns[absoluteIndex] = true;
				dt += sign * _matrix[absoluteIndex, row] * _GetLocalDeterminant(row + 1);
				_usedColumns[absoluteIndex] = false;

				localIndex++;
			}

			return dt;
		}
	}
}
