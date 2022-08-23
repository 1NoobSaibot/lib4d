using System;

namespace Rotate4DSearcher.Genetic
{
	public class Sub : BinaryOperator
	{
		public override int GetPriority() => 1;
		public Sub(IOperator a, IOperator b): base(a, b)
		{ }


		public override double Calculate(ArgsBox args)
		{
			double a = A.Calculate(args);
			double b = B.Calculate(args);
			return a - b;
		}


		public override string ToString(ArgsBox args)
		{
			string a = WrapOperandsIfItsPriorityLessThanMy(A, args);
			string b = WrapOperandsIfItsPriorityLessOrEqualsToMy(B, args);
			return a + " - " + b;
		}


		public override IOperator Clone()
		{
			return new Sub(A.Clone(), B.Clone());
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
					return new Mul(A, B);
				case 3:
					return new Sum(A, B);
				case 4:
					return new Sub(B, A);
				default:
					return BinaryOperator.CreateRandom(rnd, this);
			};
		}


		public override bool IsZero()
		{
			return A.Equals(B);
		}


		public override bool Equals(object obj)
		{
			if (obj is Sub sub)
			{
				return (this.IsZero() && sub.IsZero())
					|| (A.Equals(sub.A) && B.Equals(sub.B));
			}
			return false;
		}
	}
}
