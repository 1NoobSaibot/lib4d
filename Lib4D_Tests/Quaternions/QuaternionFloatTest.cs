﻿using Lib4D.Mathematic;

namespace Lib4D_Tests.Quaternions
{
	[TestClass]
	public class QuaternionFloatTest : QuaternionTest<float>
	{
		protected override Math<float> GetMath() => new MathFloat();
	}
}
