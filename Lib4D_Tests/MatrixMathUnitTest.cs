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

		[TestMethod]
		public void Mul()
		{
			double[,] identity = new double[1, 1] { { 1 } };
			double[,] three = new double[1, 1] { { 3 } };
			double[,] five = new double[1, 1] { { 5 } };
			double[,] fifteen = new double[1, 1] { { 15 } };

			AssertEqual(three, MatrixMath.Mul(identity, three));
			AssertEqual(fifteen, MatrixMath.Mul(three, five));

			double[,] a = new double[2, 1] { { 1 }, { 1 } };
			double[,] b = new double[1, 2] { { 1 , 1 } };

			AssertEqual(
				new double[1, 1] { { 2 } },
				MatrixMath.Mul(a, b)
			);

			AssertEqual(
				new double[2, 2]
				{
					{ 1, 1 },
					{ 1, 1 }
				},
				MatrixMath.Mul(b, a)
			);
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
