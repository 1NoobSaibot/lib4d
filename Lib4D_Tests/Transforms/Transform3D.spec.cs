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
			Transform3DDouble identityTransform = new Transform3DDouble();
			Vector3DDouble v = GetRandomVector();

			Assert.AreEqual(v, identityTransform * v);
		}


		[TestMethod]
		public void Translate3D()
		{
			Transform3DDouble transform = Transform3DDouble.GetTranslate(5, -4, 3);
			Assert.AreEqual(
				new Vector3DDouble(5, -4, 3),
				transform * new Vector3DDouble(0, 0, 0)
			);
			Assert.AreEqual(
				new Vector3DDouble(6, -2, -5),
				transform * new Vector3DDouble(1, 2, -8)
			);
		}


		[TestMethod]
		public void Scale()
		{
			Transform3DDouble transform = Transform3DDouble.GetScale(2, 4, -6);
			Assert.AreEqual(
				new Vector3DDouble(2, 4, -6),
				transform * new Vector3DDouble(1, 1, 1)
			);
		}


		[TestMethod]
		public void RotateAroundAxis()
		{
			// Around OX
			Transform3DDouble transform = Transform3DDouble.GetRotate(new Vector3DDouble(1, 0, 0), Math.PI / 2);
			_AreApproximatelyEqual(
				new Vector3DDouble(0, 0, 1),
				transform * new Vector3DDouble(0, 1, 0)
			);
			_AreApproximatelyEqual(
				new Vector3DDouble(0, -1, 1),
				transform * new Vector3DDouble(0, 1, 1)
			);

			// Around OY
			transform = Transform3DDouble.GetRotate(new Vector3DDouble(0, 1, 0), Math.PI / 2);
			_AreApproximatelyEqual(
				new Vector3DDouble(1, 0, 0),
				transform * new Vector3DDouble(0, 0, 1)
			);
			_AreApproximatelyEqual(
				new Vector3DDouble(1, 0, -1),
				transform * new Vector3DDouble(1, 0, 1)
			);

			// Around OZ
			transform = Transform3DDouble.GetRotate(new Vector3DDouble(0, 0, 1), Math.PI / 2);
			_AreApproximatelyEqual(
				new Vector3DDouble(0, 1, 0),
				transform * new Vector3DDouble(1, 0, 0)
			);
			_AreApproximatelyEqual(
				new Vector3DDouble(-1, 1, 0),
				transform * new Vector3DDouble(1, 1, 0)
			);

			transform = Transform3DDouble.GetRotate(new Vector3DDouble(1, 1, 1).Normalize(), Math.PI * 2 / 3);
			_AreApproximatelyEqual(
				new Vector3DDouble(3, 1, 2),
				transform * new Vector3DDouble(1, 2, 3)
			);
			_AreApproximatelyEqual(
				new Vector3DDouble(-3, -1, -2),
				transform * new Vector3DDouble(-1, -2, -3)
			);
		}


		[TestMethod]
		public void RotateWithQuaternion()
		{
			for (int i = 0; i < 10; i++)
			{
				Vector3DDouble axis = GetRandomVector().Normalize();
				double angle = _rnd.NextDouble() * Math.PI * 2;
				Quaternion q = Quaternion.ByAxisAndAngle(axis, angle);

				Transform3DDouble transformAxisAngle = Transform3DDouble.GetRotate(axis, angle);
				Transform3DDouble transformQuaternion = Transform3DDouble.GetRotate(q);

				Vector3DDouble point = GetRandomVector();
				_AreApproximatelyEqual(
					transformAxisAngle * point,
					transformQuaternion * point
				);
			}
		}


		private void _AreApproximatelyEqual(Vector3DDouble expected, Vector3DDouble actual)
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

			const double allowableError = 1E-15;
			Assert.IsTrue(Math.Abs(expected - actual) < allowableError);
		}


		private Vector3DDouble GetRandomVector()
		{
			const double amplitude = 1;

			return new Vector3DDouble(
				_rnd.NextDouble() * 2 * amplitude - amplitude,	
				_rnd.NextDouble() * 2 * amplitude - amplitude,	
				_rnd.NextDouble() * 2 * amplitude - amplitude
			);
		}
	}
}
