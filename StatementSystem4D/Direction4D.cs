using Lib4D;
using System;

namespace StatementSystem4D
{
	public struct Direction4D
	{
		public readonly int X, Y, Z, Q;

		// It means that this vector is positive or negative X, Y, Z or Q only
		public bool IsBasic => (Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z) + Math.Abs(Q)) == 1;

		public bool IsPositiveBasic => IsBasic && ((X + Y + Z + Q) > 0);

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


		/// <summary>
		/// Returns True if Vectors has no common set components
		/// X, YZQ => true
		/// XY, -XZ => false
		/// </summary>
		/// <param name="a"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		internal bool HasNoCommonWith(Direction4D a)
		{
			return (Math.Abs(X) == 0 || Math.Abs(a.X) == 0)
				&& (Math.Abs(Y) == 0 || Math.Abs(a.Y) == 0)
				&& (Math.Abs(Z) == 0 || Math.Abs(a.Z) == 0)
				&& (Math.Abs(Q) == 0 || Math.Abs(a.Q) == 0);
		}


		public static bool operator !=(Direction4D a, Direction4D b)
		{
			return !(a == b);
		}


		public static Direction4D operator -(Direction4D a)
		{
			return new Direction4D(-a.X, -a.Y, -a.Z, -a.Q);
		}


		public static Direction4D operator +(Direction4D a, Direction4D b)
		{
			return new Direction4D(
				_Sign(a.X + b.X),
				_Sign(a.Y + b.Y),
				_Sign(a.Z + b.Z),
				_Sign(a.Q + b.Q)
			);
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


		public Vector4DDouble ToVector4D()
		{
			Vector4DDouble v = new Vector4DDouble(X, Y, Z, Q);
			return v.Normalize();
		}


		private static int _Sign(int value)
		{
			if (value > 0)
			{
				return 1;
			}
			if (value < 0)
			{
				return -1;
			}
			return 0;
		}
	}
}
