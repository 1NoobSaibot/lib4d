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


		internal Vector4DFloat GetVector()
		{
			Vector4DFloat res = new Vector4DFloat(xPicker.Value, yPicker.Value, qPicker.Value, zPicker.Value);
			if (res.AbsQuad == 0)
			{
				res.X = 1;
			}

			return res.Normalize();
		}
	}
}
