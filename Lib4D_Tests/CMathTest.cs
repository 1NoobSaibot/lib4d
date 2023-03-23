using Lib4D;

namespace Lib4D_Tests
{
	[TestClass]
	public class CMathTest
	{
		[TestMethod]
		public void Exp()
		{
			Assert.IsTrue(
				AreApproximatelyEqual(new Complex(1, 0), CMath.Exp(new Complex()))
			);
			Assert.IsTrue(
				AreApproximatelyEqual(new Complex(Math.E, 0), CMath.Exp(new Complex(1, 0)))
			);

			// e^(i * pi) + 1 = 0   =>   e^(i * pi) = -1
			Assert.IsTrue(
				AreApproximatelyEqual(new Complex(-1, 0), CMath.Exp(new Complex(0, Math.PI)))
			);
		}


		private static bool AreApproximatelyEqual(Complex a, Complex b)
		{
			return AreApproximatelyEqual(a.R, b.R) && AreApproximatelyEqual(a.I, b.I);
		}

		private static bool AreApproximatelyEqual(double a, double b)
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
