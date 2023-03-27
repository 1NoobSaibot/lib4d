using Lib4D;

namespace HyperCube
{
	internal class Projector4dTo2d
	{
		private Projector4dTo3d _4dTo3d;
		private readonly float[,] _projectionMatrix;
		private readonly float _tan;

		public Projector4dTo2d(int width, int height, int frustumDepth = 2000)
		{
			_4dTo3d = new Projector4dTo3d(width, height, frustumDepth);

			var from = new Vector3D<float>(0, 0, 0);
			var to = new Vector3D<float>(0, 0, 2000);
			var up = new Vector3D<float>(0, height * 0.5f, 0);

			var C = Vector3D<float>.Normalize(to - from);
			var A = Vector3D<float>.Normalize(up * C);
			var B = C * A;

			_projectionMatrix = new float[3, 3]
			{
				{ A.X, A.Y, A.Z },
				{ B.X, B.Y, B.Z },
				{ C.X, C.Y, C.Z }
			};

			_tan = Vector3D<float>.Abs(up) / Vector3D<float>.Abs(from - to);
		}



		public Vector3D<float> Project(Vector4DFloat input)
		{
			Vector3D<float> p = _4dTo3d.Project(input);
			Vector3D<float> P2 = (p/* - from*/) * _projectionMatrix;
			P2.X = P2.X / (P2.Z * _tan);
			P2.Y = P2.Y / (P2.Z * _tan);

			return P2;
		}
	}
}
