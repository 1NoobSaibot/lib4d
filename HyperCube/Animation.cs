using HyperCube.Shapes;

namespace HyperCube
{
	internal class Animation
	{
		private Shape4D[] _shapes = new Shape4D[4];
		public ShapeIndex Shape = ShapeIndex.HyperCube;

		public Animation()
		{
			_shapes[(int)ShapeIndex.Dodecaplex] = new Dodecaplex(0.75f);
			_shapes[(int)ShapeIndex.Hexdecahedroid] = new Hexdecahedroid(2f);
			_shapes[(int)ShapeIndex.HyperCube] = new Hypercube(1.0f);
			_shapes[(int)ShapeIndex.Octaplex] = new Octaplex(1.5f);
		}


		public void Draw(Graphics4D g)
		{
			_shapes[(int)Shape].Draw(g);
		}
	}

	internal enum ShapeIndex
	{
		HyperCube,
		Hexdecahedroid,
		Octaplex,
		Dodecaplex
	}
}
