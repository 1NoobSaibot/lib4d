using Lib4D.Mathematic;
using System.Numerics;

namespace Lib4D_Tests.Helpers
{
	public class NumberSet<TNumber> where TNumber : INumber<TNumber>
	{
		public const float EPSILON_FLOAT = 0.0000025f;
		public const double EPSILON_DOUBLE = 0.00000000000001;

		/// <summary>
		/// It uses as a maximum absolute error in comparing two numbers approximately
		/// </summary>
		public TNumber EPSILON { get; private set; } = TNumber.Zero;
		

		public NumberSet() {
			if (typeof(TNumber) == typeof(float))
			{
				var _this = (this as NumberSet<float>)!;
				_this.EPSILON = EPSILON_FLOAT;
			}
			else if (typeof(TNumber) == typeof(double))
			{
				var _this = (this as NumberSet<double>)!;
				_this.EPSILON = EPSILON_DOUBLE;
			}
			else
			{
				throw new Exception("Exponent and PI cannot be defined when type is " +  typeof(TNumber));
			}
		}


		public TNumber[] GetNums()
		{
			return new TNumber[] {
				Math<TNumber>.Int2Number!(-7),
				Math<TNumber>.Int2Number!(-1),
				Math<TNumber>.Int2Number!(0),
				Math<TNumber>.Int2Number!(+1),
				Math<TNumber>.Int2Number!(+7)
			};
		}
	}
}
