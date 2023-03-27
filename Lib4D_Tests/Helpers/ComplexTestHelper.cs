using Lib4D;
using System.Numerics;

namespace Lib4D_Tests.Helpers
{
	internal class ComplexTestHelper<TNumber>
		: FloatTestHelper<TNumber>
		where TNumber : INumber<TNumber>
	{
		private readonly IReadOnlyList<TNumber> _values;


		public ComplexTestHelper()
		{
			_values = GetNums();
		}


		public void AssertApproximatelyEqual(Complex<TNumber> a, Complex<TNumber> b)
		{
			try
			{
				AssertApproximatelyEqual(a.R, b.R);
				AssertApproximatelyEqual(a.I, b.I);
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
			foreach (var r in _values)
			{
				foreach (var i in _values)
				{
					action(new(r, i));
				}
			}
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
