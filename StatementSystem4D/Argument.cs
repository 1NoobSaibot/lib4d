using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatementSystem4D
{
	public class Argument
	{
		public readonly Direction4D A, B;
		public readonly Angle Alpha;


		public Argument(Direction4D a, Direction4D b, Angle alpha)
		{
			A = a;
			B = b;
			Alpha = alpha;
		}


		public static Argument Parse(string arg)
		{
			arg = arg.Replace("[", "");
			string[] vectors_angle = arg.Split(']');
			string[] va_vb = vectors_angle[0].Split('|');

			Angle angle = (Angle)Int32.Parse(vectors_angle[1]);
			Direction4D a = Direction4D.Parse(va_vb[0]);
			Direction4D b = Direction4D.Parse(va_vb[1]);
			return new Argument(a, b, angle);
		}


		public static bool operator ==(Argument a, Argument b)
		{
			return a.A == b.A && a.B == b.B && a.Alpha == b.Alpha;
		}


		public static bool operator !=(Argument a, Argument b)
		{
			return !(a == b);
		}


		public override bool Equals(object obj)
		{
			if (obj is Argument a)
			{
				return this == a;
			} 
			return false;
		}


		public override string ToString()
		{
			return "[" + A.ToString() + "|" + B.ToString() + "]" + ((int)Alpha);
		}
	}


	public enum Angle
	{
		AMinus90 = -90,
		A0 = 0,
		A90 = 90,
		A120 = 120,
		AMinus120 = -120,
		A180 = 180,
	}
}
