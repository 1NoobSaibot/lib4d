namespace Lib4D.Mathematic
{
	public class MathFloat : Math<float>
	{
		protected override Func<float, float> GetAbsFn() => MathF.Abs;
		protected override Func<float, float> GetCosFn() => MathF.Cos;
		protected override Func<double, float> GetDouble2NumberFn() => (d) => (float)d;
		protected override float GetE() => float.E;
		protected override Func<float, float>? GetExpFn() => MathF.Exp;
		protected override Func<int, float> GetInt2NumberFn() => (_int) => (float)_int;
		protected override float GetPi() => float.Pi;
		protected override Func<float, float> GetSinFn() => MathF.Sin;
		protected override Func<float, float> GetSqrtFn() => MathF.Sqrt;
	}
}
