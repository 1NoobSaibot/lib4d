using Lib4D;
using Lib4D.Mathematic;
using Lib4D_Tests.Helpers;
using System.Numerics;

namespace Lib4D_Tests.Vectors
{
	public abstract class Vector3DTest<TNumber>
		: MathDependentTest<TNumber>
		where TNumber : INumber<TNumber>
	{
		private readonly VectorTestHelper<TNumber> _vth = new();


		[TestMethod]
		public void Equals()
		{
			(Vector3D<TNumber>, Vector3D<TNumber>, bool)[] samples =
			{
				(new(), new(), true),
				(new(1), new(1), true),
				(new(0, 1), new(0, 1), true),
				(new(0, 0, 1), new(0, 0, 1), true),
				(new(1, 1), new(1, 1), true),
				(new(1, 0, 1), new(1, 0, 1), true),
				(new(0, 1, 1), new(0, 1, 1), true),
				(new(1, 1, 1), new(1, 1, 1), true),

				(new(), new(1), false),
				(new(), new(0, 1), false),
				(new(), new(0, 0, 1), false),
				(new(), new(1, 1, 0), false),
				(new(), new(1, 0, 1), false),
				(new(), new(0, 1, 1), false),
				(new(), new(1, 1, 1), false),
				(new(1), new(0, 1), false),
				(new(1), new(0, 0, 1), false),
				(new(1), new(0, 1, 1), false),
				(new(1), new(1, 1), false),
				(new(1), new(1, 0, 1), false),
				(new(1), new(1, 1, 1), false),
				(new(0, 1), new(1, 0, 1), false),
				(new(0, 1), new(1, 1, 1), false),
				(new(0, 0, 1), new(1, 1, 0), false),
				(new(0, 0, 1), new(1, 1, 1), false),
			};

			EqualityTestHelper<Vector3D<TNumber>>.TestEquality(samples);
		}


		[TestMethod]
		public void ConstructorsAreAgreed()
		{
			Vector3D<TNumber> zero = new();
			TNumber z = TNumber.Zero;

			Assert.AreEqual(z, zero.X!);
			Assert.AreEqual(z, zero.Y!);
			Assert.AreEqual(z, zero.Z!);

			Assert.AreEqual(zero, new Vector3D<TNumber>(0));
			Assert.AreEqual(zero, new Vector3D<TNumber>(0, 0));
			Assert.AreEqual(zero, new Vector3D<TNumber>(0, 0, 0));

			Assert.AreEqual(new Vector3D<TNumber>(1), new Vector3D<TNumber>(1, 0));
			Assert.AreEqual(new Vector3D<TNumber>(1), new Vector3D<TNumber>(1, 0, 0));
			Assert.AreEqual(new Vector3D<TNumber>(1, 1), new Vector3D<TNumber>(1, 1, 0));

			TNumber o = TNumber.One;

			Assert.AreEqual(zero, new Vector3D<TNumber>(z));
			Assert.AreEqual(zero, new Vector3D<TNumber>(z, z));
			Assert.AreEqual(zero, new Vector3D<TNumber>(z, z, z));

			Assert.AreEqual(new Vector3D<TNumber>(o), new Vector3D<TNumber>(o, z));
			Assert.AreEqual(new Vector3D<TNumber>(o), new Vector3D<TNumber>(o, z, z));
			Assert.AreEqual(new Vector3D<TNumber>(o, o), new Vector3D<TNumber>(o, o, z));

			var v = new Vector3D<TNumber>(1, -1, 2);
			Assert.AreEqual(v, new Vector3D<TNumber>(o, -o, o + o));
			Assert.AreEqual(o, v.X);
			Assert.AreEqual(-o, v.Y);
			Assert.AreEqual(o + o, v.Z);
		}


		[TestMethod]
		public void Add()
		{
			_vth.ForEachPairOfVectors3D((v1, v2) => {
				var sum = v1 + v2;
				Assert.AreEqual(sum, v2 + v1);
				Assert.AreEqual(sum.X, v1.X + v2.X);
				Assert.AreEqual(sum.Y, v1.Y + v2.Y);
				Assert.AreEqual(sum.Z, v1.Z + v2.Z);
			});
		}


		[TestMethod]
		public void Sub()
		{
			_vth.ForEachPairOfVectors3D((v1, v2) =>
			{
				var sum = v1 + v2;
				Assert.AreEqual(v1, sum - v2);
				Assert.AreEqual(v2, sum - v1);
			});
		}


		[TestMethod]
		public void Abs()
		{
			var r3 = Math.Sqrt(3);
			(Vector3D<TNumber>, double)[] samples =
			{
				(new(), 0),
				(new(3), 3),
				(new(-3), 3),
				(new(0, 3), 3),
				(new(0, -3), 3),
				(new(0, 0, 3), 3),
				(new(0, 0, -3), 3),
				(new(3, 4), 5),
				(new(4, 3), 5),
				(new(3, 0, 4), 5),
				(new(4, 0, 3), 5),
				(new(0, 3, 4), 5),
				(new(0, 4, 3), 5),
				(new(r3, r3, r3), 3)
			};

			foreach (var sample in samples)
			{
				(var v, var expectedAbs) = sample;
				TNumber abs = Math<TNumber>.Double2Number!(expectedAbs);
				_vth.AssertApproximatelyEqualF(abs, v.Abs);
			}
		}


		[TestMethod]
		public void AbsQuad()
		{
			_vth.ForEachVector3D(v =>
			{
				var expectedAbsQuad = v.Abs * v.Abs;
				_vth.AssertApproximatelyEqualF(expectedAbsQuad, v.AbsQuad, 0.00001526);
			});
		}


		[TestMethod]
		public void MulByNumber()
		{
			_vth.ForEachVector3D(v =>
			{
				_vth.ForEachFloat(f =>
				{
					var mul = v * f;
					Assert.AreEqual(mul, f * v);
					Assert.AreEqual(f * v.X, mul.X);
					Assert.AreEqual(f * v.Y, mul.Y);
					Assert.AreEqual(f * v.Z, mul.Z);
				});
			});
		}


		[TestMethod]
		public void UnaryMinus()
		{
			_vth.ForEachVector3D(v =>
			{
				Assert.AreEqual(v * -TNumber.One, -v);
			});
		}


		[TestMethod]
		public void CrossProductIsRightHand()
		{
			Vector3D<TNumber> x = new(1, 0, 0);
			Vector3D<TNumber> y = new(0, 1, 0);
			Vector3D<TNumber> z = new(0, 0, 1);

			Assert.AreEqual(z, x * y);
			Assert.AreEqual(-z, y * x);
			Assert.AreEqual(x, y * z);
			Assert.AreEqual(-x, z * y);
			Assert.AreEqual(y, z * x);
			Assert.AreEqual(-y, x * z);
		}
	}
}
