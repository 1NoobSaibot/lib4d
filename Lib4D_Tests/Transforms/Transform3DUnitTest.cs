using Lib4D;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Lib4D_Tests
{
	[TestClass]
	public class Transform3DUnitTest
	{
		private Random _rnd = new Random();


		[TestMethod]
		public void TranslatingVectorWithIdentityTransform()
		{
			// Identity transform doesn't change Vectors
			Transform3D identityTransform = new Transform3D();
			Vector3D v = GetRandomVector();

			Assert.AreEqual(v, identityTransform * v);
		}


		[TestMethod]
		public void Translate3D()
		{
			Transform3D transform = Transform3D.GetTranslate(5, -4, 3);
			Assert.AreEqual(
				new Vector3D(5, -4, 3),
				transform * new Vector3D(0, 0, 0)
			);
			Assert.AreEqual(
				new Vector3D(6, -2, -5),
				transform * new Vector3D(1, 2, -8)
			);
		}


		[TestMethod]
		public void Scale()
		{
			Transform3D transform = Transform3D.GetScale(2, 4, -6);
			Assert.AreEqual(
				new Vector3D(2, 4, -6),
				transform * new Vector3D(1, 1, 1)
			);
		}


		private void _AreApproximatelyEqual(Vector3D expected, Vector3D actual)
		{
			try
			{
				_AreApproximatelyEqual(expected.X, actual.X);
				_AreApproximatelyEqual(expected.Y, actual.Y);
				_AreApproximatelyEqual(expected.Z, actual.Z);
			}
			catch
			{
				Assert.AreEqual(expected, actual);
			}
		}


		private void _AreApproximatelyEqual(double expected, double actual)
		{
			if (actual == expected)
			{
				return;
			}

			if (Math.Abs(expected - actual) < 1E-16)
			{
				return;
			}

			Assert.IsTrue(Math.Abs(expected - actual) < 1E-15);
		}


		private Vector3D GetRandomVector()
		{
			const double amplitude = 100;

			return new Vector3D(
				_rnd.NextDouble() * 2 * amplitude - amplitude,	
				_rnd.NextDouble() * 2 * amplitude - amplitude,	
				_rnd.NextDouble() * 2 * amplitude - amplitude
			);
		}
	}
}
