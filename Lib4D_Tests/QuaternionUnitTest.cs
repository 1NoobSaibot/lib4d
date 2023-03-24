using Lib4D;
using Lib4D_Tests.Helpers;

namespace Lib4D_Tests
{
	[TestClass]
	public class QuaternionUnitTest
	{
		private static readonly Complex C_ZERO = new();


		[TestMethod]
		public void Equals()
		{
			(Quaternion, Quaternion, bool)[] samples =
			{
				(new(), new(), true), 
				(new(7), new(7), true),
				(new(i:7), new(i:7), true),
				(new(j:7), new(j:7), true),
				(new(k:7), new(k:7), true),
				(new(1, 2, 3, 4), new(1, 2, 3, 4), true),

				(new(), new(1), false),
				(new(), new(i:1), false),
				(new(), new(j:1), false),
				(new(), new(k:1), false)
			};

			foreach (var sample in samples)
			{
				(var a, var b, var areEqual) = sample;
				Assert.AreEqual(areEqual, a == b);
				Assert.AreEqual(areEqual, a.Equals(b));
				Assert.AreEqual(areEqual, b == a);
				Assert.AreEqual(areEqual, b.Equals(a));

				Assert.AreEqual(!areEqual, a != b);
				Assert.AreEqual(!areEqual, b != a);

				if (areEqual)
				{
					Assert.AreEqual(a, b);
				}
				else
				{
					Assert.AreNotEqual(a, b);
				}
			}
		}


		[TestMethod]
		public void CastComplexToQuaternion()
		{
			ComplexTestHelper.ForEachComplex(c =>
			{
				Quaternion q = (Quaternion)c;
				Assert.AreEqual(q.ri, c);
				Assert.AreEqual(q.jk, C_ZERO);
				Assert.AreEqual(q.R, c.R);
				Assert.AreEqual(q.I, c.I);
				Assert.AreEqual(q.J, 0);
				Assert.AreEqual(q.K, 0);
			});
		}


		[TestMethod]
		public void CastFloatToQuaternion()
		{
			ComplexTestHelper.ForEachFloat(f =>
			{
				Quaternion q = (Quaternion)f;
				Assert.AreEqual(q.R, f);
				Assert.AreEqual(q.I, 0);
				Assert.AreEqual(q.J, 0);
				Assert.AreEqual(q.K, 0);
			});
		}


		[TestMethod]
		public void Add()
		{
			(Quaternion, Quaternion, Quaternion)[] samples =
			{
				(new(), new(), new()),
				(new(1), new(1), new(2)),
				(new(i:1), new(i:1), new(i:2)),
				(new(j:1), new(j:1), new(j:2)),
				(new(k:1), new(k:1), new(k:2)),
				(new(1), new(i:1), new(1, 1, 0, 0)),
				(new(1), new(j:1), new(1, 0, 1, 0)),
				(new(1), new(k:1), new(1, 0, 0, 1)),
				(new(i:1), new(j:1), new(0, 1, 1, 0)),
				(new(i:1), new(k:1), new(0, 1, 0, 1)),
				(new(j:1), new(k:1), new(0, 0, 1, 1)),
				(new(1, -2, 3, -5), new(-7, 11, -13, 17), new(-6, 9, -10, 12))
			};

			foreach (var sample in samples)
			{
				(var a, var b, var sum) = sample;
				Assert.AreEqual(sum, a + b);
				Assert.AreEqual(sum, b + a);
			}

			QuaternionTestHelper.ForEachQuaternion(q =>
			{
				ComplexTestHelper.ForEachComplex(c =>
				{
					Assert.AreEqual(q + (Quaternion)c, q + c);
					Assert.AreEqual(q + (Quaternion)c, c + q);
				});

				ComplexTestHelper.ForEachFloat(f =>
				{
					Assert.AreEqual(q + (Quaternion)f, q + f);
					Assert.AreEqual(q + (Quaternion)f, f + q);
				});
			});
		}


		[TestMethod]
		public void Sub()
		{
			QuaternionTestHelper.ForEachPairOfQuaternion((q1, q2) =>
			{
				var sum = q1 + q2;
				Assert.AreEqual(q1, sum - q2);
			});

			QuaternionTestHelper.ForEachQuaternion(q =>
			{
				ComplexTestHelper.ForEachComplex(c =>
				{
					Assert.AreEqual(q - (Quaternion)c, q - c);
					Assert.AreEqual((Quaternion)c - q, c - q);
				});

				ComplexTestHelper.ForEachFloat(f =>
				{
					Assert.AreEqual(q - (Quaternion)f, q - f);
					Assert.AreEqual((Quaternion)f - q, f - q);
				});
			});
		}


