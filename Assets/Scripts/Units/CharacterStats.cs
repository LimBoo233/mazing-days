using System;
using System.Collections.Generic;
using UnityEngine;
using GameSystemEnum;

namespace Units
{
	/// <summary>
	/// 抗性配置结构体 (在面板上配表用)
	/// </summary>
	[Serializable]
	public struct ResistanceConfig
	{
		public DamageType type;

		[Tooltip("0.5 = 50% 抗性; -0.5 = 弱点 (受到 150% 伤害); 1.0 = 免疫")]
		[Range(-2.0f, 1.0f)]
		public float value;
	}

	/// <summary>
	/// 这段代码只包含数值的运算，其余的效果在别的脚本中处理
	/// </summary>
	public class CharacterStats : MonoBehaviour
	{
		// TODO: 先暂时跳过UI设计相关内容

		#region BaseStats 基础属性

		[field: Header("基础属性")]
		[field: SerializeField]
		public string CharacterName { get; private set; }

		// 角色阵营
		[field: SerializeField] public FactionType Faction { get; private set; }

		// [SerializeField] private Sprite _characterIcon;

		[field: SerializeField] public int MaxHp { get; private set; }

		// 角色的 AC 值
		[field: SerializeField] public int Ac { get; private set; }

		// 角色速度
		// TODO: 这里应该还有先攻骰，不过暂时先不考虑，简单处理
		[field: SerializeField] public int Speed { get; private set; }

		[field: Tooltip("基础攻击修正值 (相当于 D&D 的力量/智力调整值)\n投伤害骰子时会加上这个值")]
		[field: SerializeField]
		public int BaseAttackModifier { get; private set; }

		[field: Tooltip("基础命中加成 (投 d20 时加上这个值)")]
		[field: SerializeField]
		public int BaseAccuracyBonus { get; private set; }

		// 暴击需求值，方便后续改变
		[field: SerializeField] public int CriticalNeed { get; private set; } = 20;

		#endregion

		#region 2. 资源

		// 压力上限,这个暂时不确定，后面可能还需要更改是否需要压力值
		[field: Header("Resources (BG3)")]
		[field: SerializeField]
		public int MaxSanity { get; private  set; } = 100;

		[field: SerializeField] public int MAxRp { get; private set; } // RP-职业资源
		[field: SerializeField] public int MaxAp { get; private set; } // AP-标准动作
		[field: SerializeField] public int MAxBp { get; private set; } // BP-附赠动作

		#endregion

		#region 3. Resistances 抗性配置
		
		[Header("Resistances")]
		[SerializeField]
		private List<ResistanceConfig> resistanceSettings = new();

		// 运行时字典，用于快速查找抗性
		private readonly Dictionary<DamageType, float> _resistanceDict = new();

		#endregion

		#region 4. Runtime State 状态管理

		public int CurrentHp { get; private set; }
		public int CurrentSanity { get; private set; }
		public int CurrentRp { get; private set; }
		public int CurrentAp { get; private set; }
		public int CurrentBp { get; private set; }
		public bool IsDead { get; private set; }

		#endregion
		

		#region Unity Lifecycle

		private void Awake()
		{
			InitializeStats();
			InitializeResistances();
		}

		/// <summary>
		/// 初始化角色状态
		/// </summary>
		private void InitializeStats()
		{
			// TODO： 读取存档数据
			CurrentHp = MaxHp;
			CurrentSanity = 0;
			CurrentRp = MAxRp;
			IsDead = false;
		}

		/// <summary>
		/// 初始化抗性字典
		/// </summary>
		private void InitializeResistances()
		{
			_resistanceDict.Clear();
			foreach (var config in resistanceSettings)
			{
				if (!_resistanceDict.ContainsKey(config.type))
				{
					_resistanceDict.Add(config.type, config.value);
				}
			}
		}

		#endregion

		#region 战斗逻辑相关

		public void ResetTurnResources()
		{
			CurrentAp = MaxAp;
			CurrentBp = MaxHp;
			// Rp 只应该在短修时恢复
		}

		public void TakeDamage(int damage, DamageType type)
		{
			if (IsDead) return;

			float resistance = 0.0f;
			if (_resistanceDict.ContainsKey(type))
			{
				resistance = _resistanceDict[type];
			}

			int finalDamage = Mathf.RoundToInt(damage * (1.0f - resistance));

			CurrentHp -= finalDamage;
			CurrentHp = CurrentHp <= 0 ? 0 : CurrentHp;

			// TODO: 推送血量改变事件

			// 死亡检测
			if (CurrentHp <= 0)
			{
				Die();
			}
		}

		private void Die()
		{
			IsDead = true;
			CurrentHp = 0;
			// TODO: 触发死亡事件

			Debug.Log($"[{CharacterName}] <color=red>已阵亡。</color>");
		}

		/// <summary>
		/// Debug 输出伤害信息，只负责输出不负责计算
		/// </summary>
		private void LogDamageInfo(DamageType type, int rawDice, float resistance, int finalDice)
		{
			string suffix = resistance switch
			{
				< 0f => " <color=orange>(弱点!)</color>",
				>= 1f => " <color=grey>(免疫)</color>",
				> 0f => " (抵抗)",
				_ => "" 
			};

			Debug.Log($"[{CharacterName}] 受到 [{type}] 伤害: 原始{rawDice} -> 最终{finalDice}{suffix}");
		}

		#endregion
	}
}