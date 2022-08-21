using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatementSystem4D
{
	public class StatementContainer
	{
		private List<Statement> _acceptedStatements = new List<Statement>();
		private List<Statement> _temporaryAcceptedStatements = new List<Statement>();
		private Rule[] _rules;


		public StatementContainer(Rule[] rules) {
			_rules = rules;
		}

		public List<Statement> AddStatement(Statement statement)
		{
			if (_DoesStatementExist(statement))
			{
				return new List<Statement>();
			}
			_temporaryAcceptedStatements.Add(statement);

			for (int i = 0; i < _rules.Length; i++)
			{
				Statement consequence = _rules[i].CreateNewStatement(statement, this);
				_AddConsequence(consequence);
			}

			_acceptedStatements.AddRange(_temporaryAcceptedStatements);
			List<Statement> addedStatements = _temporaryAcceptedStatements;
			_temporaryAcceptedStatements = new List<Statement>();
			return addedStatements;
		}

		private void _AddConsequence(Statement statement) {
			try
			{
				if (_DoesStatementExist(statement))
				{
					return;
				}
				_temporaryAcceptedStatements.Add(statement);

				for (int i = 0; i < _rules.Length; i++)
				{
					Statement consequence = _rules[i].CreateNewStatement(statement, this);
					_AddConsequence(consequence);
				}
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
