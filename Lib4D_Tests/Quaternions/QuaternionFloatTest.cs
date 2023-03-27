using Lib4D;
using Lib4D.Mathematic;

namespace Lib4D_Tests.Quaternions
{
	[TestClass]
	public class QuaternionFloatTest : QuaternionTest<float>
	{
		public QuaternionFloatTest() { }

		protected override void InitMath()
		{
			Math<float>.InitInstance(new MathFloat());
		}
	}
}
