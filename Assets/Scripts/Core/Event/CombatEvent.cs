using Core.Architecture;
using Features.Units.Core;

namespace Core.Event
{
	public struct TurnEndedEvent : IGameEvent
	{
		
	}

	public struct TakeDamageEvent : IGameEvent
	{
		public Unit Target;
		public int Damage;
		public bool IsCritical;
	}
	
	public struct UnitDiedEvent : IGameEvent
	{
		public Features.Units.Core.Unit DeadUnit;
	}
}