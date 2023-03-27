using Lib4D;
using Lib4D.Mathematic.Matrix;

namespace HyperCube
{
	internal class Projector4dTo3d
	{
		private readonly float[,] _projectionMatrix;
		private readonly float _tan;

		public Projector4dTo3d(int width, int height, int frustumDepth = 2000)
		{
			Vector4D<float> from = new(0, 0, 0, 0);
			Vector4D<float> to = new(0, 0, frustumDepth, 0);
			Vector4D<float> up = new(0, height * 0.5f, 0, 0);
			Vector4D<float> over = new(width * 0.5f, 0, 0, 0);

			Vector4D<float> D = (to - from).GetNormalized();
			Vector4D<float> A = Cross4(up, over, D).GetNormalized();
			Vector4D<float> B = Cross4(over, D, A).GetNormalized();
			Vector4D<float> C = Cross4(D, A, B);

			_projectionMatrix = new float[4, 4] {
				{ A.X, A.Y, A.Z, A.Q },
				{ B.X, B.Y, B.Z, B.Q },
				{ C.X, C.Y, C.Z, C.Q },
				{ D.X, D.Y, D.Z, D.Q }
			};
			_tan = up.Abs / (from - to).Abs;
		}



		public Vector3D<float> Project(Vector4D<float> input)
		{
			Vector4D<float> p = (input /* - from*/) * _projectionMatrix;

			
			Vector3D<float> res;
			res.X = p.X / (input.Q * _tan);
			res.Y = p.Y / (input.Q * _tan);
			res.Z = p.Z / (input.Q * _tan);

			return new Vector3D<float>(input.X, input.Y, input.Z);
		}



		private static Vector4D<float> Cross4(Vector4D<float> a, Vector4D<float> b, Vector4D<float> c)
		{
			Vector4D<float> res = new()
			{
				X = MatrixMath.GetDeterminant(new float[3, 3]
				{
					{ a.Y, b.Y, c.Y },
					{ a.Z, b.Z, c.Z },
					{ a.Q, b.Q, c.Q }
				}),

				Y = -MatrixMath.GetDeterminant(new float[3, 3]
				{
					{ a.X, b.X, c.X },
					{ a.Z, b.Z, c.Z },
					{ a.Q, b.Q, c.Q }
				}),

				Z = MatrixMath.GetDeterminant(new float[3, 3]
				{
					{ a.X, b.X, c.X },
					{ a.Y, b.Y, c.Y },
					{ a.Q, b.Q, c.Q }
				}),

				Q = -MatrixMath.GetDeterminant(new float[3, 3]
				{
					{ a.X, b.X, c.X },
					{ a.Y, b.Y, c.Y },
					{ a.Z, b.Z, c.Z }
				})
			};

			return res;
		}
	}
}
