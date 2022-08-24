using Lib4D;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rotate4DSearcher.Genetic
{
	public class GeneticAlgorithm
	{
		private const int GENERATION_COUNT = 10;
		private const int AMOUNT_OF_CHOOSEN = 2;

		public static int GenerationCounter { get; private set; }
		public static Candidate[] TheBest = new Candidate[AMOUNT_OF_CHOOSEN];

		private static bool _shouldStop = false;
		private static readonly Random _rnd = new Random();

		private Task _looper;
		private Candidate[] _candidates;
		private GeneticSample[] _samples;

		private static GeneticAlgorithm _instance;
		private static DateTime lastSaveTime;

		private GeneticAlgorithm()
		{
			_candidates = new Candidate[GENERATION_COUNT];
			_looper = new Task(Loop);
			_looper.Start();
		}


		public static void Start()
		{
			lastSaveTime = DateTime.Now;
			_instance = new GeneticAlgorithm();
		}


		private void Loop()
		{
			LoadCandidates();
			LoadSamples();

			do
			{
				ReproduceAndMutate();
				TestCandidates();
				MakeGenocide();
				Save();
				GenerationCounter++;
			} while (!_shouldStop);
		}

		private void LoadCandidates()
		{
			List<string[][]> candidates = CandidatesStorage.GetCandidates();

			for (int i = 0; i < candidates.Count; i++)
			{
				_candidates[i] = new Candidate(candidates[i]);
			}
		}


		private void LoadSamples()
		{
			_samples = SamplesStore.GetSamples();
		}


		private void ReproduceAndMutate()
		{
			for (int i = 0; i < _candidates.Length; i++)
			{
				if (_candidates[i] != null)
				{
					continue;
				}

				Candidate candidateA = ChooseRandomCandidate(i);
				if (_rnd.NextDouble() < 0.5)
				{
					_candidates[i] = new Candidate(_rnd, candidateA);
				}
				else
				{
					Candidate candidateB = ChooseRandomCandidate(i);
					_candidates[i] = new Candidate(_rnd, candidateA, candidateB);
				}
			}
		}


		private void TestCandidates()
		{

			for (int i = 0; i < _candidates.Length; i++)
			{
				Candidate candidate = _candidates[i];
				candidate.ResetError();

				for (int j = 0; j < _samples.Length; j++)
				{
					GeneticSample sample = _samples[j];

					Rotation rotation = sample.rotation;
					Transform4D t = candidate.CreateTransform(rotation.bivector, rotation.angle);
					GeneticPair[] pairs = sample.pairs;

					for (int k = 0; k < pairs.GetLength(0); k++)
					{
						Vector4D actual = t * pairs[k].from;
						Vector4D difference = actual - pairs[k].to;
						double error = difference.Abs;
						candidate.AddError(error);
					}
				}

				candidate.UpdateError();
			}
		}


		private void MakeGenocide()
		{
			int comparer(Candidate a, Candidate b) 
			{
				if (a.Error > b.Error) {
					return 1;
				}

				if (a.Error < b.Error) {
					return -1;
				}

				if (a.AmountOfNodes > b.AmountOfNodes) {
					return 1;
				}

				if (a.AmountOfNodes < b.AmountOfNodes) {
					return -1;
				}

				return 0;
			};
			Array.Sort(_candidates, comparer);

			for (int i = AMOUNT_OF_CHOOSEN; i < _candidates.Length; i++)
			{
				_candidates[i] = null;
			}

			for (int i = 0; i < AMOUNT_OF_CHOOSEN; i++)
			{
				TheBest[i] = _candidates[i];
			}
		}


		private void Save()
		{
			DateTime now = DateTime.Now;
			if (now - lastSaveTime < new TimeSpan(0, 2, 0))
			{
				return;
			}

			List<string[][]> serializedCandidates = new List<string[][]>(10);
			for (int i = 0; i < AMOUNT_OF_CHOOSEN; i++)
			{
				serializedCandidates.Add(_candidates[i].ToStringArray());
			}

			CandidatesStorage.Save(serializedCandidates);
			lastSaveTime = now;
		}


		private Candidate ChooseRandomCandidate(int maxIndex)
		{
			Candidate c;
			int loopCounter = 0;
			do
			{
				int index = _rnd.Next(maxIndex);
				c = _candidates[index];

				if (c != null)
				{
					return c;
				}

				loopCounter++;
			} while (c != null && loopCounter < 1000);

			for (int i = 0; i < _candidates.Length; i++)
			{
				if (_candidates[i] != null)
				{
					return _candidates[i];
				}
			}

			throw new Exception("There is an empty array of candidates");
		}


		public static void Stop()
		{
			_shouldStop = true;
		}

	}


	class GeneticSample
	{
		public readonly Rotation rotation;
		public GeneticPair[] pairs { get; private set; }


		public GeneticSample(Rotation rotation)
		{
			this.rotation = rotation;
			pairs = new GeneticPair[0];
		}


		public void PushPair(GeneticPair pair)
		{
			if (_AlreadyExists(pair))
			{
				return;
			}

			GeneticPair[] newArray = new GeneticPair[pairs.Length + 1];
			for (int i = 0; i < pairs.Length; i++)
			{
				newArray[i] = pairs[i];
			}
			newArray[pairs.Length] = pair;
			pairs = newArray;
		}


		private bool _AlreadyExists(GeneticPair pair)
		{
			for (int i = 0; i < pairs.Length; i++)
			{
				if (pairs[i] == pair)
				{
					return true;
				}
			}

			return false;
		}
	}


	struct GeneticPair
	{
		public readonly Vector4D from, to;


		public GeneticPair(Vector4D from, Vector4D to)
		{
			this.from = from;
			this.to = to;
		}


		public static bool operator ==(GeneticPair a, GeneticPair b)
		{
			return a.from == b.from && a.to == b.to;
		}


		public static bool operator !=(GeneticPair a, GeneticPair b)
		{
			return a.from != b.from || a.to != b.to;
		}
	}


	public struct Rotation
	{
		public readonly Bivector4D bivector;
		public readonly double angle;


		public Rotation(Bivector4D b, double angle)
		{
			this.bivector = b;
			this.angle = angle;
		}


		public static bool operator == (Rotation a, Rotation b)
		{
			return a.bivector == b.bivector && a.angle == b.angle;
		}


		public static bool operator !=(Rotation a, Rotation b)
		{
			return a.bivector != b.bivector || a.angle != b.angle;
		}
	}
}
