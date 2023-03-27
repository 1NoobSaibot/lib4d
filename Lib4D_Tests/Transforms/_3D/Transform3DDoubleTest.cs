using Lib4D.Mathematic;

namespace Lib4D_Tests.Transforms._3D
{
	[TestClass]
	public class Transform3DDoubleTest : Transform3DTest<double>
	{
		protected override Math<double> GetMath() => new MathDouble();
	}
}
