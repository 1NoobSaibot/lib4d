using System;

namespace Lib4D
{
	public static class CMath
	{
		public static Complex Sqrt(Complex number)
		{
			double abs = number.Abs();
			double real = Math.Sqrt((number.real + abs) / 2);
			double imaginary = _Sign(number.imaginary) * Math.Sqrt((abs - number.real) / 2);
			return new Complex(real, imaginary);
		}

		public static Complex Exp(Complex number)
		{
			double realExp = Math.Exp(number.real);
			Complex c = new Complex(Math.Cos(number.imaginary), Math.Sin(number.imaginary));
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
