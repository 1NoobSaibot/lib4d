using Lib4D;
using System.Collections.Generic;

namespace HyperCube.Shapes
{
	internal class Hypercube : Shape4D
	{
		private Vector4DFloat[,] _lines;
		private Vector4DFloat[] _vertecies;

		public Hypercube(float scale)
		{
			List<Vector4DFloat> verticies = new List<Vector4DFloat>(16);
			for (int x = -1; x < 2; x += 2)
			{
				for (int y = -1; y < 2; y += 2)
				{
					for (int z = -1; z < 2; z += 2)
					{
						for (int q = -1; q < 2; q += 2)
						{
							verticies.Add(new Vector4DFloat(x, y, z, q));
						}
					}
				}
			}
			_vertecies = verticies.ToArray();

			List<(Vector4DFloat a, Vector4DFloat b)> lines = new List<(Vector4DFloat a, Vector4DFloat b)>();
			for (int x = -1; x < 2; x += 2)
			{
				for (int y = -1; y < 2; y += 2)
				{
					for (int z = -1; z < 2; z += 2)
					{
						lines.Add((new Vector4DFloat(x, y, z, -1), new Vector4DFloat(x, y, z, 1)));
					}
				}
			}

			for (int x = -1; x < 2; x += 2)
			{
				for (int y = -1; y < 2; y += 2)
				{
					for (int q = -1; q < 2; q += 2)
					{
						lines.Add((new Vector4DFloat(x, y, -1, q), new Vector4DFloat(x, y, 1, q)));
					}
				}
			}

			for (int x = -1; x < 2; x += 2)
			{
				for (int z = -1; z < 2; z += 2)
				{
					for (int q = -1; q < 2; q += 2)
					{
						lines.Add((new Vector4DFloat(x, -1, z, q), new Vector4DFloat(x, 1, z, q)));
					}
				}
			}

			for (int y = -1; y < 2; y += 2)
			{
				for (int z = -1; z < 2; z += 2)
				{
					for (int q = -1; q < 2; q += 2)
					{
						lines.Add((new Vector4DFloat(-1, y, z, q), new Vector4DFloat(1, y, z, q)));
					}
				}
			}

			_lines = new Vector4DFloat[lines.Count, 2];
			for (int i = 0; i < lines.Count; i++)
			{
				_lines[i, 0] = lines[i].a * scale;
				_lines[i, 1] = lines[i].b * scale;
			}
		}


		public override void Draw(Graphics4D g)
		{
			for (int i = 0; i < _vertecies.Length; i++)
			{
				g.DrawVertex(_vertecies[i]);
			}

			for (int i = 0; i < _lines.GetLength(0); i++)
			{
				g.DrawLine(_lines[i, 0], _lines[i, 1]);
			}
		}
	}
}
