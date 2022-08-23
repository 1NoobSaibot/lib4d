using Rotate4DSearcher.Genetic;
using System;
using System.Collections.Generic;

namespace Rotate4DSearcher
{
	internal class ExpressionParser
	{
		public readonly IOperator root;

		private readonly Stack<MathSymbol> operators = new Stack<MathSymbol>();
		private readonly Stack<IOperator> operands = new Stack<IOperator>();
		private readonly ArgsBox args;
		
		public ExpressionParser(string expression, ArgsBox args)
		{
			this.args = args;
			root = this.Parse(expression);
		}


		private IOperator Parse(string expression)
		{
			ExpressionReader reader = new ExpressionReader(expression);

			do
			{
				MathSymbol s = new MathSymbol(reader.GetNextSymbol());

				if (s.IsOperator)
				{
					PushNewOperator(s);
				}
				else
				{
					operands.Push(ReadArgumentOrConstant(s.OriginalString));
				}
			} while (reader.IsRead() == false);

			while (operators.Count > 0)
			{
				PopOperator();
			};

			return operands.Pop();
		}


		private void PushNewOperator(MathSymbol newOperator)
		{
			if (newOperator.OriginalString == "(")
			{
				operators.Push(newOperator);
				return;
			}

			if (newOperator.OriginalString == ")")
			{
				while (operators.Peek().OriginalString != "(")
				{
					PopOperator();
				};
				operators.Pop();
				return;
			}

			while ((operators.Count > 0) && (newOperator.Priority <= operators.Peek().Priority))
			{
				PopOperator();
			}
			operators.Push(newOperator);
		}


		private void PopOperator()
		{
			MathSymbol prevOperator = operators.Pop();
			IOperator b = operands.Pop();
			IOperator a = operands.Pop();
			IOperator res = prevOperator.ToOperator(a, b);
			operands.Push(res);
		}


		private IOperator ReadArgumentOrConstant(string symbol)
		{
			try
			{
				return new Constant(Double.Parse(symbol));
			}
			catch (Exception)
			{ }

			return new Argument(args.GetIndexByName(symbol));
		}


		private class MathSymbol
		{
			public readonly string OriginalString;
			public readonly bool IsOperator;
			public readonly int Priority;


			public MathSymbol(string str)
			{
				OriginalString = str;

				if (str == ")")
				{
					IsOperator = true;
					Priority = 0;
				}
				else if (str == "+" || str == "-")
				{
					IsOperator = true;
					Priority = 1;
				}
				else if (str == "*")
				{
					IsOperator = true;
					Priority = 2;
				}
				else if (str == "(") 
				{
					IsOperator = true;
					Priority = 0;
				}
			}


			internal IOperator ToOperator(IOperator a, IOperator b)
			{
				switch (OriginalString[0])
				{
					case '+': return a + b;
					case '-': return a - b;
					case '*': return a * b;
				}
				throw new Exception("Invalid type of Operator: Original string = " + OriginalString);
			}
		}
	}
}
