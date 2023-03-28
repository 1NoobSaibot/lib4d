using Lib4D;
using Lib4D.Mathematic;
using Lib4D_Tests.Helpers;
using System.Numerics;

namespace Lib4D_Tests.Quaternions
{
	// TODO: No tests for ByAxisAndAngleTest
	public abstract class QuaternionTest<TNumber>
		: MathDependentTest<TNumber>
		where TNumber : INumber<TNumber> 
	{
		private readonly QuaternionTestHelper<TNumber> _qth;

		public QuaternionTest()
		{
			_qth = new QuaternionTestHelper<TNumber>();
		}


		[TestMethod]
		public void Equals()
		{
			(Quaternion<TNumber>, Quaternion<TNumber>, bool)[] samples =
			{
				(new(), new(), true),
				(new(7), new(7), true),
				(new(0, 7), new(0, 7), true),
				(new(0, 0, 7), new(0, 0, 7), true),
				(new(0, 0, 0, 7), new(0, 0, 0, 7), true),
				(new(1, 2, 3, 4), new(1, 2, 3, 4), true),

				(new(), new(1), false),
				(new(), new(0, 1), false),
				(new(), new(0, 0, 1), false),
				(new(), new(0, 0, 0, 1), false)
			};

			EqualityTestHelper<Quaternion<TNumber>>.TestEquality(samples);
		}


		[TestMethod]
		public void ConstructorsAreAgreed()
		{
			var zeroQ = new Quaternion<TNumber>();
			Assert.AreEqual(TNumber.Zero, zeroQ.R);
			Assert.AreEqual(TNumber.Zero, zeroQ.I);
			Assert.AreEqual(TNumber.Zero, zeroQ.J);
			Assert.AreEqual(TNumber.Zero, zeroQ.K);

			Assert.AreEqual(zeroQ, new Quaternion<TNumber>(0));
			Assert.AreEqual(zeroQ, new Quaternion<TNumber>(0, 0));
			Assert.AreEqual(zeroQ, new Quaternion<TNumber>(0, 0, 0));
			Assert.AreEqual(zeroQ, new Quaternion<TNumber>(0, 0, 0, 0));
			var z = TNumber.Zero;
			Assert.AreEqual(zeroQ, new Quaternion<TNumber>(z));
			Assert.AreEqual(zeroQ, new Quaternion<TNumber>(z, z));
			Assert.AreEqual(zeroQ, new Quaternion<TNumber>(z, z, z));
			Assert.AreEqual(zeroQ, new Quaternion<TNumber>(z, z, z, z));
			var zeroC = new Complex<TNumber>();
			Assert.AreEqual(zeroQ, new Quaternion<TNumber>(zeroC));
			Assert.AreEqual(zeroQ, new Quaternion<TNumber>(zeroC, zeroC));

			_qth.ForEachFloat(_d1 => {
				var nD1 = Math<TNumber>.Double2Number!(_d1);
				var q = new Quaternion<TNumber>(_d1);
				Assert.AreEqual(nD1, q.R);
				Assert.AreEqual(TNumber.Zero, q.I);
				Assert.AreEqual(TNumber.Zero, q.J);
				Assert.AreEqual(TNumber.Zero, q.K);

				Assert.AreEqual(q, new Quaternion<TNumber>(nD1));
				Assert.AreEqual(q, new Quaternion<TNumber>(_d1, 0));
				Assert.AreEqual(q, new Quaternion<TNumber>(nD1, z));
				Assert.AreEqual(q, new Quaternion<TNumber>(_d1, 0, 0));
				Assert.AreEqual(q, new Quaternion<TNumber>(nD1, z, z));
				Assert.AreEqual(q, new Quaternion<TNumber>(_d1, 0, 0, 0));
				Assert.AreEqual(q, new Quaternion<TNumber>(nD1, z, z, z));
				Assert.AreEqual(q, new Quaternion<TNumber>(new Complex<TNumber>(_d1)));
				Assert.AreEqual(q, new Quaternion<TNumber>(new Complex<TNumber>(_d1), new()));

				_qth.ForEachFloat(_d2 => {
					var nD2 = Math<TNumber>.Double2Number(_d2);
					q = new Quaternion<TNumber>(_d1, _d2);
					Assert.AreEqual(nD1, q.R);
					Assert.AreEqual(nD2, q.I);
					Assert.AreEqual(TNumber.Zero, q.J);
					Assert.AreEqual(TNumber.Zero, q.K);

					Assert.AreEqual(q, new Quaternion<TNumber>(nD1, nD2));
					Assert.AreEqual(q, new Quaternion<TNumber>(_d1, _d2, 0));
					Assert.AreEqual(q, new Quaternion<TNumber>(nD1, nD2, z));
					Assert.AreEqual(q, new Quaternion<TNumber>(_d1, _d2, 0, 0));
					Assert.AreEqual(q, new Quaternion<TNumber>(nD1, nD2, z, z));
					Assert.AreEqual(q, new Quaternion<TNumber>(new Complex<TNumber>(_d1, _d2)));
					Assert.AreEqual(q, new Quaternion<TNumber>(new Complex<TNumber>(_d1, _d2), new()));

					_qth.ForEachFloat(_d3 => {
						var nD3 = Math<TNumber>.Double2Number(_d3);
						q = new Quaternion<TNumber>(_d1, _d2, _d3);
						Assert.AreEqual(nD1, q.R);
						Assert.AreEqual(nD2, q.I);
						Assert.AreEqual(nD3, q.J);
						Assert.AreEqual(TNumber.Zero, q.K);

						Assert.AreEqual(q, new Quaternion<TNumber>(nD1, nD2, nD3));
						Assert.AreEqual(q, new Quaternion<TNumber>(_d1, _d2, _d3));
						Assert.AreEqual(q, new Quaternion<TNumber>(_d1, _d2, _d3, 0));
						Assert.AreEqual(q, new Quaternion<TNumber>(nD1, nD2, nD3, z));
						Assert.AreEqual(q, new Quaternion<TNumber>(new Complex<TNumber>(_d1, _d2), new(_d3)));

						_qth.ForEachFloat(_d4 => {
							var nD4 = Math<TNumber>.Double2Number(_d4);
							q = new Quaternion<TNumber>(_d1, _d2, _d3, _d4);
							Assert.AreEqual(nD1, q.R);
							Assert.AreEqual(nD2, q.I);
							Assert.AreEqual(nD3, q.J);
							Assert.AreEqual(nD4, q.K);

							Assert.AreEqual(q, new Quaternion<TNumber>(_d1, _d2, _d3, _d4));
							Assert.AreEqual(q, new Quaternion<TNumber>(nD1, nD2, nD3, nD4));
							Assert.AreEqual(q, new Quaternion<TNumber>(new Complex<TNumber>(_d1, _d2), new(_d3, _d4)));
						});
					});
				});
			});
		}



		[TestMethod]
		public void CastComplexToQuaternion()
		{
			_qth.ForEachComplex(c =>
			{
				Quaternion<TNumber> q = (Quaternion<TNumber>)c;
				Assert.AreEqual(q.RI, c);
				Assert.AreEqual(q.JK, new Complex<TNumber>());
				Assert.AreEqual(q.R, c.R);
				Assert.AreEqual(q.I, c.I);
				Assert.AreEqual(q.J, TNumber.Zero);
				Assert.AreEqual(q.K, TNumber.Zero);
			});
		}


		[TestMethod]
		public void CastFloatToQuaternion()
		{
			_qth.ForEachTNum(f =>
			{
				Quaternion<TNumber> q = (Quaternion<TNumber>)f;
				Assert.AreEqual(q.R, f);
				Assert.AreEqual(q.I, TNumber.Zero);
				Assert.AreEqual(q.J, TNumber.Zero);
				Assert.AreEqual(q.K, TNumber.Zero);
			});
		}


		[TestMethod]
		public void Add()
		{
			(Quaternion<TNumber>, Quaternion<TNumber>, Quaternion<TNumber>)[] samples =
			{
				(new(), new(), new()),
				(new(1), new(1), new(2)),
				(new(0, 1), new(0, 1), new(0, 2)),
				(new(0, 0, 1), new(0, 0, 1), new(0, 0, 2)),
				(new(0, 0, 0, 1), new(0, 0, 0, 1), new(0, 0, 0, 2)),
				(new(1), new(0, 1), new(1, 1, 0, 0)),
				(new(1), new(0, 0, 1), new(1, 0, 1, 0)),
				(new(1), new(0, 0, 0, 1), new(1, 0, 0, 1)),
				(new(0, 1), new(0, 0, 1), new(0, 1, 1, 0)),
				(new(0, 1), new(0, 0, 0, 1), new(0, 1, 0, 1)),
				(new(0, 0, 1), new(0, 0, 0, 1), new(0, 0, 1, 1)),
				(new(1, -2, 3, -5), new(-7, 11, -13, 17), new(-6, 9, -10, 12))
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

				_qth.ForEachTNum(f =>
				{
					Assert.AreEqual(q + (Quaternion<TNumber>)f, q + f);
					Assert.AreEqual(q + (Quaternion<TNumber>)f, f + q);
				});
			});
		}


