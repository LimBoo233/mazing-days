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

        public Dice(int count, int sides) => (Count, Sides) = (count, sides);

        public override string ToString() => $"{Count}d{Sides}";
        
        public int MinValues => Count;
        public int MaxValues => Count * Sides;
    }
}