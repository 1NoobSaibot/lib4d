namespace Lib4D
{
	public class Bivector4DDouble
	{
		private double[,] _matrix;
		public double[,] Matrix => _matrix;

		public double XY => _matrix[0, 1];
		public double XZ => _matrix[0, 2];
		public double XQ => _matrix[0, 3];
		public double YZ => _matrix[1, 2];
		public double YQ => _matrix[1, 3];
		public double ZQ => _matrix[2, 3];


		private Bivector4DDouble()
		{
			_matrix = new double[4, 4];
		}


		public Bivector4DDouble(Vector4DDouble a1, Vector4DDouble a2)
		{
			_matrix = new double[4, 4];
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					_matrix[i, j] = a1[i] * a2[j] - a1[j] * a2[i];
				}
			}
		}


		public static bool operator ==(Bivector4DDouble a, Bivector4DDouble b)
		{
			return a._matrix.EqualsTo(b._matrix);
		}


		public static bool operator !=(Bivector4DDouble a, Bivector4DDouble b)
		{
			return !a._matrix.EqualsTo(b._matrix);
		}


		public static bool operator ==(Bivector4DDouble b, double d) {
			double dt = b._matrix.GetDeterminant();
			return dt == d;
		}


		public static bool operator !=(Bivector4DDouble b, double d) {
			double dt = b._matrix.GetDeterminant();
			return dt != d;
		}


		public static Bivector4DDouble operator -(Bivector4DDouble b)
		{
			return -1.0 * b;
		}


		public static Bivector4DDouble operator *(double a, Bivector4DDouble b)
		{
			Bivector4DDouble res = new Bivector4DDouble()
			{
				_matrix = b._matrix.Mul(a),
			};

			return res;
		}


		public static Bivector4DDouble operator +(Bivector4DDouble a, Bivector4DDouble b)
		{
			return new Bivector4DDouble()
			{
				_matrix = MatrixMath.Add(a._matrix, b._matrix)
			};
		}
	}
}
