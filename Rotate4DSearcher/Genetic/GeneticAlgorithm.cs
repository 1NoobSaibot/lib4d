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
			_ClearErrorsInData();

			for (int i = 0; i < _candidates.Length; i++)
			{
				Candidate candidate = _candidates[i];
				candidate.Error = 0;

				for (int j = 0; j < _samples.Length; j++)
				{
					GeneticSample sample = _samples[j];

					for (int rotationIndex = 0; rotationIndex < 2; rotationIndex++)
					{
						Rotation rotation = sample.rotations[rotationIndex];
						Transform4D t = candidate.CreateTransform(rotation.bivector, rotation.angle);
						GeneticPair[] pairs = sample.pairs;

						for (int k = 0; k < pairs.GetLength(0); k++)
						{
							Vector4D actual = t * pairs[k].from;
							Vector4D difference = actual - pairs[k].to;
							double error = difference.Abs;
							candidate.Error += error;
							pairs[k].AddUpToError(error);
						}
					}
				}
			}

			_UpdateErrorsInData();
		}

		
		internal static double[][] GetErrors()
		{
			GeneticSample[] samples = _instance?._samples ?? new GeneticSample[0];
			double[][] errors = new double[samples.Length][];

			for (int i = 0; i < samples.Length; i++)
			{
				errors[i] = new double[samples[i].pairs.Length];

				for (int j = 0; j < samples[i].pairs.Length; j++)
				{
					errors[i][j] = samples[i].pairs[j].Error;
				}
			}

			return errors;
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


		private void _ClearErrorsInData()
		{
			for (int i = 0; i < _samples.Length; i++)
			{
				_samples[i].ResetError();
			}
		}


		private void _UpdateErrorsInData()
		{
			for (int i = 0; i < _samples.Length; i++)
			{
				_samples[i].UpdateError();
			}
		}

	}


	class GeneticSample
	{
		public readonly Rotation[] rotations = new Rotation[2];
		public readonly GeneticPair[] pairs;


		public GeneticSample(Sample sample)
		{
			Bivector4D bivector = new Bivector4D(sample.A.ToVector4D(), sample.B.ToVector4D());
			double angle = sample.AngleInGrad * Math.PI / 180.0;

			rotations[0] = new Rotation(bivector, angle);
			rotations[1] = new Rotation(-bivector, -angle);

			int count = sample.pairs.Count;
			pairs = new GeneticPair[count];

			for (int i = 0; i < count; i++)
			{
				QuestionAnswerPair pair = sample.pairs[i];
				Vector4D from = pair.argument.ToVector4D();
				Vector4D to = pair.expectedResult.ToVector4D();
				pairs[i] = new GeneticPair(from, to);
			}
		}


		internal void ResetError()
		{
			for (int i = 0; i < pairs.Length; i++)
			{
				pairs[i].ResetErrorBuffer();
			}
		}

		internal void UpdateError()
		{
			for (int i = 0; i < pairs.Length; i++)
			{
				pairs[i].UpdateError();
			}
		}
	}


	struct GeneticPair
	{
		public readonly Vector4D from, to;
		private double _errorBuffer;
		public double Error { get; private set; }


		public GeneticPair(Vector4D from, Vector4D to)
		{
			this.from = from;
			this.to = to;
			_errorBuffer = 0;
			Error = 0;
		}


		public void ResetErrorBuffer()
		{
			_errorBuffer = 0;
		}


		public void AddUpToError(double error)
		{
			_errorBuffer += error;
		}


		public void UpdateError()
		{
			Error = _errorBuffer;
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
	}
}

