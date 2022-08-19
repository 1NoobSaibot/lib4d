using Lib4D;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rotate4DSearcher.Genetic;
using Rotate4DSearcher.Genetic.Candidate;
using System;

namespace Rotate4DSearcherTests
{
	[TestClass]
	public class CandidateSpec
	{
		[TestMethod]
		public void CanPassTransform4DTest()
		{
			Candidate candidate = new Candidate(CandidatesStorage.FirstCandidate());

			Vector4D z = new Vector4D(0, 0, 1, 0);
			Vector4D q = new Vector4D(0, 0, 0, 1);
			Vector4D x = new Vector4D(1, 0, 0, 0);
			Vector4D y = new Vector4D(0, 1, 0, 0);

			// Axis ZQ, X => Y
			Bivector4D surface = new Bivector4D(z, q);
			Transform4D t = candidate.CreateTransform(surface, Math.PI / 2);
			_AreApproximatelyEqual(y, t * x);

			// Axis YQ, Z => X
			surface = new Bivector4D(y, q);
			t = candidate.CreateTransform(surface, Math.PI / 2);
			_AreApproximatelyEqual(x, t * z);

			// Axis YZ, Q => X
			surface = new Bivector4D(y, z);
			t = candidate.CreateTransform(surface, Math.PI / 2);
			_AreApproximatelyEqual(x, t * q);

			// Axis XQ, Y => Z
			surface = new Bivector4D(x, q);
			t = candidate.CreateTransform(surface, Math.PI / 2);
			_AreApproximatelyEqual(z, t * y);

			// Axis XZ, Q => Y
			surface = new Bivector4D(x, z);
			t = candidate.CreateTransform(surface, Math.PI / 2);
			_AreApproximatelyEqual(y, t * q);

			// Axis XY, Z => Q
			surface = new Bivector4D(x, y);
			t = candidate.CreateTransform(surface, Math.PI / 2);
			_AreApproximatelyEqual(q, t * z);
		}


		private void _AreApproximatelyEqual(Vector4D expected, Vector4D actual)
		{
			try
			{
				_AreApproximatelyEqual(expected.X, actual.X);
				_AreApproximatelyEqual(expected.Y, actual.Y);
				_AreApproximatelyEqual(expected.Z, actual.Z);
				_AreApproximatelyEqual(expected.Q, actual.Q);
			}
			catch
			{
				Assert.AreEqual(expected, actual);
			}
		}


		private void _AreApproximatelyEqual(double expected, double actual)
		{
			if (actual == expected)
			{
				return;
			}

			const double allowableError = 1E-15;
			Assert.IsTrue(Math.Abs(expected - actual) < allowableError);
		}
	}
}