		[TestMethod]
		public void Sub()
		{
			_qth.ForEachPairOfQuaternion((q1, q2) =>
			{
				var sum = q1 + q2;
				Assert.AreEqual(q1, sum - q2);
			});

			_qth.ForEachQuaternion(q =>
			{
				_qth.ForEachComplex(c =>
				{
					Assert.AreEqual(q - (Quaternion<TNumber>)c, q - c);
					Assert.AreEqual((Quaternion<TNumber>)c - q, c - q);
				});

				_qth.ForEachTNum(f =>
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
			_qth.ForEachTwoTNums((f1, f2) =>
			{
				Assert.AreEqual((Quaternion<TNumber>)(f1 * f2), (Quaternion<TNumber>)f1 * (Quaternion<TNumber>)f2);
				Assert.AreEqual((Quaternion<TNumber>)(f1 * f2), (Quaternion<TNumber>)f2 * (Quaternion<TNumber>)f1);
			});

			Quaternion<TNumber> zero = new();
			Quaternion<TNumber> two = new(2);
			Quaternion<TNumber> r = new(1);
			Quaternion<TNumber> nr = new(-1);
			Quaternion<TNumber> i = new(0, 1);
			Quaternion<TNumber> ni = new(0, -1);
			Quaternion<TNumber> j = new(0, 0, 1);
			Quaternion<TNumber> nj = new(0, 0, -1);
			Quaternion<TNumber> k = new(0, 0, 0, 1);
			Quaternion<TNumber> nk = new(0, 0, 0, -1);

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

				_qth.ForEachTNum(f =>
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
				Assert.AreEqual(q * -1, -q);
				Assert.AreEqual(-1 * q, -q);
			});
		}


		[TestMethod]
		public void Abs()
		{
			(Quaternion<TNumber>, double)[] samples =
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
				var q = sample.Item1;
				TNumber magnitude = Math<TNumber>.Double2Number!(sample.Item2);
				Assert.AreEqual(magnitude, q.Abs);
			}
		}


		[TestMethod]
		public void Div()
		{
			_qth.ForEachPairOfQuaternion((q1, q2) =>
			{
				if (q2.AbsQuad == TNumber.Zero)
				{
					return;
				}

				var m = q1 * q2;
				_qth.AssertApproximatelyEqual(q1, m / q2);
			});

			_qth.ForEachQuaternion(q =>
			{
				_qth.ForEachTNum(f =>
				{
					if (f != TNumber.Zero)
					{
						_qth.AssertApproximatelyEqual(q / (Quaternion<TNumber>)f, q / f);
					}
					if (q.AbsQuad != TNumber.Zero)
					{
						_qth.AssertApproximatelyEqual((Quaternion<TNumber>)f / q, f / q);
					}
				});

				_qth.ForEachComplex(c =>
				{
					if (c.AbsQuad() != TNumber.Zero)
					{
						_qth.AssertApproximatelyEqual(q / (Quaternion<TNumber>)c, q / c);
					}
					if (q.AbsQuad != TNumber.Zero)
					{
						_qth.AssertApproximatelyEqual((Quaternion<TNumber>)c / q, c / q);
					}
				});
			});
		}
	}
}
