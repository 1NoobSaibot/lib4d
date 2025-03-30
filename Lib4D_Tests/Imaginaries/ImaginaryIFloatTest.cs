using Lib4D.Mathematic;

namespace Lib4D_Tests.Imaginaries
{
	[TestClass]
	public class ImaginaryIFloatTest : ImaginaryITest<float>
	{
		protected override Math<float> GetMath() => new MathFloat();
	}
}
