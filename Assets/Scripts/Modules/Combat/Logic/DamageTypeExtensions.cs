using Combat;

namespace Modules.Combat.Logic
{
	/// <summary>
	/// 伤害类型枚举类的扩展方法，提供额外的功能（ex：判断伤害是否为物理伤害）
	/// </summary>
	public static class DamageTypeExtensions
	{
		/// <summary>
		/// 判断是否为物理伤害
		/// </summary>
		public static bool IsPhysical(this DamageType type) => type switch
		{
			DamageType.Bludgeoning or DamageType.Piercing or DamageType.Slashing => true,
			_ => false
		};

		/// <summary>
		/// 获取伤害类型的对应颜色 (用于 UI 显示)
		/// </summary>
		public static UnityEngine.Color GetColor(this DamageType type) =>
			type switch
			{
				DamageType.Bludgeoning => UnityEngine.Color.white,
				DamageType.Piercing => UnityEngine.Color.white,
				DamageType.Slashing => UnityEngine.Color.white,

				DamageType.Fire => new UnityEngine.Color(1f, 0.3f, 0f),
				DamageType.Cold => UnityEngine.Color.cyan,
				DamageType.Lightning => UnityEngine.Color.yellow,
				DamageType.Acid => UnityEngine.Color.green,
				DamageType.Poison => new UnityEngine.Color(0.5f, 0f, 1f),
				DamageType.Radiant => new UnityEngine.Color(1f, 0.9f, 0.6f),
				DamageType.Necrotic => new UnityEngine.Color(0.2f, 0.2f, 0.2f),

				// ... 其他颜色 ...
				_ => UnityEngine.Color.gray
			};
	}
}