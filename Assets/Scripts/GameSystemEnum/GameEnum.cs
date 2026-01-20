namespace GameSystemEnum
{
    /// <summary>
    /// 阵营类型
    /// </summary>
    public enum FactionType
    {
        Player,
        Enemy
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
    /// 伤害攻击类型（部分）
    /// </summary>
    public enum DamageType
    {
        Physical, //物理属性 后面可能还会在物理属性上继续划分
        Fire,
        Ice,
        Lightning, //雷电
        Holy, //圣属性
    }

    /// <summary>
    /// 技能类别
    /// </summary>
    public enum SkillCategory
    {
        Attack, //攻击技能,需要攻击掷投判断是否命中
        Heal, //治疗技能
        Buff,
        Debuff,
    }

    /// <summary>
    /// 攻击掷投结果,也可做鉴定是否通过
    /// </summary>
    public enum AttackResultType
    {
        CriticalMiss, // 大失败
        Miss, //未命中
        Hit, //命中
        CriticalHit //大成功
    }

    /// <summary>
    /// 包含一次命中检定的完整信息
    /// </summary>
    public struct AttackResult
    {
        public AttackResultType Type;
        public int RawRoll; // 原始投掷结果
        public int FinalValue; // 最终结果 (原始投掷结果 + 修正值)
        public bool IsSuccessful; //是否命中
    }
}