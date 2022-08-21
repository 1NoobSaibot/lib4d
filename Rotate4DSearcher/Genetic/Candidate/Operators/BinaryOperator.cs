using System;
using System.Collections.Generic;

namespace Rotate4DSearcher.Genetic
{
	public class BinaryOperator : IOperator
	{
		public IOperator A, B;
		public Action action;
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

		public void ReplaceChildren(IOperator which, IOperator to)
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


		public override string ToStringFullBracketsString(ArgsBox args)
		{
			string op = action == Action.Add
				? " + "
				: (action == Action.Subtract ? " - " : " * ");

			return "( " + A.ToStringFullBracketsString(args) + op + B.ToStringFullBracketsString(args) + " )";
		}


		public override IOperator Clone()
		{
			return new BinaryOperator(A.Clone(), action, B.Clone());
		}


		public override IOperator GetMutatedClone(Random rnd)
		{
			if (rnd.NextDouble() < 0.7)
			{
				if (rnd.NextDouble() < 0.5)
				{
					return A;
				}
				return B;
			}
			
			if (action == Action.Subtract && rnd.NextDouble() < 0.7)
			{
				IOperator temp = A;
				A = B;
				B = temp;
			}

			return ChangeAction(rnd);
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


		public override int GetAmountOfNodes()
		{
			return 1 + A.GetAmountOfNodes() + B.GetAmountOfNodes();
		}


		public override bool IsZero()
		{
			switch (action)
			{
				case Action.Add:
					return A.IsZero() && B.IsZero();
				case Action.Subtract:
					return A.Equals(B);
				case Action.Multiply:
					return A.IsZero() || B.IsZero();
				default:
					throw new Exception("BinaryOperator.IsZero(): This action is not defined " + action);
			}
		}


		public override bool Equals(object obj)
		{
			if (!(obj is BinaryOperator))
			{
				return false;
			}
			BinaryOperator other = (BinaryOperator)obj;

			if (other.action != action)
			{
				return false;
			}
			
			switch (action)
			{
				case Action.Add:
				case Action.Multiply:
					return (A.Equals(other.A) && B.Equals(other.B)) || (A.Equals(other.B) && B.Equals(other.A));
				case Action.Subtract:
					return A.Equals(other.A) && B.Equals(other.B);
				default:
					throw new Exception("BinaryOperator.Equals: Unknown action: " + action);
			}
		}
	}


	public enum Action
	{
		Add = '+',
		Subtract = '-',
		Multiply = '*'
	}
}
