using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Features.Units.Core;
using GameSystemEnum;
using Modules.Combat.FSM;
using Modules.Combat.FSM.BattleState;
using UnityEngine;

namespace Modules.Combat
{
	public class CombatManager: IDisposable
	{
		public bool IsBattleOver { get; private set; }
		private CombatUIBridge _combatUIBridge = new();

		/// <summary>
		/// 当前战斗状态管理
		/// </summary>
		private IBattleState _currentState;

		//原始战斗单位
		private List<PlayerUnit> _playerUnits;
		private List<EnemyUnit> _enemyUnits;
		
		public List<Unit> AllEnemies => AllUnits.Where(u => u.FactionType == FactionType.Enemy).ToList();
		public List<Unit> AllPlayers => AllUnits.Where(u => u.FactionType == FactionType.Player).ToList();

		//所有战斗单位，以及他们的顺序
		public List<Unit> AllUnits { get; private set; } = new();
		public List<Unit> TurnOrder { get; private set; } = new();

		//当前战斗轮次，用于记录当前是哪个单位在战斗中
		private int _currentTurnIndex;

		//当前战斗轮次对应的战斗单位
		public Unit CurrentActiveUnit => TurnOrder[_currentTurnIndex];

		/// <summary>
		/// 用来存储玩家的操作数据（中转站）
		/// </summary>
		public int SelectedSkillId { get; set; } = -1;

		public List<Unit> SelectedTargets { get; set; }

		//当 UI 确认选择时，将此标记设为 true，PlayerTurnState 会在 Update 里检测到
		public bool IsPlayerActionConfirmed { get; set; } = false;

		/// <summary>
		/// 将所有角色加入一个列表中
		/// </summary>
		/// <param name="playerUnits"></param>
		/// <param name="enemyUnits"></param>
		public void InitializeCombat(List<PlayerUnit> playerUnits, List<EnemyUnit> enemyUnits)
		{
			IsBattleOver = false;
			AllUnits.Clear();
			_playerUnits = playerUnits;
			_enemyUnits = enemyUnits;
			AllUnits.AddRange(_playerUnits);
			AllUnits.AddRange(_enemyUnits);
			if (AllUnits.Count == 0)
			{
				Debug.LogError("没有找到任何战斗单位");
				return;
			}

			//进入初始状态，SetupState 会负责重置资源等
			ChangeState(new SetupState());
		}

		/// <summary>
		/// 生成行动队列
		/// </summary>
		public void GenerateTurnOrder()
		{
			foreach (var unit in AllUnits)
			{
				unit.RollInitiativeDice();
			}

			//根据先攻值高的在前，如果一样，则Speed值高的在前
			TurnOrder = AllUnits.OrderByDescending(u => u.Initiative).ThenByDescending(u => u.Speed).ToList();
			_currentTurnIndex = 0;

			// 打印日志验证顺序(暂时测试 之后直接删掉)
			string log = "[最终行动顺序]: ";
			foreach (var u in TurnOrder)
			{
				string color = u is PlayerUnit ? "green" : "red";
				log += $"<color={color}>{u.CharacterName}({u.Initiative})</color> > ";
			}

			Debug.Log(log);
		}

		/// <summary>
		/// Unity 每帧调用：驱动当前状态的 Update
		/// </summary>
		public void Update()
		{
			if (!IsBattleOver && _currentState != null)
			{
				// 让当前状态处理每一帧的逻辑 (比如检测 UI 输入、动画状态)
				_currentState.Update(this);
			}
		}

		public void ChangeState(IBattleState newState)
		{
			// 1. 退出旧状态
			if (_currentState != null)
			{
				_currentState.Exit(this);
			}

			// 2. 切换引用
			_currentState = newState;

			// 3. 进入新状态
			if (_currentState != null)
			{
				//AI说看似开销大，实际上不如灰尘影响的多，要是看着难受，可以提前声明好所有状态
				_currentState.Enter(this);
			}
		}

		public void AdvanceTurn()
		{
			_currentTurnIndex++;
			//索引超过队列长度，说明这一轮结束,进入新的轮次
			if (_currentTurnIndex >= TurnOrder.Count)
			{
				_currentTurnIndex = 0;
				//TODO: 之后在这里处理Buff结算，CD 减少的逻辑
			}

			//如果轮到的人已经死了，直接跳过，找下一个人
			if (CurrentActiveUnit.IsDead)
			{
				AdvanceTurn();
			}
		}

		// //测试代码暂时先这么写
		// public void OnPlayerInput(int skillId, Unit target)
		// {
		// 	//只有当前轮到玩家时才允许输入
		// 	if (CurrentActiveUnit is PlayerUnit)
		// 	{
		// 		SelectedSkillId = skillId;
		// 		SelectedTarget = target;
		// 		IsPlayerActionConfirmed = true;
		// 	}
		// 	else
		// 	{
		// 		Debug.LogWarning("当前不是玩家回合");
		// 	}
		// }

		/// <summary>
		/// 检查胜负条件
		/// </summary>
		/// <returns>战斗结束返回true</returns>
		public bool CheckCombatCondition()
		{
			bool allEnemiesDead = _enemyUnits.All(u => u.IsDead);
			if (allEnemiesDead)
			{
				IsBattleOver = true;
				//进入胜利状态
				ChangeState(new WinState());
				return true;
			}

			bool allPlayersDead = _playerUnits.All(u => u.IsDead);
			if (allPlayersDead)
			{
				IsBattleOver = true;
				ChangeState(new LoseState());
				return true;
			}

			return false;
		}
		
		public void Dispose()
		{
			_combatUIBridge.Dispose();
		}
		
	}
}