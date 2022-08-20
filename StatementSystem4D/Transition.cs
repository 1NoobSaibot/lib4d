using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatementSystem4D
{
	public class Transition
	{
		public readonly Direction4D From, To;

		public Transition(Direction4D from, Direction4D to)
		{
			this.From = from;
			this.To = to;
		}


		public static Transition Parse(string arg)
		{
			arg = arg.Trim();
			arg = arg.Replace("->", "/");
			string[] from_to = arg.Split('/');

			Direction4D from = Direction4D.Parse(from_to[0]);
			Direction4D to = Direction4D.Parse(from_to[1]);

			return new Transition(from, to);
		}


		public override string ToString()
		{
			return From.ToString() + "->" + To.ToString();
		}


		public static bool operator ==(Transition a, Transition b)
		{
			return a.From == b.From && a.To == b.To;
		}


		public static bool operator !=(Transition a, Transition b)
		{
			return a.From != b.From || a.To != b.To;
		}
	}
}
