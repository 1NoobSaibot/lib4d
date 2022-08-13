using Lib4D;
using System;
using System.Drawing;

namespace Transform2DVisualization
{
	internal class Animation
	{
		private Transform2D _world;
		private FractalNode _polygon = new FractalNode(new Vector2D(), 300, 0, 6, 3);
		private double _angleSpeed = Math.PI / 15;


		public Animation(int width, int height)
		{
			_world = Transform2D.GetTranslate(width / 2, height / 2);
		}


		public void Move(double deltaTime)
		{
			_polygon.Move(deltaTime);
		}


		public void Draw(MyGraphics g)
		{
			g.PushTransform(_world);
			g.Clear(Color.Black);
			_polygon.Draw(g);
			g.PopTransform();
		}
	}
}
