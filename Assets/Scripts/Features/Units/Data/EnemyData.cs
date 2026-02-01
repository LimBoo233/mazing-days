using System;
using UnityEngine;

namespace Features.Units.Data
{
	[Serializable]
	public class EnemyData : UnitData
	{
		public Vector3 LogicalPosition { get; set; }
	}
}