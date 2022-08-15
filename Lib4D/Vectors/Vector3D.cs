namespace Lib4D
{
	public struct Vector3D
	{
		public double X, Y, Z;


		public Vector3D (double x, double y, double z)
		{
			X = x;
			Y = y;
			Z = z;
		}


		public static Vector3D operator +(Vector3D a, Vector3D b)
		{
			return new Vector3D (a.X + b.X, a.Y + b.Y, a.Z + b.Z);
		}


		public static Vector3D operator -(Vector3D a, Vector3D b)
		{
			return new Vector3D(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
		}


		public override string ToString()
		{
			return "(" + X + "; " + Y + "; " + Z + ")";
		}
	}
}
