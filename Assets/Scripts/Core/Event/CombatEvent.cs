using Core.Architecture;
using Features.Units.Core;
using Modules.Combat.Data.SO;

namespace Core.Event
{
	public struct TurnEndedEvent : IGameEvent
	{
	}

	public struct SkillSelectedEvent : IGameEvent
	{
		public SkillSelectedEvent(SkillDataSo skillData) => SkillData = skillData;
		
		public SkillDataSo SkillData;
	}

	public struct TakeDamageEvent : IGameEvent
	{
		public TakeDamageEvent(Unit target, int damage, bool isCritical) =>
			(Target, Damage, IsCritical) = (target, damage, isCritical);
		
		public Unit Target;
		public int Damage;
		public bool IsCritical;
	}

	public struct UnitDiedEvent : IGameEvent
	{
		public UnitDiedEvent(Unit deadUnit) => DeadUnit = deadUnit; 
		
		public Unit DeadUnit;
	}
}