using System;
using System.Collections.Generic;
namespace StatementSystem4D
{
	internal class Program
	{
		static Rule[] _rules = new Rule[]
		{
			// [A|B]Alpha =>  C ->  D  ===>
			// [A|B]Alpha => -C -> -D
			new Rule()
				.PickC(s => -s.C)
				.PickD(s => -s.D)
			,


			// [A|B] Alpha => C->D  ===>
			// [B|A] Alpha => D->C
			new Rule()
				.PickA(s => s.B)
				.PickB(s => s.A)
				.PickC(s => s.D)
				.PickD(s => s.C)
			,


			// [A|B] Alpha=90 => C ->  D  ===>
			// [A|B] Alpha=90 => D -> -C
			new Rule()
				.Where(s => s.Alpha == Angle.A90)
				.PickC(s => s.D)
				.PickD(s => -s.C)
			,


			// [ A|B] Alpha => C -> D  ===>
			// [-A|B] Alpha => D -> C
			new Rule()
				.PickA(s => -s.A)
				.PickC(s => s.D)
				.PickD(s => s.C)
			,
		};

		static void Main(string[] args)
		{
			StatementContainer container = new StatementContainer(_rules);
			LoadStatements(container);

			do
			{
				Console.WriteLine("Print Statement:");
				string statement = Console.ReadLine();
				Console.WriteLine("---------------------------------------");
				try
				{
					List<Statement> consequences = container.AddStatement(Statement.Parse(statement));
					PrintConsequences(consequences);
					SaveStatements(container);
				}
				catch (StatementContradictionException contradiction)
				{
					Console.WriteLine(contradiction.ToString());
				}
				catch (Exception) { }
			} while (true);
		}


		private static void SaveStatements(StatementContainer container)
		{
			StatementStorage.Save(container.GetStatementsAsArray());
		}


		private static void LoadStatements(StatementContainer container)
		{
			string[] statements = StatementStorage.Load();
			for (int i = 0; i < statements.Length; i++)
			{
				Statement s = Statement.Parse(statements[i]);
				container.AddStatement(s);
			}
		}


		private static void PrintConsequences(List<Statement> consequences)
		{
			Console.WriteLine("Added New Statements:");
			for (int i = 0; i < consequences.Count; i++)
			{
				Console.WriteLine(consequences[i].ToString());
			}
			Console.WriteLine("---------------------------------------");
		}
	}
}
