using System;
using System.Collections.Generic;

namespace Rotate4DSearcher.Genetic
{
	public class AlgebraicExpression
	{
		private static Random rnd = new Random();


		public readonly IOperator RootOperator;
		private string _AsString = null;


		AlgebraicExpression(IOperator expressionTree)
		{
			RootOperator = Optimizer.Optimize(expressionTree);
		}


		public AlgebraicExpression(string expression, ArgsBox args)
		{
			ExpressionParser parser = new ExpressionParser(expression, args);
			RootOperator = parser.root;
			RootOperator = Optimizer.Optimize(RootOperator);
		}


		public AlgebraicExpression Clone()
		{
			return new AlgebraicExpression(this.RootOperator.Clone());
		}


		public AlgebraicExpression GetMutatedClone()
		{
			IOperator clonedRoot = RootOperator.Clone();
			List<IOperator> operators = new List<IOperator>();
			clonedRoot.AddOperatorsToArray(operators);

			int randomOpIndex = rnd.Next(operators.Count);
			IOperator choosen = operators[randomOpIndex];
			IOperator clone = choosen.GetMutatedClone(rnd);
			
			if (choosen == clonedRoot)
			{
				return new AlgebraicExpression(clone);
			}
			BinaryOperator parent = FindParent(operators, choosen) as BinaryOperator;
			if (parent.A == choosen)
			{
				parent.A = clone;
			}
			else
			{
				parent.B = clone;
			}
			return new AlgebraicExpression(clonedRoot);
		}


		public override string ToString()
		{
			if (_AsString == null)
			{
				_AsString = RootOperator.ToString(ArgsBox.Empty);
			}
			return _AsString;
		}


		private IOperator FindParent(List<IOperator> list, IOperator children)
		{
			for (int i = list.Count - 1; i >= 0; i--)
			{
				if (list[i].Contains(children))
				{
					return list[i];
				}
			}

			return null;
		}
	}


	internal class ExpressionReader
	{
		private string[] _symbols;
		private int index = 0;


		public ExpressionReader(string expression)
		{
			_symbols = expression.Split(' ');
		}


		public bool IsRead()
		{
			return index >= _symbols.Length;
		}


		public string GetNextSymbol()
		{
			string symbol = _symbols[index];
			index++;
			return symbol;
		}
	}
}
