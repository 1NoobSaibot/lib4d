﻿using Lib4D;
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
			// Around OX
			var transform = Transform3D<TNumber>.GetRotate(new Vector3D<TNumber>(c1, c0, c0), PI / c2);
			AreApproximatelyEqual(
				new Vector3D<TNumber>(c0, c0, c1),
				transform * new Vector3D<TNumber>(c0, c1, c0)
			);
			AreApproximatelyEqual(
				new Vector3D<TNumber>(c0, -c1, c1),
				transform * new Vector3D<TNumber>(c0, c1, c1)
			);

			// Around OY
			transform = Transform3D<TNumber>.GetRotate(new Vector3D<TNumber>(c0, c1, c0), PI / c2);
			AreApproximatelyEqual(
				new Vector3D<TNumber>(c1, c0, c0),
				transform * new Vector3D<TNumber>(c0, c0, c1)
			);
			AreApproximatelyEqual(
				new Vector3D<TNumber>(c1, c0, -c1),
				transform * new Vector3D<TNumber>(c1, c0, c1)
			);

			// Around OZ
			transform = Transform3D<TNumber>.GetRotate(new Vector3D<TNumber>(c0, c0, c1), PI / c2);
			AreApproximatelyEqual(
				new Vector3D<TNumber>(c0, c1, c0),
				transform * new Vector3D<TNumber>(c1, c0, c0)
			);
			AreApproximatelyEqual(
				new Vector3D<TNumber>(-c1, c1, c0),
				transform * new Vector3D<TNumber>(c1, c1, c0)
			);

			transform = Transform3D<TNumber>.GetRotate(
				new Vector3D<TNumber>(c1, c1, c1).GetNormalized(),
				PI * c2 / c3
			);
			AreApproximatelyEqual(
				new Vector3D<TNumber>(c3, c1, c2),
				transform * new Vector3D<TNumber>(c1, c2, c3)
			);
			AreApproximatelyEqual(
				new Vector3D<TNumber>(-c3, -c1, -c2),
				transform * new Vector3D<TNumber>(-c1, -c2, -c3)
			);
		}


		[TestMethod]
		public void RotateWithQuaternion()
		{
			TNumber[] angles = { -c3 * PI, -c2 * PI, -PI, c0, PI, c2 * PI, c3 * PI };

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