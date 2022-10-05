using Lib4D;
using System.Collections.Generic;

namespace HyperCube.Shapes
{
	internal class Hexdecahedroid : Shape4D
	{
		private Vector4DFloat[,] _lines;

		public Hexdecahedroid(float scale)
		{
			List<(Vector4DFloat a, Vector4DFloat b)> lines = new List<(Vector4DFloat a, Vector4DFloat b)>();
			for (int x = -1; x < 2; x += 2)
			{
				for (int y = -1; y < 2; y += 2)
				{
					lines.Add((new Vector4DFloat(x, 0, 0, 0), new Vector4DFloat(0, y, 0, 0)));
				}

				for (int z = -1; z < 2; z += 2)
				{
					lines.Add((new Vector4DFloat(x, 0, 0, 0), new Vector4DFloat(0, 0, z, 0)));
				}

				for (int q = -1; q < 2; q += 2)
				{
					lines.Add((new Vector4DFloat(x, 0, 0, 0), new Vector4DFloat(0, 0, 0, q)));
				}
			}

			for (int y = -1; y < 2; y += 2)
			{
				for (int z = -1; z < 2; z += 2)
				{
					lines.Add((new Vector4DFloat(0, y, 0, 0), new Vector4DFloat(0, 0, z, 0)));
				}

				for (int q = -1; q < 2; q += 2)
				{
					lines.Add((new Vector4DFloat(0, y, 0, 0), new Vector4DFloat(0, 0, 0, q)));
				}
			}

			for (int z = -1; z < 2; z += 2)
			{
				for (int q = -1; q < 2; q += 2)
				{
					lines.Add((new Vector4DFloat(0, 0, z, 0), new Vector4DFloat(0, 0, 0, q)));
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
			for (int i = 0; i < _lines.GetLength(0); i++)
			{
				g.DrawLine(_lines[i, 0], _lines[i, 1]);
			}
		}
	}
}
