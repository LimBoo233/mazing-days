using System.Collections.Generic;
using Core;
using UnityEngine;
using Features.Units.Core;
using Features.Units.View;
using Modules.Combat;
using Modules.Combat.Data.Enums; // 引用伤害类型

public class HealthTest : MonoBehaviour
{
    // 拖拽场景里的 View 物体
    public PlayerUnitView playerView;
    public EnemyUnitView enemyView;
    private CombatManager _combatManager;
    void Start()
    {
        _combatManager = GameManager.Instance.CombatManager;
        // 1. 【后端】捏造一个数据
        PlayerUnit playerUnit = new PlayerUnit();
        playerUnit.CharacterName = "Player";
        playerUnit.MaxHp = 35;
        playerUnit.Speed = 16;
        playerUnit.InitializeStats(); 
        
        EnemyUnit enemyUnit = new EnemyUnit();
        enemyUnit.CharacterName = "Enemy";
        enemyUnit.MaxHp = 20;
        enemyUnit.Speed = 12;
        enemyUnit.InitializeStats();

        // 2. 【前端】绑定数据
        playerView.Bind(playerUnit);
        enemyView.Bind(enemyUnit);
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
            playerView.Model.TakeDamage(10, DamageType.Bludgeoning);
            
            Debug.Log($"当前血量: {playerView.Model.CurrentHp}/{playerView.Model.MaxHp}");
        }
    }
}