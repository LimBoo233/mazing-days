using GameSystemEnum;
using Utils;

namespace Combat
{
	public static class CombatResolver
	{
		/// <summary>
		/// 攻击掷投结果计算
		/// </summary>
		/// <param name="hitBonus">角色或装备的命中加成</param>
		/// <param name="targetAc">目标 AC</param>
		/// <param name="criticalNeed">暴击需要的点数</param>
		/// <returns></returns>
		public static AttackResult RollAttackValue(int hitBonus, int targetAc, int criticalNeed = 20)
		{
			// 投掷 d20 进行命中检定
			int rawRoll = DiceRoller.Roll(20);

			int finalValue = rawRoll + hitBonus;

			RollResultType type = (rawRoll, finalValue) switch
			{
				(1, _) => RollResultType.CriticalFailure,   // 规则优先：出 1 必大失败 
				
				(var r, _) when r >= criticalNeed => RollResultType.CriticalSuccess, 
        
				(_, var f) when f >= targetAc => RollResultType.Success, // 没暴击，但数值超过 AC
        
				_ => RollResultType.Failure
			};
			
			var isSuccessful = type is RollResultType.Success or RollResultType.CriticalSuccess;

			return new AttackResult 
			{ 
				Type = type,
				RawRoll = rawRoll, 
				FinalValue = finalValue,
				IsSuccessful = isSuccessful
			};
		}


		/// <summary>
		/// 伤害掷投结果计算
		/// </summary>
		/// <param name="dice">骰子配置，如 2d6</param>
		/// <param name="modifer">根据角色的主要属性值进行调整</param>
		/// <param name="isCritical">是否暴击</param>
		/// <returns></returns>
		public static int RollDamageValue(Dice dice, int modifer, bool isCritical)
		{
			// TODO：Dice 可能有多种类型（ex: 1d8 + 2d6），待扩展
			int finalCount = isCritical ? dice.Count * 2 : dice.Count;
			int total = DiceRoller.Roll(finalCount, dice.Sides);

			return total + modifer;
		}



		
	}
}