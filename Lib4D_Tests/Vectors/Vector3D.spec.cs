using Lib4D;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Lib4D_Tests.Vectors
{
	[TestClass]
	public class Vector3DSpec
	{
		private readonly Vector3DFloat x = new Vector3DFloat(1, 0, 0);
		private readonly Vector3DFloat y = new Vector3DFloat(0, 1, 0);
		private readonly Vector3DFloat z = new Vector3DFloat(0, 0, 1);


		[TestMethod]
		public void CrossProductIsRightHand()
		{
			Assert.AreEqual(z, x * y);
		}
	}
}
