using System.Numerics;

namespace Lib4D_Tests.Helpers
{
	public static class EqualityTestHelper<Type>
		where Type : IEquatable<Type>, IEqualityOperators<Type, Type, bool>
	{
		public static void TestEquality((Type, Type, bool)[] samples)
		{
			foreach (var sample in samples)
			{
				(var a, var b, var shouldBeEqual) = sample;
				Assert.AreEqual(shouldBeEqual, a == b);
				Assert.AreEqual(shouldBeEqual, b == a);
				Assert.AreEqual(!shouldBeEqual, a != b);
				Assert.AreEqual(!shouldBeEqual, b != a);
				Assert.AreEqual(shouldBeEqual, a.Equals(b));
				Assert.AreEqual(shouldBeEqual, b.Equals(a));
				Assert.AreEqual(shouldBeEqual, a.Equals((object)b));
				Assert.AreEqual(shouldBeEqual, b.Equals((object)a));

				if (shouldBeEqual)
				{
					Assert.AreEqual(a, b);
					Assert.AreEqual(b, a);
				}
				else
				{
					Assert.AreNotEqual(a, b);
					Assert.AreNotEqual(b, a);
				}
			}
		}
	}
}
