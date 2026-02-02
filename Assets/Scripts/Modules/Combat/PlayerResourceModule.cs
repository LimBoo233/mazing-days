using System;
using Features.Units.Core;
using Features.Units.Data;
using GameSystemEnum;


namespace Modules.Combat
{
	public class PlayerResourceModule
	{
		private PlayerUnit _playerUnit;
		private CharacterData _data;

		public PlayerResourceModule(PlayerUnit unit, CharacterData unitData)
		{
			_playerUnit = unit;
			_data = unitData;
		}

		// 事件
		public event Action<PlayerUnit, int> ApChanged;
		public event Action<PlayerUnit, int> BpChanged;
		public event Action<PlayerUnit, int> RpChanged;
		public event Action<PlayerUnit, int> SanityChanged;

		public void InitializeStats()
		{
			_data.FactionType = FactionType.Player;
			(_data.CurrentAp, _data.CurrentBp, _data.CurrentRp) = (_data.MaxAp, _data.MaxBp, _data.MaxRp);
			_data.CurrentSanity = _data.MaxSanity;
		}

		/// <summary>
		/// 每回合重置行动资源，包括 AP 和 BP，但不会回复资源点 Rp
		/// </summary>
		public void ResetTurnResources() => (_data.CurrentAp, _data.CurrentBp) = (_data.MaxAp, _data.MaxBp);


		/// <summary>
		/// 尝试消耗资源点
		/// </summary>
		public bool TryConsumeResources(int apCost, int bpCost, int rpCost)
		{
			if (_data.CurrentAp < apCost || _data.CurrentBp < bpCost || _data.CurrentRp < rpCost)
			{
				return false;
			}

			_data.CurrentAp -= apCost;
			_data.CurrentBp -= bpCost;
			_data.CurrentRp -= rpCost;
			return true;
		}

		public void OnApChanged(int change) => ApChanged?.Invoke(_playerUnit, change);
		public void OnBpChanged(int change) => BpChanged?.Invoke(_playerUnit, change);
		public void OnRpChanged(int change) => RpChanged?.Invoke(_playerUnit, change);
		public void OnSanityChanged(int change) => SanityChanged?.Invoke(_playerUnit, change);
	}
}