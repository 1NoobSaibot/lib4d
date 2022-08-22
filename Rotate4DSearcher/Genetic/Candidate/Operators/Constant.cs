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


		public double Calculate(ArgsBox args)
		{
			return _Value;
		}


		public IOperator Clone()
		{
			return new Constant(_Value);
		}


		public string ToStringFullBracketsString(ArgsBox args)
		{
			return _Value.ToString();
		}


		public void AddOperatorsToArray(List<IOperator> list)
		{
			list.Add(this);
		}


		public IOperator GetMutatedClone(Random rnd)
		{
			switch (rnd.Next(4))
			{
				case 0:
					return new Argument(rnd);
				case 1:
					return BinaryOperator.CreateRandom(rnd, this);
				default:
					return ChangeValue(rnd);
			}
		}


		private IOperator ChangeValue(Random rnd)
		{
			_Value += rnd.NextDouble() * 0.2 - 0.1;
			return this;
		}


		public bool Contains(IOperator children)
		{
			return false;
		}


		public int GetAmountOfNodes()
		{
			return 1;
		}


		public bool IsZero()
		{
			return Math.Abs(_Value) < 0.01;
		}


		public bool Equals(object obj)
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
	}
}
