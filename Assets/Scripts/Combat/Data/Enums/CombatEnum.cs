namespace Combat
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
		Won, //战斗胜利
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


}