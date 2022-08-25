namespace StatementSystem4D
{
	public class Conclusion
	{
		public readonly Statement oldStatement, newStatement;
		public readonly int ruleIndex;

		public Conclusion(Statement oldStatement, Statement newStatement, int ruleIndex)
		{
			this.oldStatement = oldStatement;
			this.newStatement = newStatement;
			this.ruleIndex = ruleIndex;
		}

		public override string ToString()
		{
			return oldStatement.ToString() + "   ===>   " + newStatement.ToString() + "   Rule: " + ruleIndex;
		}
	}
}
