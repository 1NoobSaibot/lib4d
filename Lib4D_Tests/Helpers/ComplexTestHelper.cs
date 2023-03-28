using Lib4D;
using System.Numerics;

namespace Lib4D_Tests.Helpers
{
	internal class ComplexTestHelper<TNumber>
		: TNumTestHelper<TNumber>
		where TNumber : INumber<TNumber>
	{
		public void AssertApproximatelyEqualC(Complex<TNumber> a, Complex<TNumber> b)
		{
			try
			{
				AssertApproximatelyEqualF(a.R, b.R);
				AssertApproximatelyEqualF(a.I, b.I);
			}
			catch (AssertFailedException ex)
			{
				throw new AssertFailedException(
					$"{typeof(TNumber).Name}: Two complex numbers {a} and {b} are not enough equal",
					ex
				);
			}
		}


		public void ForEachComplex(Action<Complex<TNumber>> action)
		{
			ForEachTwoTNums((r, i) => action(new(r, i)));
		}


		public void ForEachPairOfComplex(Action<Complex<TNumber>, Complex<TNumber>> action)
		{
			ForEachComplex(a =>
			{
				ForEachComplex(b =>
				{
					action(a, b);
				});
			});
		}
	}
}
