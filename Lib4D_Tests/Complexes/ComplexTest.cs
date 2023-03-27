using Lib4D;
using Lib4D.Mathematic;
using Lib4D_Tests.Helpers;
using System.Numerics;

namespace Lib4D_Tests.Complexes
{
	public abstract class ComplexTest<TNumber>
		: NumberSet<TNumber>
		where TNumber : INumber<TNumber>
	{
		protected abstract Math<TNumber> GetMath();
		private readonly ComplexTestHelper<TNumber> _cth;


		public ComplexTest() {
			Math<TNumber>.InitInstance(GetMath());
			_cth = new ComplexTestHelper<TNumber>();
		}


		[TestMethod]
		public void Equals()
		{
			(Complex<TNumber>, Complex<TNumber>, bool)[] samples =
			{
				(new(), new(), true),
				(new(c1), new(c1), true),
				(new(c0, c2), new(c0, c2), true),
				(new(-c7, c2), new(-c7, c2), true),

				(new(c0, c2), new(-c7, c2), false),
				(new(-c7), new(-c7, c2), false),
				(new(c0), new(-c7, c2), false),
			};


			for (int i = 0; i < samples.Length; i++)
			{
				(var a, var b, var isEqual) = samples[i];
				Assert.AreEqual(isEqual, a == b);
				Assert.AreEqual(isEqual, b == a);
				Assert.AreEqual(isEqual, a.Equals(b));
				Assert.AreEqual(isEqual, b.Equals(a));

				Assert.AreEqual(!isEqual, a != b);
				Assert.AreEqual(!isEqual, b != a);

				if (isEqual)
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
		public void CastFloatToComplex()
		{
			_cth.ForEachFloat(i =>
			{
				Complex<TNumber> c = (Complex<TNumber>)i;
				Assert.AreEqual(c.R, i);
				Assert.AreEqual(c.I, c0);
			});
		}


		[TestMethod]
		public void Add()
		{
			(Complex<TNumber>, Complex<TNumber>, Complex<TNumber>)[] samples =
			{
				(new(), new(), new()),
				(new(c1), new(c2), new(c3)),
				(new(c0, c1), new(c0, c2), new(c0, c3)),
				(new(c1, c5), new(c2, c7), new(c3, c12))
			};

			for (int i = 0; i < samples.Length; i++)
			{
				(var a, var b, var c) = samples[i];
				Assert.AreEqual(c, a + b);
				Assert.AreEqual(c, b + a);
			}

			_cth.ForEachComplex(complex =>
			{
				_cth.ForEachFloat(floatNum =>
				{
					Complex<TNumber> expectedSum = complex + (Complex<TNumber>)floatNum;
					Assert.AreEqual(expectedSum, complex + floatNum);
					Assert.AreEqual(expectedSum, floatNum + complex);
				});
			});
		}


		[TestMethod]
		public void Sub()
		{
			_cth.ForEachPairOfComplex((a, b) =>
			{
				Complex<TNumber> sum = a + b;
				Assert.AreEqual(a, sum - b);
				Assert.AreEqual(b, sum - a);
			});

			_cth.ForEachComplex(complex =>
			{
				_cth.ForEachFloat(floatNum =>
				{
					Assert.AreEqual(complex - (Complex<TNumber>)floatNum, complex - floatNum);
					Assert.AreEqual((Complex<TNumber>)floatNum - complex, floatNum - complex);
				});
			});
		}


		[TestMethod]
		public void Mul()
		{
			// Is commutative
			_cth.ForEachPairOfComplex((a, b) =>
			{
				Assert.AreEqual(a * b, b * a);
			});

			_cth.ForEachComplex(complex =>
			{
				_cth.ForEachFloat(floatNum =>
				{
					var expected = complex * (Complex<TNumber>)floatNum;
					Assert.AreEqual(expected, complex * floatNum);
					Assert.AreEqual(expected, floatNum * complex);
				});
			});

			Complex<TNumber> zero = new();
			_cth.ForEachComplex(complex =>
			{
				Assert.AreEqual(zero, complex * zero);
			});

			Complex<TNumber> one = new(c1);
			_cth.ForEachComplex(complex =>
			{
				Assert.AreEqual(complex, complex * one);
			});

			Complex<TNumber> i = new(c0, c1);
			Assert.AreEqual((Complex<TNumber>)(-c1), i * i);
			Assert.AreEqual(new Complex<TNumber>(c0, -c1), i * -c1);
			Assert.AreEqual(one, i * new Complex<TNumber>(c0, -c1));
		}


		[TestMethod]
		public void UnaryMinus()
		{
			_cth.ForEachComplex(complex =>
			{
				Assert.AreEqual(complex * -c1, -complex);
			});
		}


		[TestMethod]
		public void Magnitude()
		{
			Assert.AreEqual(c0, new Complex<TNumber>().Abs());

			Complex<TNumber> one = new(c1);
			Assert.AreEqual(c1, one.Abs());

			Complex<TNumber> negativeOne = new(c1);
			Assert.AreEqual(c1, negativeOne.Abs());

			Complex<TNumber> imaginaryOne = new(c0, c1);
			Assert.AreEqual(c1, imaginaryOne.Abs());

			Complex<TNumber> negativeImaginaryOne = new(c0, c1);
			Assert.AreEqual(c1, negativeImaginaryOne.Abs());

			Complex<TNumber> c = new(-c3, c4);
			Assert.AreEqual(c5, c.Abs());
		}


		[TestMethod]
		public void Div()
		{
			_cth.ForEachPairOfComplex((a, b) =>
			{
				if (b.Abs() == c0)
				{
					return;
				}

				var res = a / b;
				_cth.AssertApproximatelyEqual(a, res * b);
			});

			_cth.ForEachComplex(complex =>
			{
				_cth.ForEachFloat(i => {
					if (i != c0)
					{
						Assert.AreEqual(complex / (Complex<TNumber>)i, complex / i);
					}
					if (complex.Abs() != c0)
					{
						Assert.AreEqual((Complex<TNumber>)i / complex, i / complex);
					}
				});
			});
		}


		[TestMethod]
		public void Sqrt()
		{
			_cth.ForEachComplex(complex =>
			{
				var root = complex.Sqrt();
				try
				{
					_cth.AssertApproximatelyEqual(complex, root * root);
				}
				catch (AssertFailedException e)
				{
					throw new AssertFailedException(
						$"Bad square root: argument={complex}, root={root}, root*root={root * root}",
						e
					);
				}
			});

			_cth.ForEachFloat(floatNum =>
			{
				Assert.AreEqual(
					((Complex<TNumber>)floatNum).Sqrt(),
					Complex<TNumber>.Sqrt(floatNum)
				);
			});
		}


		[TestMethod]
		public void AbsQuad()
		{
			_cth.ForEachComplex(complex => {
				TNumber expected = complex.Abs();
				expected *= expected;
				_cth.AssertApproximatelyEqual(expected, complex.AbsQuad());
			});
		}


		[TestMethod]
		public void Exp()
		{
			_cth.AssertApproximatelyEqual(
				new Complex<TNumber>(c1, c0),
				Complex<TNumber>.Exp(new())
			);
			_cth.AssertApproximatelyEqual(
				new Complex<TNumber>(Math<TNumber>.E, c0),
				Complex<TNumber>.Exp(new Complex<TNumber>(c1, c0))
			);

			// e^(i * pi) + 1 = 0   =>   e^(i * pi) = -1
			_cth.AssertApproximatelyEqual(
				new Complex<TNumber>(-c1, c0),
				Complex<TNumber>.Exp(new Complex<TNumber>(c0, Math<TNumber>.PI))
			);
		}
	}
}
