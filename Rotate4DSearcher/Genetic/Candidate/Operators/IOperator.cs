using System;
using System.Collections.Generic;

namespace Rotate4DSearcher.Genetic
{
	public abstract class IOperator
	{
		public abstract double Calculate(ArgsBox args);
		public abstract string ToString(ArgsBox args);

		/// <summary>
		/// This method requires deep cloning
		/// </summary>
		/// <returns>new IOperator</returns>
		public abstract IOperator Clone();
		public abstract void AddOperatorsToArray(List<IOperator> list);

		/// <summary>
		/// This method will be called inside cloned tree.
		/// All nodes can change and return themself
		/// </summary>
		/// <param name="rnd"></param>
		/// <returns>new or this</returns>
		public abstract IOperator GetMutatedClone(Random rnd);

		public abstract bool Contains(IOperator children);

		public abstract int GetAmountOfNodes();

		public abstract bool IsZero();

		public abstract IOperator Optimize();
	}
}
