using System;
using System.Collections.Generic;
using Core.Architecture;
using Core.Event;
using Core.Utils;
using Features.Units.Data;
using GameSystemEnum;
using Modules.Combat.Data;
using Modules.Combat.Data.Enums;
using Modules.Combat.Data.SO;
using UnityEngine;

namespace Features.Units.Core
{
	[Serializable]
	public class Unit<T> : Unit where T : UnitData
	{
		public new T Data
		{
			get => (T)base.Data;
			protected set => base.Data = value;
		}

		public override void InitializeStats(UnitData data)
		{
			if (data is T tData)
			{
				base.InitializeStats(tData);
			}
			else
			{
				Debug.LogError($"数据类型错误！期望 {typeof(T)}，但收到了 {data.GetType()}");
			}
		}
	}

	[Serializable]
	public class Unit
	{
		public UnitData Data { get; protected set; }

		// 事件
		public event Action<Unit, int> HpChanged;
		public event Action<Unit> Died;

		// 运行时字典，用于快速查找抗性
		protected Dictionary<DamageType, float> ResistanceDict = new();

		/// <summary>
		/// 用于每个 Unit 的初始化，只应该在创建时调用一次
		/// </summary>
		public virtual void InitializeStats(UnitData data)
		{
			Data.CurrentHp = Data.MaxHp;
			Data.IsDead = false;
			ResistanceDict.Clear();
			foreach (var resistanceConfig in Data.ResistanceSettings)
			{
				ResistanceDict[resistanceConfig.type] = resistanceConfig.value;
			}
		}

		public virtual void InitializeStats() => InitializeStats(new UnitData());


		/// <summary>
		/// 投掷先攻骰的方法，先简单写处理：1d4 + 角色速度的一半
		/// </summary>
		public void RollInitiativeDice() => Data.Initiative = DiceRoller.Roll(1, 4) + Data.DexModifier;


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
			if (Data.IsDead) return;

			float resistance = Mathf.Min(GetResistance(type), 1.0f);

			int finalDamage = Mathf.RoundToInt(damage * (1.0f - resistance));
			if (resistance < 1.0f)
				finalDamage = Mathf.Max(1, finalDamage);

			Data.CurrentHp = Mathf.Clamp(Data.CurrentHp - finalDamage, 0, Data.MaxHp);
			if (Data.CurrentHp <= 0)
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
			foreach (var config in Data.ResistanceSettings)
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
				TargetName = Data.CharacterName,
				Damage = finalDamage,
				IsCritical = isCritical
			};
			EventBus.Publish(takeDamageEvent);
			EventBus.Publish(new TextNotifiedEvent(Data.CharacterName + " 受到 " + finalDamage + " 点伤害", 1000));
		}

		protected virtual void OnDie()
		{
			Data.IsDead = true;
			Died?.Invoke(this);
			EventBus.Publish(new UnitDiedEvent { DeadUnit = this });
		}
	}
}