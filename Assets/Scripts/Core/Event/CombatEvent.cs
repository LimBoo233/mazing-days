using Core.Architecture;
using Features.Units.Core;
using Newtonsoft.Json;

namespace Core.Event
{
	public struct TurnEndedEvent : IGameEvent
	{
		
	}

	public struct TakeDamageEvent : IGameEvent
	{
		[JsonIgnore]
		public Unit Target;
		public string TargetName;
		public int Damage;
		public bool IsCritical;
		
		public TakeDamageEvent(Unit target, int damage, bool isCritical) => (Target, TargetName, Damage, IsCritical) = (target, target.CharacterName, damage, isCritical);
	}
	
	public struct UnitDiedEvent : IGameEvent
	{
		[JsonIgnore]
		public Features.Units.Core.Unit DeadUnit;
		
		public string DeadUnitName;
	}
}