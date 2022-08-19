using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Rotate4DSearcher.Genetic
{
	public static class CandidatesStorage
	{
		private const string fileName = "Candidates.json";
		private static List<string[,]> _candidates;


		static CandidatesStorage()
		{
			_Load();
		}


		public static List<string[,]> GetCandidates()
		{
			return _candidates;
		}


		private static void _Load()
		{
			if (!File.Exists(fileName))
			{
				_candidates = new List<string[,]>();
				_candidates.Add(FirstCandidate());
				_Save();
				return;
			}

			using (StreamReader reader = new StreamReader(fileName))
			{
				string json = reader.ReadToEnd();
				_candidates = JsonConvert.DeserializeObject<List<string[,]>>(json);
			}
		}


		private static void _Save()
		{
			using (StreamWriter writer = new StreamWriter(fileName, false))
			{
				string json = JsonConvert.SerializeObject(_candidates);
				writer.Write(json);
			}
		}


		public static string[,] FirstCandidate()
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

			return new string[4, 4]
			{
				{ s00, s10, s20, s30 },
				{ s01, s11, s21, s31 },
				{ s02, s12, s22, s32 },
				{ s03, s13, s23, s33 },
			};
		}
	}
}
