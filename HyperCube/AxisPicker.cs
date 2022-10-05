using Lib4D;
using System;
using System.Windows.Forms;

namespace HyperCube
{
	public partial class AxisPicker : UserControl
	{
		public AxisPicker()
		{
			InitializeComponent();
		}


		public Bivector4D GetAxis()
		{
			Vector4D a = aVectorPicker.GetVector();
			Vector4D b = bVectorPicker.GetVector();
			return new Bivector4D(a, b);
		}


		public double GetAngle()
		{
			return anglePicker.Value / 180.0 * Math.PI;
		}
	}
}
