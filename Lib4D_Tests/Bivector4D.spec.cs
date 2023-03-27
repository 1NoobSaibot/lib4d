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

			Vector4D<double> a1 = new(0, 0, 1, 0);
			Vector4D<double> a2 = new(0, 0, 0, 1);
			Assert.IsTrue(
				new Bivector4DDouble(a1, a2) == -(new Bivector4DDouble(a2, a1))
			);
		}


		[TestMethod]
		public void ZeroBivectorWhenArgumentsAreTheSame()
		{
			// [aa] == -[aa] == 0

			Vector4D<double> a1 = new(1, 2, 3, 4);
			Bivector4DDouble zeroBivector = new(a1, a1);
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

			Vector4D<double> a1 = new(0, 0, 1, 0);
			Vector4D<double> a2 = new(0, 0, 0, 1);
			Assert.IsTrue(
				new Bivector4DDouble(a1 * 3, a2) == (3 * new Bivector4DDouble(a1, a2))
			);
			Assert.IsTrue(
				new Bivector4DDouble(a1, a2 * 3) == (3 * new Bivector4DDouble(a1, a2))
			);

			a1 = new Vector4D<double>(3, 0, -2, 7);
			a2 = new Vector4D<double>(-5, 5, 0, 1);
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

			Vector4D<double> a = new(0, 0, 0, 1);
			Vector4D<double> b = new(0, 0, 1, 0);
			Vector4D<double> c = new(0, 1, 0, 0);

			Assert.IsTrue(
				new Bivector4DDouble(a + b, c) == (new Bivector4DDouble(a, c) + new Bivector4DDouble(b, c))
			);
		}
	}
}
