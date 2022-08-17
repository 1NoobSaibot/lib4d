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


		public static void AddNewSample(Bivector4D rotationSurface)
		{
			_ThrowIfExists(rotationSurface);

			_samples.Add(new Sample() { rotationSurface = rotationSurface });
			_Save();

			SamplesChanged(_samples, null);
		}


		private static void _ThrowIfExists(Bivector4D surface)
		{
			for (int i = 0; i < _samples.Count; i++)
			{
				if (surface == _samples[i].rotationSurface)
				{
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
	}


	[Serializable]
	internal class Sample
	{
		public Bivector4D rotationSurface;
		public List<QuestionAnswerPair> pairs = new List<QuestionAnswerPair>();
	}


	[Serializable]
	internal class QuestionAnswerPair
	{
		public Vector4D question;
		public Vector4D answer;
	}
}
