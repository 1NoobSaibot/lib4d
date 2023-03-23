using System;

namespace Lib4D
{
	public static class CMath
	{
		public static Complex Exp(Complex number)
		{
			double realExp = Math.Exp(number.R);
			Complex c = new(Math.Cos(number.I), Math.Sin(number.I));
			return realExp * c; 
		}
	}
}
