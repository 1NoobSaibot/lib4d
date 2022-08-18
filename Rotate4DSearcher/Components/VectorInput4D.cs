using Lib4D;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace Rotate4DSearcher.Components
{
  class VectorInput4D : GroupBox
  {
		private const int _lineHeight = 20;
		private const int _lineSpace = 10;
		private const int _leftSpace = 4;
		private const int _labelWidth = 10;
		private const int _width = 129;
		private const int _height = 153;

		private Label[] _labelXYZQ = new Label[4];
		private TextBox[] _inputXYZQ = new TextBox[4];
		private Button _normalizeButton;
		private Label _lengthlabel;


    public VectorInput4D (Control parent)
		{
			Width = _width;
			Height = _height;
			_InitLabels();
			_InitInputs();
    }


		public Vector4D GetVector()
		{
			return new Vector4D(
				Double.Parse(_inputXYZQ[0].Text),
				Double.Parse(_inputXYZQ[1].Text),
				Double.Parse(_inputXYZQ[2].Text),
				Double.Parse(_inputXYZQ[3].Text)
			);
		}


		public void SetPosition(int x, int y)
		{
			Bounds = new Rectangle(x, y, _width, _height);
		}


		public void SetVector(Vector4D v)
		{
			_inputXYZQ[0].Text = v.X.ToString();
			_inputXYZQ[1].Text = v.Y.ToString();
			_inputXYZQ[2].Text = v.Z.ToString();
			_inputXYZQ[3].Text = v.Q.ToString();
		}


		private void _InitLabels()
		{
			for (int i = 0; i < _labelXYZQ.Length; i++)
			{
				_labelXYZQ[i] = new Label();
				_labelXYZQ[i].Parent = this;
				_labelXYZQ[i].PointToClient(new Point(10, 20 * i));
				Controls.Add(_labelXYZQ[i]);
				_labelXYZQ[i].Bounds = new Rectangle(
					_leftSpace,
					_lineSpace + 3 + (_lineSpace + _lineHeight) * i,
					10,
					20
				);
			}
			_labelXYZQ[0].Text = "X";
			_labelXYZQ[1].Text = "Y";
			_labelXYZQ[2].Text = "Z";
			_labelXYZQ[3].Text = "Q";

			_lengthlabel = new Label();
			_lengthlabel.Parent = this;
			Controls.Add(_lengthlabel);
			_lengthlabel.Bounds = new Rectangle(_leftSpace, 130, 100, 20);
			_lengthlabel.Text = "Abs:0.00000000000000000000";
		}


		private void _InitInputs()
		{
			for (int i = 0; i < _inputXYZQ.Length; i++)
			{
				_inputXYZQ[i] = new TextBox();
				_inputXYZQ[i].Parent = this;
				Controls.Add(_inputXYZQ[i]);
				_inputXYZQ[i].Text = "0";
				_inputXYZQ[i].Bounds = new Rectangle(
					_leftSpace + _labelWidth + 10,
					_lineSpace + (_lineSpace + _lineHeight) * i,
					100,
					20
				);
				_inputXYZQ[i].TextChanged += _OnValueChanged;
			}

			_normalizeButton = new Button();
			_normalizeButton.Parent = this;
			Controls.Add(_normalizeButton);
			_normalizeButton.Text = "N";
			_normalizeButton.Bounds = new Rectangle(105, 124, 20, 24);
			_normalizeButton.Click += _OnNormalizeButtonClick;
		}


		private void _OnValueChanged(object sender, EventArgs args)
		{
			try
			{
				Vector4D v = GetVector();
				_lengthlabel.Text = "Abs:" + v.Abs;
			}
			catch (Exception error)
			{}
		}


		private void _OnNormalizeButtonClick(object sender, EventArgs args)
		{
			try
			{
				Vector4D v = GetVector();
				v = v.Normalize();
				SetVector(v);
				_lengthlabel.Text = "Abs:" + v.Abs;
			}
			catch (Exception error)
			{ }
		}
	}
}
