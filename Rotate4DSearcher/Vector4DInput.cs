using Lib4D;
using System;
using System.Windows.Forms;

namespace Rotate4DSearcher
{
	internal class Vector4DInput
	{
		private TextBox _xInput;
		private TextBox _yInput;
		private TextBox _zInput;
		private TextBox _qInput;


		public Vector4DInput(TextBox xInput, TextBox yInput, TextBox zInput, TextBox qInput)
		{
			_xInput = xInput;
			_yInput = yInput;
			_zInput = zInput;
			_qInput = qInput;
		}


		public Vector4D GetVector()
		{
			return new Vector4D(
				Double.Parse(_xInput.Text),
				Double.Parse(_yInput.Text),
				Double.Parse(_zInput.Text),
				Double.Parse(_qInput.Text)
			);
		}


		public void SetVector(Vector4D v)
		{
			_xInput.Text = v.X.ToString();
			_yInput.Text = v.Y.ToString();
			_zInput.Text = v.Z.ToString();
			_qInput.Text = v.Q.ToString();
		}
	}
}
