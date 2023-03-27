using Lib4D.Mathematic;

namespace Lib4D_Tests.Transforms._4D
{
	[TestClass]
	public class Transform4DFloatTest : Transform4DTest<float>
	{
		public override Math<float> GetMath() => new MathFloat();
	}
}
