using Lib4D;

namespace Lib4D_Tests.Helpers
{
	internal static class ComplexTestHelper
	{
		private static readonly IReadOnlyList<double> _values
			= new double [] { -7, -1, 0, 1, 7 };


		public static void AssertApproximatelyEqual(Complex a, Complex b)
		{
			try
			{
				AssertApproximatelyEqual(a.R, b.R);
				AssertApproximatelyEqual(a.I, b.I);
			}
			catch (AssertFailedException)
			{
				throw new AssertFailedException($"Two complex numbers {a} and {b} are not enough equal");
			}
		}


		public static void AssertApproximatelyEqual(double a, double b)
		{
			const double epsilon = 0.0000000000001;
			double delta = Math.Abs(a - b);
			if (delta > epsilon)
			{
				throw new AssertFailedException();
			}
		}


		public static void ForEachComplex(Action<Complex> action)
		{
			for (int r = 0; r < _values.Count; r++)
			{
				for (int i = 0; i < _values.Count; i++)
				{
					action(new Complex(r, i));
				}
			}
		}


		public static void ForEachPairOfComplex(Action<Complex, Complex> action)
		{
			ForEachComplex(a =>
			{
				ForEachComplex(b =>
				{
					action(a, b);
				});
			});
		}

		public static void ForEachFloat(Action<double> action)
		{
			for (int i = 0; i < _values.Count; i++)
			{
				action(i);
			}
		}


		public static void ForEachTwoFloats(Action<double, double> action)
		{
			for (int i = 0; i < _values.Count; i++)
			{
				for (int j = 0; j < _values.Count; j++)
				{
					action(i, j);
				}
			}
		}
	}
}
