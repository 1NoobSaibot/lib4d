using System;
using System.Collections.Generic;

namespace Rotate4DSearcher.Genetic
{
	public class Argument : IOperator
	{
		private int index;


		public Argument (int index)
		{
			this.index = index;
		}

		public Argument(Random rnd)
		{
			this.index = rnd.Next(9);
		}

		public double Calculate(ArgsBox args)
		{
			return args.GetValue(index);
		}


		public string ToStringFullBracketsString(ArgsBox args)
		{
			return args.GetName(index);
		}


		public IOperator Clone()
		{
			return new Argument(index);
		}


		public void AddOperatorsToArray(List<IOperator> list)
		{
			list.Add(this);
		}


		public IOperator GetMutatedClone(Random rnd)
		{
			switch(rnd.Next(3))
			{
				case 0:
					return new Constant(rnd.NextDouble());
				case 1:
					return BinaryOperator.CreateRandom(rnd, this);
				default:
					return ChangeArgument(rnd);
			}
		}


		private IOperator ChangeArgument(Random rnd)
		{
			int newIndex;
			do
			{
				newIndex = rnd.Next(9);
			} while (index == newIndex);
			index = newIndex;
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
			return false;
		}


		public bool Equals(object b)
		{
			if (b is Argument agrument)
			{
				if (agrument.index == index)
				{
					return true;
				}
			}
			return false;
		}
	}
}
