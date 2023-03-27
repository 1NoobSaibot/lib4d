using Lib4D.Mathematic;

namespace Lib4D_Tests.Transforms._4D
{
	[TestClass]
	public class Transform4DDoubleTest : Transform4DTest<double>
	{
		public override Math<double> GetMath() => new MathDouble();
	}
}
