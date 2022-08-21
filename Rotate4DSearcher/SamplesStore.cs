using Lib4D;
using Newtonsoft.Json;
using Rotate4DSearcher.Genetic;
using StatementSystem4D;
using System;
using System.Collections.Generic;
using System.IO;

namespace Rotate4DSearcher
{
	internal static class SamplesStore
	{
		private const string fileName = "Statements.txt";


		public static GeneticSample[] GetSamples()
		{
			if (!File.Exists(fileName))
			{
				throw new Exception("Where is the file???");
			}

			using (StreamReader reader = new StreamReader(fileName))
			{
				string[] strings = reader.ReadToEnd().Split('\n');
				return _GenerateSamples(strings);
			}
		}


		private static GeneticSample[] _GenerateSamples(string[] strings)
		{
			Statement[] statements = new Statement[strings.Length];
			for (int i = 0; i < strings.Length; i++)
			{
				statements[i] = Statement.Parse(strings[i]);
			}

			return _PackInSamples(statements);
		}


		private static GeneticSample[] _PackInSamples(Statement[] statements)
		{
			List<GeneticSample> samples = new List<GeneticSample>();

			for (int i = 0; i < statements.Length; i++)
			{
				GeneticSample sample = _FindOrCreateAndAdd(statements[i], samples);

				Vector4D from = statements[i].Transition.From.ToVector4D();
				Vector4D to = statements[i].Transition.To.ToVector4D();
				GeneticPair pair = new GeneticPair(from, to);
				sample.PushPair(pair);
			}

			return samples.ToArray();
		}


		private static GeneticSample _FindOrCreateAndAdd(Statement what, List<GeneticSample> where)
		{
			Vector4D a = what.A.ToVector4D();
			Vector4D b = what.B.ToVector4D();
			Bivector4D bv = new Bivector4D(a, b);
			double angle = _ToRadians(what.Alpha);

			Rotation r = new Rotation(bv, angle);

			for (int i = 0; i < where.Count; i++)
			{
				GeneticSample _sample = where[i];
				if (_sample.rotation == r)
				{
					return _sample;
				}
			}

			GeneticSample sample = new GeneticSample(r);
			where.Add(sample);
			return sample;
		}


		private static double _ToRadians(Angle angle)
		{
			switch (angle)
			{
				case Angle.A0:
					return 0;
				case Angle.A90:
					return Math.PI / 2;
				case Angle.AMinus90:
					return -Math.PI / 2;
				case Angle.A180:
					return Math.PI;
				case Angle.A120:
					return Math.PI * 2 / 3;
				case Angle.AMinus120:
					return -Math.PI * 2 / 3;
			}

			throw new Exception("Unknow angle type: " + angle);
		}
	}
}
