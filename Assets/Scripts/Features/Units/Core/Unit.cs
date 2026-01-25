using System.Collections.Generic;
using Combat;
using Core.Architecture;
using Core.Event;
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
		
		/// <summary>
		/// 角色受伤逻辑
		/// </summary>
		/// <param name="damage">原始伤害</param>
		/// <param name="type">伤害类型</param>
		/// <param name="isCritical">是否暴击</param>
		public virtual void TakeDamage(int damage, DamageType type,bool isCritical = false)
		{ 
			if (IsDead) return;
			float resistance = 0.0f;
			if (ResistanceDict.ContainsKey(type))
			{
				resistance = ResistanceDict[type];
			}
			
			int finalDamage = Mathf.RoundToInt(damage * (1.0f - resistance));
			if(resistance<1.0f)
				finalDamage = Mathf.Max(1,finalDamage);
			
			CurrentHp -= finalDamage;
			//发送受伤事件
			//后续处理受伤音效，角色状态机，屏幕振动等效果
			EventBus.Publish(new TakeDamageEvent
			{
				//这里不传名字
				//有可能两只怪物的名字相同，一个受伤结果全都播放受伤动画
				Target = this,
				Damage = finalDamage,
				IsCritical =  isCritical
			});
			
			EventBus.Publish(new TextNotifiedEvent(this.CharacterName + " 受到 " + finalDamage + " 点伤害", 1000));
			
			LogDamageInfo(type, damage, resistance, finalDamage);
			
			if(CurrentHp<= 0 )
				Die();
			
		}

		/// <summary>
		/// 角色死亡逻辑
		/// 这里根据角色阵营不同，处理的死亡事件也应该不同
		/// 例如敌人死亡，玩家获取经验等奖励
		/// </summary>
		public virtual void Die()
		{
			IsDead = true;
			CurrentHp = 0;
			EventBus.Publish(new TextNotifiedEvent(this.CharacterName + " 已阵亡。", 1000));
			EventBus.Publish(new UnitDiedEvent { DeadUnit = this });
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
	}
}