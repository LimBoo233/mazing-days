using System.Collections.Generic;
using Core;
using UnityEngine;
using Features.Units.Core;
using Features.Units.Data;
using Modules.Combat;
using Modules.Combat.Data.Enums;
using Modules.Combat.Data.SO;
using Modules.Combat.View; // 引用伤害类型

public class HealthTest : MonoBehaviour
{
    // 拖拽场景里的 View 物体
    public PlayerCombatCombatView playerCombatView;
    public EnemyBattleCombatView enemyCombatView;
    //这里是测试，我觉得Skill应该可以在外部配置比较好，因为我现在不知道这样的架构如何方便的为角色添加Skill
    public SkillDataSo skillDataSo;
    private CombatManager _combatManager;
    void Start()
    {
        _combatManager = GameManager.CombatManager;
        // 1. 【后端】捏造一个数据
        CharacterData playerData = new CharacterData();
        playerData.CharacterName = "Player";
        playerData.MaxHp = 35;
        playerData.Speed = 16;
        playerData.Skills.Add(skillDataSo);
       
        PlayerUnit playerUnit = new PlayerUnit();
        
        playerUnit.InitializeStats(playerData); 
        
        EnemyData enemyData = new EnemyData();
        enemyData.CharacterName = "Enemy";
        enemyData.MaxHp = 20;
        enemyData.Speed = 12;
        
        EnemyUnit enemyUnit = new EnemyUnit();
        enemyUnit.InitializeStats(enemyData);

        // 2. 【前端】绑定数据
        playerCombatView.Bind(playerUnit);
        enemyCombatView.Bind(enemyUnit);
        var players = new List<PlayerUnit> { playerUnit };
        var enemies = new List<EnemyUnit> { enemyUnit };
        
        _combatManager.InitializeCombat(players, enemies);
        Debug.Log(" 测试开始：按 空格键 扣血");
    }

    void Update()
    {
        // 按空格扣 10 点血
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 直接修改【后端数据】
            // 我们观察【前端 UI】会不会自动跟着动
            var playerUnit = playerCombatView.Model;
            playerUnit.TakeDamage(10, DamageType.Bludgeoning);
            
            Debug.Log($"当前血量: {playerUnit.Data.CurrentHp}/{playerUnit.Data.MaxHp}");
        }
    }
}