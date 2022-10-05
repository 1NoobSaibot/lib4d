using Lib4D;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperCube
{
	internal class Graphics4D
	{
		private Transform4D _transform = new Transform4D();
		private Pen _pen = new Pen(new SolidBrush(Color.Green));
		private Graphics _canvas;
		private Graphics _buffer;
		private Image _bufferImage;

		public Transform4D Transform => _transform;

		public Graphics4D (Image canvas)
		{
			_canvas = Graphics.FromImage(canvas);
			_bufferImage = new Bitmap(canvas.Width, canvas.Height);
			_buffer = Graphics.FromImage(_bufferImage);
			_buffer.TranslateTransform(canvas.Width * 0.5f, canvas.Height * 0.5f);
		}


		public void SetTransform(Transform4D t)
		{
			_transform = t;
		}
		

		public void DrawLine(Vector4D a, Vector4D b)
		{
			a = _transform * a;
			b = _transform * b;
			const double cameraDistance = 1000;
			double aDistance = cameraDistance - Math.Sqrt(a.Z * a.Z + a.Q * a.Q);
			double bDistance = cameraDistance - Math.Sqrt(b.Z * b.Z + b.Q * b.Q);
			const double k = 1000;
			double aScale = k / aDistance;
			double bScale = k / bDistance;
			_buffer.DrawLine(_pen, (float)(a.X * aScale), (float)(a.Y * aScale), (float)(b.X * bScale), (float)(b.Y * bScale));
		}


		public void RedrawScreen()
		{
			_canvas.DrawImage(_bufferImage, 0, 0);
		}


		public void Clear()
		{
			_buffer.Clear(Color.Black);
		}
	}
}
