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


		[TestMethod]
		public void Rotate()
		{
			Vector4D z = new Vector4D(0, 0, 1, 0);
			Vector4D q = new Vector4D(0, 0, 0, 1);
			Vector4D x = new Vector4D(1, 0, 0, 0);
			Vector4D y = new Vector4D(0, 1, 0, 0);

			// Axis ZQ, X => Y
			Bivector4D surface = new Bivector4D(z, q);
			Transform4D t = new Transform4D();
			t.Rotate(surface, Math.PI / 2);
			_AreApproximatelyEqual(y, t * x);

			// Axis YQ, Z => X
			surface = new Bivector4D(y, q);
			t = new Transform4D();
			t.Rotate(surface, Math.PI / 2);
			_AreApproximatelyEqual(x, t * z);

			// Axis YZ, Q => X
			surface = new Bivector4D(y, z);
			t = new Transform4D();
			t.Rotate(surface, Math.PI / 2);
			_AreApproximatelyEqual(x, t * q);

			// Axis XQ, Y => Z
			surface = new Bivector4D(x, q);
			t = new Transform4D();
			t.Rotate(surface, Math.PI / 2);
			_AreApproximatelyEqual(z, t * y);

			// Axis XZ, Q => Y
			surface = new Bivector4D(x, z);
			t = new Transform4D();
			t.Rotate(surface, Math.PI / 2);
			_AreApproximatelyEqual(y, t * q);

			// Axis XY, Z => Q
			surface = new Bivector4D(x, y);
			t = new Transform4D();
			t.Rotate(surface, Math.PI / 2);
			_AreApproximatelyEqual(q, t * z);
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


		private void _AreApproximatelyEqual(Vector4D expected, Vector4D actual)
		{
			try
			{
				_AreApproximatelyEqual(expected.X, actual.X);
				_AreApproximatelyEqual(expected.Y, actual.Y);
				_AreApproximatelyEqual(expected.Z, actual.Z);
				_AreApproximatelyEqual(expected.Q, actual.Q);
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

			const double allowableError = 1E-15;
			Assert.IsTrue(Math.Abs(expected - actual) < allowableError);
		}
	}
}
