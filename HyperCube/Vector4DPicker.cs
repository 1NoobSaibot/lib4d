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


		internal Vector4D<float> GetVector()
		{
			Vector4D<float> res = new Vector4D<float>(xPicker.Value, yPicker.Value, qPicker.Value, zPicker.Value);
			if (res.AbsQuad == 0)
			{
				res.X = 1;
			}

			res.Normalize();
			return res;
		}
	}
}
