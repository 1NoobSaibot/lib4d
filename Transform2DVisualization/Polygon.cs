using Lib4D;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transform2DVisualization
{
	internal class Polygon
	{
		protected Transform2D _transform;
		private Vector2D[] _vertices;
		private Pen _pen = new Pen(new SolidBrush(Color.Green));


		public Vector2D GetVertex(int index)
		{
			return _vertices[index];
		}


		public Polygon(Vector2D center, double radius, double baseAngle, int amountOfVerticies)
		{
			_transform = Transform2D.GetTranslate(center.X, center.Y);
			_transform.Rotate(baseAngle);
			_vertices = new Vector2D[amountOfVerticies];

			double angle = Math.PI * 2 / amountOfVerticies;
			for (int i = 0; i < amountOfVerticies; i++)
			{
				double x = radius * Math.Cos(angle * i);
				double y = radius * Math.Sin(angle * i);
				_vertices[i] = new Vector2D(x, y);
			}
		}


		public void Draw(MyGraphics g)
		{
			g.PushTransform(_transform);
			g.DrawLine(_pen, _vertices[0], _vertices.Last());
			for (int i = 1; i < _vertices.Length; i++)
			{
				g.DrawLine(_pen, _vertices[i], _vertices[i - 1]);
			}
			g.PopTransform();
		}
	}
}
