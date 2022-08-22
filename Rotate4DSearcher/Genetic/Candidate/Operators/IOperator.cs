using System;
using System.Collections.Generic;

namespace Rotate4DSearcher.Genetic
{
	public interface IOperator
	{
		double Calculate(ArgsBox args);
		string ToStringFullBracketsString(ArgsBox args);

		/// <summary>
		/// This method requires deep cloning
		/// </summary>
		/// <returns>new IOperator</returns>
		IOperator Clone();
		void AddOperatorsToArray(List<IOperator> list);

		/// <summary>
		/// This method will be called inside cloned tree.
		/// All nodes can change and return themself
		/// </summary>
		/// <param name="rnd"></param>
		/// <returns>new or this</returns>
		IOperator GetMutatedClone(Random rnd);

		bool Contains(IOperator children);

		int GetAmountOfNodes();

		bool IsZero();
	}
}
