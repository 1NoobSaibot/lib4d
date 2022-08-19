using System;
using System.Collections.Generic;

namespace Rotate4DSearcher.Genetic
{
	public class Constant : IOperator
	{
		private double Value;


		public Constant(double value)
		{
			Value = value;
		}


		public override double Calculate(ArgsBox args)
		{
			return Value;
		}


		public override IOperator Clone()
		{
			return new Constant(Value);
		}


		public override string ToString(ArgsBox args)
		{
			return Value.ToString();
		}


		public override void AddOperatorsToArray(List<IOperator> list)
		{
			list.Add(this);
		}


		public override IOperator GetMutatedClone(Random rnd)
		{
			switch (rnd.Next(4))
			{
				case 0:
					return new Argument(rnd);
				case 1:
					return new BinaryOperator(rnd, this);

				default:
					return ChangeValue(rnd);
			}
		}


		private IOperator ChangeValue(Random rnd)
		{
			Value += rnd.NextDouble() * 0.2 - 0.1;
			return this;
		}

		public override bool Contains(IOperator children)
		{
			return false;
		}
	}
}
