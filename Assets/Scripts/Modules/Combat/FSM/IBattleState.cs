namespace Modules.Combat.FSM
{
	public interface IBattleState
	{
		
		public void Enter(CombatManager combatManager);
		public void Update(CombatManager combatManager);
		public void Exit(CombatManager combatManager);
	}
}