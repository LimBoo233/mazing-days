using Features.Units.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace Modules.Exploration.View
{
	public class OverworldView<TData> : MonoBehaviour where TData : UnitData
	{
		public TData UnitData { get; private set; }

		public virtual void Bind(TData unitData)
		{
			this.UnitData = unitData;
		}
	}
}