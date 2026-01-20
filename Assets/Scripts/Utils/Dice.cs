using UnityEngine;

namespace Utils
{
    /// <summary>
    /// 骰子类(类BG3)
    /// </summary>
    public struct Dice
    {
        [Tooltip("投骰子的数量")]
        public int Count; //投骰子的数量 1d8+2中的 1
        [Tooltip("投骰子的面数")]
        public int Sides; //投骰子的面数 1d8+2中的 8
        [Tooltip("投骰子的修正值")]
        public int Modifier; //投骰子的修正值 1d8+2中的 2
        
        // 构造函数
        public Dice(int count, int sides, int modifier)
        {
            this.Count = count;
            this.Sides = sides;
            this.Modifier = modifier;
        }

        public override string ToString()
        {
            string modStr = Modifier > 0 ? $"+{Modifier}" : (Modifier < 0 ? $"{Modifier}" : "");
            return $"{Count}d{Sides}{modStr}";
        }
        
        // 获取骰子最小值
        public int MinValues => Count * 1 + Modifier;
        // 获取骰子最大值
        public int MaxValues => Count * Sides + Modifier;
    }
}