using System.Numerics;

namespace Lib4D.Mathematic
{
	public abstract class Math<TNumber>
		where TNumber : INumber<TNumber>
	{
		public static TNumber E { get; private set; } = TNumber.Zero;
		protected abstract TNumber GetE();


		public static TNumber PI { get; private set; } = TNumber.Zero;
		protected abstract TNumber GetPi();


		public static Func<TNumber, TNumber>? Abs { get; private set; }
		protected abstract Func<TNumber, TNumber> GetAbsFn();


		public static Func<TNumber, TNumber>? Cos { get; private set; }
		protected abstract Func<TNumber, TNumber> GetCosFn();


		public static Func<double, TNumber>? Double2Number { get; private set; }
		protected abstract Func<double, TNumber> GetDouble2NumberFn();


		public static Func<TNumber, TNumber>? Exp { get; private set; }
		protected abstract Func<TNumber, TNumber>? GetExpFn();


		public static Func<int, TNumber>? Int2Number { get; private set; }
		protected abstract Func<int, TNumber> GetInt2NumberFn();


		public static Func<TNumber, TNumber>? Sin { get; private set; }
		protected abstract Func<TNumber, TNumber> GetSinFn();


		public static Func<TNumber, TNumber>? Sqrt { get; private set; }
		protected abstract Func<TNumber, TNumber> GetSqrtFn();


		public static void InitInstance(Math<TNumber> math)
		{
			E = math.GetE();
			PI = math.GetPi();
			Abs = math.GetAbsFn();
			Cos = math.GetCosFn();
			Double2Number = math.GetDouble2NumberFn();
			Exp = math.GetExpFn();
			Int2Number = math.GetInt2NumberFn();
			Sin = math.GetSinFn();
			Sqrt = math.GetSqrtFn();
		}


		public TNumber this[int value] => Int2Number!(value);
	}
}
