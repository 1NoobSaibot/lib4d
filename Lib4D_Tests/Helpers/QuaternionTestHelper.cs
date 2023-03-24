using Lib4D;

namespace Lib4D_Tests.Helpers
{
	internal static class QuaternionTestHelper
	{
		public static void AssertApproximatelyEqual(Quaternion a, Quaternion b)
		{
			try
			{
				ComplexTestHelper.AssertApproximatelyEqual(a.ri, b.ri);
				ComplexTestHelper.AssertApproximatelyEqual(a.jk, b.jk);
			}
			catch (AssertFailedException)
			{
				throw new AssertFailedException($"Two quaternions {a} and {b} are not enough equal");
			}
		}


		public static void ForEachQuaternion(Action<Quaternion> action)
		{
			ComplexTestHelper.ForEachPairOfComplex((ri, jk) =>
			{
				action(new Quaternion(ri, jk));
			});
		}


		public static void ForEachPairOfQuaternion(Action<Quaternion, Quaternion> action)
		{
			ForEachQuaternion(a =>
			{
				ForEachQuaternion(b =>
				{
					action(a, b);
				});
			});
		}
	}
}
