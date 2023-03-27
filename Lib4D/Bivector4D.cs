using Lib4D.Mathematic.Matrix;
using System.Numerics;

namespace Lib4D
{
	public class Bivector4D<TNumber> where TNumber : INumber<TNumber>
	{
		private TNumber[,] _matrix;
		public TNumber[,] Matrix => _matrix;

		public TNumber XY => _matrix[0, 1];
		public TNumber XZ => _matrix[0, 2];
		public TNumber XQ => _matrix[0, 3];
		public TNumber YZ => _matrix[1, 2];
		public TNumber YQ => _matrix[1, 3];
		public TNumber ZQ => _matrix[2, 3];


		private Bivector4D()
		{
			_matrix = new TNumber[4, 4];
		}


		public Bivector4D(Vector4D<TNumber> a1, Vector4D<TNumber> a2)
		{
			_matrix = new TNumber[4, 4];
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					_matrix[i, j] = a1[i] * a2[j] - a1[j] * a2[i];
				}
			}
		}


		public static bool operator ==(Bivector4D<TNumber> a, Bivector4D<TNumber> b)
		{
			return a._matrix.EqualsTo(b._matrix);
		}


		public static bool operator !=(Bivector4D<TNumber> a, Bivector4D<TNumber> b)
		{
			return !a._matrix.EqualsTo(b._matrix);
		}


		public static bool operator ==(Bivector4D<TNumber> b, TNumber d) {
			TNumber dt = b._matrix.GetDeterminant();
			return dt == d;
		}


		public static bool operator !=(Bivector4D<TNumber> b, TNumber d) {
			TNumber dt = b._matrix.GetDeterminant();
			return dt != d;
		}


		public static Bivector4D<TNumber> operator -(Bivector4D<TNumber> b)
		{
			return -(TNumber.One) * b;
		}


		public static Bivector4D<TNumber> operator *(TNumber a, Bivector4D<TNumber> b)
		{
			Bivector4D<TNumber> res = new()
			{
				_matrix = b._matrix.Mul(a),
			};

			return res;
		}


		public static Bivector4D<TNumber> operator +(Bivector4D<TNumber> a, Bivector4D<TNumber> b)
		{
			return new()
			{
				_matrix = MatrixMath.Add(a._matrix, b._matrix)
			};
		}

		public override bool Equals(object? obj)
		{
			if (ReferenceEquals(this, obj))
			{
				return true;
			}

			if (obj is null)
			{
				return false;
			}

			return obj is Bivector4D<TNumber> bv && this == bv;
		}

		public override int GetHashCode()
		{
			return _matrix.GetHashCode();
		}
	}
}
