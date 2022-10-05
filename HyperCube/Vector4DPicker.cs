using Lib4D;
using System.Windows.Forms;

namespace HyperCube
{
	public partial class Vector4DPicker : UserControl
	{
		public Vector4DPicker()
		{
			InitializeComponent();
		}


		internal Vector4D GetVector()
		{
			Vector4D res = new Vector4D(xPicker.Value, yPicker.Value, qPicker.Value, zPicker.Value);
			if (res.AbsQuad == 0)
			{
				res.X = 1;
			}

			return res.Normalize();
		}
	}
}
