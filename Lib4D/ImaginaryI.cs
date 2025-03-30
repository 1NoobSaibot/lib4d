using Lib4D.Mathematic;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Lib4D
{
	/// <summary>
	/// Imaginary i, where i = sqrt(-1); i*i = -1
	/// </summary>
	public readonly struct ImaginaryI<TNumber> :
		IAdditionOperators<ImaginaryI<TNumber>, ImaginaryI<TNumber>, ImaginaryI<TNumber>>,
		ISubtractionOperators<ImaginaryI<TNumber>, ImaginaryI<TNumber>, ImaginaryI<TNumber>>,
		IUnaryNegationOperators<ImaginaryI<TNumber>, ImaginaryI<TNumber>>,
		IEquatable<ImaginaryI<TNumber>>,
		IEqualityOperators<ImaginaryI<TNumber>, ImaginaryI<TNumber>, bool>
		where TNumber : INumber<TNumber>
	{
		public readonly TNumber Value;
		public static readonly ImaginaryI<TNumber> Zero = new(TNumber.Zero);


		#region Constructors
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ImaginaryI(TNumber value)
		{
			Value = value;
		}


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ImaginaryI()
		{
			Value = TNumber.Zero;
		}


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ImaginaryI(double value)
		{
			Value = Math<TNumber>.Double2Number!(value);
		}
		#endregion


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(ImaginaryI<TNumber> a, ImaginaryI<TNumber> b)
		{
			return a.Value == b.Value;
		}


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(ImaginaryI<TNumber> a, ImaginaryI<TNumber> b)
		{
			return a.Value != b.Value;
		}


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ImaginaryI<TNumber> operator +(ImaginaryI<TNumber> a, ImaginaryI<TNumber> b)
		{
			return new ImaginaryI<TNumber>(a.Value + b.Value);
		}


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Complex<TNumber> operator +(ImaginaryI<TNumber> a, TNumber b)
		{
			return new Complex<TNumber>(real: b, imaginary: a.Value);
		}


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Complex<TNumber> operator +(TNumber a, ImaginaryI<TNumber> b)
		{
			return new Complex<TNumber>(real: a, imaginary: b.Value);
		}


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ImaginaryI<TNumber> operator -(ImaginaryI<TNumber> a, ImaginaryI<TNumber> b)
		{
			return new ImaginaryI<TNumber>(a.Value - b.Value);
		}


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Complex<TNumber> operator -(ImaginaryI<TNumber> a, TNumber b)
		{
			return new Complex<TNumber>(real: -b, imaginary: a.Value);
		}


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Complex<TNumber> operator -(TNumber a, ImaginaryI<TNumber> b)
		{
			return new Complex<TNumber>(real: a, imaginary: -b.Value);
		}


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TNumber operator *(ImaginaryI<TNumber> a, ImaginaryI<TNumber> b)
		{
			return -(a.Value * b.Value);
		}


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ImaginaryI<TNumber> operator *(ImaginaryI<TNumber> a, TNumber b)
		{
			return new(a.Value * b);
		}


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ImaginaryI<TNumber> operator *(TNumber a, ImaginaryI<TNumber> b)
		{
			return new(a * b.Value);
		}


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TNumber operator /(ImaginaryI<TNumber> a, ImaginaryI<TNumber> b)
		{
			return a.Value / b.Value;
		}


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ImaginaryI<TNumber> operator /(ImaginaryI<TNumber> a, TNumber b)
		{
			return new(a.Value / b);
		}


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ImaginaryI<TNumber> operator /(TNumber a, ImaginaryI<TNumber> b)
		{
			return new(-(a / b.Value));
		}


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ImaginaryI<TNumber> operator -(ImaginaryI<TNumber> value)
		{
			return new(-value.Value);
		}


		public readonly override string ToString()
		{
			return Value + "i";
		}


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool Equals(ImaginaryI<TNumber> other)
		{
			return this == other;
		}


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly override bool Equals(object? obj)
		{
			return obj is ImaginaryI<TNumber> imaginary
				&& this == imaginary;
		}


		public readonly override int GetHashCode()
		{
			return Value.GetHashCode();
		}
	}
}
