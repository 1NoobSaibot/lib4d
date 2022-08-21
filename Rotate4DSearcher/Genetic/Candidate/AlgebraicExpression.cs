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
			ExpressionReader reader = new ExpressionReader(expression);
			RootOperator = Parse(reader, args);
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
			IOperator parent = FindParent(operators, choosen);
			(parent as BinaryOperator).ReplaceChildren(choosen, clone);
			return new AlgebraicExpression(clonedRoot);
		}


		public override string ToString()
		{
			if (_AsString == null)
			{
				_AsString = RootOperator.ToStringFullBracketsString(ArgsBox.Empty);
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


		private IOperator Parse(ExpressionReader reader, ArgsBox args)
		{
			string symbol;
			IOperator a;
			IOperator b;
			Action action;

			symbol = reader.GetNextSymbol();
			if (symbol == "(")
			{
				a = Parse(reader, args);
			}
			else
			{
				a = ReadArgumentOrConstant(symbol, args);
			}
			
			if (reader.IsRead())
			{
				return a;
			}

			symbol = reader.GetNextSymbol();
			action = symbol == "+"
				? Action.Add
				: (symbol == "-") ? Action.Subtract : Action.Multiply;

			symbol = reader.GetNextSymbol();
			if (symbol == "(")
			{
				b = Parse(reader, args);
			}
			else
			{
				b = ReadArgumentOrConstant(symbol, args);
			}
			if (reader.IsRead() == false)
			{
				reader.GetNextSymbol();  // it should be ")"
			}
			return new BinaryOperator(a, action, b);
		}


		private IOperator ReadArgumentOrConstant(string symbol, ArgsBox args)
		{
			try
			{
				return new Constant(Double.Parse(symbol));
			}
			catch(Exception)
			{}
			
			return new Argument(args.GetIndexByName(symbol));
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
