using Lib4D;
using System.Numerics;

namespace Lib4D_Tests.Helpers
{
	internal class ComplexTestHelper<TNumber>
		: NumberSet<TNumber>
		where TNumber : INumber<TNumber>
	{
		private readonly IReadOnlyList<TNumber> _values;
		private readonly Func<TNumber, TNumber> _abs;


		public ComplexTestHelper(Func<TNumber, TNumber> abs)
		{
			_values = GetNums();
			_abs = abs;
		}


		public void AssertApproximatelyEqual(Complex<TNumber> a, Complex<TNumber> b)
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


		public void AssertApproximatelyEqual(TNumber a, TNumber b)
		{
			var delta = _abs(a - b);
			if (delta > EPSILON)
			{
				throw new AssertFailedException();
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

		public void ForEachFloat(Action<TNumber> action)
		{
			foreach (var i in _values)
			{
				action(i);
			}
		}


		public void ForEachTwoFloats(Action<TNumber, TNumber> action)
		{
			foreach (var i in _values)
			{
				foreach (var j in _values)
				{
					action(i, j);
				}
			}
		}
	}
}
