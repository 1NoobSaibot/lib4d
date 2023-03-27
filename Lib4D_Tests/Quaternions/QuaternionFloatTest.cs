using Lib4D;
using Lib4D.Mathematic;

namespace LibFOURD_Tests.Quaternions
{
	[TestClass]
	public class QuaternionFloatTest : QuaternionTest<float>
	{
		public QuaternionFloatTest()
			: base(Quaternion<float>.Abs)
		{ }

		protected override void InitMath()
		{
			Math<float>.InitInstance(new MathFloat());
		}
	}
}
