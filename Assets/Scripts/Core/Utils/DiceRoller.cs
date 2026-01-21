using Combat;
using GameSystemEnum;
using UnityEngine;

namespace Utils
{
	public static class DiceRoller
	{

		public static int Roll(int sides) => Random.Range(0, sides);

		public static int Roll(int counts, int sides)
		{
			int total = 0;
			for (int i = 0; i < counts; i++)
			{
				total += Roll(sides);
			}

			return total;
		}
		
		/// <summary>
		/// 用给外部 UI,显示数值区间。在使用是注意不要多次计算 modifer
		/// </summary>
		public static string GetRangeString(Dice dice, int modifer = 0)
		{
			int min = dice.MinValues + modifer;
			int max = dice.MaxValues + modifer;
			return $"{min}~{max}";
		}
	}
}