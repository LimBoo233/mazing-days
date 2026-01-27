using UnityEngine;

namespace Modules.Combat.FSM.BattleState
{
    public class SetupState : IBattleState
    {
        //这个是用来计算播放开场动画的计时器，可以后面再考虑需不需要
        private float _timer;
        private const float IntroDuration = 1.0f;
        public void Enter(CombatManager combatManager)
        {
            combatManager.GenerateTurnOrder();

            _timer = 0;
        }

        public void Update(CombatManager combatManager)
        {
          _timer += Time.deltaTime;
          if (_timer >= IntroDuration)
          {
              //动画播放完毕，进入回合处理流程
              combatManager.ChangeState(new ProcessTurnState());
          }
        }

        public void Exit(CombatManager combatManager)
        {
            
        }
    }
}