using System;

namespace Lib4D
{
	public static class CMath
	{
		public static Complex Sqrt(Complex number)
		{
			double abs = number.Abs();
			double real = Math.Sqrt((number.r + abs) / 2);
			double imaginary = _Sign(number.i) * Math.Sqrt((abs - number.r) / 2);
			return new Complex(real, imaginary);
		}

		public static Complex Exp(Complex number)
		{
			double realExp = Math.Exp(number.r);
			Complex c = new Complex(Math.Cos(number.i), Math.Sin(number.i));
			return realExp * c; 
		}

		private static double _Sign(double number)
		{
			if (number >= 0)
			{
				return 1;
			}
			return -1;
		}
	}
}
