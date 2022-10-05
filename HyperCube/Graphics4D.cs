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
			const float cameraDistance = 1000;
			float aDistance = cameraDistance - (float)Math.Sqrt(a.Z * a.Z + a.Q * a.Q);
			float bDistance = cameraDistance - (float)Math.Sqrt(b.Z * b.Z + b.Q * b.Q);
			const float k = 1000;
			float aScale = k / aDistance;
			float bScale = k / bDistance;
			_buffer.DrawLine(_pen, a.X * aScale, a.Y * aScale, b.X * bScale, b.Y * bScale);
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
			const float cameraDistance = 1000;
			float aDistance = cameraDistance - (float)Math.Sqrt(v.Z * v.Z + v.Q * v.Q);
			const float k = 1000;
			float aScale = k / aDistance;

			const float R = 6;
			const float bias = R / 2;
			_buffer.FillEllipse(_pen.Brush, v.X * aScale - bias, v.Y * aScale - bias, R, R);
		}
	}
}
