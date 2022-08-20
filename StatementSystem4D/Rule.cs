using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatementSystem4D
{
	public class Rule
	{
		public readonly ArgumentVector A, B;
		public readonly TransitionVector C, D;
		private readonly Angle[] AllowedAngles;

		public Rule(ArgumentVector a, ArgumentVector b, TransitionVector c, TransitionVector d)
		{
			A = a;
			B = b;
			C = c;
			D = d;
		}

		public Rule(
			ArgumentVector a,
			ArgumentVector b,
			TransitionVector c,
			TransitionVector d,
			Angle[] whereAngleIsOneOf
		) : this(a, b, c, d)
		{
			AllowedAngles = whereAngleIsOneOf;
		}

		public Statement CreateNewStatement(Statement baseStatement)
		{
			if (_AngleIsSupported(baseStatement.Argument.Alpha) == false)
			{
				return baseStatement;
			}

			Argument argument = new Argument(
				_Take(A, baseStatement),
				_Take(B, baseStatement),
				baseStatement.Argument.Alpha
			);

			Transition transition = new Transition(
				_Take(C, baseStatement),
				_Take(D, baseStatement)
			);

			return new Statement(argument, transition);
		}


		private bool _AngleIsSupported(Angle alpha)
		{
			if (AllowedAngles == null)
			{
				return true;
			}

			return AllowedAngles.Contains(alpha);
		}


		private Direction4D _Take(ArgumentVector what, Statement from)
		{
			switch (what)
			{
				case ArgumentVector.A:
					return from.Argument.A;
				case ArgumentVector.B:
					return from.Argument.B;
				case ArgumentVector.MinusA:
					return -from.Argument.A;
				case ArgumentVector.MinusB:
					return -from.Argument.B;
			}

			throw new Exception("Unknow ArgumentVector type: " + what);
		}


		private Direction4D _Take(TransitionVector what, Statement from)
		{
			switch (what)
			{
				case TransitionVector.C:
					return from.Transition.From;
				case TransitionVector.D:
					return from.Transition.To;
				case TransitionVector.MinusC:
					return -from.Transition.From;
				case TransitionVector.MinusD:
					return -from.Transition.To;
			}

			throw new Exception("Unknown TransitionVector type: " + what);
		}
	}


	public enum ArgumentVector
	{
		A = 1,
		B = 2,
		MinusA = 3,
		MinusB = 4
	}


	public enum TransitionVector
	{
		C = 1,
		D = 2,
		MinusC = 3,
		MinusD = 4
	}
}
