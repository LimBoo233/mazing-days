using System;
using System.Collections.Generic;
using UnityEngine;
using GameSystemEnum;
using Data;// 引用 Dice 和 SkillData

namespace Units
{
    /// <summary>
    /// 抗性配置结构体 (在面板上配表用)
    /// </summary>
    [Serializable]
    public struct ResistanceConfig
    {
        public DamageType type;
        [Tooltip("0.5 = 50% 抗性; -0.5 = 弱点 (受到 150% 伤害); 1.0 = 免疫")]
        [Range(-2.0f, 1.0f)]
        public float value;
    }
    /// <summary>
    /// 这段代码只包含数值的运算，其余的效果在别的脚本中处理
    /// </summary>
    public class CharacterStats
    {
        //先暂时跳过UI设计相关内容
        #region  BaseStats(基础属性)
        
        [Header("基础属性")] [SerializeField] private string _characterName = "";
        [SerializeField] private FactionType _faction; // 角色阵营
        // [SerializeField] private Sprite _characterIcon; //角色图标
        [SerializeField] private int _maxHp = 20;
        [SerializeField] private int _armorClass = 10; //角色的AC值
        [SerializeField] private int _speed = 5; //角色速度，这里应该还有先攻骰，不过暂时先不考虑，简单处理
        [Tooltip("基础攻击修正值 (相当于 D&D 的力量/智力调整值)\n投伤害骰子时会加上这个值")]
        [SerializeField] private int _baseAttackModifier = 0; 
        [Tooltip("基础命中加成 (投 d20 时加上这个值)")]
        [SerializeField] private int _baseAccuracyBonus = 0;

        [SerializeField] private int _criticalNeed = 20;//暴击需求值，方便后续改变
        
        #endregion
        
        #region 2. Resources (BG3 资源)

        [Header("Resources (BG3)")]
        [SerializeField] private int _maxSanity = 100; // 压力上限,这个暂时不确定，后面可能还需要更改是否需要压力值
        [SerializeField] private int _maxResourcePoints = 3; // RP (职业资源)
        [SerializeField] private int _maxActionPoints = 1;   // AP (标准动作)
        [SerializeField] private int _maxBonusPoints = 1;    // BP (附赠动作)

        #endregion
        
        #region 3. Resistances (抗性配置)

        [Header("Resistances")]
        [SerializeField] private List<ResistanceConfig> _resistanceSettings = new List<ResistanceConfig>();
        // 运行时字典，用于 O(1) 快速查找抗性
        private Dictionary<DamageType, float> _resistanceDict = new Dictionary<DamageType, float>();

        #endregion
        
        #region 4. Runtime State (运行时状态)

        public int CurrentHp { get; private set; }
        public int CurrentSanity { get; private set; }
        public int CurrentRp { get; private set; }
        public int CurrentAp { get; private set; }
        public int CurrentBp { get; private set; }
        public bool IsDead { get; private set; }

        #endregion
        
        #region 5. Events (UI 监听接口)

        // UI 监听这些事件来更新血条、蓝条
        public event Action<float> OnHpChanged;     // 参数: 百分比
        public event Action<float> OnSanityChanged; // 参数: 百分比
        public event Action<int, int> OnResourceChanged; // 参数: 当前值, 最大值
        public event Action OnDeath;

        #endregion
        
        #region Public Getters (对外接口) 

        public string CharacterName => _characterName;
        public FactionType Faction => _faction;
        public int ArmorClass => _armorClass; // 对外提供 AC
        public int Speed => _speed;
        public int BaseAttackModifier => _baseAttackModifier;
        public int BaseAccuracyBonus => _baseAccuracyBonus;

        #endregion
        
        #region Unity Lifecycle

        private void Awake()
        {
            InitializeStats();
            InitializeResistances();
        }
        
        /// <summary>
        /// 初始化角色状态
        /// 以后应该根据玩家的存档来初始化角色状态
        /// </summary>
        private void InitializeStats()
        {
            CurrentHp = _maxHp;
            CurrentSanity = 0;
            CurrentRp = _maxResourcePoints;
            IsDead = false;
        }
        
        /// <summary>
        /// 初始化抗性字典
        /// </summary>
        private void InitializeResistances()
        {
            _resistanceDict.Clear();
            foreach (var config in _resistanceSettings)
            {
                if (!_resistanceDict.ContainsKey(config.type))
                {
                    _resistanceDict.Add(config.type, config.value);
                }
            }
        }
        #endregion
    }
}