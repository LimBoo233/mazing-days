using UnityEngine;
using Features.Units.Core;
using Features.Units.View;
using Modules.Combat.Data.Enums; // 引用伤害类型

public class HealthTest : MonoBehaviour
{
    // 拖拽场景里的 View 物体
    public PlayerUnitView targetView;

    void Start()
    {
        // 1. 【后端】捏造一个数据
        PlayerUnit dummyUnit = new PlayerUnit();
        dummyUnit.CharacterName = "测试假人";
        dummyUnit.MaxHp = 100;
        dummyUnit.InitializeStats(); 

        // 2. 【前端】绑定数据
        targetView.Bind(dummyUnit);

        Debug.Log(" 测试开始：按 空格键 扣血");
    }

    void Update()
    {
        // 按空格扣 10 点血
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 直接修改【后端数据】
            // 我们观察【前端 UI】会不会自动跟着动
            targetView.Model.TakeDamage(10, DamageType.Bludgeoning);
            
            Debug.Log($"当前血量: {targetView.Model.CurrentHp}/{targetView.Model.MaxHp}");
        }
    }
}