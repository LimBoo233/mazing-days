using GameSystemEnum;

namespace Combat
{
	/// <summary>
	/// 包含一次命中检定的完整信息
	/// </summary>
	public struct AttackResult
	{
		public RollResultType Type;
		public int RawRoll; // 原始投掷结果
		public int FinalValue; // 最终结果 (原始投掷结果 + 修正值)
		public bool IsSuccessful; //是否命中
	}
}