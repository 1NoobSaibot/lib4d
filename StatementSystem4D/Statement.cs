using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatementSystem4D
{
	public class Statement
	{
		// [XY|Q-Z]90 => ZQ->QZ    It's bad example
		public readonly Argument Argument;
		public readonly Transition Transition;


		public Statement(Argument argument, Transition transition)
		{
			Argument = argument;
			Transition = transition;
		}


		public static bool operator ==(Statement a, Statement b)
		{
			if (a.Argument != b.Argument)
			{
				return false;
			}
			if (a.Transition == b.Transition)
			{
				return true;
			}

			bool fromsAreEqual = a.Transition.From == b.Transition.From;
			bool tosAreEqual = a.Transition.To == b.Transition.To;
			if (fromsAreEqual ^ tosAreEqual)
			{
				throw new StatementContradictionException(a, b);
			}

			return false;
		}


		public static bool operator !=(Statement a, Statement b)
		{
			return !(a == b);
		}


		public static Statement Parse(string arg)
		{
			arg = arg.Replace("=>", "/");
			string[] argument_transition = arg.Split('/');

			Argument argument = Argument.Parse(argument_transition[0]);
			Transition transition = Transition.Parse(argument_transition[1]);

			return new Statement(argument, transition);
		}


		public override string ToString()
		{
			return Argument.ToString() + " => " + Transition.ToString();
		}
	}

	public class StatementContradictionException : Exception {
		private readonly Statement _newStatement, _oldStatement;
		private List<Statement> _statementChain = new List<Statement>();


		public StatementContradictionException(Statement newStatement, Statement oldStatement):base("Got contradiction!!")
		{
			_newStatement = newStatement;
			_oldStatement = oldStatement;
		}


		public void PushStatement(Statement previousStatement)
		{
			_statementChain.Add(previousStatement);
		}


		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append(_newStatement.ToString());
			builder.Append("  <=/=>  ");
			builder.Append(_oldStatement.ToString());

			for (int i = 0; i < _statementChain.Count; i++)
			{
				builder.Append("\n" + _statementChain[i].ToString());
			}

			return builder.ToString();
		}
	}
}
