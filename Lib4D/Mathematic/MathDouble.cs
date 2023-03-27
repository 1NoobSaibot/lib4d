namespace Lib4D.Mathematic
{
	public class MathDouble : Math<double>
	{
		protected override Func<double, double> GetAbsFn() => Math.Abs;
		protected override Func<double, double> GetCosFn() => Math.Cos;
		protected override double GetE() => double.E;
		protected override Func<double, double>? GetExpFn() => Math.Exp;
		protected override Func<int, double> GetInt2NumberFn() => (_int) => (double)_int;
		protected override double GetPi() => double.Pi;
		protected override Func<double, double> GetSinFn() => Math.Sin;
		protected override Func<double, double> GetSqrtFn() => Math.Sqrt;
	}
}
