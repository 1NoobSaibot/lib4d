using Lib4D;
using Lib4D.Math.Matrix;

namespace HyperCube
{
	internal class Projector4dTo3d
	{
		private readonly float[,] _projectionMatrix;
		private readonly float _tan;

		public Projector4dTo3d(int width, int height, int frustumDepth = 2000)
		{
			Vector4DFloat from = new(0, 0, 0, 0);
			Vector4DFloat to = new(0, 0, frustumDepth, 0);
			Vector4DFloat up = new(0, height * 0.5f, 0, 0);
			Vector4DFloat over = new(width * 0.5f, 0, 0, 0);

			Vector4DFloat D = (to - from).Normalize();
			Vector4DFloat A = Cross4(up, over, D).Normalize();
			Vector4DFloat B = Cross4(over, D, A).Normalize();
			Vector4DFloat C = Cross4(D, A, B);

			_projectionMatrix = new float[4, 4] {
				{ A.X, A.Y, A.Z, A.Q },
				{ B.X, B.Y, B.Z, B.Q },
				{ C.X, C.Y, C.Z, C.Q },
				{ D.X, D.Y, D.Z, D.Q }
			};
			_tan = up.Abs / (from - to).Abs;
		}



		public Vector3D<float> Project(Vector4DFloat input)
		{
			Vector4DFloat p = (input /* - from*/) * _projectionMatrix;

			
			Vector3D<float> res;
			res.X = p.X / (input.Q * _tan);
			res.Y = p.Y / (input.Q * _tan);
			res.Z = p.Z / (input.Q * _tan);

			return new Vector3D<float>(input.X, input.Y, input.Z);
		}



		private static Vector4DFloat Cross4(Vector4DFloat a, Vector4DFloat b, Vector4DFloat c)
		{
			Vector4DFloat res = new()
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
