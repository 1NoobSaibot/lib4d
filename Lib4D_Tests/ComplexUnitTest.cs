using Lib4D;
using Lib4D_Tests.Helpers;
using System.Numerics;

namespace Lib4D_Tests
{
	[TestClass]
	public class ComplexUnitTest : IComplexTest
	{
		private readonly IReadOnlyList<IComplexTest> _tests = new IComplexTest[]
		{
			new TypedComplexTest<float>(
				MathF.E,
				MathF.PI,
				Complex<float>.Abs,
				MathF.Abs,
				Complex<float>.Sqrt,
				Complex<float>.Sqrt,
				Complex<float>.Exp
			),
			new TypedComplexTest<double>(
				Math.E,
				Math.PI,
				Complex<double>.Abs,
				Math.Abs,
				Complex<double>.Sqrt,
				Complex<double>.Sqrt,
				Complex<double>.Exp
			)
		};

		[TestMethod]
		public void Equals()
		{
			ForEachType(test => test.Equals());
		}


		[TestMethod]
		public void CastFloatToComplex()
		{
			ForEachType(test => test.CastFloatToComplex());
		}


		[TestMethod]
		public void Add()
		{
			ForEachType(test => test.Add());
		}


		[TestMethod]
		public void Sub()
		{
			ForEachType(test => test.Sub());
		}


		[TestMethod]
		public void Mul()
		{
			ForEachType(test => test.Mul());
		}


		[TestMethod]
		public void UnaryMinus()
		{
			ForEachType(test => test.UnaryMinus());
		}


		[TestMethod]
		public void Magnitude()
		{
			ForEachType(test => test.Magnitude());
		}


		[TestMethod]
		public void Div()
		{
			ForEachType(test => test.Div());
		}


		[TestMethod]
		public void Sqrt()
		{
			ForEachType(test => test.Sqrt());
		}


		[TestMethod]
		public void AbsQuad()
		{
			ForEachType(test => test.AbsQuad());
		}



		[TestMethod]
		public void Exp()
		{
			ForEachType(test => test.Exp());
		}

		private void ForEachType(Action<IComplexTest> action)
		{
			foreach (var test in _tests)
			{
				action(test);
				/*try
				{
					
				}
				catch (Exception ex)
				{
					throw new AssertFailedException($"{test.GetType()}: {ex.Message}", ex);
				}*/
			}
		}


