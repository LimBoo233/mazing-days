using System.Collections.Generic;
using Features.Units.Core;
using Features.Units.Enemy;
using Features.Units.Player;
using Modules.Combat.FSM;

namespace Modules.Combat
{
	public class CombatManager
	{
		public bool IsBattleOver { get; private set; }

		private CombatUIBridge _combatUIBridge = new();

		/// <summary>
		/// 当前战斗状态管理
		/// </summary>
		private IBattleState _currentState;
		private List<PlayerUnit> _playerUnits;
		private List<EnemyUnit> _enemyUnits;

		/// <summary>
		/// 用来存储玩家的操作数据（中转站）
		/// </summary>
		public int SelectedSkillId { get; set; } = -1;
		public Unit SelectedTarget { get; set; }
		
		public CombatManager(List<PlayerUnit> playerUnits, List<EnemyUnit> enemyUnits) =>
			(_playerUnits, _enemyUnits) = (playerUnits, enemyUnits);
		
		public void Update()
		{
			while (!IsBattleOver)
			{
				_currentState.Update(this);
			}
		}
		
		public void ChangeState(IBattleState newState)
		{
			_currentState = newState;
		}
		
	}
}