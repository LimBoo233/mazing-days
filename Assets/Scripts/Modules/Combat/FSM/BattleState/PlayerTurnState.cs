using Core.Utils;
using Modules.Combat.Data.Enums;
using UnityEngine;

namespace Modules.Combat.FSM.BattleState
{
	public class PlayerTurnState: IBattleState
	{
		public void Enter(CombatManager combatManager)
		{
			combatManager.SelectedSkillId = -1;
			combatManager.SelectedTarget = null;
			combatManager.IsPlayerActionConfirmed = false;
			Debug.Log("等带玩家输入技能与选择目标");
		}

		public void Update(CombatManager combatManager)
		{
			//这里由于没有技能面板，所以先用键盘输入代替
			//选择技能,并攻击第一个敌人，以后这里由UI按钮点击代替
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				if (combatManager.AllUnits.Count > 1)
				{
					//这里先假设只有两个人，一个玩家一个敌人
					var target = combatManager.AllUnits.Find(u =>
						u.FactionType != combatManager.CurrentActiveUnit.FactionType && !u.IsDead);
					if (target != null)
					{
						combatManager.OnPlayerInput(0, target);
					}
				}
			}

			if (combatManager.IsPlayerActionConfirmed)
			{
				ExecuteAction(combatManager);
			}
		}

		/// <summary>
		/// 执行玩家的选择
		/// </summary>
		private void ExecuteAction(CombatManager combatManager)
		{
			var target = combatManager.SelectedTarget;
			var skillId = combatManager.SelectedSkillId;
			
			//现在还传不过来攻击，所以先临时代替(1d8+角色力量调整值)
			int damage = DiceRoller.Roll(1, 8)+ combatManager.CurrentActiveUnit.StrModifier;
			Debug.Log($" 玩家的{damage}!");
			target.TakeDamage(damage, DamageType.Bludgeoning);
			
			EndTurn(combatManager);
		}
		
		private void EndTurn(CombatManager manager)
		{
			// 切换到“处理下一回合”的状态
			manager.AdvanceTurn();
			manager.ChangeState(new ProcessTurnState());
		}

		public void Exit(CombatManager manager)
		{
			//关闭角色UI面板
			Debug.Log("玩家一个角色回合结束");
		}

		
	}
}