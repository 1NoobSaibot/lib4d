using Lib4D.Mathematic;

namespace Lib4D_Tests.Vectors._2D
{
	[TestClass]
	public class Vector2DDoubleTest : Vector2DTest<double>
	{
		protected override Math<double> GetMath() => new MathDouble();
	}
}
