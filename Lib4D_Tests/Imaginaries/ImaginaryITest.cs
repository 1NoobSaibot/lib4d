using Lib4D;
using Lib4D.Mathematic;
using Lib4D_Tests.Helpers;
using System.Numerics;

namespace Lib4D_Tests.Imaginaries
{
	public abstract class ImaginaryITest<TNumber> where TNumber : INumber<TNumber>
	{
		protected abstract Math<TNumber> GetMath();
		private readonly ComplexTestHelper<TNumber> _cth;


		public ImaginaryITest()
		{
			Math<TNumber>.InitInstance(GetMath());
			_cth = new ComplexTestHelper<TNumber>();
		}


		[TestMethod]
		public void Equals()
		{
			(ImaginaryI<TNumber>, ImaginaryI<TNumber>, bool)[] samples =
			[
				(new(), new(), true),
				(new(2), new(2), true),
				(new(0), new(2), false)
			];

			EqualityTestHelper<ImaginaryI<TNumber>>.TestEquality(samples);
		}


		[TestMethod]
		public void ConstructorsAreAgreed()
		{
			ImaginaryI<TNumber> zero = new();
			Assert.AreEqual(TNumber.Zero, zero.Value);
			Assert.AreEqual(zero, new ImaginaryI<TNumber>(0));

			TNumber z = TNumber.Zero;
			Assert.AreEqual(zero, new ImaginaryI<TNumber>(z));

			for (double i = -3; i <= 3; i++)
			{
				var c1 = new ImaginaryI<TNumber>(i);
				TNumber nI = Math<TNumber>.Double2Number!(i);

				Assert.AreEqual(nI, c1.Value);
				Assert.AreEqual(c1, new ImaginaryI<TNumber>(nI));
			}
		}


		[TestMethod]
		public void AdditionWorksAccordingToComplex()
		{
			_cth.ForEachTwoTNums((a, b) =>
			{
				TestTwoImaginaries(a, b);
				TestRealAndImaginary(a, b);

				Complex<TNumber> expectedSum = a + (Complex<TNumber>)b;
				Assert.AreEqual(expectedSum, a + b);
				Assert.AreEqual(expectedSum, b + a);
			});


			static void TestTwoImaginaries(TNumber a, TNumber b)
			{
				ImaginaryI<TNumber> ia = new(a);
				ImaginaryI<TNumber> ib = new(b);
				ImaginaryI<TNumber> ic = ia + ib;
				Assert.AreEqual(a + b, ic.Value);
			}


			static void TestRealAndImaginary(TNumber real, TNumber b)
			{
				ImaginaryI<TNumber> ib = new(b);
				Complex<TNumber> cReal = new(real: real);
				Complex<TNumber> cImaginary = new(real: TNumber.Zero, imaginary: b);

				Complex<TNumber> actual = real + ib;
				Complex<TNumber> expected = cReal + cImaginary;
				Assert.AreEqual(expected, actual);

				actual = ib + real; // Swaping arguments;
				Assert.AreEqual(expected, actual);
			}
		}


		[TestMethod]
		public void Sub()
		{
			_cth.ForEachTwoTNums((a, b) =>
			{
				TestImaginaryMinusImaginary(a, b);
				TestImaginaryMinusRealAndOpposite(a, b);
			});


			static void TestImaginaryMinusImaginary(TNumber a, TNumber b)
			{
				ImaginaryI<TNumber> ia = new(a);
				ImaginaryI<TNumber> ib = new(b);
				ImaginaryI<TNumber> actual = ia - ib;

				Assert.AreEqual(actual.Value, a - b);
			}


			static void TestImaginaryMinusRealAndOpposite(TNumber i, TNumber r)
			{
				ImaginaryI<TNumber> imaginary = new(i);
				Complex<TNumber> imaginaryAsComplex = new(real: TNumber.Zero, i);
				Complex<TNumber> realAsComplex = new(real: r);

				Complex<TNumber> actual = imaginary - r;
				Complex<TNumber> expected = imaginaryAsComplex - realAsComplex;
				Assert.AreEqual(expected, actual);

				actual = r - imaginary;
				expected = realAsComplex - imaginaryAsComplex;
				Assert.AreEqual(expected, actual);
			}
		}


