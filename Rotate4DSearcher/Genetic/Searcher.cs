using Lib4D;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MathGen.Genetic;


namespace Rotate4DSearcher.Genetic
{
	public class RotationMatrixSearcher : GeneticAlgorithm<Candidate>
	{
		private static readonly Random _rnd = new Random();
		private Task _looper;
		private GeneticSample[] _samples;
		private static RotationMatrixSearcher _instance;
		private static DateTime lastSaveTime;

		public static Candidate[] TheBest { get; private set; }

		private RotationMatrixSearcher() : base(10, 2)
		{
			_looper = new Task(Loop);
			_looper.Start();
		}


		public static void Start()
		{
			lastSaveTime = DateTime.Now;
			_instance = new RotationMatrixSearcher();
		}


		private void Loop()
		{
			LoadCandidates();
			LoadSamples();

			do
			{
				NextGeneration();
				Save();
			} while (true);
		}

		internal static int GetGenerationCount()
		{
			return _instance?.GenerationCounter ?? 0;
		}

		private void LoadCandidates()
		{
			List<string[][]> candidates = CandidatesStorage.GetCandidates();

			for (int i = 0; i < candidates.Count; i++)
			{
				Candidate candidate = new Candidate(candidates[i], _rnd);
				this.LoadCandidate(candidate);
			}
		}


		private void LoadSamples()
		{
			_samples = SamplesStore.GetSamples();
		}


		public override Candidate Mutate(Candidate model)
		{
			return new Candidate(_rnd, model);
		}


		public override Candidate Cross(Candidate modelA, Candidate modelB)
		{
			return new Candidate(_rnd, modelA, modelB);
		}


		public override void TestCandidate(Candidate candidate)
		{
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


		public override int Compare(Candidate a, Candidate b) 
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
		}


		private void Save()
		{
			Candidate[] choosen = GetChoosen();
			TheBest = choosen;

			DateTime now = DateTime.Now;
			if (now - lastSaveTime < new TimeSpan(0, 2, 0))
			{
				return;
			}

			List<string[][]> serializedCandidates = new List<string[][]>(choosen.Length);
			for (int i = 0; i < choosen.Length; i++)
			{
				serializedCandidates.Add(choosen[i].ToStringArray());
			}

			CandidatesStorage.Save(serializedCandidates);
			lastSaveTime = now;
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
