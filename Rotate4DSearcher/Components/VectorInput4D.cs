using Lib4D;
using System;
using System.Windows.Forms;

namespace Rotate4DSearcher.Components
{
	public partial class VectorInput4D : UserControl
	{
		public VectorInput4D()
		{
			InitializeComponent();
		}


		public Vector4D GetVector()
		{
			return new Vector4D(
				Double.Parse(InputX.Text),
				Double.Parse(InputY.Text),
				Double.Parse(InputZ.Text),
				Double.Parse(InputQ.Text)
			);
		}


		public void SetVector(Vector4D v)
		{
			InputX.Text = v.X.ToString();
			InputY.Text = v.Y.ToString();
			InputZ.Text = v.Z.ToString();
			InputQ.Text = v.Q.ToString();
		}


		private void NormalizeButton_Click(object sender, EventArgs e)
		{
			try
			{
				Vector4D v = GetVector();
				v = v.Normalize();
				SetVector(v);
				LengthLabel.Text = "Abs:" + v.Abs;
			}
			catch (Exception error)
			{ }
		}


		private void OnInputChanged(object sender, EventArgs e)
		{
			try
			{
				Vector4D v = GetVector();
				LengthLabel.Text = "Abs:" + v.Abs;
			}
			catch (Exception error)
			{ }
		}
	}
}
