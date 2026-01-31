using System;
using System.Collections.Generic;
using Core.Architecture;
using Core.Event;
using Core.Utils;
using GameSystemEnum;
using Modules.Combat.Data;
using Modules.Combat.Data.Enums;
using UnityEngine;

namespace Features.Units.Core
{
	[Serializable]
	public class Unit
	{
		[field: Header("基础属性")]
		[field: SerializeField] public string CharacterName { get; set; }

		[field: SerializeField] public int MaxHp { get; set; }
		[field: SerializeField] public int CriticalNeed { get; set; } = 20;

		[field: Header("四位属性：力量，速度，智力，精神")]
		[field: SerializeField] public int Strength { get; set; }

		[field: SerializeField] public int Speed { get; set; }
		[field: SerializeField] public int Intelligence { get; set; }
		[field: SerializeField] public int Spirit { get; set; }

		[Header("抗性")]
		[SerializeField] protected List<ResistanceConfig> resistanceSettings = new();


		// 调整值
		public int DexModifier => (Speed - 10) / 2;
		public int StrModifier => (Strength - 10) / 2;
		public int IntModifier => (Intelligence - 10) / 2;
		public int SpiModifier => (Spirit - 10) / 2;

		// 运行时属性
		public int CurrentHp { get; protected set; }
		public bool IsDead { get; protected set; }

		// 先攻
		public int Initiative { get; set; }

		// 攻击掷骰加值
		// TODO：伤害掷骰加值因取决于武器类型等因素，次数先简单处理
		public int BaseAccuracyBonus { get; set; }
		public int BaseAttackModifier { get; set; }

		public FactionType FactionType { get; protected set; }

		// 事件
		public event Action<Unit, int> HpChanged;
		public event Action<Unit> Died;

		// 运行时字典，用于快速查找抗性
		protected Dictionary<DamageType, float> ResistanceDict = new();


		/// <summary>
		/// 用于每个 Unit 的初始化，只应该在创建时调用一次
		/// </summary>
		public virtual void InitializeStats()
		{
			CurrentHp = MaxHp;
			IsDead = false;
			ResistanceDict.Clear();
			foreach (var resistanceConfig in resistanceSettings)
			{
				ResistanceDict[resistanceConfig.type] = resistanceConfig.value;
			}
		}


		/// <summary>
		/// 投掷先攻骰的方法，先简单写处理：1d4 + 角色速度的一半
		/// </summary>
		public void RollInitiativeDice() => Initiative = DiceRoller.Roll(1, 4) + DexModifier;


		/// <summary>
		/// 返回角色对某种伤害类型的抗性值
		/// </summary>
		public float GetResistance(DamageType type) => ResistanceDict.GetValueOrDefault(type, 0.0f);

		/// <summary>
		/// 角色受伤逻辑
		/// </summary>
		/// <param name="damage">原始伤害</param>
		/// <param name="type">伤害类型</param>
		/// <param name="isCritical">是否暴击</param>
		public virtual void TakeDamage(int damage, DamageType type, bool isCritical = false)
		{
			if (IsDead) return;

			float resistance = Mathf.Min(GetResistance(type), 1.0f);

			int finalDamage = Mathf.RoundToInt(damage * (1.0f - resistance));
			if (resistance < 1.0f)
				finalDamage = Mathf.Max(1, finalDamage);

			CurrentHp = Mathf.Clamp(CurrentHp - finalDamage, 0, MaxHp);
			if (CurrentHp <= 0)
			{
				OnDie();
			}

			OnHpChanged(finalDamage, isCritical);
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

		protected virtual void OnHpChanged(int finalDamage, bool isCritical)
		{
			HpChanged?.Invoke(this, finalDamage);
			
			var takeDamageEvent = new TakeDamageEvent
			{
				Target = this,
				TargetName = CharacterName,
				Damage = finalDamage,
				IsCritical = isCritical
			};
			EventBus.Publish(takeDamageEvent);
			EventBus.Publish(new TextNotifiedEvent(CharacterName + " 受到 " + finalDamage + " 点伤害", 1000));
		}

		protected virtual void OnDie()
		{
			IsDead = true;
			Died?.Invoke(this);
			EventBus.Publish(new UnitDiedEvent { DeadUnit = this });
		}
	}
}