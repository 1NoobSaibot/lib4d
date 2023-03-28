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
				(new(1), new(1), true),
				(new(0, 2), new(0, 2), true),
				(new(-7, 2), new(-7, 2), true),

				(new(0, 2), new(-7, 2), false),
				(new(-7), new(-7, 2), false),
				(new(0), new(-7, 2), false),
			};

			EqualityTestHelper<Complex<TNumber>>.TestEquality(samples);
		}


		[TestMethod]
		public void ConstructorsAreAgreed()
		{
			Complex<TNumber> zero = new();
			Assert.AreEqual(TNumber.Zero, zero.R!);
			Assert.AreEqual(TNumber.Zero, zero.I!);
			Assert.AreEqual(zero, new Complex<TNumber>(0));
			Assert.AreEqual(zero, new Complex<TNumber>(0, 0));
			TNumber z = TNumber.Zero;
			Assert.AreEqual(zero, new Complex<TNumber>(z));
			Assert.AreEqual(zero, new Complex<TNumber>(z, z));

			for (double r = -3; r <= 3; r++)
			{
				var c1 = new Complex<TNumber>(r);
				TNumber nR = Math<TNumber>.Double2Number!(r);
				Assert.AreEqual(nR, c1.R);
				Assert.AreEqual(z, c1.I);

				Assert.AreEqual(c1, new Complex<TNumber>(nR));

				for (double i = -3; i <= 3; i++)
				{
					c1 = new Complex<TNumber>(r, i);
					TNumber nI = Math<TNumber>.Double2Number!(i);

					Assert.AreEqual(nR, c1.R);
					Assert.AreEqual(nI, c1.I);

					Assert.AreEqual(c1, new Complex<TNumber>(nR, nI));
				}
			}
		}


		[TestMethod]
		public void CastTNumberToComplex()
		{
			_cth.ForEachTNum(f =>
			{
				Complex<TNumber> c = (Complex<TNumber>)f;
				Assert.AreEqual(c.R, f);
				Assert.AreEqual(c.I, TNumber.Zero);
			});
		}


		[TestMethod]
		public void CastDoubleToComplex()
		{
			_cth.ForEachFloat(d =>
			{
				Complex<TNumber> c = (Complex<TNumber>)d;
				TNumber nD = Math<TNumber>.Double2Number!(d);
				Assert.AreEqual(c.R, nD);
				Assert.AreEqual(c.I, TNumber.Zero);
			});
		}


		[TestMethod]
		public void Add()
		{
			(Complex<TNumber>, Complex<TNumber>, Complex<TNumber>)[] samples =
			{
				(new(), new(), new()),
				(new(1), new(2), new(3)),
				(new(0, 1), new(0, 2), new(0, 3)),
				(new(1, 5), new(2, 7), new(3, 12))
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

			Complex<TNumber> one = new(1);
			_cth.ForEachComplex(complex =>
			{
				Assert.AreEqual(complex, complex * one);
			});

			Complex<TNumber> i = new(0, 1);
			Assert.AreEqual((Complex<TNumber>)(-1), i * i);
			Assert.AreEqual(new Complex<TNumber>(0, -1), i * -1);
			Assert.AreEqual(one, i * new Complex<TNumber>(0, -1));
		}


		[TestMethod]
		public void UnaryMinus()
		{
			_cth.ForEachComplex(complex =>
			{
				Assert.AreEqual(complex * -1, -complex);
			});
		}


		[TestMethod]
		public void Magnitude()
		{
			Assert.AreEqual(TNumber.Zero, new Complex<TNumber>().Abs());

			Complex<TNumber> one = new(1);
			Assert.AreEqual(TNumber.One, one.Abs());

			Complex<TNumber> negativeOne = new(1);
			Assert.AreEqual(TNumber.One, negativeOne.Abs());

			Complex<TNumber> imaginaryOne = new(0, 1);
			Assert.AreEqual(TNumber.One, imaginaryOne.Abs());

			Complex<TNumber> negativeImaginaryOne = new(0, 1);
			Assert.AreEqual(TNumber.One, negativeImaginaryOne.Abs());

			Complex<TNumber> c = new(-3, 4);
			TNumber c5 = Math<TNumber>.Double2Number!(5);
			Assert.AreEqual(c5, c.Abs());
		}


		[TestMethod]
		public void Div()
		{
			_cth.ForEachPairOfComplex((a, b) =>
			{
				if (b.Abs() == TNumber.Zero)
				{
					return;
				}

				var res = a / b;
				_cth.AssertApproximatelyEqualC(a, res * b);
			});

			_cth.ForEachComplex(complex =>
			{
				_cth.ForEachTNum(i => {
					if (i != TNumber.Zero)
					{
						Assert.AreEqual(complex / (Complex<TNumber>)i, complex / i);
					}
					if (complex.Abs() != TNumber.Zero)
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
					_cth.AssertApproximatelyEqualC(complex, root * root);
				}
				catch (AssertFailedException e)
				{
					throw new AssertFailedException(
						$"Bad square root: argument={complex}, root={root}, root*root={root * root}",
						e
					);
				}
			});

			_cth.ForEachTNum(floatNum =>
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
				_cth.AssertApproximatelyEqualC(expected, complex.AbsQuad());
			});
		}


		[TestMethod]
		public void Exp()
		{
			_cth.AssertApproximatelyEqualC(
				new Complex<TNumber>(1, 0),
				Complex<TNumber>.Exp(new())
			);
			_cth.AssertApproximatelyEqualC(
				new Complex<TNumber>(Math<TNumber>.E, TNumber.Zero),
				Complex<TNumber>.Exp(new Complex<TNumber>(1, 0))
			);

			// e^(i * pi) + 1 = 0   =>   e^(i * pi) = -1
			_cth.AssertApproximatelyEqualC(
				new Complex<TNumber>(-1, 0),
				Complex<TNumber>.Exp(new Complex<TNumber>(TNumber.Zero, Math<TNumber>.PI))
			);
		}
	}
}
