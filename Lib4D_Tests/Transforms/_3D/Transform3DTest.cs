using Lib4D;
using Lib4D.Mathematic;
using Lib4D_Tests.Helpers;
using System.Numerics;

namespace Lib4D_Tests.Transforms._3D
{
	public abstract class Transform3DTest<TNumber>
		: MathDependentTest<TNumber>
		where TNumber : INumber<TNumber>
	{
		private readonly TNumber[] _numbers;


		public Transform3DTest()
		{
			_numbers = GetNums();
		}


		[TestMethod]
		public void TranslatingVectorWithIdentityTransform()
		{
			var identityTransform = new Transform3D<TNumber>();
			ForEachVector(v =>
			{
				Assert.AreEqual(v, identityTransform * v);
			});
		}


		[TestMethod]
		public void Translate3D()
		{
			ForEachVector(v1 =>
			{
				var transform = Transform3D<TNumber>.GetTranslate(v1);
				ForEachVector(p =>
				{
					Assert.AreEqual(p + v1, transform * p);
				});
			});
		}


		[TestMethod]
		public void Scale()
		{
			ForEachVector(scaleV =>
			{
				var transform = Transform3D<TNumber>.GetScale(scaleV);
				ForEachVector(p =>
				{
					var expectedV = new Vector3D<TNumber>(scaleV.X * p.X, scaleV.Y * p.Y, scaleV.Z * p.Z);
					Assert.AreEqual(expectedV, transform * p);
				});
			});
		}


		private static readonly TNumber PI = Math<TNumber>.PI;
		[TestMethod]
		public void RotateAroundAxis()
		{
			TNumber c2 = Math<TNumber>.Double2Number!(2);
			// Around OX
			var transform = Transform3D<TNumber>.GetRotate(new Vector3D<TNumber>(1, 0, 0), PI / c2);
			AreApproximatelyEqual(
				new Vector3D<TNumber>(0, 0, 1),
				transform * new Vector3D<TNumber>(0, 1, 0)
			);
			AreApproximatelyEqual(
				new Vector3D<TNumber>(0, -1, 1),
				transform * new Vector3D<TNumber>(0, 1, 1)
			);

			// Around OY
			transform = Transform3D<TNumber>.GetRotate(new Vector3D<TNumber>(0, 1, 0), PI / c2);
			AreApproximatelyEqual(
				new Vector3D<TNumber>(1, 0, 0),
				transform * new Vector3D<TNumber>(0, 0, 1)
			);
			AreApproximatelyEqual(
				new Vector3D<TNumber>(1, 0, -1),
				transform * new Vector3D<TNumber>(1, 0, 1)
			);

			// Around OZ
			transform = Transform3D<TNumber>.GetRotate(new Vector3D<TNumber>(0, 0, 1), PI / c2);
			AreApproximatelyEqual(
				new Vector3D<TNumber>(0, 1, 0),
				transform * new Vector3D<TNumber>(1, 0, 0)
			);
			AreApproximatelyEqual(
				new Vector3D<TNumber>(-1, 1, 0),
				transform * new Vector3D<TNumber>(1, 1, 0)
			);

			TNumber c3 = Math<TNumber>.Double2Number!(3);
			transform = Transform3D<TNumber>.GetRotate(
				new Vector3D<TNumber>(1, 1, 1).GetNormalized(),
				PI * c2 / c3
			);
			AreApproximatelyEqual(
				new Vector3D<TNumber>(3, 1, 2),
				transform * new Vector3D<TNumber>(1, 2, 3)
			);
			AreApproximatelyEqual(
				new Vector3D<TNumber>(-3, -1, -2),
				transform * new Vector3D<TNumber>(-1, -2, -3)
			);
		}


		[TestMethod]
		public void RotateWithQuaternion()
		{
			TNumber c0 = Math<TNumber>.Double2Number!(0);
			TNumber c2 = Math<TNumber>.Double2Number!(2);
			TNumber c3 = Math<TNumber>.Double2Number!(3);
			TNumber[] angles = {
				-c3 * PI,
				-c2 * PI,
				-PI, c0,
				PI, c2 * PI,
				c3 * PI
			};

			ForEachVector(axis => {
				axis = axis.GetNormalized();
				foreach (var angle in angles)
				{
					var q = Quaternion<TNumber>.ByAxisAndAngle(axis, angle);

					var transformAxisAngle = Transform3D<TNumber>.GetRotate(axis, angle);
					var transformQuaternion = Transform3D<TNumber>.GetRotate(q);

					ForEachVector(point =>
					{
						AreApproximatelyEqual(
							transformAxisAngle * point,
							transformQuaternion * point
						);
					});
				}
			});
					
		}


		private void AreApproximatelyEqual(Vector3D<TNumber> expected, Vector3D<TNumber> actual)
		{
			try
			{
				AreApproximatelyEqual(expected.X, actual.X);
				AreApproximatelyEqual(expected.Y, actual.Y);
				AreApproximatelyEqual(expected.Z, actual.Z);
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

			TNumber allowableError = EPSILON;
			Assert.IsTrue(Math<TNumber>.Abs!(expected - actual) < allowableError);
		}


		private void ForEachVector(Action<Vector3D<TNumber>> action)
		{
			foreach (var x in _numbers)
			{
				foreach (var y in _numbers)
				{
					foreach (var z in _numbers)
					{
						action(new(x, y, z));
					}
				}
			}
		}
	}
}
