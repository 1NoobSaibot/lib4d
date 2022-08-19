using Lib4D;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Rotate4DSearcher
{
	internal static class SamplesStore
	{
		private const string fileName = "Samples.json";
		public static List<Sample> Samples => GetSamples();
		private static List<Sample> _samples;
		public static event EventHandler<List<Sample>> SamplesChanged;


		static SamplesStore()
		{
			_Load();
		}


		public static List<Sample> GetSamples()
		{
			return _samples;
		}


		public static void AddNewSample(CustomBivector4D b)
		{
			_ThrowIfExists(b);

			Sample sample = new Sample() {
				A = b.A, B = b.B, AngleInGrad = b.AngleInGrad
			};

			_samples.Add(sample);
			_Save();

			_FireOnChanged();
		}



		public static void AddPair(int sampleIndex, Vector4D from, Vector4D to)
		{
			Sample sample = _samples[sampleIndex];
			sample.AddPair(from, to);
			_Save();
			_FireOnChanged();
		}


		private static void _ThrowIfExists(CustomBivector4D b) {
			for (int i = 0; i < _samples.Count; i++)
			{
				if (
					b.A == _samples[i].A
					&& b.B == _samples[i].B
					&& b.AngleInGrad == _samples[i].AngleInGrad
				) {
					throw new Exception("This surface already exists");
				}
			}
		}


		private static void _Load()
		{
			if (!File.Exists(fileName))
			{
				_samples = new List<Sample>();
				_Save();
				return;
			}

			using (StreamReader reader = new StreamReader(fileName))
			{
				string json = reader.ReadToEnd();
				_samples = JsonConvert.DeserializeObject<List<Sample>>(json);
			}
		}


		private static void _Save()
		{
			using (StreamWriter writer = new StreamWriter(fileName, false))
			{
				string json = JsonConvert.SerializeObject(_samples);
				writer.Write(json);
			}
		}


		private static void _FireOnChanged()
		{
			if (SamplesChanged != null)
			{
				SamplesChanged(_samples, null);
			}
		}


		internal static void RemoveSample(Sample b)
		{
			for (int i = 0; i < _samples.Count; i++)
			{
				if (
					b.A == _samples[i].A
					&& b.B == _samples[i].B
					&& b.AngleInGrad == _samples[i].AngleInGrad
				)
				{
					_samples.RemoveAt(i);
					_Save();
					_FireOnChanged();
					return;
				}
			}
		}

		internal static void RemovePair(Sample b, QuestionAnswerPair pair)
		{
			for (int i = 0; i < _samples.Count; i++)
			{
				if (
					b.A == _samples[i].A
					&& b.B == _samples[i].B
					&& b.AngleInGrad == _samples[i].AngleInGrad
				)
				{
					_samples[i].RemovePair(pair);
					_Save();
					_FireOnChanged();
					return;
				}
			}
		}
	}


	[Serializable]
	internal class Sample
	{
		public Vector4DSerializable A, B;
		public int AngleInGrad;
		public List<QuestionAnswerPair> pairs = new List<QuestionAnswerPair>();

		public override string ToString()
		{
			return A.ToString() + " " + B.ToString() + " " + AngleInGrad;
		}

		public void AddPair(Vector4D from, Vector4D to)
		{
			Vector4DSerializable fromS = new Vector4DSerializable(from);
			Vector4DSerializable toS = new Vector4DSerializable(to);
			_ThrowIfPairExists(fromS, toS);
			pairs.Add(new QuestionAnswerPair()
			{
				argument = fromS,
				expectedResult = toS
			});
		}


		private void _ThrowIfPairExists(Vector4DSerializable from, Vector4DSerializable to)
		{
			for (int i = 0; i < pairs.Count; i++)
			{
				if (from == pairs[i].argument || to == pairs[i].expectedResult)
				{
					throw new Exception("This pair has one already existing vector");
				}
			}
		}

		internal void RemovePair(QuestionAnswerPair p)
		{
			for (int i = 0; i < pairs.Count; i++)
			{
				if (p.argument == pairs[i].argument && p.expectedResult == pairs[i].expectedResult)
				{
					pairs.RemoveAt(i);
					return;
				}
			}
		}
	}


	[Serializable]
	internal class QuestionAnswerPair
	{
		public Vector4DSerializable argument;
		public Vector4DSerializable expectedResult;

		public override string ToString()
		{
			return argument.ToString() + " => " + expectedResult.ToString();
		}
	}


	[Serializable]
	public class Vector4DSerializable
	{
		public double X, Y, Z, Q;


		public Vector4DSerializable() { }


		public Vector4DSerializable(Vector4D v)
		{
			X = v.X;
			Y = v.Y;
			Z = v.Z;
			Q = v.Q;
		}


		public override string ToString()
		{
			return "(" + X + "; " + Y + "; " + Z + "; " + Q + ")";
		}

		internal Vector4D ToVector4D()
		{
			return new Vector4D(X, Y, Z, Q);
		}

		public static bool operator ==(Vector4DSerializable a, Vector4DSerializable b)
		{
			return a.X == b.X && a.Y == b.Y && a.Z == b.Z && a.Q == b.Q;
		}


		public static bool operator !=(Vector4DSerializable a, Vector4DSerializable b)
		{
			return !(a == b);
		}
	}
}
