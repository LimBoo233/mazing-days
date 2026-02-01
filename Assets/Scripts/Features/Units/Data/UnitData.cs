using System.Collections.Generic;
using GameSystemEnum;
using Modules.Combat.Data;
using Modules.Combat.Data.SO;
using UnityEngine;

namespace Features.Units.Data
{
	[System.Serializable]
	public class UnitData
	{
		[field: Header("基础属性")]
		[field: SerializeField] public string CharacterName { get; set; }

		[field: SerializeField] public int MaxHp { get; set; }
		[field: SerializeField] public int CriticalNeed { get; set; } = 20;

		[field: Header("四位属性：力量，速度，智力，精神")]
		[field: SerializeField] public int Strength { get; set; }

		[field: SerializeField] public int Speed { get; set; }
		[field: SerializeField] public int Intelligence { get; set; }
		[field: SerializeField] public int Spirit { get; set; }

		[field: SerializeField] public int Ac { get; set; }

		[Header("抗性")]
		[SerializeField] protected List<ResistanceConfig> resistanceSettings = new();
		public List<ResistanceConfig> ResistanceSettings => resistanceSettings;

		[Header("技能列表")]
		// 只用于在 Inspector 里配置“出生自带技能”
		[SerializeField] private List<SkillDataSo> skills = new List<SkillDataSo>();
		public List<SkillDataSo> Skills => skills;

		// 调整值
		public int DexModifier => (Speed - 10) / 2;
		public int StrModifier => (Strength - 10) / 2;
		public int IntModifier => (Intelligence - 10) / 2;
		public int SpiModifier => (Spirit - 10) / 2;

		// 运行时属性
		public int CurrentHp { get; set; }
		public bool IsDead { get; set; }

		// 先攻
		public int Initiative { get; set; }

		// 攻击掷骰加值
		// TODO：伤害掷骰加值因取决于武器类型等因素，次数先简单处理
		public int BaseAccuracyBonus { get; set; }
		public int BaseAttackModifier { get; set; }

		public FactionType FactionType { get; set; }
	}
}