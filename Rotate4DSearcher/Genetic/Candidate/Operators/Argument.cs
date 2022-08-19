﻿using System;
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

		public override double Calculate(ArgsBox args)
		{
			return args.GetValue(index);
		}


		public override string ToString(ArgsBox args)
		{
			return args.GetName(index);
		}


		public override IOperator Clone()
		{
			return new Argument(index);
		}


		public override void AddOperatorsToArray(List<IOperator> list)
		{
			list.Add(this);
		}


		public override IOperator GetMutatedClone(Random rnd)
		{
			switch(rnd.Next(3))
			{
				case 0:
					return new Constant(rnd.NextDouble());
				case 1:
					return new BinaryOperator(rnd, this);

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


		public override bool Contains(IOperator children)
		{
			return false;
		}
	}
}
