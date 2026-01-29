using System;
using GameSystemEnum;
using UnityEngine;

namespace Features.Units.Core
{
	[System.Serializable]
	public class PlayerUnit : Unit
	{
		[field: Header("战斗资源")]
		// AP-标准动作
		[field: SerializeField]
		public int MaxAp { get; protected set; }

		// BP-附赠动作
		[field: SerializeField] public int MaxBp { get; protected set; }

		// RP-职业资源
		[field: SerializeField] public int MaxRp { get; protected set; }

		// 压力上限，这个暂时不确定，后面可能还需要更改是否需要压力值
		[field: SerializeField] public int MaxSanity { get; protected set; } = 100;

		// 运行时状态
		public int CurrentAp { get; protected set; }
		public int CurrentBp { get; protected set; }
		public int CurrentRp { get; protected set; }
		public int CurrentSanity { get; protected set; }

		// 事件
		public event Action<Unit, int> ApChanged;
		public event Action<Unit, int> BpChanged;
		public event Action<Unit, int> RpChanged;
		public event Action<Unit, int> SanityChanged;

		public override void InitializeStats()
		{
			base.InitializeStats();
			FactionType = FactionType.Player;
			(CurrentAp, CurrentBp, CurrentRp) = (MaxAp, MaxBp, MaxRp);
			CurrentSanity = MaxSanity;
		}

		/// <summary>
		/// 每回合重置行动资源，包括 AP 和 BP，但不会回复资源点 Rp
		/// </summary>
		public void ResetTurnResources() => (CurrentAp, CurrentBp) = (MaxAp, MaxBp);

		
		/// <summary>
		/// 尝试消耗资源点
		/// </summary>
		public bool TryConsumeResources(int apCost,int bpCost,int rpCost)
		{
			if (CurrentAp < apCost || CurrentBp < bpCost || CurrentRp < rpCost) 
				return false;

			CurrentAp -= apCost;
			CurrentBp -= bpCost;
			CurrentRp -= rpCost;
			return true;
		}
		
		public void OnApChanged(int change) => ApChanged?.Invoke(this, change);
		public void OnBpChanged(int change) => BpChanged?.Invoke(this, change);
		public void OnRpChanged(int change) => RpChanged?.Invoke(this, change);
		public void OnSanityChanged(int change) => SanityChanged?.Invoke(this, change);
	}
}