using Lib4D.Mathematic;

namespace Lib4D_Tests.Complexes
{
	[TestClass]
	public class ComplexDoubleTest : ComplexTest<double>
	{
		protected override Math<double> GetMath()
		{
			return new MathDouble();
		}
	}
}
