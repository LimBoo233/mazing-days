using System;
using UnityEngine;

namespace Utils
{
	/// <summary>
	/// 骰子类（类 BG3）
	/// </summary>
	[Serializable]
	public struct Dice
	{
		[Tooltip("投骰子的数量")] public int Count;
		[Tooltip("投骰子的面数")] public int Sides;

		public Dice(int count, int sides) => (Count, Sides) = (count, sides);

		public override string ToString() => $"{Count}d{Sides}";

		public int MinValues => Count;
		public int MaxValues => Count * Sides;
	}
}