		[TestMethod]
		public void Mul()
		{
			_cth.ForEachImaginary(i =>
			{
				_cth.ForEachImaginary(i2 =>
				{
					TNumber actual = i * i2;

					Complex<TNumber> c1 = new(real: TNumber.Zero, imaginary: i.Value);
					Complex<TNumber> c2 = new(real: TNumber.Zero, imaginary: i2.Value);
					Complex<TNumber> expected = c1 * c2;
					Assert.AreEqual(TNumber.Zero, expected.I.Value);
					Assert.AreEqual(expected.R, actual);
				});


				_cth.ForEachTNum(r =>
				{
					Complex<TNumber> ci = new(real: TNumber.Zero, imaginary: i.Value);
					Complex<TNumber> cr = new(real: r, imaginary: TNumber.Zero);

					ImaginaryI<TNumber> actual = i * r;
					Complex<TNumber> expected = ci * cr;
					Assert.AreEqual(TNumber.Zero, expected.R);
					Assert.AreEqual(expected.I, actual);

					actual = r * i;
					Assert.AreEqual(TNumber.Zero, expected.R);
					Assert.AreEqual(expected.I, actual);
				});
			});

		}


		[TestMethod]
		public void UnaryMinus()
		{
			_cth.ForEachImaginary(i =>
			{
				Assert.AreEqual(-(i.Value), (-i).Value);
			});
		}


		[TestMethod]
		public void Div()
		{
			_cth.ForEachImaginary((i1) =>
			{
				_cth.ForEachImaginary((i2) =>
				{
					if (i2.Value == TNumber.Zero)
					{
						return;
					}

					TNumber actual = i1 / i2;

					Complex<TNumber> c1 = new(TNumber.Zero, imaginary: i1.Value);
					Complex<TNumber> c2 = new(TNumber.Zero, imaginary: i2.Value);
					Complex<TNumber> expected = c1 / c2;

					Assert.AreEqual(TNumber.Zero, expected.I.Value);
					Assert.AreEqual(expected.R, actual);
				});

				_cth.ForEachTNum(r =>
				{
					if (r != TNumber.Zero)
					{
						TestIDivR(i1, r);
					}
					if (i1.Value != TNumber.Zero)
					{
						TestRDivI(r, i1);
					}
				});
				// _cth.AssertApproximatelyEqualC(a, res * b);
			});


			static void TestIDivR(ImaginaryI<TNumber> i, TNumber r)
			{
				ImaginaryI<TNumber> actual = i / r;
				Assert.AreEqual(i.Value / r, actual.Value);

				Complex<TNumber> ci = new(real: TNumber.Zero, imaginary: i.Value);
				Complex<TNumber> cr = new(real: r, imaginary: TNumber.Zero);
				Complex<TNumber> expected = ci / cr;

				Assert.AreEqual(TNumber.Zero, expected.R);
				Assert.AreEqual(expected.I, actual);
			}


			static void TestRDivI(TNumber r, ImaginaryI<TNumber> i)
			{
				ImaginaryI<TNumber> actual = r / i;
				Assert.AreEqual(-(r / i.Value), actual.Value);

				Complex<TNumber> ci = new(real: TNumber.Zero, imaginary: i.Value);
				Complex<TNumber> cr = new(real: r, imaginary: TNumber.Zero);
				Complex<TNumber> expected = cr / ci;

				Assert.AreEqual(TNumber.Zero, expected.R);
				Assert.AreEqual(expected.I, actual);
			}
		}


		/*[TestMethod]
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
			_cth.ForEachComplex(complex =>
			{
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
		}*/
	}
}
