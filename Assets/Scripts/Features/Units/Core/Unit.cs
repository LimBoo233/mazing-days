using System;
using System.Collections.Generic;
using Core.Architecture;
using Core.Event;
using Core.Utils;
using Features.Units.Data;
using GameSystemEnum;
using Modules.Combat;
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
		
		public UnitCombatModule CombatModule { get; protected set; }

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
			Data = data;
			
			CombatModule = new UnitCombatModule(this, data);
			CombatModule.InitializeStats();
			
			CombatModule.HpChanged += (unit, damage) => HpChanged?.Invoke(unit, damage);
			CombatModule.Died += (unit) => Died?.Invoke(unit);
			
			Data.FactionType = FactionType.Neutral;
		}

		public virtual void InitializeStats() => InitializeStats(new UnitData());


		/// <summary>
		/// 投掷先攻骰的方法，先简单写处理：1d4 + 角色速度的一半
		/// </summary>
		public void RollInitiativeDice() => CombatModule.RollInitiativeDice();


		/// <summary>
		/// 返回角色对某种伤害类型的抗性值
		/// </summary>
		public float GetResistance(DamageType type) => CombatModule.GetResistance(type);

		/// <summary>
		/// 角色受伤逻辑
		/// </summary>
		/// <param name="damage">原始伤害</param>
		/// <param name="type">伤害类型</param>
		/// <param name="isCritical">是否暴击</param>
		public virtual void TakeDamage(int damage, DamageType type, bool isCritical = false) => CombatModule.TakeDamage(damage, type, isCritical);
	}
}