using Lib4D;
using Lib4D_Tests.Helpers;
using System.Numerics;

namespace Lib4D_Tests
{
	[TestClass]
	public class Transform3DUnitTest : ITestStand
	{
		private readonly ITestStand[] _stands = {
			new Transform3DTestStand<float>(
				Vector3D<float>.Abs,
				MathF.Abs,
				Transform3D<float>.GetRotate,
				Vector3D<float>.Normalize,
				Quaternion<float>.ByAxisAndAngle
			),
			new Transform3DTestStand<double>(
				Vector3D<double>.Abs,
				Math.Abs,
				Transform3D<double>.GetRotate,
				Vector3D<float>.Normalize,
				Quaternion<float>.ByAxisAndAngle
			)
		};


		[TestMethod]
		public void TranslatingVectorWithIdentityTransform()
		{
			ForEachStand(stand => stand.TranslatingVectorWithIdentityTransform());
		}


		[TestMethod]
		public void Translate3D()
		{
			ForEachStand(stand => stand.Translate3D());
		}


		[TestMethod]
		public void Scale()
		{
			ForEachStand(stand => stand.Scale());
		}


		[TestMethod]
		public void RotateAroundAxis()
		{
			ForEachStand(s => s.RotateAroundAxis());
		}


		[TestMethod]
		public void RotateWithQuaternion()
		{
			ForEachStand(s => s.RotateWithQuaternion());
		}


		private void ForEachStand(Action<ITestStand> test)
		{
			foreach (var stand in _stands)
			{
				test(stand);
			}
		}


		private class Transform3DTestStand<TNumber>
			: NumberSet<TNumber>, ITestStand
			where TNumber : INumber<TNumber>
		{
			private readonly TNumber[] _numbers;
			private readonly Func<Vector3D<TNumber>, TNumber> _getAbsV;
			private readonly Func<TNumber, TNumber> _getAbsF;
			private readonly Func<Vector3D<TNumber>, TNumber, Transform3D<TNumber>> _getRotateT;
			private readonly Func<Vector3D<TNumber>, Vector3D<TNumber>> _normalize;
			private readonly Func<Vector3D<TNumber>, TNumber, Quaternion<TNumber>> _getQ;

			public Transform3DTestStand(
				Func<Vector3D<TNumber>, TNumber> getAbsV,
				Func<TNumber, TNumber> getAbsF,
				Func<Vector3D<TNumber>, TNumber, Transform3D<TNumber>> getRotateT,
				Func<Vector3D<TNumber>, Vector3D<TNumber>> normalize,
				Func<Vector3D<TNumber>, TNumber, Quaternion<TNumber>> getQ
			)
			{
				_numbers = GetNums();
				_getAbsV = getAbsV;
				_getAbsF = getAbsF;
				_getRotateT = getRotateT;
				_normalize = normalize;
				_getQ = getQ;
			}

			public void TranslatingVectorWithIdentityTransform()
			{
				var identityTransform = new Transform3D<TNumber>();
				ForEachVector(v =>
				{
					Assert.AreEqual(v, identityTransform * v);
				});
			}


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

			public void RotateAroundAxis()
			{
				// Around OX
				var transform = _getRotateT(new Vector3D<TNumber>(c1, c0, c0), PI / c2);
				_AreApproximatelyEqual(
					new Vector3D<TNumber>(c0, c0, c1),
					transform * new Vector3D<TNumber>(c0, c1, c0)
				);
				_AreApproximatelyEqual(
					new Vector3D<TNumber>(c0, -c1, c1),
					transform * new Vector3D<TNumber>(c0, c1, c1)
				);

				// Around OY
				transform = _getRotateT(new Vector3D<TNumber>(c0, c1, c0), PI / c2);
				_AreApproximatelyEqual(
					new Vector3D<TNumber>(c1, c0, c0),
					transform * new Vector3D<TNumber>(c0, c0, c1)
				);
				_AreApproximatelyEqual(
					new Vector3D<TNumber>(c1, c0, -c1),
					transform * new Vector3D<TNumber>(c1, c0, c1)
				);

				// Around OZ
				transform = _getRotateT(new Vector3D<TNumber>(c0, c0, c1), PI / c2);
				_AreApproximatelyEqual(
					new Vector3D<TNumber>(c0, c1, c0),
					transform * new Vector3D<TNumber>(c1, c0, c0)
				);
				_AreApproximatelyEqual(
					new Vector3D<TNumber>(-c1, c1, c0),
					transform * new Vector3D<TNumber>(c1, c1, c0)
				);

				transform = _getRotateT(_normalize(new Vector3D<TNumber>(c1, c1, c1)), PI * c2 / c3);
				_AreApproximatelyEqual(
					new Vector3D<TNumber>(c3, c1, c2),
					transform * new Vector3D<TNumber>(c1, c2, c3)
				);
				_AreApproximatelyEqual(
					new Vector3D<TNumber>(-c3, -c1, -c2),
					transform * new Vector3D<TNumber>(-c1, -c2, -c3)
				);
			}


			public void RotateWithQuaternion()
			{
				TNumber[] angles = { -c3 * PI, -c2 * PI, -PI, c0, PI, c2 * PI, c3 * PI };

				ForEachVector(axis => {
					axis = _normalize(axis);
					foreach (var angle in angles)
					{
						var q = _getQ(axis, angle);

						var transformAxisAngle = _getRotateT(axis, angle);
						var transformQuaternion = Transform3D<TNumber>.GetRotate(q);

						ForEachVector(point =>
						{
							_AreApproximatelyEqual(
								transformAxisAngle * point,
								transformQuaternion * point
							);
						});
					}
				});
					
			}


			private void _AreApproximatelyEqual(Vector3D<TNumber> expected, Vector3D<TNumber> actual)
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


			private void _AreApproximatelyEqual(TNumber expected, TNumber actual)
			{
				if (actual == expected)
				{
					return;
				}

				TNumber allowableError = EPSILON;
				Assert.IsTrue(_getAbsF(expected - actual) < allowableError);
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

	public interface ITestStand
	{
		void TranslatingVectorWithIdentityTransform();
		void Translate3D();
		void Scale();
		void RotateAroundAxis();
		void RotateWithQuaternion();
	}
}
