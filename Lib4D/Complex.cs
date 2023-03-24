using System.Numerics;

namespace Lib4D
{
  /// <summary>
  /// Complex number c = a + ib,  where i = sqrt(-1); i*i = -1
  /// </summary>
  public struct Complex :
    IAdditionOperators<Complex, Complex, Complex>,
		ISubtractionOperators<Complex, Complex, Complex>,
    IMultiplyOperators<Complex, Complex, Complex>,
		IDivisionOperators<Complex, Complex, Complex>,
    IUnaryNegationOperators<Complex, Complex>,
		IEquatable<Complex>,
		IEqualityOperators<Complex, Complex, bool>

	  // IAdditiveIdentity<Complex, Complex>,
	  // IDecrementOperators<Complex>,
	  // IIncrementOperators<Complex>,
	  // IMultiplicativeIdentity<Complex, Complex>,
	  // ISpanFormattable,
	  // ISpanParsable<Complex>,
	  // IUnaryPlusOperators<Complex, Complex>,
	{
    /// <summary>
    /// Real number
    /// </summary>
    public double R;
    /// <summary>
    /// Imaginary number
    /// </summary>
    public double I;

		public double Magnitude => Abs();

		public Complex(double real, double imaginary)
    {
      R = real;
      I = imaginary;
    }

		public Complex(double real)
		{
			R = real;
			I = 0;
		}


		public double AbsQuad()
    {
      return R * R + I * I;
		}

    public double Abs()
	  {
      return Math.Sqrt(AbsQuad());
	  }


		public static bool operator ==(Complex a, Complex b)
	  {
      return a.R == b.R && a.I == b.I;
	  }
    public static bool operator !=(Complex a, Complex b)
	  {
      return a.R != b.R || a.I != b.I;
	  }


    public static Complex operator +(Complex a, Complex b)
    {
      return new Complex(a.R + b.R, a.I + b.I);
    }
		public static Complex operator +(Complex a, double b)
		{
			return new Complex(a.R + b, a.I);
		}
		public static Complex operator +(double a, Complex b)
		{
			return new Complex(a + b.R, b.I);
		}


		public static Complex operator -(Complex a, Complex b)
    {
      return new Complex(a.R - b.R, a.I - b.I);
    }
		public static Complex operator -(Complex a, double b)
		{
			return new Complex(a.R - b, a.I);
		}
		public static Complex operator -(double a, Complex b)
		{
			return new Complex(a - b.R, -b.I);
		}


		public static Complex operator *(Complex a, Complex b) {
      double real = a.R * b.R - a.I * b.I;
      double imaginary = a.R * b.I + a.I * b.R;
      return new Complex(real, imaginary);
    }
		public static Complex operator *(Complex a, double b)
		{
			return new(a.R * b, a.I * b);
		}
		public static Complex operator *(double a, Complex b)
		{
			return new(a * b.R, a * b.I);
		}


		public static Complex operator /(Complex a, Complex b)
	  {
      double denominator = b.AbsQuad();
      double realNumerator = (a.R * b.R) + (a.I * b.I);
      double imaginaryNumerator = (a.I * b.R) - (a.R * b.I);
      return new Complex(realNumerator / denominator, imaginaryNumerator / denominator);
	  }
		public static Complex operator /(Complex a, double b)
		{
			return new(a.R / b, a.I / b);
		}
		public static Complex operator /(double a, Complex b)
		{
			double denominator = b.AbsQuad();
			double realNumerator = a * b.R;
			double imaginaryNumerator = -(a * b.I);
			return new Complex(realNumerator / denominator, imaginaryNumerator / denominator);
		}

		public static Complex operator -(Complex value)
		{
			return value * -1;
		}

		public static Complex Sqrt(double value)
		{
			if (value < 0)
			{
				return new(0, Math.Sqrt(-value));
			}

			return new(Math.Sqrt(value), 0);
		}

		public static Complex Sqrt(Complex v)
		{
			double magnitude = v.Magnitude;
			double real = Math.Sqrt((magnitude + v.R) / 2);
			double imaginary = Math.Sqrt((magnitude - v.R) / 2)
				* (v.I < 0 ? -1.0 : 1.0);

			return new(real, imaginary);
		}


		public static Complex Exp(Complex number)
		{
			double realExp = Math.Exp(number.R);
			Complex c = new(Math.Cos(number.I), Math.Sin(number.I));
			return realExp * c;
		}


		public static implicit operator Complex(double n)
		{
      return new Complex(n, 0);
		}


		public override string ToString()
		{
			return "(" + R + " + " + I + "i)";
		}

		public bool Equals(Complex other)
		{
			return this == other;
		}

		// TODO: Does the method work with Int, Byte, Float types??
		public override bool Equals(object? obj)
		{
			return obj != null
				&& obj is Complex complex
				&& this == complex;
		}

		public override int GetHashCode()
		{
			return R.GetHashCode() ^ I.GetHashCode();
		}
	}
}
