using Lib4D;
using System;


namespace Rotate4DSearcher.Genetic.Candidate
{
	public class ArgsBox
	{
		private double[] _values;
		private static readonly string[] _names = new string[9] {
			"c",
			"s",
			"n",
			"xy",
			"xz",
			"xq",
			"yz",
			"yq",
			"zq"
		};


		public static readonly ArgsBox Empty = new ArgsBox(
			new Bivector4D(new Vector4D(), new Vector4D())
			, 0
		);


		public ArgsBox(Bivector4D b, double angle)
		{
			_values = new double[9];
			_values[(int)NameToIndex.C] = Math.Cos(angle);
			_values[(int)NameToIndex.S] = Math.Sin(angle);
			_values[(int)NameToIndex.N] = 1 - _values[(int)NameToIndex.C];
			_values[(int)NameToIndex.XY] =  b.XY;
			_values[(int)NameToIndex.XZ] = b.XZ;
			_values[(int)NameToIndex.XQ] = b.XQ;
			_values[(int)NameToIndex.YZ] = b.YZ;
			_values[(int)NameToIndex.YQ] = b.YQ;
			_values[(int)NameToIndex.ZQ] = b.ZQ;
		}

		public double GetValue(int index)
		{
			return _values[index];
		}


		public string GetName(int index)
		{
			return _names[index];
		}

		internal int GetIndexByName(string symbol)
		{
			for (int i = 0; i < _names.Length; i++)
			{
				if (symbol == _names[i])
				{
					return i;
				}
			}

			throw new Exception("Unknown Argument: [" + symbol + "]");
		}
	}


	public enum NameToIndex
	{
		C = 0,
		S,
		N,
		XY,
		XZ,
		XQ,
		YZ,
		YQ,
		ZQ
	}
}
