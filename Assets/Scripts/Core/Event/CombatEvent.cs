using Core.Architecture;
using Features.Units.Core;
using Modules.Combat.Data.SO;
using Newtonsoft.Json;

namespace Core.Event
{
	public struct EnterCombatEvent : IGameEvent
	{
		public int TotalRounds;
	}

	public struct ExitCombatEvent : IGameEvent
	{
		public int TotalRounds;
	}

	public struct TurnEndedEvent : IGameEvent
	{
		[JsonIgnore]
		public Unit ActiveUnit;
	}

	public struct TurnStartedEvent : IGameEvent
	{
		[JsonIgnore]
		public Unit ActiveUnit;
	}

	public struct SkillSelectedEvent : IGameEvent
	{
		public SkillDataSo SkillDataSo;

		public SkillSelectedEvent(SkillDataSo skillDataSo) => SkillDataSo = skillDataSo;
	}

	public struct TakeDamageEvent : IGameEvent
	{
		[JsonIgnore]
		public Unit Source;
		[JsonIgnore]
		public Unit Target;

		public string TargetName;
		public int Damage;
		public bool IsCritical;
	}

	public struct UnitDiedEvent : IGameEvent
	{
		[JsonIgnore]
		public Unit DeadUnit;
	}

	public struct SkillCastEvent : IGameEvent
	{
		[JsonIgnore]
		public Unit Caster;

		[JsonIgnore]
		public Unit Target;

		public SkillDataSo SkillDataSo;
	}
}