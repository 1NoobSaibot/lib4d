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


		public Bivector4DFloat GetAxis()
		{
			Vector4DFloat a = aVectorPicker.GetVector();
			Vector4DFloat b = bVectorPicker.GetVector();
			return new Bivector4DFloat(a, b);
		}


		public float GetAngle()
		{
			return (float)(anglePicker.Value / 180.0 * Math.PI);
		}
	}
}
