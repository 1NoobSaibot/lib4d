using Lib4D.Mathematic;

namespace Lib4D_Tests.Vectors._2D
{
	[TestClass]
	public class Vector2DFloatTest : Vector2DTest<float>
	{
		protected override Math<float> GetMath() => new MathFloat();
	}
}
