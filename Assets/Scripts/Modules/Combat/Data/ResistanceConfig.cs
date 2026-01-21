using System;
using Combat;
using UnityEngine;

namespace Modules.Combat.Data
{
	/// <summary>
	/// 抗性配置结构体
	/// </summary>
	[Serializable]
	public struct ResistanceConfig
	{
		public DamageType type;

		[Tooltip("0.5 = 50% 抗性; -0.5 = 弱点 (受到 150% 伤害); 1.0 = 免疫")]
		[Range(-2.0f, 1.0f)]
		public float value;
	}
}