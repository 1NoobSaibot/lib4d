using Lib4D;
using System;


namespace Rotate4DSearcher.Genetic
{
	public class ArgsBox
	{
		public static double[] ToArrayOfArguments(Bivector4D b, double angle)
		{
			double[] res = new double[8];
			res[(int)NameToIndex.C] = Math.Cos(angle);
			res[(int)NameToIndex.S] = Math.Sin(angle);
			res[(int)NameToIndex.XY] =  b.XY;
			res[(int)NameToIndex.XZ] = b.XZ;
			res[(int)NameToIndex.XQ] = b.XQ;
			res[(int)NameToIndex.YZ] = b.YZ;
			res[(int)NameToIndex.YQ] = b.YQ;
			res[(int)NameToIndex.ZQ] = b.ZQ;

			return res;
		}
	}


	public enum NameToIndex
	{
		C = 0,
		S,
		XY,
		XZ,
		XQ,
		YZ,
		YQ,
		ZQ
	}
}
