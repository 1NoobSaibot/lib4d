using Lib4D.Mathematic;

namespace HyperCube
{
	internal static class Program
	{
		/// <summary>
		/// Главная точка входа для приложения.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Math<float>.InitInstance(new MathFloat());
			Math<double>.InitInstance(new MathDouble());

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
}
