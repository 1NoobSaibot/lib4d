using Lib4D.Mathematic;

namespace Lib4D_Tests.Vectors._3D
{
	[TestClass]
	public class Vector3DDoubleTest : Vector3DTest<double>
	{
		protected override Math<double> GetMath() => new MathDouble();
	}
}
