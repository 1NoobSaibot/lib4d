using Lib4D.Mathematic;

namespace Lib4D_Tests.Transforms._2D
{
	[TestClass]
	public class Transform2DDoubleTest : Transform2DTest<double>
	{
		protected override Math<double> GetMath() => new MathDouble();
	}
}
