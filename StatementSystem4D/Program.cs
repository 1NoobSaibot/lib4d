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
				.Where((s, _) => s.Alpha == Angle.A90)
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

			
			// [X|Q] 90 => Y->Z		|			|		[XYZ|Q] 120 => Y->Z  <= We generate only this one.
			// [Y|Q] 90 => Z->X		| ==>	|		[XYZ|Q] 120 => Z->X
			// [Z|Q] 90 => X->Y		|			|		[XYZ|Q] 120 => X->Y
			new Rule()
				.Where((s, query) =>
				{
					if (
						s.Alpha != Angle.A90
						|| !s.AreAllVectorsBasic()
						|| !s.AreAllVectorsDifferent()
					)
					{
						return false;
					}

					List<Statement> statements = query((sq) => {
						if (sq.Alpha != Angle.A90 || sq.B != s.B)
						{
							return false;
						}

						return  (sq.A == s.C && sq.C == s.D && sq.D == s.A)
									||(sq.A == s.D && sq.C == s.A && sq.D == s.C);
					});

					if (statements.Count < 2)
					{
						return false;
					}

					return true;
				})
				.PickA(s => s.A + s.C + s.D)
				.PickAlpha(_ => Angle.A120)
			,


			// [X|Q] 90 => Y->Z		|			|		[XYZ|Q] 120 => Y->Z
			// [Y|Q] 90 => Z->X		| ==>	|		[XYZ|Q] 120 => Z->X  <= We generate only this one.
			// [Z|Q] 90 => X->Y		|			|		[XYZ|Q] 120 => X->Y
			new Rule()
				.Where((s, query) =>
				{
					if (
						s.Alpha != Angle.A90
						|| !s.AreAllVectorsBasic()
						|| !s.AreAllVectorsDifferent()
					)
					{
						return false;
					}

					List<Statement> statements = query((sq) => {
						if (sq.Alpha != Angle.A90 || sq.B != s.B)
						{
							return false;
						}

						return  (sq.A == s.C && sq.C == s.D && sq.D == s.A)
									||(sq.A == s.D && sq.C == s.A && sq.D == s.C);
					});

					if (statements.Count < 2)
					{
						return false;
					}

					return true;
				})
				.PickA(s => s.A + s.C + s.D)
				.PickAlpha(_ => Angle.A120)
				.PickC(s => s.D)
				.PickD(s => s.A)
			,


			// [X|Q] 90 => Y->Z		|			|		[XYZ|Q] 120 => Y->Z
			// [Y|Q] 90 => Z->X		| ==>	|		[XYZ|Q] 120 => Z->X
			// [Z|Q] 90 => X->Y		|			|		[XYZ|Q] 120 => X->Y  <= We generate only this one.
			new Rule()
				.Where((s, query) =>
				{
					if (
						s.Alpha != Angle.A90
						|| !s.AreAllVectorsBasic()
						|| !s.AreAllVectorsDifferent()
					)
					{
						return false;
					}

					int Count = query((sq) => {
						if (sq.Alpha != Angle.A90 || sq.B != s.B)
						{
							return false;
						}

						return  (sq.A == s.C && sq.C == s.D && sq.D == s.A)
									||(sq.A == s.D && sq.C == s.A && sq.D == s.C);
					}).Count;

					if (Count < 2)
					{
						return false;
					}

					return true;
				})
				.PickA(s => s.A + s.C + s.D)
				.PickAlpha(_ => Angle.A120)
				.PickC(s => s.A)
				.PickD(s => s.C)
			,


			// [A|B] Angle => C->D		===>
			// [A|B] 0		 => C->C
			new Rule()
				.Where((s, _) => s.Alpha != Angle.A0)
				.PickAlpha(_ => Angle.A0)
				.PickD(s => s.C)
			,


			// [A|B] 90  => C ->  D    =>>>>
			// [A|B] 180 => C -> -C
			new Rule()
				.Where((s, _) => s.Alpha == Angle.A90)
				.PickAlpha(_ => Angle.A180)
				.PickD(s => -s.C)
			,


			// [A|B] 90  => C -> D    =>>>>
			// [A|B] -90 => D -> C
			new Rule()
				.Where((s, _) => s.Alpha == Angle.A90)
				.PickAlpha(_ => Angle.AMinus90)
				.PickC(s => s.D)
				.PickD(s => s.C)
			,


			// [A|B] 90  => C->D			=>>>>
			// [A|B] 90	 => CD->-CD
			new Rule()
				.Where((s, _) => s.Alpha == Angle.A90 && s.AreAllVectorsBasic() && s.AreAllVectorsDifferent())
				.PickC(s => s.C + s.D)
				.PickD(s => -s.C + s.D)
		};

		static void Main(string[] args)
		{
			StatementContainer container = new StatementContainer(_rules);
			LoadStatements(container);
			SaveStatements(container);

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
