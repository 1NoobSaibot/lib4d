namespace Lib4D
{
	public struct Quaternion
	{
		public Complex ri, jk;
		#region Getters
		public double R
		{
			get => ri.r;
			set => ri.r = value;
		}
		public double I
		{
			get => ri.i;
			set => ri.i = value;
		}
		public double J
		{
			get => jk.r;
			set => jk.r = value;
		}
		public double K
		{
			get => jk.i;
			set => jk.i = value;
		}
		#endregion

		#region Constructors
		public Quaternion(double r)
		{
			this.ri = new Complex(r, 0);
			this.jk = new Complex();
		}

		public Quaternion(double r, double i)
		{
			this.ri = new Complex(r, i);
			this.jk = new Complex();
		}
		public Quaternion(double r, double i, double j)
		{
			this.ri = new Complex(r, i);
			this.jk = new Complex(j, 0);
		}
		public Quaternion(double r, double i, double j, double k)
		{
			this.ri = new Complex(r, i);
			this.jk = new Complex(j, k);
		}

		public Quaternion(Complex ri)
		{
			this.ri = ri;
			this.jk = new Complex();
		}
		public Quaternion(Complex ri, Complex jk)
		{
			this.ri = ri;
			this.jk = jk;
		}
		#endregion

		#region Math Operators
		public static Quaternion operator +(Quaternion a, Quaternion b)
		{
			return new Quaternion(a.ri + b.ri, a.jk + b.jk);
		}

		public static Quaternion operator -(Quaternion a, Quaternion b)
		{
			return new Quaternion(a.ri - b.ri, a.jk - b.jk);
		}

		public static Quaternion operator *(Quaternion a, Quaternion b)
		{
			double r = a.R * b.R - (a.I * b.I + a.J * b.J + a.K * b.K);
			double i = a.R * b.I + a.I * b.R + (a.J * b.K - a.K * b.J);
			double j = a.R * b.J + a.J * b.R + (a.K * b.I - a.I * b.K);
			double k = a.R * b.K + a.K * b.R + (a.I * b.J - a.J * b.I);

			return new Quaternion(r, i, j, k);
		}
		#endregion

		#region Comparisons
		public static bool operator ==(Quaternion a, Quaternion b)
		{
			return a.ri == b.ri && a.jk == b.jk;
		}

		public static bool operator !=(Quaternion a, Quaternion b)
		{
			return a.ri != b.ri || a.jk != b.jk;
		}
		#endregion

		public override string ToString()
		{
			return "(" + R + " + i" + I + " + j" + J + " + k" + K + ")";
		}
	}
}
