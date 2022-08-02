using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib4D
{
  /// <summary>
  /// Complex number c = a + ib,  where i = sqrt(-1); i*i = -1
  /// </summary>
  public struct Complex
  {
    public double real;
    public double imaginary;

    public Complex(double real, double imaginary)
    {
      this.real = real;
      this.imaginary = imaginary;
    }

    public double Abs()
	  {
      return Math.Sqrt(real * real + imaginary * imaginary);
	  }

		public static bool operator ==(Complex a, Complex b)
	  {
      return a.real == b.real && a.imaginary == b.imaginary;
	  }

    public static bool operator !=(Complex a, Complex b)
	  {
      return a.real != b.real || a.imaginary != b.imaginary;
	  }

    public static Complex operator +(Complex a, Complex b)
    {
      return new Complex(a.real + b.real, a.imaginary + b.imaginary);
    }

    public static Complex operator -(Complex a, Complex b)
    {
      return new Complex(a.real - b.real, a.imaginary - b.imaginary);
    }


    public static Complex operator *(Complex a, Complex b) {
      /*
       a * b = (a.r + a.i) * (b.r + b.i) = 
        = a.r * b.r + a.r * b.i + a.i * b.r + a.i * b.i
        = (a.r * b.r - a.i * b.i) + (a.r * b.i + a.i * b.r)
       */
      double real = a.real * b.real - a.imaginary * b.imaginary;
      double imaginary = a.real * b.imaginary + a.imaginary * b.real;
      return new Complex(real, imaginary);
    }

    public static Complex operator /(Complex a, Complex b)
	  {
      /*
        a / b = (a.r + a.i) / (b.r + b.i) =
        = ((a.r + a.i)*(b.r - b.i))/(b.r^2 - -(b.i^2))
        = (a.r * b.r - a.r * b.i + a.i * b.r - a.i * b.i) / (b.r^2 + b.i^2)
        = ((a.r * b.r - a.i * b.i) + (a.i * b.r - a.r * b.i)) / (b.r^2 + b.i^2)

        a^2 - b^2 = (a + b)(a - b)
       */
      double denominator = b.real * b.real + b.imaginary * b.imaginary;
      double realNumerator = (a.real * b.real) + (a.imaginary * b.imaginary);
      double imaginaryNumerator = (a.imaginary * b.real) - (a.real * b.imaginary);
      return new Complex(realNumerator / denominator, imaginaryNumerator / denominator);
	  }
  }
}
