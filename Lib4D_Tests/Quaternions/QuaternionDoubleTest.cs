﻿using Lib4D.Mathematic;

namespace Lib4D_Tests.Quaternions
{
	[TestClass]
	public class QuaternionDoubleTest : QuaternionTest<double>
	{
		protected override Math<double> GetMath() => new MathDouble();
	}
}
