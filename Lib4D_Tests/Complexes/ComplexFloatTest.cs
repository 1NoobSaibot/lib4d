using Lib4D.Mathematic;

namespace Lib4D_Tests.Complexes
{
	[TestClass]
	public class ComplexFloatTest : ComplexTest<float>
	{
		protected override Math<float> GetMath()
		{
			return new MathFloat();
		}
	}
}
