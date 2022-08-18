using System.Windows.Forms;

namespace Rotate4DSearcher.Components
{
	public partial class RotationSurfaceInput4D : UserControl
	{
		public RotationSurfaceInput4D()
		{
			InitializeComponent();
		}


		public CustomBivector4D GetBivector()
		{
			return new CustomBivector4D()
			{
				A = new Vector4DSerializable(vectorAInput.GetVector()),
				B = new Vector4DSerializable(vectorBInput.GetVector()),
				AngleInGrad = int.Parse(AngleGradInput.Text)
			};
		}


		public void SetBivector(CustomBivector4D bv)
		{
			vectorAInput.SetVector(bv.A.ToVector4D());
			vectorBInput.SetVector(bv.B.ToVector4D());
			AngleGradInput.Text = bv.AngleInGrad.ToString();
		}
	}
}
