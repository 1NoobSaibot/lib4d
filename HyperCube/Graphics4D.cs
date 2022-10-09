using Lib4D;
using System;
using System.Drawing;

namespace HyperCube
{
	internal class Graphics4D
	{
		private Transform4DFloat _transform = new Transform4DFloat();
		private Pen _pen = new Pen(new SolidBrush(Color.Green));
		private Graphics _canvas;
		private Graphics _buffer;
		private Image _bufferImage;

		public Transform4DFloat Transform => _transform;

		public Graphics4D (Image canvas)
		{
			_canvas = Graphics.FromImage(canvas);
			_bufferImage = new Bitmap(canvas.Width, canvas.Height);
			_buffer = Graphics.FromImage(_bufferImage);
			_buffer.TranslateTransform(canvas.Width * 0.5f, canvas.Height * 0.5f);
		}


		public void SetTransform(Transform4DFloat t)
		{
			_transform = t;
		}
		

		public void DrawLine(Vector4DFloat a, Vector4DFloat b)
		{
			a = _transform * a;
			b = _transform * b;
			Vector3DFloat aP = _World3DToCamera(new Vector3DFloat(a.X, a.Y, a.Z));
			Vector3DFloat bP = _World3DToCamera(new Vector3DFloat(b.X, b.Y, b.Z));
			_buffer.DrawLine(_pen, a.X, a.Y, b.X, b.Y);
		}


		public void RedrawScreen()
		{
			_canvas.DrawImage(_bufferImage, 0, 0);
		}


		public void Clear()
		{
			_buffer.Clear(Color.Black);
		}

		internal void DrawVertex(Vector4DFloat v)
		{
			v = _transform * v;
			Vector3DFloat vProjected = _World3DToCamera(new Vector3DFloat(v.X, v.Y, v.Z));
			const float R = 6;
			const float bias = R / 2;
			_buffer.FillEllipse(_pen.Brush, vProjected.X - bias, vProjected.Y - bias, R, R);
		}

		private Vector3DFloat _World3DToCamera(Vector3DFloat input)
		{
			Vector3DFloat from = new Vector3DFloat(0, 0, 0);
			Vector3DFloat to = new Vector3DFloat(0, 0, 2000);
			Vector3DFloat up = new Vector3DFloat(0, _bufferImage.Height * 0.5f, 0);

			Vector3DFloat C = (to - from).Normalize();
			Vector3DFloat A = (up * C).Normalize();
			Vector3DFloat B = C * A;

			float[,] projectionMatrix = new float[3, 3]
			{
				{ A.X, A.Y, A.Z },
				{ B.X, B.Y, B.Z },
				{ C.X, C.Y, C.Z }
			};
			// projectionMatrix = MatrixMathF.Transpose(projectionMatrix);

			return (input - from) * projectionMatrix;
		}
	}
}
