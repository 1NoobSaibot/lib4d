using System;
using System.Numerics;

namespace Lib4D
{
	public struct Quaternion:
		IAdditionOperators<Quaternion, Quaternion, Quaternion>,
		ISubtractionOperators<Quaternion, Quaternion, Quaternion>,
		IMultiplyOperators<Quaternion, Quaternion, Quaternion>,
		IDivisionOperators<Quaternion, Quaternion, Quaternion>,
		IUnaryNegationOperators<Quaternion, Quaternion>,
		IEquatable<Quaternion>,
		IEqualityOperators<Quaternion, Quaternion, bool>
	{
		public Complex ri, jk;

		#region Getters
		public double R
		{
			get => ri.R;
			set => ri.R = value;
		}
		public double I
		{
			get => ri.I;
			set => ri.I = value;
		}
		public double J
		{
			get => jk.R;
			set => jk.R = value;
		}
		public double K
		{
			get => jk.I;
			set => jk.I = value;
		}

		public double AbsQuad => R*R + I*I + J*J + K*K;
		public double Abs => Math.Sqrt(AbsQuad);

		public Quaternion ConjugateQuaternion => new(R, -I, -J, -K);
		#endregion

		#region Constructors
		public Quaternion(double r = 0, double i = 0, double j = 0, double k = 0)
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


		public static Quaternion ByAxisAndAngle(Vector3DDouble u, double alpha) {
			double sinHalfA = Math.Sin(alpha * 0.5);

			return new Quaternion(
				Math.Cos(alpha * 0.5),
				u.X * sinHalfA,
				u.Y * sinHalfA,
				u.Z * sinHalfA
			);
		}
		#endregion

		#region Math Operators
		#region Plus
		public static Quaternion operator +(Quaternion a, Quaternion b)
		{
			return new Quaternion(a.ri + b.ri, a.jk + b.jk);
		}
		public static Quaternion operator +(Quaternion a, Complex b)
		{
			return new Quaternion(a.ri + b, a.jk);
		}
		public static Quaternion operator +(Complex a, Quaternion b)
		{
			return new Quaternion(a + b.ri, b.jk);
		}
		public static Quaternion operator +(Quaternion a, double b)
		{
			return new Quaternion(a.ri + b, a.jk);
		}
		public static Quaternion operator +(double a, Quaternion b)
		{
			return new Quaternion(a + b.ri, b.jk);
		}
		#endregion


		#region Minus
		public static Quaternion operator -(Quaternion a, Quaternion b)
		{
			return new Quaternion(a.ri - b.ri, a.jk - b.jk);
		}
		public static Quaternion operator -(Quaternion a, Complex b)
		{
			return new Quaternion(a.ri - b, a.jk);
		}
		public static Quaternion operator -(Quaternion a, double b)
		{
			return new Quaternion(a.ri - b, a.jk);
		}
		public static Quaternion operator -(Complex a, Quaternion b)
		{
			return new Quaternion(a - b.ri, -b.jk);
		}
		public static Quaternion operator -(double a, Quaternion b)
		{
			return new Quaternion(a - b.ri, -b.jk);
		}
		#endregion


		#region Multiplication
		public static Quaternion operator *(double a, Quaternion b)
		{
			return new Quaternion(a * b.R, a * b.I, a * b.J, a * b.K);
		}
		public static Quaternion operator *(Quaternion b, double a)
		{
			return new Quaternion(a * b.R, a * b.I, a * b.J, a * b.K);
		}

		public static Quaternion operator *(Quaternion a, Quaternion b)
		{
			double r = a.R * b.R - (a.I * b.I + a.J * b.J + a.K * b.K);
			double i = a.R * b.I + a.I * b.R + (a.J * b.K - a.K * b.J);
			double j = a.R * b.J + a.J * b.R + (a.K * b.I - a.I * b.K);
			double k = a.R * b.K + a.K * b.R + (a.I * b.J - a.J * b.I);

			return new Quaternion(r, i, j, k);
		}
		public static Quaternion operator *(Quaternion a, Complex b)
		{
			double r = a.R * b.R - a.I * b.I;
			double i = a.R * b.I + a.I * b.R;
			double j = a.J * b.R + a.K * b.I;
			double k = a.K * b.R - a.J * b.I;

			return new Quaternion(r, i, j, k);
		}
		public static Quaternion operator *(Complex a, Quaternion b)
		{
			double r = a.R * b.R - a.I * b.I;
			double i = a.R * b.I + a.I * b.R;
			double j = a.R * b.J - a.I * b.K;
			double k = a.R * b.K + a.I * b.J;

			return new Quaternion(r, i, j, k);
		}
		#endregion


		#region Div
		public static Quaternion operator /(Quaternion a, Quaternion b)
		{
			double d = 1.0 / b.AbsQuad;
			Quaternion d_ = b.ConjugateQuaternion;
			return d * a * d_;
		}
		public static Quaternion operator /(Quaternion a, double b)
		{
			return new(a.R / b, a.I / b, a.J / b, a.K / b);
		}
		public static Quaternion operator /(double a, Quaternion b)
		{
			double d = 1.0 / b.AbsQuad;
			Quaternion d_ = new(b.R, -b.I, -b.J, -b.K);
			return d * a * d_;
		}
		public static Quaternion operator /(Quaternion a, Complex b)
		{
			double d = 1.0 / b.AbsQuad();
			Complex d_ = new(b.R, -b.I);
			return d * a * d_;
		}
		public static Quaternion operator /(Complex a, Quaternion b)
		{
			double d = 1.0 / b.AbsQuad;
			Quaternion d_ = b.ConjugateQuaternion;
			return d * a * d_;
		}
		#endregion
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


		public static Quaternion operator -(Quaternion v)
		{
			return v * -1;
		}
		

		public static implicit operator Quaternion(Complex value)
		{
			return new Quaternion(value);
		}


		public static implicit operator Quaternion(double value)
		{
			return new Quaternion(value);
		}


		public override string ToString()
		{
			return $"({R} + i{I} + j{J} + k{K})";
		}

		public bool Equals(Quaternion other)
		{
			return this == other;
		}
	}
}
