using Lib4D;
using Lib4D_Tests.Helpers;

namespace Lib4D_Tests
{
	[TestClass]
	public class Transform2DUnitTest
	{
		[TestMethod]
		public void Calculate()
		{
			var identityTransform = new Transform2D<float>();
			Vector2D<float> v = new Vector2D<float>(7, 4);

			Assert.AreEqual(v, identityTransform * v);

			Transform2D<float> scaleTransform = new Transform2D<float>();
			scaleTransform.Scale(2, 3);

			Assert.AreEqual(
				new Vector2D<float>(14, 12),
				scaleTransform * v
			);
		}


		[TestMethod]
		public void Translate()
		{
			Transform2D<float> transform = Transform2D<float>.GetTranslate(5, -4);
			Assert.AreEqual(
				new Vector2D<float>(5, -4),
				transform * new Vector2D<float>(0, 0)
			);
			Assert.AreEqual(
				new Vector2D<float>(6, -2),
				transform * new Vector2D<float>(1, 2)
			);
		}


		[TestMethod]
		public void Scale()
		{
			Transform2D<float> transform = Transform2D<float>.GetScale(2, 4);
			Assert.AreEqual(
				new Vector2D<float>(2, 4),
				transform * new Vector2D<float>(1, 1)
			);
		}


		[TestMethod]
		public void Rotate()
		{
			Transform2D<float> transform = Transform2D<float>.GetRotate(MathF.PI / 2f);
			_AreApproximatelyEqual(
				new Vector2D<float>(0, 1),
				transform * new Vector2D<float>(1, 0)
			);
			_AreApproximatelyEqual(
				new Vector2D<float>(-1, 1),
				transform * new Vector2D<float>(1, 1)
			);
		}


		[TestMethod]
		public void RotateAndTranslate()
		{
			Transform2D<float> t = Transform2D<float>.GetRotate(MathF.PI / 2f);
			t.Translate(2, 3);

			_AreApproximatelyEqual(
				new Vector2D<float>(-3, 2),
				t * new Vector2D<float>(0, 0)
			);
			_AreApproximatelyEqual(
				new Vector2D<float>(-4, 3),
				t * new Vector2D<float>(1, 1)
			);
		}


		private void _AreApproximatelyEqual(Vector2D<float> expected, Vector2D<float> actual)
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
			const float epsilon = NumberSet<float>.EPSILON_FLOAT;
			if (actual == expected)
			{
				return;
			}

			if (Math.Abs(expected - actual) < epsilon)
			{
				return;
			}


			Assert.IsTrue(Math.Abs(expected - actual) < epsilon);
		}
	}
}
