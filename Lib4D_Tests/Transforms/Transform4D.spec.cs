using Lib4D;

namespace Lib4D_Tests
{
	[TestClass]
	public class Transform4DUnitTest
	{
		private readonly Random _rnd = new();


		[TestMethod]
		public void TranslatingVectorWithIdentityTransform()
		{
			// Identity transform doesn't change Vectors
			Transform4DDouble identityTransform = new();
			Vector4D<double> v = GetRandomVector();

			Assert.AreEqual(v, identityTransform * v);
		}


		[TestMethod]
		public void Translate()
		{
			Transform4DDouble transform = Transform4DDouble.GetTranslate(5, -4, 3, -7);
			Assert.AreEqual(
				new Vector4D<double>(5, -4, 3, -7),
				transform * new Vector4D<double>()
			);
			Assert.AreEqual(
				new Vector4D<double>(6, -2, -5, 6),
				transform * new Vector4D<double>(1, 2, -8, 13)
			);
		}


		[TestMethod]
		public void Scale()
		{
			Transform4DDouble transform = Transform4DDouble.GetScale(2, 4, -6, 7);
			Assert.AreEqual(
				new Vector4D<double>(2, 4, -6, 7),
				transform * new Vector4D<double>(1, 1, 1, 1)
			);
		}


		[TestMethod]
		public void Rotate()
		{
			Vector4D<double> z = new(0, 0, 1, 0);
			Vector4D<double> q = new(0, 0, 0, 1);
			Vector4D<double> x = new(1, 0, 0, 0);
			Vector4D<double> y = new(0, 1, 0, 0);

			// Axis ZQ, X => Y
			Bivector4D<double> surface = new(z, q);
			Transform4DDouble t = new();
			t.Rotate(surface, Math.PI / 2);
			AreApproximatelyEqual(y, t * x);

			// Axis YQ, Z => X
			surface = new(y, q);
			t = new();
			t.Rotate(surface, Math.PI / 2);
			AreApproximatelyEqual(x, t * z);

			// Axis YZ, Q => X
			surface = new(y, z);
			t = new();
			t.Rotate(surface, Math.PI / 2);
			AreApproximatelyEqual(x, t * q);

			// Axis XQ, Y => Z
			surface = new(x, q);
			t = new();
			t.Rotate(surface, Math.PI / 2);
			AreApproximatelyEqual(z, t * y);

			// Axis XZ, Q => Y
			surface = new(x, z);
			t = new();
			t.Rotate(surface, Math.PI / 2);
			AreApproximatelyEqual(y, t * q);

			// Axis XY, Z => Q
			surface = new(x, y);
			t = new();
			t.Rotate(surface, Math.PI / 2);
			AreApproximatelyEqual(q, t * z);


			// Axis ZQ 180, X => -X
			t = new();
			surface = new(z, q);
			t.Rotate(surface, Math.PI);
			AreApproximatelyEqual(x * -1, t * x);
			AreApproximatelyEqual(y * -1, t * y);

			t = new();
			surface = new(new Vector4D<double>(1, 1, 1).GetNormalized(), q);
			t.Rotate(surface, Math.PI / 3 * 2);
			AreApproximatelyEqual(y, t * x);
			AreApproximatelyEqual(z, t * y);
			AreApproximatelyEqual(x, t * z);
		}


		private Vector4D<double> GetRandomVector()
		{
			const double amplitude = 1;

			return new Vector4D<double>(
				_rnd.NextDouble() * 2 * amplitude - amplitude,	
				_rnd.NextDouble() * 2 * amplitude - amplitude,	
				_rnd.NextDouble() * 2 * amplitude - amplitude,
				_rnd.NextDouble() * 2 * amplitude - amplitude
			);
		}


		private static void AreApproximatelyEqual(Vector4D<double> expected, Vector4D<double> actual)
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


		private static void AreApproximatelyEqual(double expected, double actual)
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