		private class TypedComplexTest<TNumber> : IComplexTest
			where TNumber : INumber<TNumber>
		{
			private readonly ComplexTestHelper<TNumber> _cth;
			private readonly Func<Complex<TNumber>, TNumber> _getAbs;
			private readonly Func<Complex<TNumber>, Complex<TNumber>> _getSqrtC;
			private readonly Func<TNumber, Complex<TNumber>> _getSqrtF;
			private readonly Func<Complex<TNumber>, Complex<TNumber>> _getExp;
			private readonly TNumber ZERO = TNumber.Zero;
			private readonly TNumber ONE = TNumber.One;
			private readonly TNumber TWO;
			private readonly TNumber THREE;
			private readonly TNumber FOUR;
			private readonly TNumber FIVE;
			private readonly TNumber SEVEN;
			private readonly TNumber TWELVE;
			private readonly TNumber E;
			private readonly TNumber PI;

			public TypedComplexTest(
				TNumber E,
				TNumber PI,
				Func<Complex<TNumber>, TNumber> getAbsC,
				Func<TNumber, TNumber> getAbsF,
				Func<Complex<TNumber>, Complex<TNumber>> getSqrtC,
				Func<TNumber, Complex<TNumber>> getSqrtF,
				Func<Complex<TNumber>, Complex<TNumber>> getExp
			)
			{
				this.E = E;
				this.PI = PI;
				TWO = ONE + ONE;
				THREE = TWO + ONE;
				FOUR = THREE + ONE;
				FIVE = TWO + THREE;
				SEVEN = TWO + FIVE;
				TWELVE = SEVEN + FIVE;
				_cth = new ComplexTestHelper<TNumber>(getAbsF);
				_getAbs = getAbsC;
				_getSqrtC = getSqrtC;
				_getSqrtF = getSqrtF;
				_getExp = getExp;
			}


			public void Equals()
			{
				(Complex<TNumber>, Complex<TNumber>, bool)[] samples =
				{
				(new(), new(), true),
				(new(ONE), new(ONE), true),
				(new(ZERO, TWO), new(ZERO, TWO), true),
				(new(-SEVEN, TWO), new(-SEVEN, TWO), true),

				(new(ZERO, TWO), new(-SEVEN, TWO), false),
				(new(-SEVEN), new(-SEVEN, TWO), false),
				(new(ZERO), new(-SEVEN, TWO), false),
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


			public void CastFloatToComplex()
			{
				_cth.ForEachFloat(i =>
				{
					Complex<TNumber> c = (Complex<TNumber>)i;
					Assert.AreEqual(c.R, i);
					Assert.AreEqual(c.I, ZERO);
				});
			}


			public void Add()
			{
				(Complex<TNumber>, Complex<TNumber>, Complex<TNumber>)[] samples =
				{
				(new(), new(), new()),
				(new(ONE), new(TWO), new(THREE)),
				(new(ZERO, ONE), new(ZERO, TWO), new(ZERO, THREE)),
				(new(ONE, FIVE), new(TWO, SEVEN), new(THREE, TWELVE))
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

				Complex<TNumber> one = new(ONE);
				_cth.ForEachComplex(complex =>
				{
					Assert.AreEqual(complex, complex * one);
				});

				Complex<TNumber> i = new(ZERO, ONE);
				Assert.AreEqual((Complex<TNumber>)(-ONE), i * i);
				Assert.AreEqual(new Complex<TNumber>(ZERO, -ONE), i * -ONE);
				Assert.AreEqual(one, i * new Complex<TNumber>(ZERO, -ONE));
			}


			public void UnaryMinus()
			{
				_cth.ForEachComplex(complex =>
				{
					Assert.AreEqual(complex * -ONE, -complex);
				});
			}


			public void Magnitude()
			{
				Assert.AreEqual(ZERO, _getAbs(new Complex<TNumber>()));

				Complex<TNumber> one = new(ONE);
				Assert.AreEqual(ONE, _getAbs(one));

				Complex<TNumber> negativeOne = new(ONE);
				Assert.AreEqual(ONE, _getAbs(negativeOne));

				Complex<TNumber> imaginaryOne = new(ZERO, ONE);
				Assert.AreEqual(ONE, _getAbs(imaginaryOne));

				Complex<TNumber> negativeImaginaryOne = new(ZERO, ONE);
				Assert.AreEqual(ONE, _getAbs(negativeImaginaryOne));

				Complex<TNumber> c = new(-THREE, FOUR);
				Assert.AreEqual(FIVE, _getAbs(c));
			}


			public void Div()
			{
				_cth.ForEachPairOfComplex((a, b) =>
				{
					if (_getAbs(b) == ZERO)
					{
						return;
					}

					var res = a / b;
					_cth.AssertApproximatelyEqual(a, res * b);
				});

				_cth.ForEachComplex(complex =>
				{
					_cth.ForEachFloat(i => {
						if (i != ZERO)
						{
							Assert.AreEqual(complex / (Complex<TNumber>)i, complex / i);
						}
						if (_getAbs(complex) != ZERO)
						{
							Assert.AreEqual((Complex<TNumber>)i / complex, i / complex);
						}
					});
				});
			}


			public void Sqrt()
			{
				_cth.ForEachComplex(complex =>
				{
					var root = _getSqrtC(complex);
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
						_getSqrtC((Complex<TNumber>)floatNum),
						_getSqrtF(floatNum)
					);
				});
			}


			public void AbsQuad()
			{
				_cth.ForEachComplex(complex => {
					TNumber expected = _getAbs(complex);
					expected *= expected;
					_cth.AssertApproximatelyEqual(expected, complex.AbsQuad());
				});
			}


			public void Exp()
			{
				_cth.AssertApproximatelyEqual(
					new Complex<TNumber>(ONE, ZERO),
					_getExp(new())
				);
				_cth.AssertApproximatelyEqual(
					new Complex<TNumber>(E, ZERO),
					_getExp(new Complex<TNumber>(ONE, ZERO))
				);

				// e^(i * pi) + 1 = 0   =>   e^(i * pi) = -1
				_cth.AssertApproximatelyEqual(
					new Complex<TNumber>(-ONE, ZERO),
					_getExp(new Complex<TNumber>(ZERO, PI))
				);
			}
		}
	}


	public interface IComplexTest
	{
		void Equals();
		void CastFloatToComplex();
		void Add();
		void Sub();
		void Mul();
		void UnaryMinus();
		void Magnitude();
		void Div();
		void Sqrt();
		void AbsQuad();
		void Exp();
	}
}
