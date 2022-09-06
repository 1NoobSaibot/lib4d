using Lib4D;
using MathGen.Double;
using MathGen.Double.Compression;
using System;

namespace Rotate4DSearcher.Genetic
{
	public class Candidate
	{
		private static StandartOptimizer _stdOptimizer = new StandartOptimizer();
		private static HardOptimizer _hardOptimizer = new HardOptimizer(TimeSpan.FromSeconds(5));
		
		private Function[,] _formulas;
		private double _errorBuf = 0;
		public double Error { get; private set; } = 0;

		public readonly int AmountOfNodes;

		public Candidate(string[][] formulas, Random rnd)
		{
			ArgsDescription args = new ArgsDescription("c", "s", "xy", "xz", "xq", "yz", "yq", "zq");
			FunctionRandomContext rndCtx = new FunctionRandomContext(args, rnd);
			_formulas = new Function[4, 4];
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					_formulas[i, j] = new Function(rndCtx, formulas[i][j]);
					_formulas[i, j] = _hardOptimizer.Optimize(_formulas[i, j]);
				}
			}

			AmountOfNodes = CalculateAmountOfNodes();
		}

		public Candidate(Random rnd, Candidate candidateA)
		{
			_formulas = new Function[4, 4];
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					_formulas[i, j] = candidateA._formulas[i, j].Clone();
				}
			}

			int x = rnd.Next(4);
			int y = rnd.Next(4);
			_formulas[x, y] = _stdOptimizer.Optimize(_formulas[x, y].GetMutatedClone());

			AmountOfNodes = CalculateAmountOfNodes();
		}

		public Candidate(Random rnd, Candidate candidateA, Candidate candidateB)
		{
			_formulas = new Function[4, 4];
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					bool fromA = rnd.NextDouble() < 0.5;
					_formulas[i, j] = fromA
						? candidateA._formulas[i, j].Clone()
						: candidateB._formulas[i, j].Clone();
				}
			}

			int x = rnd.Next(4);
			int y = rnd.Next(4);
			_formulas[x, y] = _stdOptimizer.Optimize(_formulas[x, y].GetMutatedClone());

			AmountOfNodes = CalculateAmountOfNodes();
		}

		public Transform4D CreateTransform(Bivector4D surface, double angle)
		{
			double[] args = ArgsBox.ToArrayOfArguments(surface, angle);
			double[,] matrix = new double[5, 5];
			matrix[4, 4] = 1;
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					matrix[i, j] = _formulas[i, j].Calculate(args);
				}
			}
			return new Transform4D(matrix);
		}

		internal string[][] ToStringArray()
		{
			string[][] res = new string[4][];
			for (int i = 0; i < 4; i++)
			{
				res[i] = new string[4];

				for (int j = 0; j < 4; j++)
				{
					res[i][j] = _formulas[i, j].ToString();
				}
			}
			return res;
		}


		public override string ToString()
		{
			string[][] array = ToStringArray();
			string res = Error + "  |  Nodes: " + AmountOfNodes;
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					res += "\n   " + _formulas[i, j].ToString();
				}
			}

			return res;
		}


		private int CalculateAmountOfNodes()
		{
			int amount = 0;
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					amount += _formulas[i, j].GetAmountOfNodes();
				}
			}
			return amount;
		}


		public void ResetError()
		{
			_errorBuf = 0;
		}


		public void UpdateError()
		{
			Error = _errorBuf;
		}


		public void AddError(double deltaError)
		{
			_errorBuf += deltaError;
		}
	}
}

