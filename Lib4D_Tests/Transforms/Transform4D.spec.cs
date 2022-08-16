using Lib4D;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Lib4D_Tests
{
	[TestClass]
	public class Transform4DUnitTest
	{
		private Random _rnd = new Random();


		[TestMethod]
		public void TranslatingVectorWithIdentityTransform()
		{
			// Identity transform doesn't change Vectors
			Transform4D identityTransform = new Transform4D();
			Vector4D v = GetRandomVector();

			Assert.AreEqual(v, identityTransform * v);
		}


		[TestMethod]
		public void Translate()
		{
			Transform4D transform = Transform4D.GetTranslate(5, -4, 3, -7);
			Assert.AreEqual(
				new Vector4D(5, -4, 3, -7),
				transform * new Vector4D()
			);
			Assert.AreEqual(
				new Vector4D(6, -2, -5, 6),
				transform * new Vector4D(1, 2, -8, 13)
			);
		}


		[TestMethod]
		public void Scale()
		{
			Transform4D transform = Transform4D.GetScale(2, 4, -6, 7);
			Assert.AreEqual(
				new Vector4D(2, 4, -6, 7),
				transform * new Vector4D(1, 1, 1, 1)
			);
		}


		private Vector4D GetRandomVector()
		{
			const double amplitude = 1;

			return new Vector4D(
				_rnd.NextDouble() * 2 * amplitude - amplitude,	
				_rnd.NextDouble() * 2 * amplitude - amplitude,	
				_rnd.NextDouble() * 2 * amplitude - amplitude,
				_rnd.NextDouble() * 2 * amplitude - amplitude
			);
		}
	}
}
