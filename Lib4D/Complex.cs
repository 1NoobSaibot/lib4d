using Lib4D.Mathematic;
using System.Numerics;

namespace Lib4D
{
  /// <summary>
  /// Complex number c = a + ib,  where i = sqrt(-1); i*i = -1
  /// </summary>
  public struct Complex<TNumber> :
    IAdditionOperators<Complex<TNumber>, Complex<TNumber>, Complex<TNumber>>,
		ISubtractionOperators<Complex<TNumber>, Complex<TNumber>, Complex<TNumber>>,
    IMultiplyOperators<Complex<TNumber>, Complex<TNumber>, Complex<TNumber>>,
		IDivisionOperators<Complex<TNumber>, Complex<TNumber>, Complex<TNumber>>,
    IUnaryNegationOperators<Complex<TNumber>, Complex<TNumber>>,
		IEquatable<Complex<TNumber>>,
		IEqualityOperators<Complex<TNumber>, Complex<TNumber>, bool>
		where TNumber : INumber<TNumber>

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
    public TNumber R;
    /// <summary>
    /// Imaginary number
    /// </summary>
    public TNumber I;

		public Complex(TNumber real, TNumber imaginary)
    {
      R = real;
      I = imaginary;
    }

		public Complex(TNumber real)
		{
			R = real;
			I = TNumber.Zero;
		}


		public TNumber AbsQuad()
    {
      return R * R + I * I;
		}


		public TNumber Abs()
		{
			return Math<TNumber>.Sqrt!(AbsQuad());
		}


		public static bool operator ==(Complex<TNumber> a, Complex<TNumber> b)
	  {
      return a.R == b.R && a.I == b.I;
	  }
    public static bool operator !=(Complex<TNumber> a, Complex<TNumber> b)
	  {
      return a.R != b.R || a.I != b.I;
	  }


    public static Complex<TNumber> operator +(Complex<TNumber> a, Complex<TNumber> b)
    {
      return new Complex<TNumber>(a.R + b.R, a.I + b.I);
    }
		public static Complex<TNumber> operator +(Complex<TNumber> a, TNumber b)
		{
			return new Complex<TNumber>(a.R + b, a.I);
		}
		public static Complex<TNumber> operator +(TNumber a, Complex<TNumber> b)
		{
			return new Complex<TNumber>(a + b.R, b.I);
		}


		public static Complex<TNumber> operator -(Complex<TNumber> a, Complex<TNumber> b)
    {
      return new Complex<TNumber>(a.R - b.R, a.I - b.I);
    }
		public static Complex<TNumber> operator -(Complex<TNumber> a, TNumber b)
		{
			return new Complex<TNumber>(a.R - b, a.I);
		}
		public static Complex<TNumber> operator -(TNumber a, Complex<TNumber> b)
		{
			return new Complex<TNumber>(a - b.R, -b.I);
		}


		public static Complex<TNumber> operator *(Complex<TNumber> a, Complex<TNumber> b) {
      TNumber real = a.R * b.R - a.I * b.I;
      TNumber imaginary = a.R * b.I + a.I * b.R;
      return new Complex<TNumber>(real, imaginary);
    }
		public static Complex<TNumber> operator *(Complex<TNumber> a, TNumber b)
		{
			return new(a.R * b, a.I * b);
		}
		public static Complex<TNumber> operator *(TNumber a, Complex<TNumber> b)
		{
			return new(a * b.R, a * b.I);
		}


		public static Complex<TNumber> operator /(Complex<TNumber> a, Complex<TNumber> b)
	  {
      TNumber denominator = b.AbsQuad();
      TNumber realNumerator = (a.R * b.R) + (a.I * b.I);
      TNumber imaginaryNumerator = (a.I * b.R) - (a.R * b.I);
      return new Complex<TNumber>(realNumerator / denominator, imaginaryNumerator / denominator);
	  }
		public static Complex<TNumber> operator /(Complex<TNumber> a, TNumber b)
		{
			return new(a.R / b, a.I / b);
		}
		public static Complex<TNumber> operator /(TNumber a, Complex<TNumber> b)
		{
			TNumber denominator = b.AbsQuad();
			TNumber realNumerator = a * b.R;
			TNumber imaginaryNumerator = -(a * b.I);
			return new(realNumerator / denominator, imaginaryNumerator / denominator);
		}

		public static Complex<TNumber> operator -(Complex<TNumber> value)
		{
			return value * -TNumber.One;
		}


		public static Complex<TNumber> Sqrt(TNumber value)
		{
			if (value < TNumber.Zero)
			{
				return new(TNumber.Zero, Math<TNumber>.Sqrt!(-value));
			}

			return new(Math<TNumber>.Sqrt!(value), TNumber.Zero);
		}


		private static readonly TNumber c2 = Math<TNumber>.Int2Number!(2);
		public Complex<TNumber> Sqrt()
		{
			var magnitude = Abs();
			var real = Math<TNumber>.Sqrt!((magnitude + R) / c2);
			var imaginary = Math<TNumber>.Sqrt!((magnitude - R) / c2)
				* (I < TNumber.Zero ? -TNumber.One : TNumber.One);

			return new(real, imaginary);
		}


		public static Complex<TNumber> Exp(Complex<TNumber> number)
		{
			var realExp = Math<TNumber>.Exp!(number.R);
			Complex<TNumber> c = new(Math<TNumber>.Cos!(number.I), Math<TNumber>.Sin!(number.I));
			return realExp * c;
		}


		public static implicit operator Complex<TNumber>(TNumber n)
		{
      return new(n, TNumber.Zero);
		}


		public override string ToString()
		{
			return "(" + R + " + " + I + "i)";
		}

		public bool Equals(Complex<TNumber> other)
		{
			return this == other;
		}

		// TODO: Does the method work with Int, Byte, Float types??
		public override bool Equals(object? obj)
		{
			return obj != null
				&& obj is Complex<TNumber> complex
				&& this == complex;
		}

		public override int GetHashCode()
		{
			return R.GetHashCode() ^ I.GetHashCode();
		}
	}
}
