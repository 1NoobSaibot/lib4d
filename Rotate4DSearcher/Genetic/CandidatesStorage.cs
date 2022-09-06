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
			string s00 = "( c * ( ( yz + yq ) + zq ) ) + ( ( 1 - c ) * ( ( xq + xz ) + xy ) * 0,5 )";
			string s01 = "( ( 1 - c ) * xy ) - ( s * zq )";
			string s02 = "( ( 1 - c ) * xz ) + ( s * yq )";
			string s03 = "( ( 1 - c ) * xq ) + ( s * yz )";

			string s10 = "( ( 1 - c ) * xy ) + ( s * zq )";
			string s11 = "( c * ( ( xz + xq ) + zq ) ) + ( ( 1 - c ) * ( ( yq + yz ) + xy ) * 0,5 )";
			string s12 = "( ( 1 - c ) * yz ) - ( s * xq )";
			string s13 = "( ( 1 - c ) * yq ) + ( s * xz )";

			string s20 = "( ( 1 - c ) * xz ) - ( s * yq )";
			string s21 = "( ( 1 - c ) * yz ) + ( s * xq )";
			string s22 = "( c * ( ( xy + xq ) + yq ) ) + ( ( 1 - c ) * ( ( yz + xz ) + zq ) * 0,5 )";
			string s23 = "( ( 1 - c ) * zq ) - ( s * xy )";

			string s30 = "( ( 1 - c ) * xq ) - ( s * yz )";
			string s31 = "( ( 1 - c ) * yq ) - ( s * xz )";
			string s32 = "( ( 1 - c ) * zq ) + ( s * xy )";
			string s33 = "( c * ( ( xy + xz ) + yz ) ) + ( ( 1 - c ) * ( ( xq + yq ) + zq ) * 0,5 )";

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
