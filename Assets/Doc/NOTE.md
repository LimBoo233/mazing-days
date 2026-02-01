# 开发备忘录

## TODO

## Refactor

- [ ] 将 Unit 中的战斗相关数据分离到单独的类中 (`CharacterData`)
  1. 此外，`CharacterData`的本职工作不仅包括所有战斗相关的属性，所有所有跨场景、需要存档的持久化信息。探索相关的数据（比如位置、当前朝向、拥有的地图指令等级）也须存在这里。
  2. `CharacterData`在 Unit 当中是应该是一个只读属性，所有对其数据的修改都应通过 Unit 的方法来完成。
- [ ] 将`UnitView`重命名为`BattleUnitView`，将其特化为战斗专用，存放于 Comabat 文件下
  1. 这是因为之后，Combat 和 Exploration 会需要使用不同的 View 实现。

## 关于战斗、探索模块间关系的粗略设想

```mermaid
classDiagram
%% 全局管理层
  class GameManager {
    +ExplorationManager Exploration
    +CombatManager Combat
    +GameState CurrentState
    +SwitchState(GameState newState)
  }

%% 场景/阶段管理层
  class ExplorationManager {
    +List~Unit~ ActiveUnits
    +CheckEncounter()
    +SyncAllOverworldViews()
  }

  class CombatManager {
    +List~Unit~ TurnOrder
    +StartCombat(List~Unit~ participants)
    +ProcessTurn()
  }

%% 核心实体（容器）
  class Unit {
    +CharacterData Data
    +CombatModule Combat
    +ExplorationModule Exploration
    +Init(CharacterData data)
  }

%% 逻辑模块
  class CombatModule {
    +TakeDamage(int dmg)
    +CalculateAction()
  }

  class ExplorationModule {
    +Move(Vector2 input)
    +Vector3 LogicalPosition
  }

%% 表现层
  class OverworldView {
    +Unit unit
    +Bind(Unit u)
    +Update()
  }

  class PlayerBattleView {
    +Unit unit
    +Bind(Unit u)
    +PlayAttackAnim()
  }

%% 数据层
  class CharacterData {
    +string Name
    +int Hp
    +Vector3 LogicalPosition
  }

%% 关系描述
  GameManager --> ExplorationManager
  GameManager --> CombatManager

  ExplorationManager ..> Unit : 管理并驱动
  CombatManager ..> Unit : 管理并驱动

  Unit *-- CharacterData : 持有持久化数据
  Unit *-- CombatModule : 包含战斗逻辑
  Unit *-- ExplorationModule : 包含探索逻辑

  OverworldView --> Unit : 观察并同步 (Exploration)
  PlayerBattleView --> Unit : 观察并同步 (Combat)
```