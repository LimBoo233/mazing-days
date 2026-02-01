using Core.Utils;
using Features.Units.Core;
using UnityEngine;

namespace Modules.Combat.FSM.BattleState
{
	public class EnemyTurnState: IBattleState
	{
		private float _timer;
		private bool _hasActed;
		private Unit _currentUnit;
		public void Enter(CombatManager combatManager)
		{
			_currentUnit = combatManager.CurrentActiveUnit;
			Debug.Log($"😈 [FSM] 轮到敌人: <color=red>{_currentUnit.Data.CharacterName}</color>");
			
			_timer = 0f;
			_hasActed = false;
		}

		public void Update(CombatManager combatManager)
		{
			_timer += Time.deltaTime;

			// 1. 模拟“思考”时间 (1秒后行动)
			if (_timer > 1.0f && !_hasActed)
			{
				PerformEnemyAction(combatManager);
				_hasActed = true;
			}

			// 2. 模拟“攻击动画”时间 (再等1秒后结束回合)
			if (_timer > 2.0f && _hasActed)
			{
				combatManager.AdvanceTurn();
				combatManager.ChangeState(new ProcessTurnState());
			}
		}
		
		private void PerformEnemyAction(CombatManager manager)
		{
			// 简单的 AI：随机找一个活着的玩家打
			// 简单做法：从 AllUnits 里找 FactionType 是 Player 的
            
			var target = manager.AllUnits.Find(u => u.Data.FactionType == GameSystemEnum.FactionType.Player && !u.Data.IsDead);

			if (target != null)
			{
				Debug.Log($" 敌人 {_currentUnit.Data.CharacterName} 攻击了 {target.Data.CharacterName}!");
				int damage = DiceRoller.Roll(1, 8);
				Debug.Log($" 敌人的{damage}!");
				target.TakeDamage(damage, Modules.Combat.Data.Enums.DamageType.Bludgeoning);
			}
			else
			{
				Debug.Log(" 敌人找不到目标，发呆中...");
			}
			
		}

		public void Exit(CombatManager manager)
		{
			
		}

		
	}
}