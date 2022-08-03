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
			Assert.IsTrue(_AreApproximatelyEqual(zero * random, zero));
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
			Assert.IsTrue(_AreApproximatelyEqual(negativeOne * k, j * i));
		}

		private Quaternion getQuaternion()
		{
			return new Quaternion(
				_rnd.Next(200) - 100,
				_rnd.Next(200) - 100,
				_rnd.Next(200) - 100,
				_rnd.Next(200) - 100
			);
		}

		private bool _AreApproximatelyEqual (Quaternion a, Quaternion b)
		{
			return _AreApproximatelyEqual(a.R, b.R)
				&& _AreApproximatelyEqual(a.I, b.I)
				&& _AreApproximatelyEqual(a.J, b.J)
				&& _AreApproximatelyEqual(a.K, b.K);
		}

		private bool _AreApproximatelyEqual(double a, double b)
		{
			if (a == b)
			{
				return true;
			}

			const double p = 0.00000000000000002;
			double min = Math.Min(a, b);
			double max = Math.Max(a, b);

			return (min <= max) && ((min + p) >= max);
		}
	}
}
