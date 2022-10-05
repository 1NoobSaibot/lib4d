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
			Transform4DDouble identityTransform = new Transform4DDouble();
			Vector4DDouble v = GetRandomVector();

			Assert.AreEqual(v, identityTransform * v);
		}


		[TestMethod]
		public void Translate()
		{
			Transform4DDouble transform = Transform4DDouble.GetTranslate(5, -4, 3, -7);
			Assert.AreEqual(
				new Vector4DDouble(5, -4, 3, -7),
				transform * new Vector4DDouble()
			);
			Assert.AreEqual(
				new Vector4DDouble(6, -2, -5, 6),
				transform * new Vector4DDouble(1, 2, -8, 13)
			);
		}


		[TestMethod]
		public void Scale()
		{
			Transform4DDouble transform = Transform4DDouble.GetScale(2, 4, -6, 7);
			Assert.AreEqual(
				new Vector4DDouble(2, 4, -6, 7),
				transform * new Vector4DDouble(1, 1, 1, 1)
			);
		}


		[TestMethod]
		public void Rotate()
		{
			Vector4DDouble z = new Vector4DDouble(0, 0, 1, 0);
			Vector4DDouble q = new Vector4DDouble(0, 0, 0, 1);
			Vector4DDouble x = new Vector4DDouble(1, 0, 0, 0);
			Vector4DDouble y = new Vector4DDouble(0, 1, 0, 0);

			// Axis ZQ, X => Y
			Bivector4DDouble surface = new Bivector4DDouble(z, q);
			Transform4DDouble t = new Transform4DDouble();
			t.Rotate(surface, Math.PI / 2);
			_AreApproximatelyEqual(y, t * x);

			// Axis YQ, Z => X
			surface = new Bivector4DDouble(y, q);
			t = new Transform4DDouble();
			t.Rotate(surface, Math.PI / 2);
			_AreApproximatelyEqual(x, t * z);

			// Axis YZ, Q => X
			surface = new Bivector4DDouble(y, z);
			t = new Transform4DDouble();
			t.Rotate(surface, Math.PI / 2);
			_AreApproximatelyEqual(x, t * q);

			// Axis XQ, Y => Z
			surface = new Bivector4DDouble(x, q);
			t = new Transform4DDouble();
			t.Rotate(surface, Math.PI / 2);
			_AreApproximatelyEqual(z, t * y);

			// Axis XZ, Q => Y
			surface = new Bivector4DDouble(x, z);
			t = new Transform4DDouble();
			t.Rotate(surface, Math.PI / 2);
			_AreApproximatelyEqual(y, t * q);

			// Axis XY, Z => Q
			surface = new Bivector4DDouble(x, y);
			t = new Transform4DDouble();
			t.Rotate(surface, Math.PI / 2);
			_AreApproximatelyEqual(q, t * z);


			// Axis ZQ 180, X => -X
			t = new Transform4DDouble();
			surface = new Bivector4DDouble(z, q);
			t.Rotate(surface, Math.PI);
			_AreApproximatelyEqual(x * -1, t * x);
			_AreApproximatelyEqual(y * -1, t * y);

			t = new Transform4DDouble();
			surface = new Bivector4DDouble(new Vector4DDouble(1, 1, 1).Normalize(), q);
			t.Rotate(surface, Math.PI / 3 * 2);
			_AreApproximatelyEqual(y, t * x);
			_AreApproximatelyEqual(z, t * y);
			_AreApproximatelyEqual(x, t * z);
		}


		private Vector4DDouble GetRandomVector()
		{
			const double amplitude = 1;

			return new Vector4DDouble(
				_rnd.NextDouble() * 2 * amplitude - amplitude,	
				_rnd.NextDouble() * 2 * amplitude - amplitude,	
				_rnd.NextDouble() * 2 * amplitude - amplitude,
				_rnd.NextDouble() * 2 * amplitude - amplitude
			);
		}


		private void _AreApproximatelyEqual(Vector4DDouble expected, Vector4DDouble actual)
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
