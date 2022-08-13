using Lib4D;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transform2DVisualization
{
	internal class MyGraphics
	{
		private Graphics _generalGraphics;
		private Stack<Transform2D> _transforms = new Stack<Transform2D>();
		private Image _bufferImage;
		private Graphics _bufferGraphics;
		public MyGraphics(Graphics g, int width, int height)
		{
			_generalGraphics = g;
			_bufferImage = new Bitmap(width, height);
			_bufferGraphics = Graphics.FromImage(_bufferImage);
			_transforms.Push(new Transform2D());
		}


		public void PushTransform(Transform2D transform)
		{
			_transforms.Push(_transforms.First() * transform);
		}


		public void PopTransform()
		{
			_transforms.Pop();
		}


		public void DrawLine(Pen pen, Vector2D a, Vector2D b)
		{
			a = _transforms.First() * a;
			b = _transforms.First() * b;
			_bufferGraphics.DrawLine(pen, (float)a.X, (float)a.Y, (float)b.X, (float)b.Y);
		}


		public void Clear(Color color)
		{
			_bufferGraphics.Clear(color);
		}


		public void RedrawFrame()
		{
			_generalGraphics.DrawImage(_bufferImage, 0, 0);
		}
	}
}
