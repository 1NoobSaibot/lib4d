using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatementSystem4D
{
	public struct Direction4D
	{
		public readonly int X, Y, Z, Q;

		public Direction4D(int x, int y, int z, int q)
		{
			X = x;
			Y = y;
			Z = z;
			Q = q;
		}


		public static bool operator ==(Direction4D a, Direction4D b)
		{
			return a.X == b.X && a.Y == b.Y && a.Z == b.Z && a.Q == b.Q;
		}


		public static bool operator !=(Direction4D a, Direction4D b)
		{
			return !(a == b);
		}


		public static Direction4D operator -(Direction4D a)
		{
			return new Direction4D(-a.X, -a.Y, -a.Z, -a.Q);
		}


		public override string ToString()
		{
			string
			res  = X != 0 ? (X > 0 ? "X" : "-X") : "";
			res += Y != 0 ? (Y > 0 ? "Y" : "-Y") : "";
			res += Z != 0 ? (Z > 0 ? "Z" : "-Z") : "";
			res += Q != 0 ? (Q > 0 ? "Q" : "-Q") : "";
			return res;
		}


		public static Direction4D Parse(string arg)
		{
			Assert(String.IsNullOrEmpty(arg) == false);

			arg = arg.ToUpper();
			int x = 0, y = 0, z = 0, q = 0;

			bool sign = false;
			for (int i = 0; i < arg.Length; i++)
			{
				char c = arg[i];
				if (c == '-')
				{
					Assert(sign == false);
					sign = true;
					continue;
				}

				if (c == 'X')
				{
					Assert(x == 0);
					x = sign ? -1 : 1;
					sign = false;
				}
				else if (c == 'Y') {
					Assert(y == 0);
					y = sign ? -1 : 1;
					sign = false;
				}
				else if (c == 'Z')
				{
					Assert(z == 0);
					z = sign ? -1 : 1;
					sign = false;
				}
				else if (c == 'Q')
				{
					Assert(q == 0);
					q = sign ? -1 : 1;
					sign = false;
				}
				else
				{
					Assert(false);
				}
			}

			return new Direction4D(x, y, z, q);

			void Assert(bool statement) {
				if (!statement) {
					throw new Exception($"Parse Error: '{arg}'");
				}
			}
		}
	}
}
