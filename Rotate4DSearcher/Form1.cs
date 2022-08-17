using Lib4D;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rotate4DSearcher
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}


		private void onLoad(object sender, EventArgs e)
		{
			SamplesStore.GetSamples();
		}

		private void addRotationSurfaceButton_Click(object sender, EventArgs e)
		{
			try
			{
				Vector4D a = new Vector4D();
				Vector4D b = new Vector4D();

				a.X = Double.Parse(aXInput.Text);
				a.Y = Double.Parse(aYInput.Text);
				a.Z = Double.Parse(aZInput.Text);
				a.Q = Double.Parse(aQInput.Text);

				b.X = Double.Parse(bXInput.Text);
				b.Y = Double.Parse(bYInput.Text);
				b.Z = Double.Parse(bZInput.Text);
				b.Q = Double.Parse(bQInput.Text);

				int angleInGrad = Int32.Parse(rotationAngleInput.Text);
				SamplesStore.AddNewSample(a, b, angleInGrad);
			}
			catch (Exception error)
			{
				logLabel.Text = error.Message;
			} 
		}
	}
}
