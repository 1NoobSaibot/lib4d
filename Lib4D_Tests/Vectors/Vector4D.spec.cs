using Lib4D;

namespace Lib4D_Tests.Vectors
{
	[TestClass]
	public class Vector4DSpec
	{
		[TestMethod]
		public void GettingComponentsByIndexer()
		{
			Vector4D<double> v = new(3, 5, 7, 11);
			Assert.AreEqual(3, v[0]);
			Assert.AreEqual(5, v[1]);
			Assert.AreEqual(7, v[2]);
			Assert.AreEqual(11, v[3]);

			Assert.AreEqual(v.X, v[0]);
			Assert.AreEqual(v.Y, v[1]);
			Assert.AreEqual(v.Z, v[2]);
			Assert.AreEqual(v.Q, v[3]);
		}


		[TestMethod]
		public void SettingcomponentsByIndexer()
		{
			Vector4D<double> v = new();
			v[0] = 1;
			v[1] = 2;
			v[2] = 3;
			v[3] = 4;
			Assert.AreEqual(new Vector4D<double>(1, 2, 3, 4), v);
		}


		[TestMethod]
		public void IndexerThrowsErrorsWhenIndexIsOutOfRange()
		{
			Vector4D<double> v = new();
			Assert.ThrowsException<Exception>(() => v[-1] = 4);
			Assert.ThrowsException<Exception>(() => v[4] = 4);
			Assert.ThrowsException<Exception>(() => { double d = v[-1]; });
			Assert.ThrowsException<Exception>(() => { double d = v[4]; });
		}
	}
}
