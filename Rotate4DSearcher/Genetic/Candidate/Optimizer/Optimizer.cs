namespace Rotate4DSearcher.Genetic
{
	public static class Optimizer
	{
		private static Rule[] _rules = new Rule[]
		{
			// Constant $ Constant => Constant
			new Rule()
				.Where(op =>
				{
					return op is BinaryOperator binOp
						&& binOp.A is Constant
						&& binOp.B is Constant;
				})
				.Replace(op => new Constant(op.Calculate(ArgsBox.Empty)))
			,


			// 0 * Any => 0
			new Rule()
				.Where(op =>
				{
					return op is BinaryOperator binOp
						&& binOp.action == Action.Multiply
						&& (binOp.A.IsZero() || binOp.B.IsZero());
				})
				.Replace(_ => new Constant(0))
			,


			// A + 0 => A;		0 + 0 => 0
			new Rule()
				.Where(op => {
					return op is BinaryOperator b
						&& b.action == Action.Add
						&& (b.A.IsZero() || b.B.IsZero());
				})
				.Replace(op =>
				{
					BinaryOperator binOp = op as BinaryOperator;
					if (binOp.A.IsZero())
					{
						if (binOp.B.IsZero())
						{
							return new Constant(0);
						}
						return binOp.B;
					}
					return binOp.A;
				})
			,


			// A - A => 0
			new Rule()
				.Where(op =>
				{
					return op is BinaryOperator binOp
						&& binOp.action == Action.Subtract
						&& binOp.A.Equals(binOp.B);
				})
				.Replace(_ => new Constant(0))
			,


			// 1 * A => A
			new Rule()
				.Where(op =>
				{
					return op is BinaryOperator binOp
						&& binOp.action == Action.Multiply
						&& ((binOp.A is Constant ca && ca.Value == 1) || (binOp.B is Constant cb && cb.Value == 1));
				})
				.Replace(op =>
				{
					BinaryOperator binOp = op as BinaryOperator;
					if (binOp.A is Constant ca && ca.Value == 1)
					{
						return binOp.B;
					}
					return binOp.A;
				})
			,


			// (A * B) + (A * C) => A * (B + C)
			new Rule()
				.Where(op =>
				{
					if (op is BinaryOperator b && b.action == Action.Add)
					{
						if (b.A is BinaryOperator _a && b.B is BinaryOperator _b)
						{
							if (_a.action == Action.Multiply && _b.action == Action.Multiply)
							{
								return _a.A.Equals(_b.A)
									|| _a.A.Equals(_b.B)
									|| _a.B.Equals(_b.A)
									|| _a.B.Equals(_b.B);
							}
						}
					}
					return false;
				})
				.Replace(op =>
				{
					BinaryOperator root = op as BinaryOperator;
					BinaryOperator a = root.A as BinaryOperator;
					BinaryOperator b = root.B as BinaryOperator;

					if (a.A.Equals(b.A))
					{
						IOperator sum1 = new BinaryOperator(a.B, Action.Add, b.B);
						return new BinaryOperator(a.A, Action.Multiply, sum1);
					}
					if (a.A.Equals(b.B))
					{
						IOperator sum2 = new BinaryOperator(a.B, Action.Add, b.A);
						return new BinaryOperator(a.A, Action.Multiply, sum2);
					}
					if (a.B.Equals(b.A))
					{
						IOperator sum3 = new BinaryOperator(a.A, Action.Add, b.B);
						return new BinaryOperator(a.B, Action.Multiply, sum3);
					}
					IOperator sum4 = new BinaryOperator(a.A, Action.Add, b.A);
					return new BinaryOperator(a.B, Action.Multiply, sum4);
				})
		};


		public static IOperator Optimize(IOperator root) {
			if (root is BinaryOperator b)
			{
				b.A = Optimize(b.A);
				b.B = Optimize(b.B);
			}

			for (int i = 0; i < _rules.Length; i++)
			{
				root = _rules[i].Optimize(root);
			}

			return root;
		}
	}
}
