using System;
using System.Collections.Generic;

namespace Rotate4DSearcher.Genetic
{
	public class Constant : IOperator
	{
		private double _Value;
		public double Value => _Value;


		public Constant(double value)
		{
			_Value = value;
		}


		public override double Calculate(ArgsBox args)
		{
			return _Value;
		}


		public override IOperator Clone()
		{
			return new Constant(_Value);
		}


		public override string ToStringFullBracketsString(ArgsBox args)
		{
			return _Value.ToString();
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
			_Value += rnd.NextDouble() * 0.2 - 0.1;
			return this;
		}


		public override bool Contains(IOperator children)
		{
			return false;
		}


		public override int GetAmountOfNodes()
		{
			return 1;
		}


		public override bool IsZero()
		{
			return _Value == 0;
		}


		public override bool Equals(object obj)
		{
			if (obj is Constant constant)
			{
				if (constant._Value == _Value)
				{
					return true;
				}
			}
			return false;
		}


		public override IOperator Optimize()
		{
			return this;
		}
	}
}
