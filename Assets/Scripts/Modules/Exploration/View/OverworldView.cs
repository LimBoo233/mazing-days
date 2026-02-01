using Features.Units.Data;
using UnityEngine;

namespace Modules.Exploration.View
{
	public class OverworldView<TData> : MonoBehaviour where TData : IUnitData
	{
		private TData _unitData;

		public virtual void Bind(TData unitData)
		{
			_unitData = unitData;
		}
	}
}