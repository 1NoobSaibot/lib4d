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
			Vector3DFloat aP = _World4DToCamera(a);
			Vector3DFloat bP = _World4DToCamera(b);

			aP = _ClearParams(aP);
			bP = _ClearParams(bP);

			_buffer.DrawLine(_pen, aP.X, aP.Y, bP.X, bP.Y);
		}

		private Vector3DFloat _ClearParams(Vector3DFloat v)
		{
			if (v.X != v.X)
			{
				v.X = 0;
			}

			if (v.Y != v.Y)
			{
				v.Y = 0;
			}

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

		internal void DrawVertex(Vector4DFloat v)
		{
			v = _transform * v;
			Vector3DFloat vProjected = _World4DToCamera(v);
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
			
			float tan = up.Abs / (from - to).Abs;
			Vector3DFloat P2 = (input - from) * projectionMatrix;
			P2.X = P2.X / (P2.Z * tan) * _bufferImage.Height * 0.5f;
			P2.Y = P2.Y / (P2.Z * tan) * _bufferImage.Height * 0.5f;

			return P2;
		}


		private Vector3DFloat _World4DTo3D(Vector4DFloat input)
		{
			Vector4DFloat from = new Vector4DFloat(0, 0, 0, 0);
			Vector4DFloat to = new Vector4DFloat(0, 0, 2000, 0);
			Vector4DFloat up = new Vector4DFloat(0, _bufferImage.Height * 0.5f, 0, 0);
			Vector4DFloat over = new Vector4DFloat(_bufferImage.Width * 0.5f, 0, 0, 0);

			Vector4DFloat D = (to - from).Normalize();
			Vector4DFloat A = Cross4(up, over, D).Normalize();
			Vector4DFloat B = Cross4(over, D, A).Normalize();
			Vector4DFloat C = Cross4(D, A, B);

			float[,] projectMatrix = new float[4, 4] {
				{ A.X, A.Y, A.Z, A.Q },
				{ B.X, B.Y, B.Z, B.Q },
				{ C.X, C.Y, C.Z, C.Q },
				{ D.X, D.Y, D.Z, D.Q }
			};

			Vector4DFloat p = (input - from) * projectMatrix;

			float tan = up.Abs / (from - to).Abs;
			Vector3DFloat res;
			res.X = p.X * _bufferImage.Height * 0.5f / (input.Q * tan);
			res.Y = p.Y * _bufferImage.Height * 0.5f / (input.Q * tan);
			res.Z = p.Z * _bufferImage.Height * 0.5f / (input.Q * tan);

			return new Vector3DFloat(input.X, input.Y, input.Z);
		}


		private Vector3DFloat _World4DToCamera(Vector4DFloat v)
		{
			return _World3DToCamera(_World4DTo3D(v));
		}

		private Vector4DFloat Cross4(Vector4DFloat a, Vector4DFloat b, Vector4DFloat c)
		{
			Vector4DFloat res = new Vector4DFloat();

			res.X = MatrixMathF.GetDeterminant(new float[3, 3]
			{
				{ a.Y, b.Y, c.Y },
				{ a.Z, b.Z, c.Z },
				{ a.Q, b.Q, c.Q }
			});

			res.Y = -MatrixMathF.GetDeterminant(new float[3, 3]
			{
				{ a.X, b.X, c.X },
				{ a.Z, b.Z, c.Z },
				{ a.Q, b.Q, c.Q }
			});

			res.Z = MatrixMathF.GetDeterminant(new float[3, 3]
			{
				{ a.X, b.X, c.X },
				{ a.Y, b.Y, c.Y },
				{ a.Q, b.Q, c.Q }
			});

			res.Q = -MatrixMathF.GetDeterminant(new float[3, 3]
			{
				{ a.X, b.X, c.X },
				{ a.Y, b.Y, c.Y },
				{ a.Z, b.Z, c.Z }
			});

			return res;
		}
	}
}
