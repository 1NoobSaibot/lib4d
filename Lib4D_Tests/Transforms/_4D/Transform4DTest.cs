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
		private readonly VectorTestHelper<TNumber> _vth = new();


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
				.GetTranslate(5, -4, 3, -7);
			Assert.AreEqual(
				new Vector4D<TNumber>(5, -4, 3, -7),
				transform * new Vector4D<TNumber>()
			);
			Assert.AreEqual(
				new Vector4D<TNumber>(6, -2, -5, 6),
				transform * new Vector4D<TNumber>(1, 2, -8, 13)
			);
		}


		[TestMethod]
		public void Scale()
		{
			Transform4D<TNumber> transform = Transform4D<TNumber>
				.GetScale(2, 4, -6, 7);
			Assert.AreEqual(
				new Vector4D<TNumber>(2, 4, -6, 7),
				transform * new Vector4D<TNumber>(1, 1, 1, 1)
			);
		}


		[TestMethod]
		public void RotateOriginal()
		{
			Vector4D<TNumber> z = new(0, 0, 1, 0);
			Vector4D<TNumber> q = new(0, 0, 0, 1);
			Vector4D<TNumber> x = new(1, 0, 0, 0);
			Vector4D<TNumber> y = new(0, 1, 0, 0);

			// Axis ZQ, X => Y
			Transform4D<TNumber> t = new();
			t.Rotate(z, q, Math.PI / 2);
			AreApproximatelyEqual(y, t * x);

			// Axis YQ, Z => X
			t = new();
			t.Rotate(y, q, Math.PI / 2);
			AreApproximatelyEqual(x, t * z);

			// Axis YZ, Q => X
			t = new();
			t.Rotate(y, z, Math.PI / 2);
			AreApproximatelyEqual(x, t * q);

			// Axis XQ, Y => Z
			t = new();
			t.Rotate(x, q, Math.PI / 2);
			AreApproximatelyEqual(z, t * y);

			// Axis XZ, Q => Y
			t = new();
			t.Rotate(x, z, Math.PI / 2);
			AreApproximatelyEqual(y, t * q);

			// Axis XY, Z => Q
			t = new();
			t.Rotate(x, y, Math.PI / 2);
			AreApproximatelyEqual(q, t * z);


			// Axis ZQ 180, X => -X
			t = new();
			t.Rotate(z, q, Math<TNumber>.PI);
			AreApproximatelyEqual(x * -TNumber.One, t * x);
			AreApproximatelyEqual(y * -TNumber.One, t * y);

			t = new();
			t.Rotate(new Vector4D<TNumber>(1, 1, 1).GetNormalized(), q, Math.PI / 3 * 2);
			AreApproximatelyEqual(y, t * x);
			AreApproximatelyEqual(z, t * y);
			AreApproximatelyEqual(x, t * z);
		}


		[TestMethod]
		public void RotateOptimized()
		{
			const double angle = Math.PI * 2 / 3;
			TNumber angleAsTNum = Math<TNumber>.Double2Number!(angle);
			Vector4D<TNumber> randPoint = new(3, 7, 2, 5);

			_vth.ForEachPairOfVectors4D((axis1, axis2) => {
				Transform4D<TNumber> tStandart = new();
				tStandart.Rotate(axis1, axis2, angle);

				Transform4D<TNumber> tOptimized = new();
				tOptimized.RotateOriginal(axis1, axis2, angleAsTNum);

				Assert.AreEqual(tStandart * randPoint, tOptimized * randPoint); 
			});
		}


		[TestMethod]
		public void RotateOptimizedIsFaster()
		{
			const int rotationNumber = 100000;
			var t = new Transform4D<TNumber>();

			TimeSpan originTime = TimeSpan.Zero;
			TimeSpan optimizedTime = TimeSpan.Zero;

			for (int i = 0; i < 10; i++)
			{
				DateTime start = DateTime.Now;
				for (int j = 0; j < rotationNumber; j++)
				{
					t.RotateOriginal(new(), new(), TNumber.One);
				}
				originTime += DateTime.Now - start;

				start = DateTime.Now;
				for (int j = 0; j < rotationNumber; j++)
				{
					t.Rotate(new(), new(), TNumber.One);
				}
				optimizedTime += DateTime.Now - start;
			}

			Assert.IsTrue(optimizedTime < originTime);
		}


		private Vector4D<TNumber> GetRandomVector()
		{
			const double amplitude = 1;

			return new Vector4D<TNumber>(
				_rnd.NextDouble() * 2 * amplitude - amplitude,	
				_rnd.NextDouble() * 2 * amplitude - amplitude,	
				_rnd.NextDouble() * 2 * amplitude - amplitude,
				_rnd.NextDouble() * 2 * amplitude - amplitude
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
