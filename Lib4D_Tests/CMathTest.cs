using Lib4D;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Lib4D_Tests
{
	[TestClass]
	public class CMathTest
	{
		[TestMethod]
		public void Sqrt()
		{
			Assert.AreEqual(new Complex(), CMath.Sqrt(new Complex()));
			Assert.AreEqual(new Complex(1, 0), CMath.Sqrt(new Complex(1, 0)));
			Assert.AreEqual(new Complex(0, 1), CMath.Sqrt(new Complex(-1, 0)));
		}

		[TestMethod]
		public void Exp()
		{
			Assert.IsTrue(
				_AreApproximatelyEqual(new Complex(1, 0), CMath.Exp(new Complex()))
			);
			Assert.IsTrue(
				_AreApproximatelyEqual(new Complex(Math.E, 0), CMath.Exp(new Complex(1, 0)))
			);

			// e^(i * pi) + 1 = 0   =>   e^(i * pi) = -1
			Assert.IsTrue(
				_AreApproximatelyEqual(new Complex(-1, 0), CMath.Exp(new Complex(0, Math.PI)))
			);
		}


		private bool _AreApproximatelyEqual(Complex a, Complex b)
		{
			return _AreApproximatelyEqual(a.r, b.r) && _AreApproximatelyEqual(a.i, b.i);
		}

		private bool _AreApproximatelyEqual(double a, double b)
		{
			if (a == b)
			{
				return true;
			}

			const double precission = 0.0000000000000002;
			double min = Math.Min(a, b);
			double max = Math.Max(a, b);
			return min <= max && (min + precission) >= max;
		}
	}
}
