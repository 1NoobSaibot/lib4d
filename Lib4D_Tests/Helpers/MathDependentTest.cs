using Lib4D.Mathematic;
using System.Numerics;

namespace Lib4D_Tests.Helpers
{
	public abstract class MathDependentTest<TNumber>
		: NumberSet<TNumber>
		where TNumber : INumber<TNumber>
	{
		public MathDependentTest() {
			Math<TNumber>.InitInstance(GetMath());
		}

		protected abstract Math<TNumber> GetMath();
	}
}
