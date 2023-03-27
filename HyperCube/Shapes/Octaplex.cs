using Lib4D;

namespace HyperCube.Shapes
{
	internal class Octaplex : Shape4D
	{
		private readonly Vector4D<float>[,] _lines;
		private readonly Vector4D<float>[] _verteces;

		public Octaplex(float scale)
		{
			List<Vector4D<float>> verteces = new(24);
			
			for (int x = -1; x < 2; x += 2)
			{
				for (int y = -1; y < 2; y += 2)
				{
					verteces.Add(new Vector4D<float>(x, y, 0, 0));
				}

				for (int z = -1; z < 2; z += 2)
				{
					verteces.Add(new Vector4D<float>(x, 0, z, 0));
				}

				for (int q = -1; q < 2; q += 2)
				{
					verteces.Add(new Vector4D<float>(x, 0, 0, q));
				}
			}

			for (int y = -1; y < 2; y += 2)
			{
				for (int z = -1; z < 2; z += 2)
				{
					verteces.Add(new Vector4D<float>(0, y, z, 0));
				}

				for (int q = -1; q < 2; q += 2)
				{
					verteces.Add(new Vector4D<float>(0, y, 0, q));
				}
			}

			for (int z = -1; z < 2; z += 2)
			{
				for (int q = -1; q < 2; q += 2)
				{
					verteces.Add(new Vector4D<float>(0, 0, z, q));
				}
			}

			
			_verteces = verteces.ToArray();
			for (int i = 0; i < _verteces.Length; i++)
			{
				_verteces[i] *= scale;
			}

			List<(Vector4D<float> a, Vector4D<float> b)> lines = new();
			for (int i = 0; i < verteces.Count - 1; i++)
			{
				for (int j = i + 1; j < verteces.Count; j++)
				{
					int diffOne = 0;
					int equals = 0;
					Vector4D<float> a = verteces[i], b = verteces[j];

					for (int coord = 0; coord < 4; coord ++)
					{
						float diff = Math.Abs(a[coord] - b[coord]);

						if (diff == 0)
						{
							equals++;
						}
						else if (diff == 1)
						{
							diffOne++;
						}
					}

					if (diffOne == 2 && equals == 2)
					{
						lines.Add((a, b));
					}
				}
			}

			_lines = new Vector4D<float>[lines.Count, 2];
			for (int i = 0; i < lines.Count; i++)
			{
				_lines[i, 0] = lines[i].a * scale;
				_lines[i, 1] = lines[i].b * scale;
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
