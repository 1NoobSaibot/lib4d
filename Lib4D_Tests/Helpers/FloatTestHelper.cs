using System.Numerics;

namespace Lib4D_Tests.Helpers
{
	internal class FloatTestHelper<TNumber>
		: NumberSet<TNumber>
		where TNumber : INumber<TNumber>
	{
		private readonly IReadOnlyList<double> _values = new double[] { -7, -1, 0, 1, 7 };


		public void ForEachFloat(Action<double> action)
		{
			foreach (var f in _values)
			{
				action(f);
			}
		}


		public void ForEachTwoFloats(Action<double, double> action)
		{
			foreach (var f1 in _values)
			{
				foreach (var f2 in _values)
				{
					action(f1, f2);
				}
			}
		}


		public void ForEachThreeFloats(Action<double, double, double> action)
		{
			foreach (var f1 in _values)
			{
				foreach (var f2 in _values)
				{
					foreach (var f3 in _values)
					{
						action(f1, f2, f3);
					}
				}
			}
		}


		public void ForEachFourFloats(Action<double, double, double, double> action)
		{
			foreach (var f1 in _values)
			{
				foreach (var f2 in _values)
				{
					foreach (var f3 in _values)
					{
						foreach (var f4 in _values)
						{
							action(f1, f2, f3, f4);
						}
					}
				}
			}
		}
	}
}
