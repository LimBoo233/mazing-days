using System;
using Features.Units.Data;
using GameSystemEnum;
using Modules.Combat;
using Modules.Exploration;
using UnityEngine;

namespace Features.Units.Core
{
	[Serializable]
	public class PlayerUnit : Unit<CharacterData>
	{
		public ExplorationModule ExplorationModule { get; protected set; }
		public PlayerResourceModule PlayerResourceModule { get; private set; }

		// 事件
		public event Action<PlayerUnit, int> ApChanged;
		public event Action<PlayerUnit, int> BpChanged;
		public event Action<PlayerUnit, int> RpChanged;
		public event Action<PlayerUnit, int> SanityChanged;

		public override void InitializeStats(UnitData data)
		{
			base.InitializeStats(data);

			ExplorationModule = new ExplorationModule(Data);
			PlayerResourceModule = new PlayerResourceModule(this, Data);
			PlayerResourceModule.InitializeStats();
			
			PlayerResourceModule.ApChanged += (unit, val) => ApChanged?.Invoke(unit, val);
			PlayerResourceModule.BpChanged += (unit, val) => BpChanged?.Invoke(unit, val);
			PlayerResourceModule.RpChanged += (unit,val) => RpChanged?.Invoke(unit, val);
			PlayerResourceModule.SanityChanged += (unit,val) => SanityChanged?.Invoke(unit, val);
			
			Data.FactionType = FactionType.Player;
		}

		/// <summary>
		/// 每回合重置行动资源，包括 AP 和 BP，但不会回复资源点 Rp
		/// </summary>
		public void ResetTurnResources() => PlayerResourceModule.ResetTurnResources();


		/// <summary>
		/// 尝试消耗资源点
		/// </summary>
		public bool TryConsumeResources(int apCost, int bpCost, int rpCost) =>
			PlayerResourceModule.TryConsumeResources(apCost, bpCost, rpCost);
	}
}