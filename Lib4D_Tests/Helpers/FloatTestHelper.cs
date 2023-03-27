using Lib4D.Mathematic;
using System.Numerics;

namespace Lib4D_Tests.Helpers
{
	internal class FloatTestHelper<TNumber>
		: NumberSet<TNumber>
		where TNumber : INumber<TNumber>
	{
		private readonly IReadOnlyList<TNumber> _values;


		public FloatTestHelper()
		{
			_values = GetNums();
		}


		public void AssertApproximatelyEqual(TNumber a, TNumber b)
		{
			var delta = Math<TNumber>.Abs!(a - b);
			if (delta > EPSILON)
			{
				throw new AssertFailedException();
			}
		}


		public void ForEachFloat(Action<TNumber> action)
		{
			foreach (var f in _values)
			{
				action(f);
			}
		}


		public void ForEachTwoFloats(Action<TNumber, TNumber> action)
		{
			foreach (var f1 in _values)
			{
				foreach (var f2 in _values)
				{
					action(f1, f2);
				}
			}
		}


		public void ForEachThreeFloats(Action<TNumber, TNumber, TNumber> action)
		{
			foreach (var f1 in _values)
			{
				foreach (var f2 in _values)
				{
					foreach (var f3 in _values)
					{
						action(f1, f2, f3);
					}
				}
			}
		}


		public void ForEachFourFloats(Action<TNumber, TNumber, TNumber, TNumber> action)
		{
			foreach (var f1 in _values)
			{
				foreach (var f2 in _values)
				{
					foreach (var f3 in _values)
					{
						foreach (var f4 in _values)
						{
							action(f1, f2, f3, f4);
						}
					}
				}
			}
		}
	}
}
