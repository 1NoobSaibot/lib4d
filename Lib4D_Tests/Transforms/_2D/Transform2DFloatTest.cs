using Lib4D.Mathematic;

namespace Lib4D_Tests.Transforms._2D
{
	[TestClass]
	public class Transform2DFloatTest : Transform2DTest<float>
	{
		protected override Math<float> GetMath() => new MathFloat();
	}
}
