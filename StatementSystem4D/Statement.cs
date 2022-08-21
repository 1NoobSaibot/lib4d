using System;
using System.Collections.Generic;
using System.Text;

namespace StatementSystem4D
{
	public class Statement
	{
		// [XY|Q-Z]90 => ZQ->QZ    It's bad example
		public readonly Argument Argument;
		public readonly Transition Transition;


		public Direction4D A => Argument.A;
		public Direction4D B => Argument.B;
		public Direction4D C => Transition.From;
		public Direction4D D => Transition.To;
		public Angle Alpha => Argument.Alpha;


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


		public bool AreAllVectorsDifferent()
		{
			return A != B
					&& A != C
					&& A != D
					&& B != C
					&& B != D
					&& C != D;
		}

		public bool AreAllVectorsBasic()
		{
			return A.IsBasic && B.IsBasic && C.IsBasic && D.IsBasic;
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
