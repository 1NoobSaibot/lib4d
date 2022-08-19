using System;
using System.Collections.Generic;

namespace Rotate4DSearcher.Genetic.Candidate.Operators
{
	public class BinaryOperator : IOperator
	{
		private IOperator A, B;
		private Action action;
		private static readonly Action[] intToAction = new Action[] {
			Action.Add,
			Action.Subtract,
			Action.Multiply
		};


		public BinaryOperator(IOperator a, Action act, IOperator b)
		{
			A = a;
			B = b;
			action = act;
		}

		public BinaryOperator(Random rnd, IOperator arg)
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
			
			action = intToAction[rnd.Next(intToAction.Length)];
			if (rnd.NextDouble() < 0.5)
			{
				A = arg;
				B = anotherArg;
			}
			else
			{
				A = anotherArg;
				B = arg;
			}
		}

		public void ReplaceChild(IOperator which, IOperator to)
		{
			if (which == A)
			{
				A = to;
			}
			else if (which == B)
			{
				B = to;
			}
		}


		internal void ReplaceChildren(IOperator which, IOperator newChild)
		{
			if (A == which)
			{
				A = newChild;
				return;
			}
			if (B == which)
			{
				B = newChild;
			}
		}


		public override void AddOperatorsToArray(List<IOperator> list)
		{
			A.AddOperatorsToArray(list);
			B.AddOperatorsToArray(list);
			list.Add(this);
		}


		public override double Calculate(ArgsBox args)
		{
			double a = A.Calculate(args);
			double b = B.Calculate(args);
			switch (action)
			{
				case Action.Add:
					return a + b;
				case Action.Subtract:
					return a - b;
				case Action.Multiply:
					return a * b;
			}

			throw new Exception("Action is not valid");
		}


		public override string ToString(ArgsBox args)
		{
			string op = action == Action.Add
				? " + "
				: (action == Action.Subtract ? " - " : " * ");

			return "( " + A.ToString() + op + B.ToString() + " )";
		}


		public override IOperator Clone()
		{
			return new BinaryOperator(A.Clone(), action, B.Clone());
		}


		public override IOperator GetMutatedClone(Random rnd)
		{
			if (rnd.NextDouble() < 0.7)
			{
				return ChangeAction(rnd);
			}

			if (action == Action.Subtract && rnd.NextDouble() < 0.7)
			{
				IOperator temp = A;
				A = B;
				B = temp;
			}

			if (rnd.NextDouble() < 0.5)
			{
				return A;
			}
			return B;
		}


		private IOperator ChangeAction(Random rnd)
		{
			Action newAction;
			do
			{
				newAction = intToAction[rnd.Next(intToAction.Length)];
			} while (newAction != action);
			action = newAction;
			return this;
		}


		public override bool Contains(IOperator children)
		{
			return A == children || B == children;
		}


		public enum Action
		{
			Add = '+',
			Subtract = '-',
			Multiply = '*'
		}
	}
}
