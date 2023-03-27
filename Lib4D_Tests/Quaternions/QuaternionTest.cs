using Lib4D;
using Lib4D_Tests.Helpers;
using System.Numerics;

namespace LibFOURD_Tests.Quaternions
{
	public abstract class QuaternionTest<TNumber>
		: NumberSet<TNumber>
		where TNumber : INumber<TNumber> 
	{
		private readonly QuaternionTestHelper<TNumber> _qth;
		private readonly Func<Quaternion<TNumber>, TNumber> _absQ;

		public QuaternionTest(
			Func<Quaternion<TNumber>, TNumber> absQ
		)
		{
			InitMath();
			_qth = new QuaternionTestHelper<TNumber>();
			_absQ = absQ;
		}


		protected abstract void InitMath();


		[TestMethod]
		public void Equals()
		{
			(Quaternion<TNumber>, Quaternion<TNumber>, bool)[] samples =
			{
				(new(), new(), true),
				(new(c7), new(c7), true),
				(new(c0, c7), new(c0, c7), true),
				(new(c0, c0, c7), new(c0, c0, c7), true),
				(new(c0, c0, c0, c7), new(c0, c0, c0, c7), true),
				(new(c1, c2, c3, c4), new(c1, c2, c3, c4), true),

				(new(), new(c1), false),
				(new(), new(c0, c1), false),
				(new(), new(c0, c0, c1), false),
				(new(), new(c0, c0, c0, c1), false)
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
			_qth.ForEachComplex(c =>
			{
				Quaternion<TNumber> q = (Quaternion<TNumber>)c;
				Assert.AreEqual(q.ri, c);
				Assert.AreEqual(q.jk, new Complex<TNumber>());
				Assert.AreEqual(q.R, c.R);
				Assert.AreEqual(q.I, c.I);
				Assert.AreEqual(q.J, c0);
				Assert.AreEqual(q.K, c0);
			});
		}


		[TestMethod]
		public void CastFloatToQuaternion()
		{
			_qth.ForEachFloat(f =>
			{
				Quaternion<TNumber> q = (Quaternion<TNumber>)f;
				Assert.AreEqual(q.R, f);
				Assert.AreEqual(q.I, c0);
				Assert.AreEqual(q.J, c0);
				Assert.AreEqual(q.K, c0);
			});
		}


		[TestMethod]
		public void Add()
		{
			(Quaternion<TNumber>, Quaternion<TNumber>, Quaternion<TNumber>)[] samples =
			{
				(new(), new(), new()),
				(new(c1), new(c1), new(c2)),
				(new(c0, c1), new(c0, c1), new(c0, c2)),
				(new(c0, c0, c1), new(c0, c0, c1), new(c0, c0, c2)),
				(new(c0, c0, c0, c1), new(c0, c0, c0, c1), new(c0, c0, c0, c2)),
				(new(c1), new(c0, c1), new(c1, c1, c0, c0)),
				(new(c1), new(c0, c0, c1), new(c1, c0, c1, c0)),
				(new(c1), new(c0, c0, c0, c1), new(c1, c0, c0, c1)),
				(new(c0, c1), new(c0, c0, c1), new(c0, c1, c1, c0)),
				(new(c0, c1), new(c0, c0, c0, c1), new(c0, c1, c0, c1)),
				(new(c0, c0, c1), new(c0, c0, c0, c1), new(c0, c0, c1, c1)),
				(new(c1, -c2, c3, -c5), new(-c7, c11, -c13, c17), new(-c6, c9, -c10, c12))
			};

			foreach (var sample in samples)
			{
				(var a, var b, var sum) = sample;
				Assert.AreEqual(sum, a + b);
				Assert.AreEqual(sum, b + a);
			}

			_qth.ForEachQuaternion(q =>
			{
				_qth.ForEachComplex(c =>
				{
					Assert.AreEqual(q + (Quaternion<TNumber>)c, q + c);
					Assert.AreEqual(q + (Quaternion<TNumber>)c, c + q);
				});

				_qth.ForEachFloat(f =>
				{
					Assert.AreEqual(q + (Quaternion<TNumber>)f, q + f);
					Assert.AreEqual(q + (Quaternion<TNumber>)f, f + q);
				});
			});
		}


		[TestMethod]
		public void Sub()
		{
			_qth.ForEachPairOfQuaternion((qONE, qTWO) =>
			{
				var sum = qONE + qTWO;
				Assert.AreEqual(qONE, sum - qTWO);
			});

			_qth.ForEachQuaternion(q =>
			{
				_qth.ForEachComplex(c =>
				{
					Assert.AreEqual(q - (Quaternion<TNumber>)c, q - c);
					Assert.AreEqual((Quaternion<TNumber>)c - q, c - q);
				});

				_qth.ForEachFloat(f =>
				{
					Assert.AreEqual(q - (Quaternion<TNumber>)f, q - f);
					Assert.AreEqual((Quaternion<TNumber>)f - q, f - q);
				});
			});
		}


		[TestMethod]
		public void Mul()
		{
			// Works as simple float when all i, j, k are zero
			_qth.ForEachTwoFloats((fONE, fTWO) =>
			{
				Assert.AreEqual((Quaternion<TNumber>)(fONE * fTWO), (Quaternion<TNumber>)fONE * (Quaternion<TNumber>)fTWO);
				Assert.AreEqual((Quaternion<TNumber>)(fONE * fTWO), (Quaternion<TNumber>)fTWO * (Quaternion<TNumber>)fONE);
			});

			Quaternion<TNumber> zero = new();
			Quaternion<TNumber> two = new(c2);
			Quaternion<TNumber> r = new(c1);
			Quaternion<TNumber> nr = new(-c1);
			Quaternion<TNumber> i = new(c0, c1);
			Quaternion<TNumber> ni = new(c0, -c1);
			Quaternion<TNumber> j = new(c0, c0, c1);
			Quaternion<TNumber> nj = new(c0, c0, -c1);
			Quaternion<TNumber> k = new(c0, c0, c0, c1);
			Quaternion<TNumber> nk = new(c0, c0, c0, -c1);

			_qth.ForEachQuaternion(q =>
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

			_qth.ForEachQuaternion(q =>
			{
				_qth.ForEachComplex(c =>
				{
					Assert.AreEqual(q * (Quaternion<TNumber>)c, q * c);
					Assert.AreEqual((Quaternion<TNumber>)c * q, c * q);
				});

				_qth.ForEachFloat(f =>
				{
					Assert.AreEqual(q * (Quaternion<TNumber>)f, q * f);
					Assert.AreEqual((Quaternion<TNumber>)f * q, f * q);
				});
			});
		}


		[TestMethod]
		public void UnaryMinus()
		{
			_qth.ForEachQuaternion(q =>
			{
				Assert.AreEqual(q * -c1, -q);
				Assert.AreEqual(-c1 * q, -q);
			});
		}


		[TestMethod]
		public void Abs()
		{
			(Quaternion<TNumber>, TNumber)[] samples =
			{
				(new(), c0),
				(new(c1, c0, c0, c0), c1),
				(new(c0, c2, c0, c0), c2),
				(new(c0, c0, c3, c0), c3),
				(new(c0, c0, c0, c4), c4),

				(new(c3, c4, c0, c0), c5),
				(new(-c4, c3, c0, c0), c5),
				(new(-c3, c0, c4, c0), c5),
				(new(c3, c0, c0, c4), c5),
				(new(c0, c3, -c4, c0), c5),
				(new(c0, -c3, c0, -c4), c5),
				(new(c0, c0, c3, c4), c5)
			};

			foreach (var sample in samples)
			{
				(var q, var magnitude) = sample;
				Assert.AreEqual(magnitude, _absQ(q));
			}
		}


		[TestMethod]
		public void Div()
		{
			_qth.ForEachPairOfQuaternion((qONE, qTWO) =>
			{
				if (qTWO.AbsQuad == c0)
				{
					return;
				}

				var m = qONE * qTWO;
				_qth.AssertApproximatelyEqual(qONE, m / qTWO);
			});

			_qth.ForEachQuaternion(q =>
			{
				_qth.ForEachFloat(f =>
				{
					if (f != c0)
					{
						_qth.AssertApproximatelyEqual(q / (Quaternion<TNumber>)f, q / f);
					}
					if (q.AbsQuad != c0)
					{
						_qth.AssertApproximatelyEqual((Quaternion<TNumber>)f / q, f / q);
					}
				});

				_qth.ForEachComplex(c =>
				{
					if (c.AbsQuad() != c0)
					{
						_qth.AssertApproximatelyEqual(q / (Quaternion<TNumber>)c, q / c);
					}
					if (q.AbsQuad != c0)
					{
						_qth.AssertApproximatelyEqual((Quaternion<TNumber>)c / q, c / q);
					}
				});
			});
		}
	}
}
