using System;
using System.Collections.Generic;
using Modules.Combat.Data.Enums;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace Modules.Combat.Data.SO
{
	[CreateAssetMenu(fileName = "NewSkill", menuName = "Mazing Days/SO/Skill Data")]
	public class SkillDataSo : ScriptableObject
	{
		[field: Header("基础信息")]
		[field: SerializeField]
		public int SkillID { get; private set; }

		[field: SerializeField] public string SkillName { get; private set; }

		[field: TextArea]
		[field: SerializeField]
		public string Description { get; private set; }

		[field: SerializeField] public Sprite SkillIcon { get; private set; }

		[field: Header("资源消耗")]
		[field: SerializeField]
		public int ApCost { get; private set; } = 1;

		[field: SerializeField] public int BpCost { get; private set; }
		[field: SerializeField] public int RpCost { get; private set; }

		[field: Header("技能效果与伤害类型")]
		[field: SerializeField]
		public SkillCategory SkillCategory { get; private set; }
		
		[field: Header("伤害配置列表")]
		[field: SerializeField]
		public List<DamageDefinition> DamageList { get; private set; }

		[field: Header("技能施法距离")]
		[field: Tooltip("技能距离取决于什么？（例如武器攻击范围，法术的固定范围）")]
		[field: SerializeField]
		public RangeSource RangeSource { get; private set; }

		[field: Tooltip("此属性会与 RangeSource 加算，共同决定最终技能距离")]
		[field: SerializeField]
		public int Range { get; private set; } = 1;

		// TODO：技能特效相关
		[field: Header("技能表现相关")]
		[field: SerializeField]
		public GameObject HitVfx { get; private set; }

		[field: SerializeField] public AudioClip HitSound { get; private set; }
		[field: SerializeField] public string AnimTrigger { get; private set; }
	}

	[Serializable]
	public struct DamageDefinition
	{
		[Header("1. 伤害数值来源")]
		[Tooltip("None=纯法术(用骰子), MainHand=武器伤害")]
		public DamageBase valueSource;

		[Tooltip("当 ValueSource=None 时生效，作为基础伤害骰子")]
		// 比如至圣斩的 2d8 填在这里
		public Dice fixedDice;

		[Header("2. 伤害类型来源")]
		[Tooltip("Fixed=固定类型(如火焰), MainHand=跟随武器类型")]
		public DamageTypeSource typeSource;

		[Tooltip("当 TypeSource=Fixed 时生效")]
		public DamageType fixedType;

		[Header("3. 加成与修正")]
		// 吃什么属性加成
		public StatAttribute scalingStat;

		// 伤害倍率 (通常是 1.0)
		public float damageMultiplier;
	}
}