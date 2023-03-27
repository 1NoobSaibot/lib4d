using Lib4D;
using Lib4D.Mathematic;

namespace LibFOURD_Tests.Quaternions
{
	[TestClass]
	public class QuaternionDoubleTest : QuaternionTest<double>
	{
		public QuaternionDoubleTest()
		{ }

		protected override void InitMath()
		{
			Math<double>.InitInstance(new MathDouble());
		}
	}
}
