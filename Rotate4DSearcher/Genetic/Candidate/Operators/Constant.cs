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


		public override string ToString(ArgsBox args)
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
					return BinaryOperator.CreateRandom(rnd, this);
				default:
					return ChangeValue(rnd);
			}
		}


		private IOperator ChangeValue(Random rnd)
		{
			double amplitude = 2;

			double divByTen_Times = rnd.Next(15);
			double divB = 1;
			for (int i = 0; i < divByTen_Times; i++)
			{
				divB *= 10;
			}

			amplitude /= divB;
			
			_Value += rnd.NextDouble() * amplitude - (amplitude / 2);
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
			return Math.Abs(_Value) == 0.0;
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
	}
}
