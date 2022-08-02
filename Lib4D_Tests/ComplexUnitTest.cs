using Lib4D;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Lib4D_Tests
{
	[TestClass]
	public class ComplexUnitTest
	{
		private Random _rnd = new Random();

		[TestMethod]
		public void Equals()
		{
			Complex a = new Complex();
			Complex b = new Complex(0, 0);
			Assert.IsTrue(a == b);

			a = new Complex(-1, 2);
			b = new Complex(-1, 2);
			Assert.IsTrue(a == b);

			a = new Complex(0, 3);
			b = new Complex(2, 3);
			Assert.IsFalse(a == b);

			a = new Complex(2, -3);
			b = new Complex(2, 3);
			Assert.IsFalse(a == b);
		}

		[TestMethod]
		public void Add()
		{
			Complex a = new Complex(0, 0);
			Complex b = new Complex(1, 1);
			Assert.AreEqual(a + b, b);

			a = new Complex(0, 1);
			Assert.AreEqual(a + a, new Complex(0, 2));
			b = new Complex(1, 0);
			Assert.AreEqual(b + b, new Complex(2, 0));
		}

		[TestMethod]
		public void Sub()
		{
			Complex a = new Complex(0, 0);
			Complex b = new Complex(1, 1);
			Assert.AreEqual(b + a, b);

			a = new Complex(3, 0);
			b = new Complex(2, 0);
			Assert.AreEqual(a - b, new Complex(1, 0));

			a = new Complex(0, 3);
			b = new Complex(0, 2);
			Assert.AreEqual(a - b, new Complex(0, 1));
		}

		[TestMethod]
		public void Mul()
		{
			Complex zero = new Complex();
			Complex b = new Complex(2, 5);
			Assert.AreEqual(zero * b, zero);

			Complex one = new Complex(1, 0);
			Assert.AreEqual(one * b, b);

			Complex imaginaryOne = new Complex(0, 1);
			Assert.AreEqual(b * imaginaryOne, new Complex(-5, 2));
		}

		[TestMethod]
		public void Abs()
		{
			Assert.AreEqual(new Complex().Abs(), 0);

			Complex one = new Complex(1, 0);
			Assert.AreEqual(one.Abs(), 1);

			Complex negativeOne = new Complex(1, 0);
			Assert.AreEqual(negativeOne.Abs(), 1);

			Complex imaginaryOne = new Complex(0, 1);
			Assert.AreEqual(imaginaryOne.Abs(), 1);

			Complex negativeImaginaryOne = new Complex(0, 1);
			Assert.AreEqual(negativeImaginaryOne.Abs(), 1);

			Complex c = new Complex(-3, 4);
			Assert.AreEqual(c.Abs(), 5);
		}

		[TestMethod]
		public void Div()
		{
			Complex two = new Complex(2, 0);
			Complex four = new Complex(4, 0);
			Assert.AreEqual(four / two, two);


			Complex a = getComplexWithNotNullAbs();
			Complex b = getComplexWithNotNullAbs();
			Complex c = a * b;
			Assert.AreEqual(c / a, b);
			Assert.AreEqual(c / b, a);
		}
		
		private Complex getComplexWithNotNullAbs()
		{
			Complex a;
			do
			{
				a = new Complex(_rnd.Next(200) - 100, _rnd.Next(200) - 100);
			} while (a.Abs() == 0);
			return a;
		}
	}
}
