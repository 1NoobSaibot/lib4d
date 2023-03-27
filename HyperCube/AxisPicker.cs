using Lib4D;

namespace HyperCube
{
	public partial class AxisPicker : UserControl
	{
		public AxisPicker()
		{
			InitializeComponent();
		}


		public Bivector4D<float> GetAxis()
		{
			var a = aVectorPicker.GetVector();
			var b = bVectorPicker.GetVector();
			return new(a, b);
		}


		public float GetAngle()
		{
			return (float)(anglePicker.Value / 180.0 * Math.PI);
		}
	}
}
