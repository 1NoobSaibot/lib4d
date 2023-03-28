﻿using Lib4D;
using System.Numerics;

namespace Lib4D_Tests.Helpers
{
	internal class QuaternionTestHelper<TNumber>
		: ComplexTestHelper<TNumber>
		where TNumber : INumber<TNumber>
	{
		public QuaternionTestHelper()
		{ }


		public void AssertApproximatelyEqual(Quaternion<TNumber> a, Quaternion<TNumber> b)
		{
			try
			{
				base.AssertApproximatelyEqualC(a.RI, b.RI);
				base.AssertApproximatelyEqualC(a.JK, b.JK);
			}
			catch (AssertFailedException ex)
			{
				throw new AssertFailedException(
					$"{typeof(TNumber).Name}: Two quaternions {a} and {b} are not enough equal",
					ex
				);
			}
		}


		public void ForEachQuaternion(Action<Quaternion<TNumber>> action)
		{
			ForEachPairOfComplex((ri, jk) =>
			{
				action(new(ri, jk));
			});
		}


		public void ForEachPairOfQuaternion(Action<Quaternion<TNumber>, Quaternion<TNumber>> action)
		{
			ForEachQuaternion(a =>
			{
				ForEachQuaternion(b =>
				{
					action(a, b);
				});
			});
		}
	}
}
