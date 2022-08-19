using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Rotate4DSearcher.Genetic
{
	public static class CandidatesStorage
	{
		private const string fileName = "Candidates.json";


		public static List<string[][]> GetCandidates()
		{
			if (!File.Exists(fileName))
			{
				List<string[][]> candidates = new List<string[][]>();
				candidates.Add(FirstCandidate());
				return candidates;
			}

			using (StreamReader reader = new StreamReader(fileName))
			{
				string json = reader.ReadToEnd();
				if (string.IsNullOrEmpty(json))
				{
					List<string[][]> candidates = new List<string[][]>();
					candidates.Add(FirstCandidate());
					return candidates;
				}
				return JsonConvert.DeserializeObject<List<string[][]>>(json);
			}
		}


		public static void Save(List<string[][]> candidates)
		{
			using (StreamWriter writer = new StreamWriter(fileName, false))
			{
				string json = JsonConvert.SerializeObject(candidates);
				writer.Write(json);
			}
		}


		public static string[][] FirstCandidate()
		{
			string s00 = "( c * ( ( yz + yq ) + zq ) ) + ( n * ( ( xq + xz ) + xy ) * 0.5 )";
			string s01 = "( n * xy ) - ( s * zq )";
			string s02 = "( n * xz ) + ( s * yq )";
			string s03 = "( n * xq ) + ( s * yz )";

			string s10 = "( n * xy ) + ( s * zq )";
			string s11 = "( c * ( ( xz + xq ) + zq ) ) + ( n * ( ( yq + yz ) + xy ) * 0.5 )";
			string s12 = "( n * yz ) - ( s * xq )";
			string s13 = "( n * yq ) + ( s * xz )";

			string s20 = "( n * xz ) - ( s * yq )";
			string s21 = "( n * yz ) + ( s * xq )";
			string s22 = "( c * ( ( xy + xq ) + yq ) ) + ( n * ( ( yz + xz ) + zq ) * 0.5 )";
			string s23 = "( n * zq ) - ( s * xy )";

			string s30 = "( n * xq ) - ( s * yz )";
			string s31 = "( n * yq ) - ( s * xz )";
			string s32 = "( n * zq ) + ( s * xy )";
			string s33 = "( c * ( ( xy + xz ) + yz ) ) + ( n * ( ( xq + yq ) + zq ) * 0.5 )";

			return new string[4][]
			{
				new string[4] { s00, s10, s20, s30 },
				new string[4] { s01, s11, s21, s31 },
				new string[4] { s02, s12, s22, s32 },
				new string[4] { s03, s13, s23, s33 },
			};
		}
	}
}
