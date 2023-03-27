using System.Numerics;

namespace Lib4D_Tests.Helpers
{
	public class NumberSet<TNumber> where TNumber : INumber<TNumber>
	{
		public const float EPSILON_FLOAT = 0.0000025f;
		public const double EPSILON_DOUBLE = 0.00000000000001;

		public TNumber E { get; private set; } = TNumber.Zero;
		public TNumber PI { get; private set; } = TNumber.Zero;
		/// <summary>
		/// It uses as a maximum absolute error in comparing two numbers approximately
		/// </summary>
		public TNumber EPSILON { get; private set; } = TNumber.Zero;
		public readonly TNumber c0 = TNumber.Zero;
		public readonly TNumber c1 = TNumber.One;
		public readonly TNumber c2 = TNumber.One + TNumber.One;
		public readonly TNumber c3;
		public readonly TNumber c4;
		public readonly TNumber c5;
		public readonly TNumber c6;
		public readonly TNumber c7;
		public readonly TNumber c8;
		public readonly TNumber c9;
		public readonly TNumber c10;
		public readonly TNumber c11;
		public readonly TNumber c12;
		public readonly TNumber c13;
		public readonly TNumber c17;


		public NumberSet() {
			if (typeof(TNumber) == typeof(float))
			{
				var _this = (this as NumberSet<float>)!;
				_this.E = MathF.E;
				_this.PI = MathF.PI;
				_this.EPSILON = EPSILON_FLOAT;
			}
			else if (typeof(TNumber) == typeof(double))
			{
				var _this = (this as NumberSet<double>)!;
				_this.E = System.Math.E;
				_this.PI = System.Math.PI;
				_this.EPSILON = EPSILON_DOUBLE;
			}
			else
			{
				throw new Exception("Exponent and PI cannot be defined when type is " +  typeof(TNumber));
			}
			
			c3 = c2 + c1;
			c4 = c3 + c1;
			c5 = c4 + c1;
			c6 = c5 + c1;
			c7 = c6 + c1;
			c8 = c7 + c1;
			c9 = c8 + c1;
			c10 = c9 + c1;
			c11 = c10 + c1;
			c12 = c11 + c1;
			c13 = c12 + c1;
			c17 = c13 + c4;
		}


		public TNumber[] GetNums()
		{
			return new TNumber[] { -c7, -c1, c0, c1, c7 };
		}
	}
}
