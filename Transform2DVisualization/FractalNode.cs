using Lib4D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transform2DVisualization
{
	internal class FractalNode : Polygon
	{
		private FractalNode[] _children;


		public FractalNode(Vector2D center, double radius, double baseAngle, int amountOfVerticies, int depth)
			: base(center, radius, baseAngle, amountOfVerticies)
		{
			if (depth == 0)
			{
				_children = new FractalNode[0];
				return;
			}

			_children = new FractalNode[amountOfVerticies];
			double childRadius = radius * 0.333;
			double angle = Math.PI * 2 / amountOfVerticies;
			depth--;
			for (int i = 0; i < amountOfVerticies; i++)
			{
				Vector2D childCenter = GetVertex(i);
				_children[i] = new FractalNode(childCenter, childRadius, angle * i, amountOfVerticies, depth);
			}
		}


		public new void Draw(MyGraphics g)
		{
			base.Draw(g);
			g.PushTransform(_transform);

			for (int i = 0; i < _children.Length; i++)
			{
				_children[i].Draw(g);
			}

			g.PopTransform();
		}
	}
}
