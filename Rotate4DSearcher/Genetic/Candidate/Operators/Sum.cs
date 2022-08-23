using System;

namespace Rotate4DSearcher.Genetic
{
	public class Sum : BinaryOperator
	{
		public override int GetPriority() => 1;
		public Sum(IOperator a, IOperator b):base (a, b)
		{ }


		public override double Calculate(ArgsBox args)
		{
			double a = A.Calculate(args);
			double b = B.Calculate(args);
			return a + b;
		}


		public override string ToString(ArgsBox args)
		{
			string a = WrapOperandsIfItsPriorityLessThanMy(A, args);
			string b = WrapOperandsIfItsPriorityLessThanMy(B, args);
			return a + " + " + b;
		}


		public override IOperator Clone()
		{
			return new Sum(A.Clone(), B.Clone());
		}


		public override IOperator GetMutatedClone(Random rnd)
		{
			switch(rnd.Next(6))
			{
				case 0:
					return A;
				case 1:
					return B;
				case 2:
					return new Sub(A, B);
				case 3:
					return new Sub(B, A);
				case 4:
					return new Mul(A, B);
				default:
					return BinaryOperator.CreateRandom(rnd, this);
			};
		}


		public override bool IsZero()
		{
			return A.IsZero() && B.IsZero();
		}


		public override bool Equals(object obj)
		{
			if (obj is Sum sum)
			{
				return (this.IsZero() && sum.IsZero())
					||(
							(sum.A.Equals(A) && sum.B.Equals(B))
						||(sum.A.Equals(B) && sum.B.Equals(A))
					);
			}
			return false;
		}
	}
}
