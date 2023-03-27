using Lib4D;

namespace LibFOURD_Tests.Quaternions
{
	[TestClass]
	public class QuaternionDoubleTest : QuaternionTest<double>
	{
		public QuaternionDoubleTest()
			: base(Math.Abs, Quaternion<double>.Abs)
		{ }
	}
}
