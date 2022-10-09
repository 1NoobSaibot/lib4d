using Lib4D;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HyperCube
{
	public partial class Form1 : Form
	{
		private Animation animation = new Animation();
		private Graphics4D graphics;
		private readonly float _width, _height;
		private DateTime _prevTime = DateTime.Now;


		public Form1()
		{
			InitializeComponent();
			_width = Screen.PrimaryScreen.Bounds.Width;
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


		private readonly Vector4DFloat _axisX = new Vector4DFloat(1, 0, 0, 0);
		private readonly Vector4DFloat _axisY = new Vector4DFloat(0, 1, 0, 0);
		private readonly Vector4DFloat _axisZ = new Vector4DFloat(0, 0, 1, 0);
		private readonly Vector4DFloat _axisQ = new Vector4DFloat(0, 0, 0, 1);
		private readonly Pen _penX = new Pen(Color.Green, 3);
		private readonly Pen _penY = new Pen(Color.Red, 3);
		private readonly Pen _penZ = new Pen(Color.LightBlue, 3);
		private readonly Pen _penQ = new Pen(Color.Yellow, 3);
		private void looper_Tick(object sender, EventArgs e)
		{
			graphics.Clear();
			Vector4DFloat zero = new Vector4DFloat();
			graphics.DrawLine(_penX, zero, _axisX);
			graphics.DrawLine(_penY, zero, _axisY);
			graphics.DrawLine(_penZ, zero, _axisZ);
			graphics.DrawLine(_penQ, zero, _axisQ);

			Bivector4DFloat b = axisPicker.GetAxis();
			DateTime currentTime = DateTime.Now;
			float angle = (float)((currentTime - _prevTime).TotalMilliseconds * 0.0001);
			angle *= axisPicker.GetAngle();
			_prevTime = currentTime;
			
			graphics.Transform.Rotate(b, angle);

			animation.Shape = (ShapeIndex)shapePicker.SelectedItem;
			animation.Draw(graphics);
			graphics.RedrawScreen();
			canvas.Invalidate();
		}
	}
}
