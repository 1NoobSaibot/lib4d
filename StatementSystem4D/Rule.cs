using System.Collections.Generic;

namespace StatementSystem4D
{
	public class Rule
	{
		private VectorPickDelegate A = (Statement s) => { return s.Argument.A; };
		private VectorPickDelegate B = (Statement s) => { return s.Argument.B; };

		private AnglePickDelegate Alpha = (Statement s) => { return s.Argument.Alpha; };

		private VectorPickDelegate C = (Statement s) => { return s.Transition.To; };
		private VectorPickDelegate D = (Statement s) => { return s.Transition.To; };

		private readonly List<WhereDelegate> _tests = new List<WhereDelegate>();


		public Statement CreateNewStatement(Statement baseStatement)
		{
			if (_StatementIsValid(baseStatement) == false)
			{
				return baseStatement;
			}

			Direction4D a = this.A(baseStatement);
			Direction4D b = this.B(baseStatement);
			Angle alpha = this.Alpha(baseStatement);

			Direction4D from = this.C(baseStatement);
			Direction4D to = this.D(baseStatement);

			Argument argument = new Argument(a, b, alpha);
			Transition transition = new Transition(from, to);

			return new Statement(argument, transition);
		}

		private bool _StatementIsValid(Statement statement)
		{
			for (int i = 0; i < _tests.Count; i++)
			{
				if (_tests[i](statement) == false)
				{
					return false;
				}
			}

			return true;
		}


		public Rule Where(WhereDelegate tester)
		{
			_tests.Add(tester);
			return this;
		}


		public Rule PickA(VectorPickDelegate picker)
		{
			this.A = picker;
			return this;
		}


		public Rule PickB(VectorPickDelegate picker)
		{
			this.B = picker;
			return this;
		}


		public Rule PickC(VectorPickDelegate picker)
		{
			this.C = picker;
			return this;
		}


		public Rule PickD(VectorPickDelegate picker)
		{
			this.D = picker;
			return this;
		}


		public Rule PickAlpha(AnglePickDelegate picker)
		{
			this.Alpha = picker;
			return this;
		}
	}
}
