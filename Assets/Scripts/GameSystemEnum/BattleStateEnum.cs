namespace GameSystemEnum
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
    
    
}

