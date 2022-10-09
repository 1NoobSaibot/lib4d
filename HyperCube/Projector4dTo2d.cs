using Lib4D;

namespace HyperCube
{
	internal class Projector4dTo2d
	{
		private Projector4dTo3d _4dTo3d;
		private float[,] _projectionMatrix;
		private float _tan;

		public Projector4dTo2d(int width, int height, int frustumDepth = 2000)
		{
			_4dTo3d = new Projector4dTo3d(width, height, frustumDepth);

			Vector3DFloat from = new Vector3DFloat(0, 0, 0);
			Vector3DFloat to = new Vector3DFloat(0, 0, 2000);
			Vector3DFloat up = new Vector3DFloat(0, height * 0.5f, 0);

			Vector3DFloat C = (to - from).Normalize();
			Vector3DFloat A = (up * C).Normalize();
			Vector3DFloat B = C * A;

			_projectionMatrix = new float[3, 3]
			{
				{ A.X, A.Y, A.Z },
				{ B.X, B.Y, B.Z },
				{ C.X, C.Y, C.Z }
			};

			_tan = up.Abs / (from - to).Abs;
		}



		public Vector3DFloat Project(Vector4DFloat input)
		{
			Vector3DFloat p = _4dTo3d.Project(input);
			Vector3DFloat P2 = (p/* - from*/) * _projectionMatrix;
			P2.X = P2.X / (P2.Z * _tan);
			P2.Y = P2.Y / (P2.Z * _tan);

			return P2;
		}
	}
}
