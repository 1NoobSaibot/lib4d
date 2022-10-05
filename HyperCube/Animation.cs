using Lib4D;

namespace HyperCube
{
	internal class Animation
	{
		private Vector4DFloat[,,,] vertexes = new Vector4DFloat[2, 2, 2, 2];


		public Animation()
		{
			for (int x = 0; x < 2; x++)
			{
				for (int y = 0; y < 2; y++)
				{
					for (int z = 0; z < 2; z++)
					{
						for (int q = 0; q < 2; q++)
						{
							vertexes[x, y, z, q] = new Vector4DFloat(
								x == 0 ? -1 : 1,
								y == 0 ? -1 : 1,
								z == 0 ? -1 : 1,
								q == 0 ? -1 : 1
							);
						}
					}
				}
			}
		}


		public void Draw(Graphics4D g)
		{
			for (int x = 0; x < 2; x++)
			{
				for (int y = 0; y < 2; y++)
				{
					for (int z = 0; z < 2; z++)
					{
						g.DrawLine(vertexes[x, y, z, 0], vertexes[x, y, z, 1]);
					}
				}
			}

			for (int x = 0; x < 2; x++)
			{
				for (int y = 0; y < 2; y++)
				{
					for (int q = 0; q < 2; q++)
					{
						g.DrawLine(vertexes[x, y, 0, q], vertexes[x, y, 1, q]);
					}
				}
			}

			for (int x = 0; x < 2; x++)
			{
				for (int z = 0; z < 2; z++)
				{
					for (int q = 0; q < 2; q++)
					{
						g.DrawLine(vertexes[x, 0, z, q], vertexes[x, 1, z, q]);
					}
				}
			}

			for (int y = 0; y < 2; y++)
			{
				for (int z = 0; z < 2; z++)
				{
					for (int q = 0; q < 2; q++)
					{
						g.DrawLine(vertexes[0, y, z, q], vertexes[1, y, z, q]);
					}
				}
			}
		}
	}
}
