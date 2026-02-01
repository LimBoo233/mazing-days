using System.Collections.Generic;
using Core;
using UnityEngine;
using Features.Units.Core;
using Modules.Combat;
using Modules.Combat.Data.Enums;
using Modules.Combat.View; // 引用伤害类型

public class HealthTest : MonoBehaviour
{
    // 拖拽场景里的 View 物体
    public PlayerCombatCombatView playerCombatView;
    public EnemyBattleCombatView enemyCombatView;
    private CombatManager _combatManager;
    void Start()
    {
        _combatManager = GameManager.CombatManager;
        // 1. 【后端】捏造一个数据
        PlayerUnit playerUnit = new PlayerUnit();
        playerUnit.Data.CharacterName = "Player";
        playerUnit.Data.MaxHp = 35;
        playerUnit.Data.Speed = 16;
        playerUnit.InitializeStats(); 
        
        EnemyUnit enemyUnit = new EnemyUnit();
        enemyUnit.Data.CharacterName = "Enemy";
        enemyUnit.Data.MaxHp = 20;
        enemyUnit.Data.Speed = 12;
        enemyUnit.InitializeStats();

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