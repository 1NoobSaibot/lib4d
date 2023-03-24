using Lib4D;

namespace Lib4D_Tests.Vectors
{
	[TestClass]
	public class Vector3DSpec
	{
		private readonly Vector3DFloat x = new(1, 0, 0);
		private readonly Vector3DFloat y = new(0, 1, 0);
		private readonly Vector3DFloat z = new(0, 0, 1);


		[TestMethod]
		public void CrossProductIsRightHand()
		{
			Assert.AreEqual(z, x * y);
		}
	}
}
