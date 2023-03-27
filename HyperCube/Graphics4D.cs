using Lib4D;

namespace HyperCube
{
	internal class Graphics4D
	{
		private Transform4DFloat _transform = new();
		private readonly Pen _pen = new(Color.White);
		private readonly Graphics _canvas;
		private readonly Graphics _buffer;
		private readonly Image _bufferImage;

		private Projector4dTo2d _4d_to_2D;

		public Transform4DFloat Transform => _transform;

		public Graphics4D (Image canvas)
		{
			_canvas = Graphics.FromImage(canvas);
			_bufferImage = new Bitmap(canvas.Width, canvas.Height);
			_buffer = Graphics.FromImage(_bufferImage);
			_buffer.TranslateTransform(canvas.Width * 0.5f, canvas.Height * 0.5f);

			_4d_to_2D = new Projector4dTo2d(canvas.Width, canvas.Height, canvas.Height * 2);
		}


		public void SetTransform(Transform4DFloat t)
		{
			_transform = t;
		}
		

		public void DrawLine(Vector4DFloat a, Vector4DFloat b)
		{
			DrawLine(_pen, a, b);
		}


		public void DrawLine(Pen pen, Vector4DFloat a, Vector4DFloat b)
		{
			a = _transform * a;
			b = _transform * b;
			Vector3D<float> aP = _World4DToCamera(a);
			Vector3D<float> bP = _World4DToCamera(b);

			aP = _ClearParams(aP);
			bP = _ClearParams(bP);

			_buffer.DrawLine(pen, aP.X, aP.Y, bP.X, bP.Y);
		}


		private Vector3D<float> _ClearParams(Vector3D<float> v)
		{
#pragma warning disable CS1718 // Выполнено сравнение с той же переменной
			if (v.X != v.X)
			{
				v.X = 0;
			}

			if (v.Y != v.Y)
			{
				v.Y = 0;
			}
#pragma warning restore CS1718 // Выполнено сравнение с той же переменной

			return v;
		}

		public void RedrawScreen()
		{
			_canvas.DrawImage(_bufferImage, 0, 0);
		}


		public void Clear()
		{
			_buffer.Clear(Color.Black);
		}

		public void DrawVertex(Vector4DFloat v)
		{
			v = _transform * v;
			Vector3D<float> vProjected = _World4DToCamera(v);
			const float R = 6;
			const float bias = R / 2;
			_buffer.FillEllipse(_pen.Brush, vProjected.X - bias, vProjected.Y - bias, R, R);
		}


		private Vector3D<float> _World4DToCamera(Vector4DFloat v)
		{
			return _4d_to_2D.Project(v) * (_bufferImage.Height / 2f);
		}
	}
}
