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
		private readonly double _width, _height;
		private DateTime _prevTime = DateTime.Now;


		public Form1()
		{
			InitializeComponent();
			_width = canvas.Width;
			_height = canvas.Height;
			canvas.Image = new Bitmap(canvas.Width, canvas.Height);
			graphics = new Graphics4D(canvas.Image);
			float scale = 100;
			// graphics.Transform.Translate(_width * 0.5, _height * 0.5, 0, 0);
			graphics.Transform.Scale(scale, scale, scale, scale);
			looper.Start();

			shapePicker.Items.Add(ShapeIndex.HyperCube);
			shapePicker.Items.Add(ShapeIndex.Hexdecahedroid);
			shapePicker.Items.Add(ShapeIndex.Octaplex);
			shapePicker.Items.Add(ShapeIndex.Dodecaplex);
			shapePicker.SelectedIndex = 0;
		}


		private void looper_Tick(object sender, EventArgs e)
		{
			graphics.Clear();

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
