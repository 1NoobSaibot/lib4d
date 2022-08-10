using Lib4D;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Lib4D_Tests
{
	[TestClass]
	public class MatrixMathUnitTest
	{
		private Random _rnd = new Random();

		[TestMethod]
		public void Transpose()
		{
			double[,] m = new double[3, 3]
			{
				{  0,  2, 73 },
				{ -1,  0,  2 },
				{ -2, -1,  1 }
			};

			double[,] m2 = new double[3, 3]
			{
				{  0, -1, -2 },
				{  2,  0, -1 },
				{ 73,  2,  1 }
			};

			AssertEqual(m2, m.Transpose());
		}

		private void AssertEqual(double[,] a, double[,] b)
		{
			try
			{
				Assert.IsTrue(a.EqualsTo(b));
			}
			catch
			{
				Assert.AreEqual(a, b);
			}
		}
	}
}
