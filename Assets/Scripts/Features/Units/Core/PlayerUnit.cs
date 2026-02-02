using System;
using Features.Units.Data;
using GameSystemEnum;
using Modules.Exploration;
using UnityEngine;

namespace Features.Units.Core
{
	[Serializable]
	public class PlayerUnit : Unit<CharacterData>
	{
		public ExplorationModule ExplorationModule {get; protected set;}
		
		// 事件
		public event Action<Unit, int> ApChanged;
		public event Action<Unit, int> BpChanged;
		public event Action<Unit, int> RpChanged;
		public event Action<Unit, int> SanityChanged;

		public override void InitializeStats(UnitData data)
		{
			base.InitializeStats(data);
			Data.FactionType = FactionType.Player;
			(Data.CurrentAp, Data.CurrentBp, Data.CurrentRp) = (Data.MaxAp, Data.MaxBp, Data.MaxRp);
			Data.CurrentSanity = Data.MaxSanity;
			
			ExplorationModule = new ExplorationModule(Data);
		}

		/// <summary>
		/// 每回合重置行动资源，包括 AP 和 BP，但不会回复资源点 Rp
		/// </summary>
		public void ResetTurnResources() => (Data.CurrentAp, Data.CurrentBp) = (Data.MaxAp, Data.MaxBp);


		/// <summary>
		/// 尝试消耗资源点
		/// </summary>
		public bool TryConsumeResources(int apCost, int bpCost, int rpCost)
		{
			if (Data.CurrentAp < apCost || Data.CurrentBp < bpCost || Data.CurrentRp < rpCost)
			{
				return false;
			}

			Data.CurrentAp -= apCost;
			Data.CurrentBp -= bpCost;
			Data.CurrentRp -= rpCost;
			return true;
		}

		public void OnApChanged(int change) => ApChanged?.Invoke(this, change);
		public void OnBpChanged(int change) => BpChanged?.Invoke(this, change);
		public void OnRpChanged(int change) => RpChanged?.Invoke(this, change);
		public void OnSanityChanged(int change) => SanityChanged?.Invoke(this, change);
	}
}