using System.Collections.Generic;
using Core.Utils;
using Features.Units.Core;
using GameSystemEnum;
using Modules.Combat.Data;
using Modules.Combat.Data.Enums;
using Modules.Combat.Data.SO;
using Modules.Combat.Logic;
using UnityEngine;

namespace Modules.Combat.FSM.BattleState
{
    public class ExecuteSkillState:IBattleState
    {
        private float _timer = 0f;
        private bool _isAnimationFinished = false;
        //传入动画时长
        public float AnimationTime = 1f;
        public void Enter(CombatManager combatManager)
        {
            //获取施法者的技能，目标，等信息
            Unit attacker = combatManager.CurrentActiveUnit;
            SkillDataSo skill = attacker.Skills.Find
                (skill => skill.SkillID == combatManager.SelectedSkillId);
            List<Unit> targets = combatManager.SelectedTargets;
            if (skill == null || targets == null || targets.Count == 0)
            {
                Debug.LogError("数据丢失，自行进行检查");
            }
            //扣除角色的技能消耗
            foreach (Unit target in targets)
            {
                ExecuteSkillOnTarget(attacker, target, skill);
            }
        }

        private void ExecuteSkillOnTarget(Unit attacker, Unit target, SkillDataSo skill)
        {
            bool isHit = false;
            bool isCritical = false;
            // 用于 Log 显示的详细信息
            string logDetail = "";
            //必中
            if (skill.HitType == SkillHitType.Guaranteed)
            {
                isHit = true;
            }
            else if (skill.HitType == SkillHitType.AttackRoll) //攻击掷投鉴定
            {
                //进行攻击掷投判断
                AttackResult attackResult = CombatResolver.RollAttackValue(attacker.BaseAttackModifier, target.DexModifier,attacker.CriticalNeed);
                isHit = attackResult.IsSuccessful;
                isCritical = attackResult.Type == RollResultType.CriticalSuccess;
                
                //调式信息
                string resultStr = isHit ? (isCritical ? "<color=yellow>暴击!</color>" : "命中") : "未命中";
                logDetail = $"[{resultStr}] 掷骰:{attackResult.RawRoll}+{attacker.BaseAttackModifier}={attackResult.FinalValue} (VS AC{target.Ac})";
                
            }
            else if (skill.HitType == SkillHitType.SavingThrow) //豁免掷投鉴定
            {
                //进行豁免掷投判断
            }

            if (isHit)
            {
                //进行伤害掷投
            }
            //未命中
            else
            {
                
            }
            
        }

        public void Update(CombatManager combatManager)
        {
            
        }

        public void Exit(CombatManager combatManager)
        {
            
        }
    }
}