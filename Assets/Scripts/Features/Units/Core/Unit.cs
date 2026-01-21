using System.Collections.Generic;
using Combat;
using GameSystemEnum;
using Modules.Combat.Data;
using UnityEngine;

namespace Features.Units.Core
{
	// TODO: 先暂时跳过UI设计相关内容
	public class Unit : MonoBehaviour
	{
		#region 1. BaseStats 基础属性

		[field: Header("基础属性")]
		[field: SerializeField]
		public string CharacterName { get; protected set; }

		// 角色阵营
		[field: SerializeField] public FactionType Faction { get; protected set; }
		
		[field: SerializeField] public int MaxHp { get; protected set; }

		// 角色的 AC 值
		[field: SerializeField] public int Ac { get; protected set; }

		// 角色速度
		// TODO: 这里应该还有先攻骰，不过暂时先不考虑，简单处理
		[field: SerializeField] public int Speed { get; protected set; }

		[field: Tooltip("基础攻击修正值 (相当于 D&D 的力量/智力调整值)\n投伤害骰子时会加上这个值")]
		[field: SerializeField]
		public int BaseAttackModifier { get; protected set; }

		[field: Tooltip("基础命中加成 (投 d20 时加上这个值)")]
		[field: SerializeField]
		public int BaseAccuracyBonus { get; protected set; }

		// 暴击需求值，方便后续改变
		[field: SerializeField] public int CriticalNeed { get; protected set; } = 20;

		#endregion
		
		#region 2. Resistances 抗性配置
		
		[Header("Resistances")]
		[SerializeField]
		protected List<ResistanceConfig> resistanceSettings = new();

		// 运行时字典，用于快速查找抗性
		protected readonly Dictionary<DamageType, float> ResistanceDict = new();

		#endregion
		
		#region 3. Runtime State 状态管理

		public int CurrentHp { get; protected set; }

		public bool IsDead { get; protected set; }

		#endregion
		
		// [SerializeField] private Sprite _characterIcon;
		
		protected virtual void Awake()
		{
			InitializeStats();
			InitializeResistances();
		}

		/// <summary>
		/// 初始化角色状态
		/// </summary>
		protected virtual void InitializeStats()
		{
			// TODO： 读取数据
			CurrentHp = MaxHp;
			IsDead = false;
		}
		
		/// <summary>
		/// 初始化抗性字典
		/// </summary>
		protected virtual void InitializeResistances()
		{
			ResistanceDict.Clear();
			foreach (var config in resistanceSettings)
			{
				if (!ResistanceDict.ContainsKey(config.type))
				{
					ResistanceDict.Add(config.type, config.value);
				}
			}
		}
		
		
	}
}