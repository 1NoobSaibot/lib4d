using Lib4D.Mathematic;

namespace Lib4D_Tests.Imaginaries
{
	[TestClass]
	public class ImaginaryIDoubleTest : ImaginaryITest<double>
	{
		protected override Math<double> GetMath() => new MathDouble();
	}
}
