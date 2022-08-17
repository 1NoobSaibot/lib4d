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


		public static List<Sample> GetSamples()
		{
			if (_samples == null)
			{
				_Load();
			}

			return _samples;
		}


		public static void AddNewSample(Vector4D a, Vector4D b, int angleInGrad)
		{
			if (_samples == null)
			{
				_Load();
			}

			Vector4DSerializable aS = new Vector4DSerializable(a);
			Vector4DSerializable bS = new Vector4DSerializable(b);
			_ThrowIfExists(aS, bS, angleInGrad);

			Sample sample = new Sample() {
				A = aS,
				B = bS,
				AngleInGrad = angleInGrad
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


		private static void _ThrowIfExists(
			Vector4DSerializable a,
			Vector4DSerializable b,
			int angleInGrad
		) {
			for (int i = 0; i < _samples.Count; i++)
			{
				if (
					a == _samples[i].A
					&& b == _samples[i].B
					&& angleInGrad == _samples[i].AngleInGrad
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
	internal class Vector4DSerializable
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
