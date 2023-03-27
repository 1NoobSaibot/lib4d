using Lib4D;
using Lib4D.Mathematic;
using Lib4D_Tests.Helpers;
using System.Numerics;

namespace Lib4D_Tests.Transforms._4D
{
	[TestClass]
	public abstract class Transform4DTest<TNumber>
		: NumberSet<TNumber>
		where TNumber : INumber<TNumber>
	{
		private readonly Random _rnd = new();


		public Transform4DTest()
		{
			Math<TNumber>.InitInstance(GetMath());
		}


		public abstract Math<TNumber> GetMath();


		[TestMethod]
		public void TranslatingVectorWithIdentityTransform()
		{
			// Identity transform doesn't change Vectors
			Transform4D<TNumber> identityTransform = new();
			var v = GetRandomVector();

			Assert.AreEqual(v, identityTransform * v);
		}


		[TestMethod]
		public void Translate()
		{
			var transform = Transform4D<TNumber>
				.GetTranslate(this[5], this[-4], this[3], this[-7]);
			Assert.AreEqual(
				new Vector4D<TNumber>(this[5], this[-4], this[3], this[-7]),
				transform * new Vector4D<TNumber>()
			);
			Assert.AreEqual(
				new Vector4D<TNumber>(this[6], this[-2], this[-5], this[6]),
				transform * new Vector4D<TNumber>(this[1], this[2], this[-8], this[13])
			);
		}


		[TestMethod]
		public void Scale()
		{
			Transform4D<TNumber> transform = Transform4D<TNumber>
				.GetScale(this[2], this[4], this[-6], this[7]);
			Assert.AreEqual(
				new Vector4D<TNumber>(this[2], this[4], this[-6], this[7]),
				transform * new Vector4D<TNumber>(this[1], this[1], this[1], this[1])
			);
		}


		[TestMethod]
		public void Rotate()
		{
			Vector4D<TNumber> z = new(this[0], this[0], this[1], this[0]);
			Vector4D<TNumber> q = new(this[0], this[0], this[0], this[1]);
			Vector4D<TNumber> x = new(this[1], this[0], this[0], this[0]);
			Vector4D<TNumber> y = new(this[0], this[1], this[0], this[0]);

			// Axis ZQ, X => Y
			Bivector4D<TNumber> surface = new(z, q);
			Transform4D<TNumber> t = new();
			t.Rotate(surface, Math<TNumber>.PI / this[2]);
			AreApproximatelyEqual(y, t * x);

			// Axis YQ, Z => X
			surface = new(y, q);
			t = new();
			t.Rotate(surface, Math<TNumber>.PI / this[2]);
			AreApproximatelyEqual(x, t * z);

			// Axis YZ, Q => X
			surface = new(y, z);
			t = new();
			t.Rotate(surface, Math<TNumber>.PI / this[2]);
			AreApproximatelyEqual(x, t * q);

			// Axis XQ, Y => Z
			surface = new(x, q);
			t = new();
			t.Rotate(surface, Math<TNumber>.PI / this[2]);
			AreApproximatelyEqual(z, t * y);

			// Axis XZ, Q => Y
			surface = new(x, z);
			t = new();
			t.Rotate(surface, Math<TNumber>.PI / this[2]);
			AreApproximatelyEqual(y, t * q);

			// Axis XY, Z => Q
			surface = new(x, y);
			t = new();
			t.Rotate(surface, Math<TNumber>.PI / this[2]);
			AreApproximatelyEqual(q, t * z);


			// Axis ZQ 180, X => -X
			t = new();
			surface = new(z, q);
			t.Rotate(surface, Math<TNumber>.PI);
			AreApproximatelyEqual(x * this[-1], t * x);
			AreApproximatelyEqual(y * this[-1], t * y);

			t = new();
			surface = new(new Vector4D<TNumber>(this[1], this[1], this[1]).GetNormalized(), q);
			t.Rotate(surface, Math<TNumber>.PI / this[3] * this[2]);
			AreApproximatelyEqual(y, t * x);
			AreApproximatelyEqual(z, t * y);
			AreApproximatelyEqual(x, t * z);
		}


		private Vector4D<TNumber> GetRandomVector()
		{
			const double amplitude = 1;

			return new Vector4D<TNumber>(
				this[_rnd.NextDouble() * 2 * amplitude - amplitude],	
				this[_rnd.NextDouble() * 2 * amplitude - amplitude],	
				this[_rnd.NextDouble() * 2 * amplitude - amplitude],
				this[_rnd.NextDouble() * 2 * amplitude - amplitude]
			);
		}


		private void AreApproximatelyEqual(Vector4D<TNumber> expected, Vector4D<TNumber> actual)
		{
			try
			{
				AreApproximatelyEqual(expected.X, actual.X);
				AreApproximatelyEqual(expected.Y, actual.Y);
				AreApproximatelyEqual(expected.Z, actual.Z);
				AreApproximatelyEqual(expected.Q, actual.Q);
			}
			catch
			{
				Assert.AreEqual(expected, actual);
			}
		}


		private void AreApproximatelyEqual(TNumber expected, TNumber actual)
		{
			if (actual == expected)
			{
				return;
			}

			Assert.IsTrue(Math<TNumber>.Abs!(expected - actual) < EPSILON);
		}
	}
}
