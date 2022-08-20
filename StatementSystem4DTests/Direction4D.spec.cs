using Microsoft.VisualStudio.TestTools.UnitTesting;
using StatementSystem4D;
using System;

namespace StatementSystem4DTests
{
	[TestClass]
	public class direction4DSpec
	{
		[TestMethod]
		public void Parse()
		{
			(string, Direction4D)[] pairs = new (string, Direction4D)[3]
			{
				("X", new Direction4D(1, 0, 0, 0)),
				("Xyzq", new Direction4D(1, 1, 1, 1)),
				("-Q-X-y-Z", new Direction4D(-1, -1, -1, -1))
			};

			for (int i = 0; i < pairs.Length; i++)
			{
				(string arg, Direction4D expected) = pairs[i];
				Direction4D actual = Direction4D.Parse(arg);
				Assert.AreEqual(expected, actual);
			}
			
		}
	}
}
