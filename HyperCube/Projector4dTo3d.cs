using Lib4D;

namespace HyperCube
{
	internal class Projector4dTo3d
	{
		private float[,] _projectionMatrix;
		private float _tan;

		public Projector4dTo3d(int width, int height, int frustumDepth = 2000)
		{
			Vector4DFloat from = new Vector4DFloat(0, 0, 0, 0);
			Vector4DFloat to = new Vector4DFloat(0, 0, frustumDepth, 0);
			Vector4DFloat up = new Vector4DFloat(0, height * 0.5f, 0, 0);
			Vector4DFloat over = new Vector4DFloat(width * 0.5f, 0, 0, 0);

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
			float _tan = up.Abs / (from - to).Abs;
		}



		public Vector3DFloat Project(Vector4DFloat input)
		{
			Vector4DFloat p = (input /* - from*/) * _projectionMatrix;

			
			Vector3DFloat res;
			res.X = p.X / (input.Q * _tan);
			res.Y = p.Y / (input.Q * _tan);
			res.Z = p.Z / (input.Q * _tan);

			return new Vector3DFloat(input.X, input.Y, input.Z);
		}



		private Vector4DFloat Cross4(Vector4DFloat a, Vector4DFloat b, Vector4DFloat c)
		{
			Vector4DFloat res = new Vector4DFloat();

			res.X = MatrixMathF.GetDeterminant(new float[3, 3]
			{
				{ a.Y, b.Y, c.Y },
				{ a.Z, b.Z, c.Z },
				{ a.Q, b.Q, c.Q }
			});

			res.Y = -MatrixMathF.GetDeterminant(new float[3, 3]
			{
				{ a.X, b.X, c.X },
				{ a.Z, b.Z, c.Z },
				{ a.Q, b.Q, c.Q }
			});

			res.Z = MatrixMathF.GetDeterminant(new float[3, 3]
			{
				{ a.X, b.X, c.X },
				{ a.Y, b.Y, c.Y },
				{ a.Q, b.Q, c.Q }
			});

			res.Q = -MatrixMathF.GetDeterminant(new float[3, 3]
			{
				{ a.X, b.X, c.X },
				{ a.Y, b.Y, c.Y },
				{ a.Z, b.Z, c.Z }
			});

			return res;
		}
	}
}
