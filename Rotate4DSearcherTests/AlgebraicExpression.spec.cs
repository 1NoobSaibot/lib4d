using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rotate4DSearcher.Genetic;
using System;

namespace Rotate4DSearcherTests
{
	[TestClass]
	public class AlgebraicExpressionSpec
	{
		[TestMethod]
		public void Parse()
		{
			AlgebraicExpression expression = new AlgebraicExpression("2 + 2", ArgsBox.Empty);
			Assert.AreEqual(4, expression.RootOperator.Calculate(ArgsBox.Empty));
		}
	}
}
