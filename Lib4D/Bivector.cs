namespace Lib4D
{
	public class Bivector4D
	{
		private double[,] _matrix;
		public double[,] Matrix => _matrix;

		public double XY => _matrix[0, 1];
		public double XZ => _matrix[0, 2];
		public double XQ => _matrix[0, 3];
		public double YZ => _matrix[1, 2];
		public double YQ => _matrix[1, 3];
		public double ZQ => _matrix[2, 3];


		private Bivector4D()
		{
			_matrix = new double[4, 4];
		}


		public Bivector4D(Vector4D a1, Vector4D a2)
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


		public static bool operator ==(Bivector4D a, Bivector4D b)
		{
			return a._matrix.EqualsTo(b._matrix);
		}


		public static bool operator !=(Bivector4D a, Bivector4D b)
		{
			return !a._matrix.EqualsTo(b._matrix);
		}


		public static bool operator ==(Bivector4D b, double d) {
			double dt = b._matrix.GetDeterminant();
			return dt == d;
		}


		public static bool operator !=(Bivector4D b, double d) {
			double dt = b._matrix.GetDeterminant();
			return dt != d;
		}


		public static Bivector4D operator -(Bivector4D b)
		{
			return -1.0 * b;
		}


		public static Bivector4D operator *(double a, Bivector4D b)
		{
			Bivector4D res = new Bivector4D()
			{
				_matrix = b._matrix.Mul(a),
			};

			return res;
		}


		public static Bivector4D operator +(Bivector4D a, Bivector4D b)
		{
			return new Bivector4D()
			{
				_matrix = MatrixMath.Add(a._matrix, b._matrix)
			};
		}
	}
}
