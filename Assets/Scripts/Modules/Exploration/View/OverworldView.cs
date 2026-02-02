using Features.Units.Core;
using Features.Units.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace Modules.Exploration.View
{
	public class OverworldView<TUnit> : MonoBehaviour where TUnit : Unit
	{
		public Unit Unit { get; private set; }

		public virtual void Bind(TUnit unit)
		{
			Unit = unit;
		}
		
	}
}