		[TestMethod]
		public void Mul()
		{
			// Works as simple float when all i, j, k are zero
			ComplexTestHelper.ForEachTwoFloats((f1, f2) =>
			{
				Assert.AreEqual((Quaternion)(f1 * f2), (Quaternion)f1 * (Quaternion)f2);
				Assert.AreEqual((Quaternion)(f1 * f2), (Quaternion)f2 * (Quaternion)f1);
			});

			Quaternion zero = new();
			Quaternion two = new(2);
			Quaternion r = new(1);
			Quaternion nr = new(-1);
			Quaternion i = new(i: 1);
			Quaternion ni = new(i: -1);
			Quaternion j = new(j: 1);
			Quaternion nj = new(j: -1);
			Quaternion k = new(k: 1);
			Quaternion nk = new(k: -1);

			QuaternionTestHelper.ForEachQuaternion(q =>
			{
				Assert.AreEqual(zero, q * zero);
				Assert.AreEqual(zero, zero * q);
				Assert.AreEqual(q, q * r);
				Assert.AreEqual(q, r * q);
				Assert.AreEqual(zero - q, q * nr);
				Assert.AreEqual(zero - q, nr * q);
				Assert.AreEqual(q + q, two * q);
			});

			Assert.AreEqual(nr, i * i);
			Assert.AreEqual(r, ni * i);
			Assert.AreEqual(r, i * ni);

			Assert.AreEqual(nr, j * j);
			Assert.AreEqual(r, nj * j);
			Assert.AreEqual(r, j * nj);

			Assert.AreEqual(nr, k * k);
			Assert.AreEqual(r, nk * k);
			Assert.AreEqual(r, k * nk);

			Assert.AreEqual(nr, i * j * k);
			Assert.AreEqual(r, ni * j * k);
			Assert.AreEqual(r, i * nj * k);
			Assert.AreEqual(r, i * j * nk);

			Assert.AreEqual(k, i * j);
			Assert.AreEqual(nk, j * i);
			Assert.AreEqual(nj, i * k);
			Assert.AreEqual(j, k * i);
			Assert.AreEqual(i, j * k);
			Assert.AreEqual(ni, k * j);

			QuaternionTestHelper.ForEachQuaternion(q =>
			{
				ComplexTestHelper.ForEachComplex(c =>
				{
					Assert.AreEqual(q * (Quaternion)c, q * c);
					Assert.AreEqual((Quaternion)c * q, c * q);
				});

				ComplexTestHelper.ForEachFloat(f =>
				{
					Assert.AreEqual(q * (Quaternion)f, q * f);
					Assert.AreEqual((Quaternion)f * q, f * q);
				});
			});
		}


		[TestMethod]
		public void UnaryMinus()
		{
			QuaternionTestHelper.ForEachQuaternion(q =>
			{
				Assert.AreEqual(q * -1, -q);
				Assert.AreEqual(-1 * q, -q);
			});
		}


		[TestMethod]
		public void Abs()
		{
			(Quaternion, double)[] samples =
			{
				(new(), 0),
				(new(1, 0, 0, 0), 1),
				(new(0, 2, 0, 0), 2),
				(new(0, 0, 3, 0), 3),
				(new(0, 0, 0, 4), 4),

				(new(3, 4, 0, 0), 5),
				(new(-4, 3, 0, 0), 5),
				(new(-3, 0, 4, 0), 5),
				(new(3, 0, 0, 4), 5),
				(new(0, 3, -4, 0), 5),
				(new(0, -3, 0, -4), 5),
				(new(0, 0, 3, 4), 5)
			};

			foreach (var sample in samples)
			{
				(var q, var magnitude) = sample;
				Assert.AreEqual(magnitude, q.Abs);
			}
		}


		[TestMethod]
		public void Div()
		{
			QuaternionTestHelper.ForEachPairOfQuaternion((q1, q2) =>
			{
				if (q2.Abs == 0)
				{
					return;
				}

				var m = q1 * q2;
				QuaternionTestHelper.AssertApproximatelyEqual(q1, m / q2);
			});

			QuaternionTestHelper.ForEachQuaternion(q =>
			{
				ComplexTestHelper.ForEachFloat(f =>
				{
					if (f != 0)
					{
						QuaternionTestHelper.AssertApproximatelyEqual(q / (Quaternion)f, q / f);
					}
					if (q.AbsQuad != 0)
					{
						QuaternionTestHelper.AssertApproximatelyEqual((Quaternion)f / q, f / q);
					}
				});

				ComplexTestHelper.ForEachComplex(c =>
				{
					if (c.AbsQuad() != 0)
					{
						QuaternionTestHelper.AssertApproximatelyEqual(q / (Quaternion)c, q / c);
					}
					if (q.AbsQuad != 0)
					{
						QuaternionTestHelper.AssertApproximatelyEqual((Quaternion)c / q, c / q);
					}
				});
			});
		}
	}
}
