using System;
using System.Collections.Generic;
using Core.Utils;
using Features.Units.Core;
using Features.Units.View;
using GameSystemEnum;
using Modules.Combat.Data.Enums;
using Modules.Combat.Data.SO;
using UnityEngine;

namespace Modules.Combat.FSM.BattleState
{
	public class PlayerTurnState: IBattleState
	{
		private enum TurnPhase
		{
			ChooseSkill, 
			SelectTarget,
			Executing
		}
		private TurnPhase _phase;
		private Unit _currentUnit;
		
		private SkillDataSo _selectedSkill;
		
		public void Enter(CombatManager combatManager)
		{
			combatManager.SelectedSkillId = -1;
			combatManager.SelectedTargets = null;
			combatManager.IsPlayerActionConfirmed = false;
			Debug.Log("等带玩家输入技能与选择目标");
			EnterChooseSkillPhase(combatManager);
		}

		public void Update(CombatManager combatManager)
		{
			switch (_phase)
			{
				case TurnPhase.ChooseSkill:
					UpdateChooseSkill(combatManager);
					break;
				case TurnPhase.SelectTarget:
					UpdateSelectTarget(combatManager);
					break;
				case TurnPhase.Executing:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		/// <summary>
		/// 进入选择技能阶段
		/// </summary>
		/// <param name="combatManager"></param>
		private void EnterChooseSkillPhase(CombatManager combatManager)
		{
			_phase = TurnPhase.ChooseSkill;
			combatManager.SelectedSkillId = -1;
			_selectedSkill = null;
			//打开技能面板 目前还没写UIMgr
			Debug.Log("等待玩家选择技能");
		}

		private void UpdateChooseSkill(CombatManager combatManager)
		{
			if (combatManager.SelectedSkillId != -1)
			{
				_selectedSkill = combatManager.CurrentActiveUnit.Skills.Find
					(s => s.SkillID == combatManager.SelectedSkillId);
			}
			if (_selectedSkill != null)
			{
				EnterSelectTargetPhase(combatManager);
			}
		}
		
		private void EnterSelectTargetPhase(CombatManager combatManager)
		{
			_phase = TurnPhase.SelectTarget;
			Debug.Log("等待玩家选择目标");
		}
		
		private void UpdateSelectTarget(CombatManager combatManager)
		{
			//右键点击 反悔选择技能
			if (Input.GetMouseButtonDown(1))
			{
				EnterChooseSkillPhase(combatManager);
				return;
			}
			//按下左键 尝试选中敌人
			if (Input.GetMouseButtonDown(0))
			{
				List<Unit> finalTarget = new List<Unit>();
				bool isValidSelection = false;
				
				Unit target = TrySelectTarget();
				if (target == null)
					return;
				switch (_selectedSkill.TargetType)
				{
					//单体敌人
					case TargetType.SingleEnemy:
						if (!target.IsDead && target.FactionType == FactionType.Enemy)
						{
							finalTarget.Add(target);
							isValidSelection = true;
						}
						break;
					case TargetType.AllEnemies:
						if (target.FactionType == FactionType.Enemy)
						{
							finalTarget = combatManager.AllEnemies;
							isValidSelection = true;
						}
						break;
					case TargetType.Self:
						if (target == _currentUnit)
						{
							finalTarget.Add(target);
							isValidSelection = true;
						}
						break;
					case TargetType.SingleAlly:
						if (!target.IsDead && target.FactionType == FactionType.Player)
						{
							finalTarget.Add(target);
							isValidSelection = true;
						}
						break;
					case TargetType.AllAllies:
						if (target.FactionType == FactionType.Player)
						{
							finalTarget = combatManager.AllPlayers;
							isValidSelection = true;
						}
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
				
				if (isValidSelection && finalTarget.Count > 0)
				{
					ExecuteAction(combatManager,finalTarget);
				}
			}
		}

		/// <summary>
		/// 尝试选中目标，不论敌人还是队友均可选择
		/// 后面应该添加鼠标在目标身上时，会有描边的效果
		/// </summary>
		/// <returns></returns>
		private Unit TrySelectTarget()
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out RaycastHit hit, 1000f))
			{
				var view = hit.collider.GetComponentInParent<UnitView<Unit>>();
				if (view != null)
				{
					return view.Model;
				}
			}
			return null;
		}

		/// <summary>
		/// 执行玩家的选择
		/// </summary>
		private void ExecuteAction(CombatManager combatManager,List<Unit> targets)
		{
			combatManager.SelectedTargets = targets;
			combatManager.IsPlayerActionConfirmed = true;
			//调试用
			foreach (var target in targets)
			{
				Debug.Log($"已经选择的目标是{target}");
			}
			combatManager.ChangeState(new ExecuteSkillState());
		}
		
		public void Exit(CombatManager manager)
		{
			//关闭角色UI面板
			Debug.Log("玩家一个角色回合结束");
		}
		
	}
}