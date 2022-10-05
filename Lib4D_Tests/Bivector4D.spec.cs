using Lib4D;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lib4D_Tests
{
	[TestClass]
	public class Bivector4DDoubleSpec
	{
		[TestMethod]
		public void ChangesSignWithSwapingArguments()
		{
			// [a1 a2] == -[a2 a1]

			Vector4DDouble a1 = new Vector4DDouble(0, 0, 1, 0);
			Vector4DDouble a2 = new Vector4DDouble(0, 0, 0, 1);
			Assert.IsTrue(
				new Bivector4DDouble(a1, a2) == -(new Bivector4DDouble(a2, a1))
			);
		}


		[TestMethod]
		public void ZeroBivectorWhenArgumentsAreTheSame()
		{
			// [aa] == -[aa] == 0

			Vector4DDouble a1 = new Vector4DDouble(1, 2, 3, 4);
			Bivector4DDouble zeroBivector = new Bivector4DDouble(a1, a1);
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

			Vector4DDouble a1 = new Vector4DDouble(0, 0, 1, 0);
			Vector4DDouble a2 = new Vector4DDouble(0, 0, 0, 1);
			Assert.IsTrue(
				new Bivector4DDouble(a1 * 3, a2) == (3 * new Bivector4DDouble(a1, a2))
			);
			Assert.IsTrue(
				new Bivector4DDouble(a1, a2 * 3) == (3 * new Bivector4DDouble(a1, a2))
			);

			a1 = new Vector4DDouble(3, 0, -2, 7);
			a2 = new Vector4DDouble(-5, 5, 0, 1);
			Assert.IsTrue(
				new Bivector4DDouble(a1 * 3, a2) == (3 * new Bivector4DDouble(a1, a2))
			);
			Assert.IsTrue(
				new Bivector4DDouble(a1, a2 * 3) == (3 * new Bivector4DDouble(a1, a2))
			);
		}


		[TestMethod]
		public void Test_34_8()
		{
			// [a + b, c] == [a, c] + [b, c]

			Vector4DDouble a = new Vector4DDouble(0, 0, 0, 1);
			Vector4DDouble b = new Vector4DDouble(0, 0, 1, 0);
			Vector4DDouble c = new Vector4DDouble(0, 1, 0, 0);

			Assert.IsTrue(
				new Bivector4DDouble(a + b, c) == (new Bivector4DDouble(a, c) + new Bivector4DDouble(b, c))
			);
		}
	}
}
