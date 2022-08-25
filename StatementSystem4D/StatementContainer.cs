using System.Collections.Generic;

namespace StatementSystem4D
{
	public class StatementContainer
	{
		private List<Statement> _acceptedStatements = new List<Statement>();
		private List<Statement> _temporaryAcceptedStatements;
		private List<Conclusion> _conclusions;
		private Rule[] _rules;


		public StatementContainer(Rule[] rules) {
			_rules = rules;
		}

		public List<Conclusion> AddStatement(Statement statement)
		{
			_temporaryAcceptedStatements = new List<Statement>();
			_conclusions = new List<Conclusion>();

			_AddConsequence(statement);

			_acceptedStatements.AddRange(_temporaryAcceptedStatements);
			return _conclusions;
		}

		private bool _AddConsequence(Statement statement) {
			try
			{
				if (_DoesStatementExist(statement))
				{
					return false;
				}
				_temporaryAcceptedStatements.Add(statement);

				for (int i = 0; i < _rules.Length; i++)
				{
					Statement consequence = _rules[i].CreateNewStatement(statement, this);
					bool accepted = _AddConsequence(consequence);
					if (accepted)
					{
						_conclusions.Add(new Conclusion(statement, consequence, i));
					}
				}
				return true;
			}
			catch (StatementContradictionException statementExeption)
			{
				statementExeption.PushStatement(statement);
				throw statementExeption;
			}
		}


		public List<Statement> Query(QueryDelegate test)
		{
			List<Statement> result = new List<Statement>();

			for (int i = 0; i < _acceptedStatements.Count; i++)
			{
				Statement s = _acceptedStatements[i];
				if (test(s))
				{
					result.Add(s);
				}
			}

			for (int i = 0; i < _temporaryAcceptedStatements.Count; i++)
			{
				Statement s = _temporaryAcceptedStatements[i];
				if (test(s)) {
					result.Add(s);
				}
			}

			return result;
		}


		internal Statement[] GetStatementsAsArray()
		{
			return _acceptedStatements.ToArray();
		}


		private bool _DoesStatementExist(Statement s)
		{
			for (int i = 0; i < _temporaryAcceptedStatements.Count; i++)
			{
				if (s == _temporaryAcceptedStatements[i])
				{
					return true;
				}
			}

			for (int i = 0; i < _acceptedStatements.Count; i++)
			{
				if (s == _acceptedStatements[i])
				{
					return true;
				}
			}

			return false;
		}
	}
}
