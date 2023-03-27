using Lib4D.Mathematic;

namespace Lib4D_Tests.Transforms._3D
{
	[TestClass]
	public class Transform3DFloatTest : Transform3DTest<float>
	{
		protected override Math<float> GetMath() => new MathFloat();
	}
}
