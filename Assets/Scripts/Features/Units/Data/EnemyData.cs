using System;
using UnityEngine;

namespace Features.Units.Data
{
	[Serializable]
	public class EnemyData : IUnitData
	{
		public Vector3 LogicalPosition { get; set; }
	}
}