namespace Modules.Combat.Data.Enums
{
	/// <summary>
	/// 战斗状态枚举
	/// </summary>
	public enum BattleState
	{
		BattleStart, // 战斗开始初始化（生成敌人重置位置）
		PlayerTurn,
		EnemyTurn,
		Busy, //忙碌状态 （技能效果中，动画播放中）
		Victory, //战斗胜利
		Defeat //战斗失败
	}
	
	/// <summary>
	/// 技能目标类型
	/// </summary>
	public enum TargetType
	{
		SingleEnemy, // 单个敌人
		AllEnemies,  // 全体敌人
		Self,        // 自己
		SingleAlly,  // 单个队友
		AllAllies    // 全体队友
	}
	
	/// <summary>
	/// 伤害类型 (基于 DnD 5e SRD 标准)
	/// </summary>
	public enum DamageType
	{
		// --- 0. 未知/通用 ---
		None = 0,

		// --- 1. 物理类 (Physical) ---
		Bludgeoning, // 钝击 (锤子、坠落)
		Piercing,    // 穿刺 (长矛、弓箭)
		Slashing,    // 挥砍 (长剑、斧头)

		// --- 2. 元素类 (Elemental) ---
		Acid,        // 强酸 (物理腐蚀)
		Cold,        // 寒冷 (也就是 Ice)
		Fire,        // 火焰
		Lightning,   // 闪电 (瞬时的电击)
		Thunder,     // 雷鸣 (音波/冲击波，注意与 Lightning 区分)

		// --- 3. 魔法/特异类 (Esoteric) ---
		Poison,      // 毒素 (体内生效)
		Necrotic,    // 黯蚀/死灵 (凋零、不死生物)
		Radiant,     // 光耀 (也就是 Holy，神圣伤害)
		Psychic,     // 心灵 (精神攻击)
		Force        // 力场 (纯粹的魔法能量，如魔法飞弹)
	}

	/// <summary>
	/// 技能类别
	/// </summary>
	public enum SkillCategory
	{
		Attack,
		Heal,
		Buff,
		Debuff,
	}
	
	/// <summary>
	/// 伤害来源
	/// </summary>
	public enum DamageTypeSource
	{
		Fixed,      // 固定类型 (比如火球术永远是 Fire)
		MainHand,   // 跟随主手武器 (战士普攻)
		OffHand,    // 跟随副手武器
		Reaction    // 跟随反应源 (比如反伤甲)
	}
	
	/// <summary>
	/// 决定基础骰子从哪来
	/// </summary>
	public enum DamageBaseSource
	{
		FixedDice,      // 使用 SO 里填写的骰子 (如火球术 8d6)
		MainHandWeapon, // 使用主手武器的面板骰子 (如长剑 1d8)
		// OffHandWeapon, // (可选：副手)
	}
	
	/// <summary>
	/// 决定吃哪个属性的调整值 (DnD Modifier)
	/// 伤害数值通常由三个部分组成：基础（Base） + 额外骰子（Plus） + 调整值（Modifier）。
	/// </summary>
	public enum StatAttribute
	{
		None,           // 无加成 (大多数法术伤害)
		Strength,       // 力量 (近战)
		Dexterity,      // 敏捷 (弓箭/细剑)
		Intelligence,   // 智力 (法师法术)
		// ... 其他属性
		// AdaptiveStrDex // (自动取力量敏捷较高者，类似“灵巧”武器)
	}
	
	/// <summary>
	/// 技能的基础伤害的来源
	/// 伤害数值通常由三个部分组成：基础（Base） + 额外骰子（Plus） + 调整值（Modifier）。
	/// </summary>
	public enum DamageBase
	{
		None,           // 无基础 (纯法术)
		MainHandWeapon, // 继承主手武器面板
		// OffHandWeapon, // (预留)
	}
	
	/// <summary>
	/// 技能距离来源
	/// </summary>
	public enum RangeSource
	{
		Fixed,          // 固定距离 (法术)
		MainHandWeapon, // 跟随主手武器 (普攻)
		// WeaponPlusBonus // (进阶：武器距离 + 额外距离)
	}
}