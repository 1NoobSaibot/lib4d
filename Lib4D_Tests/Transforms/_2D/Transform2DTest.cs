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
			Vector2D<TNumber> v = new(this[7], this[4]);

			Assert.AreEqual(v, identityTransform * v);

			var scaleTransform = new Transform2D<TNumber>();
			scaleTransform.Scale(this[2], this[3]);

			Assert.AreEqual(
				new Vector2D<TNumber>(this[14], this[12]),
				scaleTransform * v
			);
		}


		[TestMethod]
		public void Translate()
		{
			var transform = Transform2D<TNumber>.GetTranslate(this[5], this[-4]);
			Assert.AreEqual(
				new Vector2D<TNumber>(this[5], this[-4]),
				transform * new Vector2D<TNumber>(this[0], this[0])
			);
			Assert.AreEqual(
				new Vector2D<TNumber>(this[6], this[-2]),
				transform * new Vector2D<TNumber>(this[1], this[2])
			);
		}


		[TestMethod]
		public void Scale()
		{
			Transform2D<TNumber> transform = Transform2D<TNumber>.GetScale(this[2], this[4]);
			Assert.AreEqual(
				new Vector2D<TNumber>(this[2], this[4]),
				transform * new Vector2D<TNumber>(this[1], this[1])
			);
		}


		[TestMethod]
		public void Rotate()
		{
			var transform = Transform2D<TNumber>.GetRotate(Math<TNumber>.PI / this[2]);
			AreApproximatelyEqual(
				new Vector2D<TNumber>(this[0], this[1]),
				transform * new Vector2D<TNumber>(this[1], this[0])
			);
			AreApproximatelyEqual(
				new Vector2D<TNumber>(this[-1], this[1]),
				transform * new Vector2D<TNumber>(this[1], this[1])
			);
		}


		[TestMethod]
		public void RotateAndTranslate()
		{
			var t = Transform2D<TNumber>.GetRotate(Math<TNumber>.PI / this[2]);
			t.Translate(this[2], this[3]);

			AreApproximatelyEqual(
				new Vector2D<TNumber>(this[-3], this[2]),
				t * new Vector2D<TNumber>(this[0], this[0])
			);
			AreApproximatelyEqual(
				new Vector2D<TNumber>(this[-4], this[3]),
				t * new Vector2D<TNumber>(this[1], this[1])
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
