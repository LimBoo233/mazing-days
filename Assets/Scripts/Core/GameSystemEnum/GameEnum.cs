namespace GameSystemEnum
{
    /// <summary>
    /// 阵营类型
    /// </summary>
    public enum FactionType
    {
        Player,
        Enemy,
        Neutral
    }



    /// <summary>
    /// 掷骰结果
    /// </summary>
    public enum RollResultType
    {
        CriticalFailure,  // 大失败
        Failure,
        Success, 
        CriticalSuccess // 大成功
    }


}