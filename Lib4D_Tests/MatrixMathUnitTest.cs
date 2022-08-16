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
		public void Determinant()
		{
			// Determinant of Matrixes 1*1 are equal its number
			double[,] m1_1 = new double[1, 1] { { 0 } };
			Assert.AreEqual(0, m1_1.GetDeterminant());
			m1_1[0, 0] = 7;
			Assert.AreEqual(7, m1_1.GetDeterminant());

			// Determinant of identity quad matrixes always equals to 1
			for (int i = 2; i <= 10; i++)
			{
				double[,] identity = MatrixMath.CreateIdentity(i);
				Assert.AreEqual(1, identity.GetDeterminant());
			}

			double[,] matrix = new double[2, 2]
			{
				{ 7, -13 },
				{ 3,  -5 },
			};
			Assert.AreEqual(4, matrix.GetDeterminant());

			matrix = new double[3, 3]
			{
				{ 7, -13, 2 },
				{ 3, -5, 0 },
				{ 1, 4, 12 }
			};
			Assert.AreEqual(82, matrix.GetDeterminant());
		}


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
		public void MulWithMatrix()
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


		[TestMethod]
		public void MulWithNumber()
		{
			double[,] identity = new double[1, 1] { { 1 } };
			double[,] three = new double[1, 1] { { 3 } };
			AssertEqual(three, identity.Mul(3));

			identity = MatrixMath.CreateIdentity(3);
			AssertEqual(
				new double[3, 3]
				{
					{ -3,  0,  0 },
					{  0, -3,  0 },
					{  0,  0, -3 }
				},
				identity.Mul(-3)
			);

			double[,] m = new double[2, 2]
			{
				{ 1, 1 },
				{ 1, 1 }
			};
			AssertEqual(
				new double[2, 2]
				{
					{ 0, 0 },
					{ 0, 0 }
				},
				m.Mul(0)
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
