using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatementSystem4D
{
	public static class StatementStorage
	{
		private const string fileName = "Statements.txt";


		public static string[] Load()
		{
			if (!File.Exists(fileName))
			{
				return new string[0];
			}

			using (StreamReader reader = new StreamReader(fileName))
			{
				string[] res = reader.ReadToEnd().Split('\n');
				return res;
			}
		}


		public static void Save(Statement[] statements)
		{
			using (StreamWriter writer = new StreamWriter(fileName, false))
			{
				StringBuilder builder = new StringBuilder(statements[0].ToString());
				for (int i = 1; i < statements.Length; i++)
				{
					builder.Append("\n" + statements[i].ToString());
				}
				writer.Write(builder.ToString());
			}
		}
	}
}
