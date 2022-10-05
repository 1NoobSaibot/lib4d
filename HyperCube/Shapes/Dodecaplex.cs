using Lib4D;
using System;
using System.Collections.Generic;

namespace HyperCube.Shapes
{
	internal class Dodecaplex : Shape4D
	{
		private Vector4DFloat[,] _lines;
		private Vector4DFloat[] _vertecies;
		private static readonly float sqrt5 = (float)Math.Sqrt(5);
		private static readonly float F = (1 + sqrt5) / 2;

		public Dodecaplex(float scale)
		{
			List<Vector4DFloat> verteces = new List<Vector4DFloat>(600);

			// 24 vertexes are (0, 0, +-2, +-2) all combinations;
			for (int c1 = 0; c1 < 3; c1++)
			{
				for (int c2 = c1 + 1; c2 < 4; c2++)
				{
					for (int v1 = -2; v1 < 3; v1 += 4)
					{
						for (int v2 = -2; v2 < 3; v2 += 4)
						{
							Vector4DFloat a = new Vector4DFloat();
							a[c1] = v1;
							a[c2] = v2;
							verteces.Add(a);
						}
					}
				}
			}

			// 64 vertexes are combinations of (+-1, +-1,+-1; +-Sqrt(5))
			for (int cs = 0; cs < 4; cs++)
			{
				for (int x = -1; x < 2; x += 2)
				{
					for (int y = -1; y < 2; y += 2)
					{
						for (int z = -1; z < 2; z += 2)
						{
							for (int q = -1; q < 2; q += 2)
							{
								Vector4DFloat a = new Vector4DFloat(x, y, z, q);
								a[cs] = sqrt5 * a[cs];
								verteces.Add(a);
							}
						}
					}
				}
			}

			// 64 verteces are combinations of (+-F, +-F, +-F, +-Pow(F, -2))
			for (int cs = 0; cs < 4; cs++)
			{
				for (int x = -1; x < 2; x += 2)
				{
					for (int y = -1; y < 2; y += 2)
					{
						for (int z = -1; z < 2; z += 2)
						{
							for (int q = -1; q < 2; q += 2)
							{
								Vector4DFloat a = new Vector4DFloat(x, y, z, q);
								for (int i = 0; i < 4; i++)
								{
									if (i == cs)
									{
										a[i] = a[i] / (F * F);
									}
									else
									{
										a[i] = a[i] * F;
									}
								}
								verteces.Add(a);
							}
						}
					}
				}
			}

			// 64 verteces are combinations of (+-Pow(F, -1), +-Pow(F, -1), +-Pow(F, -1), +-FF)
			for (int cs = 0; cs < 4; cs++)
			{
				for (int x = -1; x < 2; x += 2)
				{
					for (int y = -1; y < 2; y += 2)
					{
						for (int z = -1; z < 2; z += 2)
						{
							for (int q = -1; q < 2; q += 2)
							{
								Vector4DFloat a = new Vector4DFloat(x, y, z, q);
								for (int i = 0; i < 4; i++)
								{
									if (i == cs)
									{
										a[i] = a[i] * F * F;
									}
									else
									{
										a[i] = a[i] / F;
									}
								}
								verteces.Add(a);
							}
						}
					}
				}
			}

			// 96 verticies are EVEN combinations of (0, +-Pow(F, -2), +-1, +-FF)
			for (int zero = 0; zero < 4; zero++)
			{
				for (int fMinus2 = 0; fMinus2 < 4; fMinus2++)
				{
					if (fMinus2 == zero)
					{
						continue;
					}

					for (int one = 0; one < 4; one++)
					{
						if (one == zero || one == fMinus2)
						{
							continue;
						}

						for (int fQuad = 0; fQuad < 4; fQuad++)
						{
							if (fQuad == zero || fQuad == fMinus2 || fQuad == one)
							{
								continue;
							}

							int[] combination = new int[4];
							combination[zero] = 1;
							combination[fMinus2] = 2;
							combination[one] = 3;
							combination[fQuad] = 4;

							if (!isEven(combination))
							{
								continue;
							}

							for (int fMinus2Sign = -1; fMinus2Sign < 2; fMinus2Sign += 2)
							{
								for (int oneSign = -1; oneSign < 2; oneSign += 2)
								{
									for (int fQuadSign = -1; fQuadSign < 2; fQuadSign += 2)
									{
										Vector4DFloat v = new Vector4DFloat();
										v[fMinus2] = 1 / (F * F) * fMinus2Sign;
										v[one] = oneSign;
										v[fQuad] = F * F * fQuadSign;
										verteces.Add(v);
									}
								}
							}
						}
					}
				}
			}

			// 96 verticies are EVEN combinations of (0, +-1/F, +-F, +-Sqrt5)
			for (int zero = 0; zero < 4; zero++)
			{
				for (int fMinus1 = 0; fMinus1 < 4; fMinus1++)
				{
					if (fMinus1 == zero)
					{
						continue;
					}

					for (int fPos = 0; fPos < 4; fPos++)
					{
						if (fPos == zero || fPos == fMinus1)
						{
							continue;
						}

						for (int sqrt5Pos = 0; sqrt5Pos < 4; sqrt5Pos++)
						{
							if (sqrt5Pos == zero || sqrt5Pos == fMinus1 || sqrt5Pos == fPos)
							{
								continue;
							}

							int[] combination = new int[4];
							combination[zero] = 1;
							combination[fMinus1] = 2;
							combination[fPos] = 3;
							combination[sqrt5Pos] = 4;

							if (!isEven(combination))
							{
								continue;
							}

							for (int fMinus1Sign = -1; fMinus1Sign < 2; fMinus1Sign += 2)
							{
								for (int fSign = -1; fSign < 2; fSign += 2)
								{
									for (int sqrt5Sign = -1; sqrt5Sign < 2; sqrt5Sign += 2)
									{
										Vector4DFloat v = new Vector4DFloat();
										v[fMinus1] = 1 / F * fMinus1Sign;
										v[fPos] = F * fSign;
										v[sqrt5Pos] = sqrt5 * sqrt5Sign;
										verteces.Add(v);
									}
								}
							}
						}
					}
				}
			}

			// 192 verticies are every EVEN combinations of (+-1/F, +-1, +-F, +-2)
			for (int fMinus1Pos = 0; fMinus1Pos < 4; fMinus1Pos++)
			{
				for (int onePos = 0; onePos < 4; onePos++)
				{
					if (onePos == fMinus1Pos)
					{
						continue;
					}

					for (int fPos = 0; fPos < 4; fPos++)
					{
						if (fPos == fMinus1Pos || fPos == onePos)
						{
							continue;
						}

						for (int twoPos = 0; twoPos < 4; twoPos++)
						{
							if (twoPos == fMinus1Pos || twoPos == onePos || twoPos == fPos)
							{
								continue;
							}

							int[] combination = new int[4];
							combination[fMinus1Pos] = 1;
							combination[onePos] = 2;
							combination[fPos] = 3;
							combination[twoPos] = 4;

							if (!isEven(combination))
							{
								continue;
							}

							for (int fMinus1Sign = -1; fMinus1Sign < 2; fMinus1Sign += 2)
							{
								for (int oneSign = -1; oneSign < 2; oneSign += 2)
								{
									for (int fSign = -1; fSign < 2; fSign += 2)
									{
										for (int twoSign = -1; twoSign < 2; twoSign += 2)
										{
											Vector4DFloat v = new Vector4DFloat();
											v[fMinus1Pos] = fMinus1Sign / F;
											v[onePos] = oneSign;
											v[fPos] = fSign * F;
											v[twoPos] = twoSign * 2;
											verteces.Add(v);
										}
									}
								}
							}
						}
					}
				}
			}

			
			for (int i = 0; i < verteces.Count; i++)
			{
				verteces[i] *= scale;
			}
			_vertecies = verteces.ToArray();
			float lineLengthQuad = findSmallestDistanceQuadBetween(verteces);
			List<(Vector4DFloat a, Vector4DFloat b)> lines = new List<(Vector4DFloat a, Vector4DFloat b)>(1200);
			
			for (int i = 0; i < verteces.Count - 1; i++)
			{
				int foundPairs = 0;
				for (int j = i + 1; j < verteces.Count; j++)
				{
					if (Math.Abs((verteces[i] - verteces[j]).AbsQuad - lineLengthQuad) < 0.001)
					{
						foundPairs++;
						lines.Add((verteces[i], verteces[j]));
					}
				}

				if (foundPairs > 4)
				{
					throw new Exception("Found more than 4 pairs for a vertex in Dodecaplex");
				}
			}

			if (lines.Count != 1200)
			{
				throw new Exception("Dodecaplex should be built with 1200 edges, but found " + lines.Count);
			}

			_lines = new Vector4DFloat[lines.Count, 2];
			for (int i = 0; i < lines.Count; i++)
			{
				_lines[i, 0] = lines[i].a;
				_lines[i, 1] = lines[i].b;
			}
		}


		private float findSmallestDistanceQuadBetween(List<Vector4DFloat> verteces)
		{
			float min = 10000;
			for (int i = 0; i < verteces.Count - 1; i++)
			{
				for (int j = i + 1; j < verteces.Count; j++)
				{
					float distanceQuad = (verteces[i] - verteces[j]).AbsQuad;
					if (min > distanceQuad)
					{
						min = distanceQuad;
					}
				}
			}
			return min;
		}


		private bool isEven(int[] combination)
		{
			int inversions = 0;
			for (int i = 0; i < combination.Length - 1; i++)
			{
				for (int j = i + 1; j < combination.Length; j++)
				{
					if (combination[i] > combination[j])
					{
						inversions++;
					}
				}
			}
			return inversions % 2 == 0;
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
