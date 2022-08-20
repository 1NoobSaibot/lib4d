using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatementSystem4D
{
	internal class Program
	{
		static Rule[] _rules = new Rule[]
		{
			new Rule(ArgumentVector.A, ArgumentVector.B, TransitionVector.MinusC, TransitionVector.MinusD),
			new Rule(ArgumentVector.B, ArgumentVector.A, TransitionVector.D, TransitionVector.C),
			new Rule(ArgumentVector.A, ArgumentVector.B, TransitionVector.D, TransitionVector.MinusC, new Angle[]{Angle.A90}),
			new Rule(ArgumentVector.MinusA, ArgumentVector.B, TransitionVector.D, TransitionVector.C),
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
