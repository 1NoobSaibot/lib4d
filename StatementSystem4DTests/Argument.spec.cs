using Microsoft.VisualStudio.TestTools.UnitTesting;
using StatementSystem4D;
using System;

namespace StatementSystem4DTests
{
	[TestClass]
	public class ArgumentSpec
	{
		[TestMethod]
		public void Parse()
		{
			(string, Argument)[] pairs = new (string, Argument)[3]
			{
				("[X|Y]90", new Argument(
					new Direction4D(1, 0, 0, 0),
					new Direction4D(0, 1, 0, 0),
					Angle.A90
				)),
				("[Xy|zq]120 ", new Argument(
					new Direction4D(1, 1, 0, 0),
					new Direction4D(0, 0, 1, 1),
					Angle.A120
				)),
				("[-Q-X|-y-Z]180", new Argument(
					new Direction4D(-1, 0, 0, -1),
					new Direction4D(0, -1, -1, 0),
					Angle.A180
				))
			};

			for (int i = 0; i < pairs.Length; i++)
			{
				(string arg, Argument expected) = pairs[i];
				Argument actual = Argument.Parse(arg);
				Assert.AreEqual(expected, actual);
			}
			
		}
	}
}
