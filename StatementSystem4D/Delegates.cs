using System.Collections.Generic;

namespace StatementSystem4D
{
	/// <summary>
	/// This delegate tests Statements before accept rule.
	/// Rule will not be accepted when returned false.
	/// </summary>
	/// <param name="statement"></param>
	/// <param name="container"></param>
	/// <returns></returns>
	public delegate bool WhereDelegate(Statement statement, QueryFunction query);


	/// <summary>
	/// Defines where to get a Vector you need from.
	/// </summary>
	/// <param name="statement"></param>
	/// <returns></returns>
	public delegate Direction4D VectorPickDelegate(Statement statement);


	/// <summary>
	/// Picks angle.
	/// </summary>
	/// <param name="statement"></param>
	/// <returns></returns>
	public delegate Angle AnglePickDelegate(Statement statement);


	/// <summary>
	/// Describes statements to pick
	/// </summary>
	/// <param name="statement"></param>
	/// <returns></returns>
	public delegate bool QueryDelegate(Statement statement);


	/// <summary>
	/// Returns data by query
	/// </summary>
	/// <param name="query"></param>
	/// <returns></returns>
	public delegate List<Statement> QueryFunction(QueryDelegate query);
}
