using Lib4D;
using Lib4D_Tests.Helpers;
using System.Numerics;

namespace Lib4D_Tests.Vectors._2D
{
	public abstract class Vector2DTest<TNum>
		: MathDependentTest<TNum>
		where TNum : INumber<TNum>
	{
		private readonly VectorTestHelper<TNum> _vth = new();

		[TestMethod]
		public void Equals()
		{
			(Vector2D<TNum>, Vector2D<TNum>, bool)[] samples =
			{
				(new(), new(), true),
				(new(1), new(1), true),
				(new(-1), new(-1), true),
				(new(0, 1), new(0, 1), true),
				(new(0, -1), new(0, -1), true),
				(new(1, 1), new(1, 1), true),
				(new(-1, 1), new(-1, 1), true),
				(new(1, -1), new(1, -1), true),
				(new(-1, -1), new(-1, -1), true),

				(new(), new(1), false),
				(new(), new(-1), false),
				(new(1), new(-1), false),
				(new(0, 0), new(0, 1), false),
				(new(0, 0), new(0, -1), false),
				(new(0, 1), new(0, -1), false),
			};

			// TODO: This code is common between a lot of tests here
			foreach (var sample in samples)
			{
				(var v1, var v2, var areEqual) = sample;
				Assert.AreEqual(areEqual, v1 == v2);
				Assert.AreEqual(areEqual, v2 == v1);
				Assert.AreEqual(!areEqual, v1 != v2);
				Assert.AreEqual(!areEqual, v2 != v1);
				Assert.AreEqual(areEqual, v1.Equals(v2));
				Assert.AreEqual(areEqual, v2.Equals(v1));

				if (areEqual)
				{
					Assert.AreEqual(v1, v2);
				}
				else
				{
					Assert.AreNotEqual(v1, v2);
				}
			}
		}


		[TestMethod]
		public void ConstructorsMustBeAgreed()
		{
			var zeroV = new Vector2D<TNum>();
			Assert.AreEqual(TNum.Zero, zeroV.X!);
			Assert.AreEqual(TNum.Zero, zeroV.Y!);

			Assert.AreEqual(new Vector2D<TNum>(), new Vector2D<TNum>(0));
			Assert.AreEqual(new Vector2D<TNum>(), new Vector2D<TNum>(0, 0));
			Assert.AreEqual(new Vector2D<TNum>(1), new Vector2D<TNum>(1, 0));

			var z = TNum.Zero;
			var o = TNum.One;
			Assert.AreEqual(new Vector2D<TNum>(), new Vector2D<TNum>(z));
			Assert.AreEqual(new Vector2D<TNum>(), new Vector2D<TNum>(z, z));
			Assert.AreEqual(new Vector2D<TNum>(o), new Vector2D<TNum>(o, z));
		}


		[TestMethod]
		public void Add()
		{
			(Vector2D<TNum>, Vector2D<TNum>, Vector2D<TNum>)[] samples =
			{
				(new(), new(), new()),

				(new(1), new(1), new(2)),
				(new(-1), new(-1), new(-2)),
				(new(1), new(-1), new()),

				(new(0, 1), new(0, 1), new(0, 2)),
				(new(0, -1), new(0, -1), new(0, -2)),
				(new(0, 1), new(0, -1), new()),

				(new(1, -1), new(1, -1), new(2, -2)),
				(new(1, -1), new(-1, 1), new())
			};

			foreach (var sample in samples)
			{
				(var v1, var v2, var res) = sample;
				Assert.AreEqual(res, v1 + v2);
				Assert.AreEqual(res, v2 + v1);
			}

			var zero = new Vector2D<TNum>();
			_vth.ForEachVector2D(v =>
			{
				Assert.AreEqual(v, v + zero);
			});
		}


		[TestMethod]
		public void Sub()
		{
			_vth.ForEachPairOfVectors2D((v1, v2) =>
			{
				var sum = v1 + v2;
				Assert.AreEqual(v1, sum - v2);
				Assert.AreEqual(v2, sum - v1);
			});
		}
	}
}
