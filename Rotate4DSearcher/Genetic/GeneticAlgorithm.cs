using Lib4D;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rotate4DSearcher.Genetic
{
	public class GeneticAlgorithm
	{
		private const int GENERATION_COUNT = 1000;
		private const int AMOUNT_OF_CHOOSEN = 10;

		public static Candidate[] TheBest = new Candidate[AMOUNT_OF_CHOOSEN];

		private static bool _samplesWereUpdated = true;
		private static bool _shouldStop = false;
		private static readonly Random _rnd = new Random();

		private Task _looper;
		private Candidate[] _candidates;
		private GeneticSample[] _samples;

		private static GeneticAlgorithm _instance;

		private GeneticAlgorithm()
		{
			_candidates = new Candidate[GENERATION_COUNT];
			_looper = new Task(Loop);
			_looper.Start();
		}


		public static void Start()
		{
			_instance = new GeneticAlgorithm();
			SamplesStore.SamplesChanged += OnSamplesUpdated;
		}


		private void Loop()
		{
			LoadCandidates();

			do
			{
				LoadSamples();
				ReproduceAndMutate();
				TestCandidates();
				MakeGenocide();
				Save();
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
			if (_samplesWereUpdated == false)
			{
				return;
			}

			List<Sample> samples = SamplesStore.GetSamples();
			_samples = new GeneticSample[samples.Count];
			for (int i = 0; i < _samples.Length;i++)
			{
				_samples[i] = new GeneticSample(samples[i]);
			}
			_samplesWereUpdated = false;
		}


		private void ReproduceAndMutate()
		{
			for (int i = 0; i < _candidates.Length; i++)
			{
				if (_candidates[i] != null)
				{
					continue;
				}

				Candidate candidateA = ChooseRandomCandidate();
				if (_rnd.NextDouble() < 0.5)
				{
					_candidates[i] = new Candidate(_rnd, candidateA);
				}
				else
				{
					Candidate candidateB = ChooseRandomCandidate();
					_candidates[i] = new Candidate(_rnd, candidateA, candidateB);
				}
			}
		}


		private void TestCandidates()
		{
			for (int i = 0; i < _candidates.Length; i++)
			{
				Candidate candidate = _candidates[i];
				candidate.Error = 0;

				for (int j = 0; j < _samples.Length; j++)
				{
					GeneticSample sample = _samples[j];
					Transform4D t = candidate.CreateTransform(sample.bivector, sample.angle);
					Vector4D[,] pairs = sample.pairs;

					for (int k = 0; k < pairs.GetLength(0); k++)
					{
						Vector4D actual = t * pairs[k, 0];
						Vector4D difference = actual - pairs[k, 1];
						double error = difference.Abs;
						candidate.Error += error;
					}
				}
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
			List<string[][]> serializedCandidates = new List<string[][]>(10);
			for (int i = 0; i < AMOUNT_OF_CHOOSEN; i++)
			{
				serializedCandidates.Add(_candidates[i].ToStringArray());
			}

			CandidatesStorage.Save(serializedCandidates);
		}


		private Candidate ChooseRandomCandidate()
		{
			Candidate c;
			int loopCounter = 0;
			do
			{
				int index = _rnd.Next(AMOUNT_OF_CHOOSEN);
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


		private static void OnSamplesUpdated(object sender, List<Sample> args)
		{
			_samplesWereUpdated = true;
		}

	}


	class GeneticSample
	{
		public readonly Bivector4D bivector;
		public readonly double angle;
		public readonly Vector4D[,] pairs;


		public GeneticSample(Sample sample)
		{
			bivector = new Bivector4D(sample.A.ToVector4D(), sample.B.ToVector4D());
			angle = sample.AngleInGrad * Math.PI / 180.0;

			int count = sample.pairs.Count;
			pairs = new Vector4D[count, 2];

			for (int i = 0; i < count; i++)
			{
				QuestionAnswerPair pair = sample.pairs[i];
				pairs[i, 0] = pair.argument.ToVector4D();
				pairs[i, 1] = pair.expectedResult.ToVector4D();
			}
		}
	}
}

