using Lib4D.Mathematic;
using System.Numerics;

namespace Lib4D
{
	public struct Quaternion<TNumber> :
		IAdditionOperators<Quaternion<TNumber>, Quaternion<TNumber>, Quaternion<TNumber>>,
		ISubtractionOperators<Quaternion<TNumber>, Quaternion<TNumber>, Quaternion<TNumber>>,
		IMultiplyOperators<Quaternion<TNumber>, Quaternion<TNumber>, Quaternion<TNumber>>,
		IDivisionOperators<Quaternion<TNumber>, Quaternion<TNumber>, Quaternion<TNumber>>,
		IUnaryNegationOperators<Quaternion<TNumber>, Quaternion<TNumber>>,
		IEquatable<Quaternion<TNumber>>,
		IEqualityOperators<Quaternion<TNumber>, Quaternion<TNumber>, bool>
		where TNumber : INumber<TNumber>
	{
		public Complex<TNumber> RI, JK;

		#region Getters
		public TNumber R
		{
			get => RI.R;
			set => RI.R = value;
		}
		public TNumber I
		{
			get => RI.I;
			set => RI.I = value;
		}
		public TNumber J
		{
			get => JK.R;
			set => JK.R = value;
		}
		public TNumber K
		{
			get => JK.I;
			set => JK.I = value;
		}

		public TNumber AbsQuad => R*R + I*I + J*J + K*K;
		public TNumber Abs => Math<TNumber>.Sqrt!(AbsQuad);


		public Quaternion<TNumber> ConjugateQuaternion => new(R, -I, -J, -K);
		#endregion


		#region Constructors
		public Quaternion(TNumber r)
		{
			RI = r;
		}
		public Quaternion(TNumber r, TNumber i)
		{
			RI = new Complex<TNumber>(r, i);
		}
		public Quaternion(TNumber r, TNumber i, TNumber j)
		{
			RI = new Complex<TNumber>(r, i);
			JK = j;
		}
		public Quaternion(TNumber r, TNumber i, TNumber j, TNumber k)
		{
			RI = new Complex<TNumber>(r, i);
			JK = new Complex<TNumber>(j, k);
		}

		public Quaternion(double r)
		{
			RI = r;
		}
		public Quaternion(double r, double i)
		{
			RI = new Complex<TNumber>(r, i);
		}
		public Quaternion(double r, double i, double j)
		{
			RI = new Complex<TNumber>(r, i);
			JK = j;
		}
		public Quaternion(double r, double i, double j, double k)
		{
			RI = new Complex<TNumber>(r, i);
			JK = new Complex<TNumber>(j, k);
		}

		public Quaternion(Complex<TNumber> ri)
		{
			RI = ri;
			JK = new Complex<TNumber>();
		}
		public Quaternion(Complex<TNumber> ri, Complex<TNumber> jk)
		{
			RI = ri;
			JK = jk;
		}


		public static Quaternion<TNumber> ByAxisAndAngle(Vector3D<TNumber> u, double alpha)
		{
			return ByAxisAndAngle(u, Math<TNumber>.Double2Number!(alpha));
		}

		public static Quaternion<TNumber> ByAxisAndAngle(Vector3D<TNumber> u, TNumber alpha)
		{
			TNumber c0_5 = Math<TNumber>.Double2Number!(0.5);
			var sinHalfA = Math<TNumber>.Sin!(alpha * c0_5);

			return new Quaternion<TNumber>(
				Math<TNumber>.Cos!(alpha * c0_5),
				u.X * sinHalfA,
				u.Y * sinHalfA,
				u.Z * sinHalfA
			);
		}
		#endregion

		#region Math Operators
		#region Plus
		public static Quaternion<TNumber> operator +(Quaternion<TNumber> a, Quaternion<TNumber> b)
		{
			return new Quaternion<TNumber>(a.RI + b.RI, a.JK + b.JK);
		}
		public static Quaternion<TNumber> operator +(Quaternion<TNumber> a, Complex<TNumber> b)
		{
			return new Quaternion<TNumber>(a.RI + b, a.JK);
		}
		public static Quaternion<TNumber> operator +(Complex<TNumber> a, Quaternion<TNumber> b)
		{
			return new Quaternion<TNumber>(a + b.RI, b.JK);
		}
		public static Quaternion<TNumber> operator +(Quaternion<TNumber> a, TNumber b)
		{
			return new Quaternion<TNumber>(a.RI + b, a.JK);
		}
		public static Quaternion<TNumber> operator +(TNumber a, Quaternion<TNumber> b)
		{
			return new Quaternion<TNumber>(a + b.RI, b.JK);
		}
		#endregion


		#region Minus
		public static Quaternion<TNumber> operator -(Quaternion<TNumber> a, Quaternion<TNumber> b)
		{
			return new Quaternion<TNumber>(a.RI - b.RI, a.JK - b.JK);
		}
		public static Quaternion<TNumber> operator -(Quaternion<TNumber> a, Complex<TNumber> b)
		{
			return new Quaternion<TNumber>(a.RI - b, a.JK);
		}
		public static Quaternion<TNumber> operator -(Quaternion<TNumber> a, TNumber b)
		{
			return new Quaternion<TNumber>(a.RI - b, a.JK);
		}
		public static Quaternion<TNumber> operator -(Complex<TNumber> a, Quaternion<TNumber> b)
		{
			return new Quaternion<TNumber>(a - b.RI, -b.JK);
		}
		public static Quaternion<TNumber> operator -(TNumber a, Quaternion<TNumber> b)
		{
			return new Quaternion<TNumber>(a - b.RI, -b.JK);
		}
		#endregion


		#region Multiplication
		public static Quaternion<TNumber> operator *(TNumber a, Quaternion<TNumber> b)
		{
			return new Quaternion<TNumber>(a * b.R, a * b.I, a * b.J, a * b.K);
		}
		public static Quaternion<TNumber> operator *(Quaternion<TNumber> b, TNumber a)
		{
			return new Quaternion<TNumber>(a * b.R, a * b.I, a * b.J, a * b.K);
		}

		public static Quaternion<TNumber> operator *(Quaternion<TNumber> a, Quaternion<TNumber> b)
		{
			TNumber r = a.R * b.R - (a.I * b.I + a.J * b.J + a.K * b.K);
			TNumber i = a.R * b.I + a.I * b.R + (a.J * b.K - a.K * b.J);
			TNumber j = a.R * b.J + a.J * b.R + (a.K * b.I - a.I * b.K);
			TNumber k = a.R * b.K + a.K * b.R + (a.I * b.J - a.J * b.I);

			return new Quaternion<TNumber>(r, i, j, k);
		}
		public static Quaternion<TNumber> operator *(Quaternion<TNumber> a, Complex<TNumber> b)
		{
			TNumber r = a.R * b.R - a.I * b.I;
			TNumber i = a.R * b.I + a.I * b.R;
			TNumber j = a.J * b.R + a.K * b.I;
			TNumber k = a.K * b.R - a.J * b.I;

			return new Quaternion<TNumber>(r, i, j, k);
		}
		public static Quaternion<TNumber> operator *(Complex<TNumber> a, Quaternion<TNumber> b)
		{
			TNumber r = a.R * b.R - a.I * b.I;
			TNumber i = a.R * b.I + a.I * b.R;
			TNumber j = a.R * b.J - a.I * b.K;
			TNumber k = a.R * b.K + a.I * b.J;

			return new Quaternion<TNumber>(r, i, j, k);
		}
		#endregion


		#region Div
		public static Quaternion<TNumber> operator /(Quaternion<TNumber> a, Quaternion<TNumber> b)
		{
			TNumber d = TNumber.One / b.AbsQuad;
			Quaternion<TNumber> d_ = b.ConjugateQuaternion;
			return d * a * d_;
		}
		public static Quaternion<TNumber> operator /(Quaternion<TNumber> a, TNumber b)
		{
			return new(a.R / b, a.I / b, a.J / b, a.K / b);
		}
		public static Quaternion<TNumber> operator /(TNumber a, Quaternion<TNumber> b)
		{
			TNumber d = TNumber.One / b.AbsQuad;
			Quaternion<TNumber> d_ = new(b.R, -b.I, -b.J, -b.K);
			return d * a * d_;
		}
		public static Quaternion<TNumber> operator /(Quaternion<TNumber> a, Complex<TNumber> b)
		{
			TNumber d = TNumber.One / b.AbsQuad();
			Complex<TNumber> d_ = new(b.R, -b.I);
			return d * a * d_;
		}
		public static Quaternion<TNumber> operator /(Complex<TNumber> a, Quaternion<TNumber> b)
		{
			TNumber d = TNumber.One / b.AbsQuad;
			Quaternion<TNumber> d_ = b.ConjugateQuaternion;
			return d * a * d_;
		}
		#endregion
		#endregion

		#region Comparisons
		public static bool operator ==(Quaternion<TNumber> a, Quaternion<TNumber> b)
		{
			return a.RI == b.RI && a.JK == b.JK;
		}

		public static bool operator !=(Quaternion<TNumber> a, Quaternion<TNumber> b)
		{
			return a.RI != b.RI || a.JK != b.JK;
		}
		#endregion


		public static Quaternion<TNumber> operator -(Quaternion<TNumber> v)
		{
			return new(-v.R, -v.I, -v.J, -v.K);
		}
		

		public static implicit operator Quaternion<TNumber>(Complex<TNumber> value)
		{
			return new Quaternion<TNumber>(value);
		}


		public static implicit operator Quaternion<TNumber>(TNumber value)
		{
			return new Quaternion<TNumber>(value);
		}


		public override string ToString()
		{
			return $"({R} + i{I} + j{J} + k{K})";
		}

		public bool Equals(Quaternion<TNumber> other)
		{
			return this == other;
		}

		public override bool Equals(object? obj)
		{
			return obj is Quaternion<TNumber> q && Equals(q);
		}

		public override int GetHashCode()
		{
			return RI.GetHashCode() ^ JK.GetHashCode();
		}
	}
}
