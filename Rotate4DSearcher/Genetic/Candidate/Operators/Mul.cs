using System;

namespace Rotate4DSearcher.Genetic
{
	public class Mul : BinaryOperator
	{
		public Mul(IOperator a, IOperator b): base(a, b)
		{ }

		public override double Calculate(ArgsBox args)
		{
			double a = A.Calculate(args);
			double b = B.Calculate(args);
			return a * b;
		}


		public override string ToStringFullBracketsString(ArgsBox args)
		{
			return "( " + A.ToStringFullBracketsString(args) + " * " + B.ToStringFullBracketsString(args) + " )";
		}


		public override IOperator Clone()
		{
			return new Mul(A.Clone(), B.Clone());
		}


		public override IOperator GetMutatedClone(Random rnd)
		{
			switch (rnd.Next(6))
			{
				case 0:
					return A;
				case 1:
					return B;
				case 2:
					return new Sum(A, B);
				case 3:
					return new Sub(A, B);
				case 4:
					return new Sub(B, A);
				default:
					return BinaryOperator.CreateRandom(rnd, this);
			}
		}


		public override bool IsZero()
		{
			return A.IsZero() || B.IsZero();
		}


		public override bool Equals(object obj)
		{
			if (obj is Mul mul)
			{
				return (this.IsZero() && mul.IsZero())
					||(mul.A.Equals(A) && mul.B.Equals(B)) || (mul.B.Equals(A) && mul.A.Equals(B));
			}
			return false;
		}
	}
}
