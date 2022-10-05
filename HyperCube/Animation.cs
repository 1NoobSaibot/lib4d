using HyperCube.Shapes;

namespace HyperCube
{
	internal class Animation
	{
		private Shape4D _shape = new Octaplex(1.5f);


		public void Draw(Graphics4D g)
		{
			_shape.Draw(g);
		}
	}
}
