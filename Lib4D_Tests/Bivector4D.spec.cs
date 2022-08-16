using Lib4D;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lib4D_Tests
{
	[TestClass]
	public class Bivector4DSpec
	{
		[TestMethod]
		public void ChangesSignWithSwapingArguments()
		{
			// [a1 a2] == -[a2 a1]

			Vector4D a1 = new Vector4D(0, 0, 1, 0);
			Vector4D a2 = new Vector4D(0, 0, 0, 1);
			Assert.IsTrue(
				new Bivector4D(a1, a2) == -(new Bivector4D(a2, a1))
			);
		}


		[TestMethod]
		public void ZeroBivectorWhenArgumentsAreTheSame()
		{
			// [aa] == -[aa] == 0

			Vector4D a1 = new Vector4D(1, 2, 3, 4);
			Bivector4D zeroBivector = new Bivector4D(a1, a1);
			Assert.IsTrue(
				zeroBivector == -zeroBivector
			);
			Assert.IsTrue(
				zeroBivector == 0
			);
		}


		[TestMethod]
		public void WhenOneOfArgumentsIsScaledResultIsScaledToo()
		{
			// [k*a1, a2] == k * [a1, a2]

			Vector4D a1 = new Vector4D(0, 0, 1, 0);
			Vector4D a2 = new Vector4D(0, 0, 0, 1);
			Assert.IsTrue(
				new Bivector4D(a1 * 3, a2) == (3 * new Bivector4D(a1, a2))
			);
			Assert.IsTrue(
				new Bivector4D(a1, a2 * 3) == (3 * new Bivector4D(a1, a2))
			);

			a1 = new Vector4D(3, 0, -2, 7);
			a2 = new Vector4D(-5, 5, 0, 1);
			Assert.IsTrue(
				new Bivector4D(a1 * 3, a2) == (3 * new Bivector4D(a1, a2))
			);
			Assert.IsTrue(
				new Bivector4D(a1, a2 * 3) == (3 * new Bivector4D(a1, a2))
			);
		}


		[TestMethod]
		public void Test_34_8()
		{
			// [a + b, c] == [a, c] + [b, c]

			Vector4D a = new Vector4D(0, 0, 0, 1);
			Vector4D b = new Vector4D(0, 0, 1, 0);
			Vector4D c = new Vector4D(0, 1, 0, 0);

			Assert.IsTrue(
				new Bivector4D(a + b, c) == (new Bivector4D(a, c) + new Bivector4D(b, c))
			);
		}
	}
}
