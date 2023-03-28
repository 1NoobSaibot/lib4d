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


		public void AssertApproximatelyEqualF(TNumber a, TNumber b)
		{
			AssertApproximatelyEqualF(a, b, EPSILON);
		}


		public void AssertApproximatelyEqualF(TNumber a, TNumber b, double epsilon)
		{
			AssertApproximatelyEqualF(a, b, Math<TNumber>.Double2Number!(epsilon));
		}


		public static void AssertApproximatelyEqualF(TNumber a, TNumber b, TNumber epsilon)
		{
			var delta = Math<TNumber>.Abs!(a - b);
			if (delta > epsilon)
			{
				throw new AssertFailedException(
					$"{typeof(TNumber).Name}: approximate equality falls. Epsilon={epsilon}, delta={delta}"
				);
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
