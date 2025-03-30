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
		public ImaginaryI<TNumber> I;


		#region Constructors
		public Complex(TNumber real)
		{
			R = real;
			I = ImaginaryI<TNumber>.Zero;
		}
		public Complex(TNumber real, TNumber imaginary)
		{
			R = real;
			I = new(imaginary);
		}
		public Complex(TNumber real, ImaginaryI<TNumber> imaginary)
		{
			R = real;
			I = imaginary;
		}
		public Complex(double real)
			: this(real: Math<TNumber>.Double2Number!(real))
		{ }
		public Complex(double real, double imaginary)
			: this
		(
			real: Math<TNumber>.Double2Number!(real),
			imaginary: Math<TNumber>.Double2Number!(imaginary)
		)
		{ }
		#endregion



		public readonly TNumber AbsQuad()
		{
			return R * R + I.Value * I.Value;
		}


		public readonly TNumber Abs()
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


		public static Complex<TNumber> operator *(Complex<TNumber> a, Complex<TNumber> b)
		{
			TNumber real = a.R * b.R + a.I * b.I;
			ImaginaryI<TNumber> imaginary = a.R * b.I + a.I * b.R;
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
			TNumber realNumerator = (a.R * b.R) + (a.I.Value * b.I.Value);
			ImaginaryI<TNumber> imaginaryNumerator = (a.I * b.R) - (a.R * b.I);
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
			ImaginaryI<TNumber> imaginaryNumerator = -(a * b.I);
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
		public readonly Complex<TNumber> Sqrt()
		{
			var magnitude = Abs();
			var real = Math<TNumber>.Sqrt!((magnitude + R) / c2);
			var imaginary = Math<TNumber>.Sqrt!((magnitude - R) / c2)
				* (I.Value < TNumber.Zero ? -TNumber.One : TNumber.One);

			return new(real, imaginary);
		}


		public static Complex<TNumber> Exp(Complex<TNumber> number)
		{
			var realExp = Math<TNumber>.Exp!(number.R);
			Complex<TNumber> c = new(Math<TNumber>.Cos!(number.I.Value), Math<TNumber>.Sin!(number.I.Value));
			return realExp * c;
		}


		public static implicit operator Complex<TNumber>(TNumber n)
		{
			return new(n, TNumber.Zero);
		}
		public static implicit operator Complex<TNumber>(double n)
		{
			return new(Math<TNumber>.Double2Number!(n), TNumber.Zero);
		}


		public readonly override string ToString()
		{
			return "(" + R + " + " + I + "i)";
		}

		public readonly bool Equals(Complex<TNumber> other)
		{
			return this == other;
		}

		// TODO: Does the method work with Int, Byte, Float types??
		public readonly override bool Equals(object? obj)
		{
			return obj != null
				&& obj is Complex<TNumber> complex
				&& this == complex;
		}

		public readonly override int GetHashCode()
		{
			return R.GetHashCode() ^ I.GetHashCode();
		}
	}
}
