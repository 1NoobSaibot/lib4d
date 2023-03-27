using Lib4D.Mathematic.Matrix;

namespace Lib4D
{
	public class Bivector4DFloat
	{
		private float[,] _matrix;
		public float[,] Matrix => _matrix;

		public float XY => _matrix[0, 1];
		public float XZ => _matrix[0, 2];
		public float XQ => _matrix[0, 3];
		public float YZ => _matrix[1, 2];
		public float YQ => _matrix[1, 3];
		public float ZQ => _matrix[2, 3];


		private Bivector4DFloat()
		{
			_matrix = new float[4, 4];
		}


		public Bivector4DFloat(Vector4D<float> a1, Vector4D<float> a2)
		{
			_matrix = new float[4, 4];
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					_matrix[i, j] = a1[i] * a2[j] - a1[j] * a2[i];
				}
			}
		}


		public static bool operator ==(Bivector4DFloat a, Bivector4DFloat b)
		{
			return a._matrix.EqualsTo(b._matrix);
		}


		public static bool operator !=(Bivector4DFloat a, Bivector4DFloat b)
		{
			return !a._matrix.EqualsTo(b._matrix);
		}


		public static bool operator ==(Bivector4DFloat b, float d) {
			float dt = b._matrix.GetDeterminant();
			return dt == d;
		}


		public static bool operator !=(Bivector4DFloat b, float d) {
			float dt = b._matrix.GetDeterminant();
			return dt != d;
		}


		public static Bivector4DFloat operator -(Bivector4DFloat b)
		{
			return -1.0f * b;
		}


		public static Bivector4DFloat operator *(float a, Bivector4DFloat b)
		{
			Bivector4DFloat res = new()
			{
				_matrix = b._matrix.Mul(a),
			};

			return res;
		}


		public static Bivector4DFloat operator +(Bivector4DFloat a, Bivector4DFloat b)
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

			return obj is Bivector4DFloat bv && this == bv;
		}

		public override int GetHashCode()
		{
			return _matrix.GetHashCode();
		}
	}
}
