using Lib4D;
using Lib4D_Tests.Helpers;

namespace Lib4D_Tests
{
	[TestClass]
	public class ComplexUnitTest
	{
		[TestMethod]
		public void Equals()
		{
			(Complex, Complex, bool)[] samples =
			{
				(new(), new(), true),
				(new(1), new(1), true),
				(new(0, 2), new(0, 2), true),
				(new(-7, 2), new(-7, 2), true),

				(new(0, 2), new(-7, 2), false),
				(new(-7), new(-7, 2), false),
				(new(0), new(-7, 2), false),
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
			for (double i = -10; i < 10; i += 0.1)
			{
				Complex c = (Complex)i;
				Assert.AreEqual(c.R, i);
				Assert.AreEqual(c.I, 0);
			}
		}


		[TestMethod]
		public void Add()
		{
			(Complex, Complex, Complex)[] samples =
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

			ComplexTestHelper.ForEachComplex(complex =>
			{
				for (int floatNum = -5; floatNum < 5; floatNum++)
				{
					Complex expectedSum = complex + (Complex)floatNum;
					Assert.AreEqual(expectedSum, complex + (double)floatNum);
					Assert.AreEqual(expectedSum, (double)floatNum + complex);
				}
			});
		}


		[TestMethod]
		public void Sub()
		{
			ComplexTestHelper.ForEachPairOfComplex((a, b) =>
			{
				Complex sum = a + b;
				Assert.AreEqual(a, sum - b);
				Assert.AreEqual(b, sum - a);
			});

			ComplexTestHelper.ForEachComplex(complex =>
			{
				for (int floatNum = -5; floatNum < 5; floatNum++)
				{
					Assert.AreEqual(complex - (Complex)floatNum, complex - floatNum);
					Assert.AreEqual((Complex)floatNum - complex, floatNum - complex);
				}
			});
		}


		[TestMethod]
		public void Mul()
		{
			// Is commutative
			ComplexTestHelper.ForEachPairOfComplex((a, b) =>
			{
				Assert.AreEqual(a * b, b * a);
			});

			ComplexTestHelper.ForEachComplex(complex =>
			{
				for (double floatNum = -5; floatNum < 5; floatNum += 0.25)
				{
					Complex expected = complex * (Complex)floatNum;
					Assert.AreEqual(expected, complex * floatNum);
					Assert.AreEqual(expected, floatNum * complex);
				}
			});

			Complex zero = new();
			ComplexTestHelper.ForEachComplex(complex =>
			{
				Assert.AreEqual(zero, complex * zero);
			});

			Complex one = new(1);
			ComplexTestHelper.ForEachComplex(complex =>
			{
				Assert.AreEqual(complex, complex * one);
			});

			Complex i = new(0, 1);
			Assert.AreEqual((Complex)(-1), i * i);
			Assert.AreEqual(new Complex(0, -1), i * -1);
			Assert.AreEqual(one, i * new Complex(0, -1));
		}


		[TestMethod]
		public void UnaryMinus()
		{
			ComplexTestHelper.ForEachComplex(complex =>
			{
				Assert.AreEqual(complex * -1, -complex);
			});
		}


		[TestMethod]
		public void Magnitude()
		{
			Assert.AreEqual(0, new Complex().Magnitude);

			Complex one = new(1);
			Assert.AreEqual(one.Magnitude, 1);

			Complex negativeOne = new(1);
			Assert.AreEqual(negativeOne.Magnitude, 1);

			Complex imaginaryOne = new(0, 1);
			Assert.AreEqual(imaginaryOne.Magnitude, 1);

			Complex negativeImaginaryOne = new(0, 1);
			Assert.AreEqual(negativeImaginaryOne.Magnitude, 1);

			Complex c = new(-3, 4);
			Assert.AreEqual(c.Magnitude, 5);
		}


		[TestMethod]
		public void Div()
		{
			ComplexTestHelper.ForEachPairOfComplex((a, b) =>
			{
				if (b.Magnitude == 0)
				{
					return;
				}

				var res = a / b;
				ComplexTestHelper.AssertApproximatelyEqual(a, res * b);
			});

			ComplexTestHelper.ForEachComplex(complex =>
			{
				for (double i = -5; i < 5; i += 0.125)
				{
					if (i != 0)
					{
						Assert.AreEqual(complex / (Complex)i, complex / i);
					}
					if (complex.Magnitude != 0)
					{
						Assert.AreEqual((Complex)i / complex, i / complex);
					}
				}
			});
		}


		[TestMethod]
		public void Sqrt()
		{
			ComplexTestHelper.ForEachComplex(complex =>
			{
				var root = Complex.Sqrt(complex);
				try
				{
					ComplexTestHelper.AssertApproximatelyEqual(complex, root * root);
				}
				catch (AssertFailedException)
				{
					root = Complex.Sqrt(complex);
					throw new AssertFailedException(
						$"Bad square root: argument={complex}, root={root}, root*root={root * root}"
					);
				}
			});

			for (double floatNum = -10; floatNum < 10; floatNum += 0.125)
			{
				Assert.AreEqual(
					Complex.Sqrt((Complex)floatNum),
					Complex.Sqrt(floatNum)
				);
			}
		}


		[TestMethod]
		public void AbsQuad()
		{
			ComplexTestHelper.ForEachComplex(complex => {
				ComplexTestHelper.AssertApproximatelyEqual(complex.Abs() * complex.Abs(), complex.AbsQuad());
			});
		}



		[TestMethod]
		public void Exp()
		{
			ComplexTestHelper.AssertApproximatelyEqual(new Complex(1, 0), Complex.Exp(new Complex()));
			ComplexTestHelper.AssertApproximatelyEqual(new Complex(Math.E, 0), Complex.Exp(new Complex(1, 0)));

			// e^(i * pi) + 1 = 0   =>   e^(i * pi) = -1
			ComplexTestHelper.AssertApproximatelyEqual(new Complex(-1, 0), Complex.Exp(new Complex(0, Math.PI)));
		}
	}
}
