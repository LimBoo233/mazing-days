using GameSystemEnum;
using UnityEngine;
namespace Utils
{
    public static class DiceRoller
    {
        /// <summary>
        /// 攻击掷投结果计算
        /// </summary>
        /// <param name="hitBonus">角色或装备的命中加成</param>
        /// <param name="targetAC">敌人的AC</param>
        /// <param name="criticalNeed">暴击需要的点数</param>
        /// <returns></returns>
        public static AttackResult RollAttackValue(int hitBonus, int targetAC,int criticalNeed)
        {
            AttackResult result = new AttackResult();
            result.RawRoll = Random.Range(1, 20);
            result.FinalValue = result.RawRoll + hitBonus;
            if (result.RawRoll == 1)
            {
                result.Type = AttackResultType.CriticalMiss;
                result.IsSuccessful = false;
            }
            else if (result.RawRoll == 20)
            {
                result.Type = AttackResultType.CriticalHit;
                result.IsSuccessful = true;
            }
            else if (result.FinalValue >= targetAC)
            {
                result.Type = AttackResultType.Hit;
                result.IsSuccessful = true;
            }
            else
            {
                result.Type = AttackResultType.Miss;
                result.IsSuccessful = false;
            }
            return result;
        }

        /// <summary>
        /// 伤害掷投结果计算
        /// </summary>
        /// <param name="dice">骰子配置如2d6</param>
        /// <param name="modifer">根据角色的主要属性值进行调整</param>
        /// <param name="isCritical">是否暴击</param>
        /// <returns></returns>
        public static int RollDamageValue(Dice dice, int modifer, bool isCritical)
        {
            int total = 0;
            int finalCount = isCritical ? dice.Count * 2 : dice.Count;
            for (int i = 0; i < finalCount; i++)
            {
                total += Random.Range(1, dice.Sides + 1);
            }

            return total + modifer;
        }
        
        /// <summary>
        /// 用给外部UI,显示伤害范围
        /// </summary>
        /// <param name="dice"></param>
        /// <param name="modifer"></param>
        /// <returns></returns>
        public static string GetRangeString(Dice dice,int modifer)
        {
            int min = dice.MinValues + modifer;
            int max = dice.MaxValues + modifer;
            return $"{min}~{max}";
        }

    }
}