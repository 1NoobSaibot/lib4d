using System;

namespace Lib4D
{
  /// <summary>
  /// Complex number c = a + ib,  where i = sqrt(-1); i*i = -1
  /// </summary>
  public struct Complex
  {
    /// <summary>
    /// Real number
    /// </summary>
    public double r;
    /// <summary>
    /// Imaginary number
    /// </summary>
    public double i;

    public Complex(double real, double imaginary)
    {
      this.r = real;
      this.i = imaginary;
    }

    public double Abs()
	  {
      return Math.Sqrt(r * r + i * i);
	  }

		public static bool operator ==(Complex a, Complex b)
	  {
      return a.r == b.r && a.i == b.i;
	  }

    public static bool operator !=(Complex a, Complex b)
	  {
      return a.r != b.r || a.i != b.i;
	  }

    public static Complex operator +(Complex a, Complex b)
    {
      return new Complex(a.r + b.r, a.i + b.i);
    }

    public static Complex operator -(Complex a, Complex b)
    {
      return new Complex(a.r - b.r, a.i - b.i);
    }


    public static Complex operator *(Complex a, Complex b) {
      double real = a.r * b.r - a.i * b.i;
      double imaginary = a.r * b.i + a.i * b.r;
      return new Complex(real, imaginary);
    }

    public static Complex operator /(Complex a, Complex b)
	  {
      double denominator = b.r * b.r + b.i * b.i;
      double realNumerator = (a.r * b.r) + (a.i * b.i);
      double imaginaryNumerator = (a.i * b.r) - (a.r * b.i);
      return new Complex(realNumerator / denominator, imaginaryNumerator / denominator);
	  }

    public static implicit operator Complex(double n)
		{
      return new Complex(n, 0);
		}

		public override string ToString()
		{
			return "(" + r + " + " + i + "i)";
		}
	}
}
