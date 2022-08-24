using Lib4D;
using System;

namespace Rotate4DSearcher.Genetic
{
	public class Candidate
	{
		private AlgebraicExpression[,] _formulas;
		private double _errorBuf = 0;
		public double Error { get; private set; } = 0;

		public readonly int AmountOfNodes;

		public Candidate(string[][] formulas)
		{
			_formulas = new AlgebraicExpression[4, 4];
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					_formulas[i, j] = new AlgebraicExpression(formulas[i][j], ArgsBox.Empty);
				}
			}

			AmountOfNodes = CalculateAmountOfNodes();
		}

		public Candidate(Random rnd, Candidate candidateA)
		{
			_formulas = new AlgebraicExpression[4, 4];
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					_formulas[i, j] = candidateA._formulas[i, j].Clone();
				}
			}

			int x = rnd.Next(4);
			int y = rnd.Next(4);
			_formulas[x, y] = _formulas[x, y].GetMutatedClone();

			AmountOfNodes = CalculateAmountOfNodes();
		}

		public Candidate(Random rnd, Candidate candidateA, Candidate candidateB)
		{
			_formulas = new AlgebraicExpression[4, 4];
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
			_formulas[x, y] = _formulas[x, y].GetMutatedClone();

			AmountOfNodes = CalculateAmountOfNodes();
		}

		public Transform4D CreateTransform(Bivector4D surface, double angle)
		{
			ArgsBox args = new ArgsBox(surface, angle);
			double[,] matrix = new double[5, 5];
			matrix[4, 4] = 1;
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					matrix[i, j] = _formulas[i, j].RootOperator.Calculate(args);
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
			string res = Error + "  |";
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
					amount += _formulas[i, j].RootOperator.GetAmountOfNodes();
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

