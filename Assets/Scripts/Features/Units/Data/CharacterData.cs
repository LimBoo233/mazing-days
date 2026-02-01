using System;
using System.Collections.Generic;
using Modules.Combat.Data;
using Modules.Combat.Data.Enums;
using Modules.Combat.Data.SO;
using UnityEngine;
using UnityEngine.Serialization;

namespace Features.Units.Data
{
	/// <summary>
	/// 存放角色数据
	/// </summary>
	[Serializable]
	public class CharacterData : UnitData
	{
		public Vector3 LogicalPosition { get; set; }
		
		[field: Header("战斗资源")]
		// AP-标准动作
		[field: SerializeField]
		public int MaxAp { get; set; }

		// BP-附赠动作
		[field: SerializeField] public int MaxBp { get; set; }

		// RP-职业资源
		[field: SerializeField] public int MaxRp { get; set; }

		// 压力上限，这个暂时不确定，后面可能还需要更改是否需要压力值
		[field: SerializeField] public int MaxSanity { get; set; } = 100;

		// 运行时状态
		public int CurrentAp { get; set; }
		public int CurrentBp { get; set; }
		public int CurrentRp { get; set; }
		public int CurrentSanity { get; set; }
	}
}