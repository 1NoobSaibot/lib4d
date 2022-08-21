namespace Rotate4DSearcher.Genetic
{
	public class Rule
	{
		private WhereDelegate _tester;
		private ReplaceDelegate _rebuilder;


		public IOperator Optimize(IOperator baseExpression)
		{
			if (_tester(baseExpression) == false)
			{
				return baseExpression;
			}

			return _rebuilder(baseExpression);
		}


		public Rule Where(WhereDelegate tester)
		{
			this._tester = tester;
			return this;
		}


		public Rule Replace(ReplaceDelegate rebuilder)
		{
			this._rebuilder = rebuilder;
			return this;
		}
	}
}
