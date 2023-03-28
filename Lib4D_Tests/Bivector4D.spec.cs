using Lib4D;
using Lib4D.Mathematic;

namespace Lib4D_Tests
{
	[TestClass]
	public class Bivector4DSpec
	{
		[TestInitialize] public void Init()
		{
			// TODO: The best way to fix this test is to make it generic
			Math<double>.InitInstance(new MathDouble());	
		}


		[TestMethod]
		public void ChangesSignWithSwapingArguments()
		{
			// [a1 a2] == -[a2 a1]

			Vector4D<double> a1 = new(0, 0, 1, 0);
			Vector4D<double> a2 = new(0, 0, 0, 1);
			Assert.IsTrue(
				new Bivector4D<double>(a1, a2) == -(new Bivector4D<double>(a2, a1))
			);
		}


		[TestMethod]
		public void ZeroBivectorWhenArgumentsAreTheSame()
		{
			// [aa] == -[aa] == 0

			Vector4D<double> a1 = new(1, 2, 3, 4);
			Bivector4D<double> zeroBivector = new(a1, a1);
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
				new Bivector4D<double>(a1 * 3, a2) == (3 * new Bivector4D<double>(a1, a2))
			);
			Assert.IsTrue(
				new Bivector4D<double>(a1, a2 * 3) == (3 * new Bivector4D<double>(a1, a2))
			);

			a1 = new Vector4D<double>(3, 0, -2, 7);
			a2 = new Vector4D<double>(-5, 5, 0, 1);
			Assert.IsTrue(
				new Bivector4D<double>(a1 * 3, a2) == (3 * new Bivector4D<double>(a1, a2))
			);
			Assert.IsTrue(
				new Bivector4D<double>(a1, a2 * 3) == (3 * new Bivector4D<double>(a1, a2))
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
				new Bivector4D<double>(a + b, c) == (new Bivector4D<double>(a, c) + new Bivector4D<double>(b, c))
			);
		}
	}
}
