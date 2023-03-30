using Lib4D;

namespace HyperCube
{
	public partial class AxisPicker : UserControl
	{
		public AxisPicker()
		{
			InitializeComponent();
		}


		public (Vector4D<float>, Vector4D<float>) GetAxis()
		{
			var a = aVectorPicker.GetVector();
			var b = bVectorPicker.GetVector();
			return (a, b);
		}


		public float GetAngle()
		{
			return (float)(anglePicker.Value / 180.0 * Math.PI);
		}
	}
}
