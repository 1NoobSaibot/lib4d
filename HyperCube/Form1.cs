using Lib4D;

namespace HyperCube
{
	public partial class Form1 : Form
	{
		private readonly Animation animation = new();
		private readonly Graphics4D graphics;
		private readonly float _width, _height;
		private DateTime _prevTime = DateTime.Now;


		public Form1()
		{
			InitializeComponent();
			_width = Screen.PrimaryScreen!.Bounds.Width;
			_height = Screen.PrimaryScreen.Bounds.Height;
			canvas.Image = new Bitmap((int)_width, (int)_height);
			graphics = new Graphics4D(canvas.Image);
			graphics.Transform.Translate(0, 0, -10, 0);
			looper.Start();

			shapePicker.Items.Add(ShapeIndex.HyperCube);
			shapePicker.Items.Add(ShapeIndex.Hexdecahedroid);
			shapePicker.Items.Add(ShapeIndex.Octaplex);
			shapePicker.Items.Add(ShapeIndex.Dodecaplex);
			shapePicker.SelectedIndex = 0;
		}


		private readonly Vector4D<float> _axisX = new(1, 0, 0, 0);
		private readonly Vector4D<float> _axisY = new(0, 1, 0, 0);
		private readonly Vector4D<float> _axisZ = new(0, 0, 1, 0);
		private readonly Vector4D<float> _axisQ = new(0, 0, 0, 1);
		private readonly Pen _penX = new(Color.Green, 3);
		private readonly Pen _penY = new(Color.Red, 3);
		private readonly Pen _penZ = new(Color.LightBlue, 3);
		private readonly Pen _penQ = new(Color.Yellow, 3);
		private void Looper_Tick(object sender, EventArgs e)
		{
			graphics.Clear();
			Vector4D<float> zero = new();
			graphics.DrawLine(_penX, zero, _axisX);
			graphics.DrawLine(_penY, zero, _axisY);
			graphics.DrawLine(_penZ, zero, _axisZ);
			graphics.DrawLine(_penQ, zero, _axisQ);

			(var a1, var a2) = axisPicker.GetAxis();
			DateTime currentTime = DateTime.Now;
			float angle = (float)((currentTime - _prevTime).TotalMilliseconds * 0.0001);
			angle *= axisPicker.GetAngle();
			_prevTime = currentTime;

			graphics.Transform.Rotate(a1, a2, angle);

			animation.Shape = (ShapeIndex)shapePicker.SelectedItem!;
			animation.Draw(graphics);
			graphics.RedrawScreen();
			canvas.Invalidate();
		}
	}
}
