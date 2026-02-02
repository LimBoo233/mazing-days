using System.Collections.Generic;
using Features.Units.Core;
using Features.Units.Data;
using UnityEngine.TextCore.Text;

namespace Modules.Exploration
{
	public class ExplorationManager
	{
		public int MaxTeamSize { get; private set; } = 4;
		public int CurrentTeamSize => _playerTeam.Count;

		private PlayerUnit _leaderUnit;
		private List<PlayerUnit> _playerTeam = new();

		/// <summary>
		/// 尝试将一个玩家单位加入队伍
		/// </summary>
		public bool TryAddToTeam(PlayerUnit playerUnit)
		{
			if (_playerTeam.Count >= MaxTeamSize)
			{
				return false;
			}

			// 如果队伍中还没有队长，则将该单位设为队长
			_leaderUnit ??= playerUnit;
			_playerTeam.Add(playerUnit);
			return true;
		}


		/// <summary>
		/// 尝试将一个玩家单位从队伍中移除
		/// </summary>
		/// <returns>成功移除时返回 true，如果 Team 中本就不存在该 playerUnit 则会返回 false</returns>
		public bool TryRemoveFromTeam(PlayerUnit playerUnit)
		{
			var isSucceed = _playerTeam.Remove(playerUnit);
			if (isSucceed && _leaderUnit == playerUnit)
			{
				// 如果被移除的单位是队长，则随机选取一个新的队长
				_leaderUnit = GetRandomTeamMember();
			}

			return isSucceed;
		}

		/// <summary>
		/// 获取队伍中的随机成员
		/// </summary>
		public PlayerUnit GetRandomTeamMember()
		{
			if (_playerTeam.Count == 0) return null;
			int randomIndex = UnityEngine.Random.Range(0, _playerTeam.Count);
			return _playerTeam[randomIndex];
		}
	}
}