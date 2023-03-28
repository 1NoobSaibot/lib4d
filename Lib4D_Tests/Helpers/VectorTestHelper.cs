using Lib4D;
using System.Numerics;

namespace Lib4D_Tests.Helpers
{
	internal class VectorTestHelper<TNumber>
		: FloatTestHelper<TNumber>
		where TNumber : INumber<TNumber>
	{
		public void AssertApproximatelyEqual(Vector2D<TNumber> a, Vector2D<TNumber> b)
		{
			try
			{
				AssertApproximatelyEqualF(a.X, b.X);
				AssertApproximatelyEqualF(a.Y, b.Y);
			}
			catch (AssertFailedException ex)
			{
				throw new AssertFailedException(
					$"{typeof(TNumber).Name}: Two vectors {a} and {b} are not enough equal",
					ex
				);
			}
		}


		public void ForEachVector2D(Action<Vector2D<TNumber>> action)
		{
			ForEachTwoFloats((x, y) => {
				action(new(x, y));
			});
		}


		public void ForEachPairOfVectors2D(Action<Vector2D<TNumber>, Vector2D<TNumber>> action)
		{
			ForEachVector2D(v1 => {
				ForEachVector2D(v2 => {
					action(v1, v2);
				});
			});
		}


		public void ForEachVector3D(Action<Vector3D<TNumber>> action)
		{
			ForEachThreeFloats((x, y, z) => {
				action(new(x, y, z));
			});
		}


		public void ForEachPairOfVectors3D(Action<Vector3D<TNumber>, Vector3D<TNumber>> action)
		{
			ForEachVector3D(v1 => {
				ForEachVector3D(v2 => {
					action(v1, v2);
				});
			});
		}



		public void ForEachVector4D(Action<Vector4D<TNumber>> action)
		{
			ForEachFourFloats((x, y, z, q) => {
				action(new(x, y, z, q));
			});
		}


		public void ForEachPairOfVectors4D(Action<Vector4D<TNumber>, Vector4D<TNumber>> action)
		{
			ForEachVector4D(v1 => {
				ForEachVector4D(v2 => {
					action(v1, v2);
				});
			});
		}
	}
}
