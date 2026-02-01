using System.Collections.Generic;
using Features.Units.Core;
using Features.Units.Data;
using UnityEngine.TextCore.Text;

namespace Modules.Exploration
{
	public class ExplorationManager
	{
		public int MaxTeamSize { get; private set; } = 4;

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

			_playerTeam.Add(playerUnit);
			return true;
		}

		/// <summary>
		/// 尝试将一个玩家单位从队伍中移除
		/// </summary>
		/// <returns>成功移除时返回 true，如果 Team 中本就不存在该 playerUnit 则会返回 false</returns>
		public bool TryRemoveFromTeam(PlayerUnit playerUnit) => _playerTeam.Remove(playerUnit);
		
		
	}
}