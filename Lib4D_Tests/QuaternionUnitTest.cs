using Lib4D;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Lib4D_Tests
{
	[TestClass]
	public class QuaternionUnitTest
	{
		private Random _rnd = new Random();

		[TestMethod]
		public void Equals()
		{
			Quaternion a = new Quaternion();
			Quaternion b = new Quaternion(0, 0, 0, 0);
			Assert.IsTrue(a == b);

			a = new Quaternion(-1, 2, 3, -4);
			b = new Quaternion(-1, 2, 3, -4);
			Assert.IsTrue(a == b);

			a = new Quaternion(-3, 2, 3, -4);
			b = new Quaternion(-1, 2, 3, -4);
			Assert.IsFalse(a == b);

			a = new Quaternion(-1, 2, 3, -4);
			b = new Quaternion(-1, 3, 3, -4);
			Assert.IsFalse(a == b);

			a = new Quaternion(-1, 2, 3, -4);
			b = new Quaternion(-1, 2, -3, -4);
			Assert.IsFalse(a == b);

			a = new Quaternion(-1, 2, 3, -5);
			b = new Quaternion(-1, 2, 3, -4);
			Assert.IsFalse(a == b);
		}

		[TestMethod]
		public void Add()
		{
			Quaternion a = new Quaternion(0, 0);
			Quaternion b = new Quaternion(1, 1);
			Assert.AreEqual(a + b, b);

			a = new Quaternion(1, 0, 0, 0);
			Assert.AreEqual(a + a, new Quaternion(2, 0, 0, 0));
			a = new Quaternion(0, 1, 0, 0);
			Assert.AreEqual(a + a, new Quaternion(0, 2, 0, 0));
			a = new Quaternion(0, 0, 1, 0);
			Assert.AreEqual(a + a, new Quaternion(0, 0, 2, 0));
			a = new Quaternion(0, 0, 0, 1);
			Assert.AreEqual(a + a, new Quaternion(0, 0, 0, 2));

			a = new Quaternion(-3, 2, -1, 4);
			b = new Quaternion(10, 1, -5, -1);
			Quaternion c = new Quaternion(7, 3, -6, 3);
			Assert.AreEqual(a + b, c);

			for (int i = 0; i < 10; i++)
			{
				a = getQuaternion();
				b = getQuaternion();
				Assert.AreEqual(a + b, b + a);
			}
		}

		[TestMethod]
		public void Sub()
		{
			Quaternion zero = new Quaternion(0, 0, 0, 0);
			Quaternion ones = new Quaternion(1, 1, 1, 1);
			Assert.AreEqual(ones - zero, ones);
			Assert.AreEqual(ones - ones, zero);

			for (int i = 0; i < 10; i++)
			{
				Quaternion a = getQuaternion();
				Quaternion b = getQuaternion();
				Quaternion c = a + b;

				Assert.AreEqual(c - a, b);
				Assert.AreEqual(c - b, a);
			}
		}

		[TestMethod]
		public void Mul()
		{
			Quaternion zero = new Quaternion();
			Quaternion one = new Quaternion(1);
			Quaternion negativeOne = new Quaternion(-1);
			Quaternion random = getQuaternion();
			_AreApproximatelyEqual(zero * random, zero);
			Assert.AreEqual(one * random, random);
			Assert.AreEqual(zero - random, negativeOne * random);


			Quaternion i = new Quaternion(0, 1, 0, 0);
			Quaternion j = new Quaternion(0, 0, 1, 0);
			Quaternion k = new Quaternion(0, 0, 0, 1);
			Assert.AreEqual(i * i, negativeOne);
			Assert.AreEqual(j * j, negativeOne);
			Assert.AreEqual(k * k, negativeOne);
			Assert.AreEqual(i * j * k, negativeOne);

			Assert.AreEqual(k, i * j);
			_AreApproximatelyEqual(negativeOne * k, j * i);
		}

		[TestMethod]
		public void Abs()
		{
			Assert.AreEqual(new Quaternion().Abs, 0);

			Quaternion one = new Quaternion(1);
			Assert.AreEqual(one.Abs, 1);

			Quaternion negativeOne = new Quaternion(1, 0);
			Assert.AreEqual(negativeOne.Abs, 1);

			Quaternion imaginaryOne = new Quaternion(0, 1);
			Assert.AreEqual(imaginaryOne.Abs, 1);

			Quaternion negativeImaginaryOne = new Quaternion(0, 1);
			Assert.AreEqual(negativeImaginaryOne.Abs, 1);

			Quaternion c = new Quaternion(-3, 4);
			Assert.AreEqual(c.Abs, 5);
		}

		[TestMethod]
		public void Div()
		{
			Quaternion two = new Quaternion(2);
			Quaternion four = new Quaternion(4);
			Assert.AreEqual(four / two, two);

			for (int i = 0; i < 1000000; i++)
			{
				Quaternion _a = getQuaternionWithNotNullAbs();
				Quaternion _b = getQuaternionWithNotNullAbs();
				Quaternion _c = _a * _b;
				_AreApproximatelyEqual(_c / _b, _a);
			}
		}

		private Quaternion getQuaternionWithNotNullAbs()
		{
			Quaternion a;
			do
			{
				a = getQuaternion();
			} while (a.Abs == 0);
			return a;
		}
		private Quaternion getQuaternion()
		{
			const int amplitude = 200;
			const double half = amplitude / -2.0;

			return new Quaternion(
				_rnd.Next(amplitude) - half,
				_rnd.Next(amplitude) - half,
				_rnd.Next(amplitude) - half,
				_rnd.Next(amplitude) - half
			);
		}

		

		private void _AreApproximatelyEqual (Quaternion actual, Quaternion expected)
		{
			try
			{
				_AreApproximatelyEqual(actual.R, expected.R);
				_AreApproximatelyEqual(actual.I, expected.I);
				_AreApproximatelyEqual(actual.J, expected.J);
				_AreApproximatelyEqual(actual.K, expected.K);
			}
			catch
			{
				Assert.AreEqual(actual, expected);
			}
		}

		private void _AreApproximatelyEqual(double actual, double expected)
		{
			if (actual == expected)
			{
				return;
			}

			const double relatedError = 1E-15;
			const double k = 1.0 + relatedError;
			double min = Math.Min(actual, expected);
			double max = Math.Max(actual, expected);

			Assert.IsTrue((min <= max) && ((min * k) >= max));
		}
	}
}
