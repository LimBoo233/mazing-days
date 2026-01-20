using UnityEngine;
using GameSystemEnum;
using UnityEngine.Serialization;
using Utils;

namespace Data
{
    [CreateAssetMenu(fileName = "NewSkill", menuName = "MazingDays/Skill Data")]
    public class SkillData : ScriptableObject
    {
        #region  技能基础信息

        [Header("基础信息")] 
        [SerializeField]private string _skillName; //技能名称
        [SerializeField ,TextArea] private string _skillDescription; //技能描述
        [SerializeField] private Sprite _skillIcon; //技能图标
        
        public string SkillName => _skillName; 
        public string Description => _skillDescription;
        public Sprite Icon => _skillIcon;
        
        #endregion

        #region 资源消耗

        [Header("资源消耗")] 
        [SerializeField] private int _actionPointCost = 1; //消耗动作点
        [SerializeField] private int _bonusPointCost = 1; //附赠动作点
        [SerializeField] private int _resourcePointCost = 1; //通用资源点，可以理解成法师的法术位，战士的怒气等
        
        public int ActionPointCost => _actionPointCost;
        public int BonusPointCost => _bonusPointCost;
        public int ResourcePointCost => _resourcePointCost;

        #endregion
        
        #region 技能效果与数值
        [Header("技能效果")]
        [SerializeField] private SkillCategory _skillCategory; // 技能类别(增益技能，直接伤害技能。。。)
        [SerializeField] private DamageType _damageType; //技能攻击类型
        [SerializeField] private Dice _valueDice; //技能伤害骰子
        
        public SkillCategory SkillCategory => _skillCategory;
        public DamageType DamageType => _damageType;
        public Dice ValueDice => _valueDice;

        #endregion

        #region Positoning(暗黑地牢站位 判断角色所处的位置是否能释放技能)
        [Tooltip("我方站在哪些位置可以使用此技能? (索引0-3 对应 位置1-4)")]
        [SerializeField] private bool[] _launchPositions = new bool[4];
        [Tooltip("此技能能打到敌方哪些位置? (索引0-3 对应 位置1-4)")]
        [SerializeField] private bool[] _targetPositions = new bool[4];
        
        // 辅助方法：判断位置是否合法
        /// <summary>
        /// 判断技能是否可以在指定位置释放
        /// </summary>
        /// <param name="positionIndex">角色所在的站位</param>
        /// <returns></returns>
        public bool CanLaunchFrom(int positionIndex)
        {
            if (positionIndex < 0 || positionIndex >= _launchPositions.Length) 
                return false;
            return _launchPositions[positionIndex];
        }
        /// <summary>
        /// 判断技能是否可以在指定位置打到
        /// </summary>
        /// <param name="positionIndex">地方的站位</param>
        /// <returns></returns>
        public bool CanHitTargetAt(int positionIndex)
        {
            if (positionIndex < 0 || positionIndex >= _targetPositions.Length) 
                return false;
            return _targetPositions[positionIndex];
        }
        
        #endregion

        #region Visual(技能表现相关)
        //后续会继续添加
        [Header("Visual Effects")] 
        [SerializeField] private GameObject _hitVfx; // 技能命中特效
        [SerializeField] private AudioClip _hitSound;//技能音效
        [SerializeField] private string _animTrigger;
        
        public GameObject HitVfx => _hitVfx;
        public AudioClip HitSound => _hitSound;
        public string AnimTrigger => _animTrigger;

        #endregion

    }
}