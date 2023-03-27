using Lib4D;

namespace LibFOURD_Tests.Quaternions
{
	[TestClass]
	public class QuaternionFloatTest : QuaternionTest<float>
	{

		public QuaternionFloatTest()
			: base(MathF.Abs, Quaternion<float>.Abs)
		{ }
	}
}
