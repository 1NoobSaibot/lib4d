using Lib4D.Mathematic;

namespace Lib4D_Tests.Vectors._3D
{
	[TestClass]
	public class Vector3DFloatTest : Vector3DTest<float>
	{
		protected override Math<float> GetMath() => new MathFloat();
	}
}
