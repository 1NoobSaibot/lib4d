using Lib4D;
using Rotate4DSearcher.Genetic.Candidate.Operators;
using System.Linq;

namespace Rotate4DSearcher.Genetic.Candidate
{
	public class Candidate
	{
		private IOperator[] _operator;
		private AlgebraicExpression[,] _formulas;


		public Candidate(string[,] formulas)
		{
			_formulas = new AlgebraicExpression[4, 4];
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					_formulas[i, j] = new AlgebraicExpression(formulas[i, j], ArgsBox.Empty);
				}
			}
		}


		private IOperator RootOperator => _operator.Last();


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

		public void CreateRotationMatrix(Bivector4D b, double angle)
		{
			ArgsBox _args = new ArgsBox(b, angle);
			RootOperator.Calculate(_args);
		}
	}
	
}

