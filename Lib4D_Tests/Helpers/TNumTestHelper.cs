using Lib4D.Mathematic;
using System.Numerics;

namespace Lib4D_Tests.Helpers
{
	internal class TNumTestHelper<TNumber>
		: FloatTestHelper<TNumber>
		where TNumber : INumber<TNumber>
	{
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


		public void ForEachTNum(Action<TNumber> action)
		{
			ForEachFloat(f => action(
				Math<TNumber>.Double2Number!(f)
			));
		}


		public void ForEachTwoTNums(Action<TNumber, TNumber> action)
		{
			ForEachTwoFloats((f1, f2) => action(
				Math<TNumber>.Double2Number!(f1),
				Math<TNumber>.Double2Number!(f2)
			));
		}


		public void ForEachThreeTNums(Action<TNumber, TNumber, TNumber> action)
		{
			ForEachThreeFloats((f1, f2, f3) => action(
				Math<TNumber>.Double2Number!(f1),
				Math<TNumber>.Double2Number!(f2),
				Math<TNumber>.Double2Number!(f3)
			));
		}


		public void ForEachFourTNums(Action<TNumber, TNumber, TNumber, TNumber> action)
		{
			ForEachFourFloats((f1, f2, f3, f4) => action(
				Math<TNumber>.Double2Number!(f1),
				Math<TNumber>.Double2Number!(f2),
				Math<TNumber>.Double2Number!(f3),
				Math<TNumber>.Double2Number!(f4)
			));
		}
	}
}
