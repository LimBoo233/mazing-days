using Features.Units.Core;
using UnityEngine;

namespace Modules.Combat.FSM.BattleState
{
    public class ProcessTurnState: IBattleState
    {
        public void Enter(CombatManager combatManager)
        {
            // 先检查每回合开始前是不是战斗结束了
            if (combatManager.CheckCombatCondition())
            {
                return;
            }
            var currentUnit = combatManager.CurrentActiveUnit;
            if (currentUnit == null)
            {
                return;
            }
            if (currentUnit is PlayerUnit)
            {
                combatManager.ChangeState(new PlayerTurnState());
            }
            else if (currentUnit is EnemyUnit)
            {
                combatManager.ChangeState(new EnemyTurnState());
            }
            else
            {
                Debug.LogWarning("未知类型单位，暂不处理先跳过");
                combatManager.AdvanceTurn();
                combatManager.ChangeState(new ProcessTurnState());
            }
        }

        public void Update(CombatManager combatManager)
        {
            
        }

        public void Exit(CombatManager manager)
        {
           
        }
        
    }
}