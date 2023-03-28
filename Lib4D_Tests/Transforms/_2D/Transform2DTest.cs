using Lib4D;
using Lib4D.Mathematic;
using Lib4D_Tests.Helpers;
using System.Numerics;

namespace Lib4D_Tests.Transforms._2D
{
	public abstract class Transform2DTest<TNumber>
		: MathDependentTest<TNumber>
		where TNumber : INumber<TNumber>
	{
		[TestMethod]
		public void Calculate()
		{
			var identityTransform = new Transform2D<TNumber>();
			Vector2D<TNumber> v = new(7, 4);

			Assert.AreEqual(v, identityTransform * v);

			var scaleTransform = new Transform2D<TNumber>();
			scaleTransform.Scale(2, 3);

			Assert.AreEqual(
				new Vector2D<TNumber>(14, 12),
				scaleTransform * v
			);
		}


		[TestMethod]
		public void Translate()
		{
			var transform = Transform2D<TNumber>.GetTranslate(5, -4);
			Assert.AreEqual(
				new Vector2D<TNumber>(5, -4),
				transform * new Vector2D<TNumber>(0, 0)
			);
			Assert.AreEqual(
				new Vector2D<TNumber>(6, -2),
				transform * new Vector2D<TNumber>(1, 2)
			);
		}


		[TestMethod]
		public void Scale()
		{
			Transform2D<TNumber> transform = Transform2D<TNumber>.GetScale(2, 4);
			Assert.AreEqual(
				new Vector2D<TNumber>(2, 4),
				transform * new Vector2D<TNumber>(1, 1)
			);
		}


		[TestMethod]
		public void Rotate()
		{
			var transform = Transform2D<TNumber>.GetRotate(Math.PI / 2);
			AreApproximatelyEqual(
				new Vector2D<TNumber>(0, 1),
				transform * new Vector2D<TNumber>(1, 0)
			);
			AreApproximatelyEqual(
				new Vector2D<TNumber>(-1, 1),
				transform * new Vector2D<TNumber>(1, 1)
			);
		}


		[TestMethod]
		public void RotateAndTranslate()
		{
			var t = Transform2D<TNumber>.GetRotate(Math.PI / 2);
			t.Translate(2, 3);

			AreApproximatelyEqual(
				new Vector2D<TNumber>(-3, 2),
				t * new Vector2D<TNumber>(0, 0)
			);
			AreApproximatelyEqual(
				new Vector2D<TNumber>(-4, 3),
				t * new Vector2D<TNumber>(1, 1)
			);
		}


		private void AreApproximatelyEqual(Vector2D<TNumber> expected, Vector2D<TNumber> actual)
		{
			try
			{
				AreApproximatelyEqual(expected.X, actual.X);
				AreApproximatelyEqual(expected.Y, actual.Y);
			}
			catch
			{
				Assert.AreEqual(expected, actual);
			}
		}

		private void AreApproximatelyEqual(TNumber expected, TNumber actual)
		{
			TNumber epsilon = EPSILON;
			if (actual == expected)
			{
				return;
			}

			if (Math<TNumber>.Abs!(expected - actual) < epsilon)
			{
				return;
			}


			Assert.IsTrue(Math<TNumber>.Abs!(expected - actual) < epsilon);
		}
	}
}
