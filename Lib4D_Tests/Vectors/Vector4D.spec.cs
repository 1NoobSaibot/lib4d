using Lib4D;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Lib4D_Tests.Vectors
{
	[TestClass]
	public class Vector4DSpec
	{
		[TestMethod]
		public void GettingComponentsByIndexer()
		{
			Vector4D vector = new Vector4D(3, 5, 7, 11);
			Assert.AreEqual(3, vector[0]);
			Assert.AreEqual(5, vector[1]);
			Assert.AreEqual(7, vector[2]);
			Assert.AreEqual(11, vector[3]);
		}


		[TestMethod]
		public void SettingcomponentsByIndexer()
		{
			Vector4D v = new Vector4D();
			v[0] = 1;
			v[1] = 2;
			v[2] = 3;
			v[3] = 4;
			Assert.AreEqual(new Vector4D(1, 2, 3, 4), v);
		}


		[TestMethod]
		public void IndexerThrowsErrorsWhenIndexIsOutOfRange()
		{
			Vector4D v = new Vector4D();
			Assert.ThrowsException<Exception>(() => v[-1] = 4);
			Assert.ThrowsException<Exception>(() => v[4] = 4);
			Assert.ThrowsException<Exception>(() => { double d = v[-1]; });
			Assert.ThrowsException<Exception>(() => { double d = v[4]; });
		}
	}
}
