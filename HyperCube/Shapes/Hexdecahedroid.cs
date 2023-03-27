﻿using Lib4D;

namespace HyperCube.Shapes
{
	internal class Hexdecahedroid : Shape4D
	{
		private readonly Vector4D<float>[,] _lines;
		private readonly Vector4D<float>[] _verteces;

		public Hexdecahedroid(float scale)
		{
			List<(Vector4D<float> a, Vector4D<float> b)> lines = new();
			List<Vector4D<float>> verteces = new(8);

			for (int x = -1; x < 2; x += 2)
			{
				verteces.Add(new Vector4D<float>(x, 0, 0, 0));
				for (int y = -1; y < 2; y += 2)
				{
					lines.Add((new Vector4D<float>(x, 0, 0, 0), new Vector4D<float>(0, y, 0, 0)));
					verteces.Add(new Vector4D<float>(0, y, 0, 0));
				}

				for (int z = -1; z < 2; z += 2)
				{
					lines.Add((new Vector4D<float>(x, 0, 0, 0), new Vector4D<float>(0, 0, z, 0)));
					verteces.Add(new Vector4D<float>(0, 0, z, 0));
				}

				for (int q = -1; q < 2; q += 2)
				{
					lines.Add((new Vector4D<float>(x, 0, 0, 0), new Vector4D<float>(0, 0, 0, q)));
					verteces.Add(new Vector4D<float>(0, 0, 0, q));
				}
			}

			for (int y = -1; y < 2; y += 2)
			{
				for (int z = -1; z < 2; z += 2)
				{
					lines.Add((new Vector4D<float>(0, y, 0, 0), new Vector4D<float>(0, 0, z, 0)));
				}

				for (int q = -1; q < 2; q += 2)
				{
					lines.Add((new Vector4D<float>(0, y, 0, 0), new Vector4D<float>(0, 0, 0, q)));
				}
			}

			for (int z = -1; z < 2; z += 2)
			{
				for (int q = -1; q < 2; q += 2)
				{
					lines.Add((new Vector4D<float>(0, 0, z, 0), new Vector4D<float>(0, 0, 0, q)));
				}
			}

			_lines = new Vector4D<float>[lines.Count, 2];
			for (int i = 0; i < lines.Count; i++)
			{
				_lines[i, 0] = lines[i].a * scale;
				_lines[i, 1] = lines[i].b * scale;
			}

			_verteces = verteces.ToArray();
			for(int i = 0; i < _verteces.Length; i++)
			{
				_verteces[i] *= scale;
			}
		}


		public override void Draw(Graphics4D g)
		{
			for (int i = 0; i < _verteces.Length; i++)
			{
				g.DrawVertex(_verteces[i]);
			}

			for (int i = 0; i < _lines.GetLength(0); i++)
			{
				g.DrawLine(_lines[i, 0], _lines[i, 1]);
			}
		}
	}
}
