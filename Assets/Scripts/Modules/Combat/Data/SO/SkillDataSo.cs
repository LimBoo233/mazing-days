using Combat;
using UnityEngine;
using Utils;

namespace Modules.Combat.Data.SO
{
	[CreateAssetMenu(fileName = "NewSkill", menuName = "MazingDays/Skill Data")]
	public class SkillDataSo : ScriptableObject
	{
		[field: Header("基础信息")]
		[field: SerializeField]
		public string SkillName { get; private set; }

		[field: TextArea]
		[field: SerializeField]
		public string Description { get; private set; }

		[field: SerializeField] public Sprite SkillIcon { get; private set; }

		[field: Header("资源消耗")] public int ApCost { get; private set; } = 1;

		[field: SerializeField] public int BpCost { get; private set; } = 1;
		[field: SerializeField] public int RpCost { get; private set; } = 1;


		[field: Header("技能效果")]
		[field: SerializeField]
		public SkillCategory SkillCategory { get; private set; }

		[field: SerializeField] public DamageType DamageType { get; private set; }
		[field: SerializeField] public DamageType TargetType { get; private set; }
		
		[field: SerializeField] public int[] Dice { get; set; }


		//技能伤害骰子
		[field: SerializeField] public Dice ValueDice;
		// 技能距离
		[field: SerializeField] public int Range { get; private set; } = 1;
		
		// TODO：技能特效相关
		[field: Header("技能表现相关")]
		[field: SerializeField]
		public GameObject HitVfx { get; private set; }

		[field: SerializeField] public AudioClip HitSound { get; private set; }
		[field: SerializeField] public string AnimTrigger { get; private set; }
	}
}