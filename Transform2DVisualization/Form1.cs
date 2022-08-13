using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Transform2DVisualization
{
	public partial class Form1 : Form
	{
		private Animation animation;
		private MyGraphics _graphics;
		private DateTime _timeInSeconds;


		public Form1()
		{
			InitializeComponent();
			_graphics = new MyGraphics(this.CreateGraphics(), this.Width, this.Height);
			animation = new Animation(this.Width, this.Height);
			_timeInSeconds = DateTime.Now;
			looper.Start();
		}

		private void canvas_Click(object sender, EventArgs e)
		{

		}

		private void looper_Tick(object sender, EventArgs e)
		{
			DateTime currentMoment = DateTime.Now;
			animation.Move((currentMoment - _timeInSeconds).Ticks / 10000000.0);
			animation.Draw(_graphics);
			_graphics.RedrawFrame();
			_timeInSeconds = currentMoment;
		}
	}
}
