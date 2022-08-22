using System;
using System.Collections.Generic;

namespace Rotate4DSearcher.Genetic
{
	public abstract class BinaryOperator : IOperator
	{
		public IOperator A, B;


		public BinaryOperator(IOperator a, IOperator b)
		{
			A = a;
			B = b;
		}

		public static BinaryOperator CreateRandom(Random rnd, IOperator arg)
		{
			IOperator anotherArg;
			if (arg is Constant)
			{
				anotherArg = new Argument(rnd);
			}
			else
			{
				anotherArg = (rnd.Next() < 0.5)
					? (IOperator)new Constant(rnd.NextDouble())
					: new Argument(rnd);
			}
			
			switch (rnd.Next(4))
			{
				case 0:
					return new Sub(arg, anotherArg);
				case 1:
					return new Sub(anotherArg, arg);
				case 2:
					return new Mul(arg, anotherArg);
				default:
					return new Sum(arg, anotherArg);
			}
		}


		public void AddOperatorsToArray(List<IOperator> list)
		{
			A.AddOperatorsToArray(list);
			B.AddOperatorsToArray(list);
			list.Add(this);
		}


		public abstract double Calculate(ArgsBox args);

		public abstract string ToStringFullBracketsString(ArgsBox args);


		public abstract IOperator Clone();


		public abstract IOperator GetMutatedClone(Random rnd);


		public bool Contains(IOperator children)
		{
			return A == children || B == children;
		}


		public int GetAmountOfNodes()
		{
			return 1 + A.GetAmountOfNodes() + B.GetAmountOfNodes();
		}


		public abstract bool IsZero();
	}
}
