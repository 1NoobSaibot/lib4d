using Lib4D;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Lib4D_Tests
{
	[TestClass]
	public class Transform2DUnitTest
	{
		[TestMethod]
		public void Calculate()
		{
			Transform2D identityTransform = new Transform2D();
			Vector2D v = new Vector2D(7, 4);

			Assert.AreEqual(v, identityTransform * v);

			Transform2D scaleTransform = new Transform2D();
			scaleTransform.Scale(2, 3);

			Assert.AreEqual(
				new Vector2D(14, 12),
				scaleTransform * v
			);
		}


		[TestMethod]
		public void Translate()
		{
			Transform2D transform = Transform2D.GetTranslate(5, -4);
			Assert.AreEqual(
				new Vector2D(5, -4),
				transform * new Vector2D(0, 0)
			);
			Assert.AreEqual(
				new Vector2D(6, -2),
				transform * new Vector2D(1, 2)
			);
		}


		[TestMethod]
		public void Scale()
		{
			Transform2D transform = Transform2D.GetScale(2, 4);
			Assert.AreEqual(
				new Vector2D(2, 4),
				transform * new Vector2D(1, 1)
			);
		}


		[TestMethod]
		public void Rotate()
		{
			Transform2D transform = Transform2D.GetRotate(Math.PI / 2);
			_AreApproximatelyEqual(
				new Vector2D(0, 1),
				transform * new Vector2D(1, 0)
			);
			_AreApproximatelyEqual(
				new Vector2D(-1, 1),
				transform * new Vector2D(1, 1)
			);
		}


		[TestMethod]
		public void RotateAndTranslate()
		{
			Transform2D t = Transform2D.GetRotate(Math.PI / 2);
			t.Translate(2, 3);

			_AreApproximatelyEqual(
				new Vector2D(-3, 2),
				t * new Vector2D(0, 0)
			);
			_AreApproximatelyEqual(
				new Vector2D(-4, 3),
				t * new Vector2D(1, 1)
			);
		}


		private void _AreApproximatelyEqual(Vector2D expected, Vector2D actual)
		{
			try
			{
				_AreApproximatelyEqual(expected.X, actual.X);
				_AreApproximatelyEqual(expected.Y, actual.Y);
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
	}
}
