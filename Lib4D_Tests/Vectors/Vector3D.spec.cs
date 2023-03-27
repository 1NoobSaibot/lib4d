using Lib4D;

namespace Lib4D_Tests.Vectors
{
	[TestClass]
	public class Vector3DSpec
	{
		private readonly Vector3D<float> x = new(1, 0, 0);
		private readonly Vector3D<float> y = new(0, 1, 0);
		private readonly Vector3D<float> z = new(0, 0, 1);


		[TestMethod]
		public void CrossProductIsRightHand()
		{
			Assert.AreEqual(z, x * y);
		}
	}
}
