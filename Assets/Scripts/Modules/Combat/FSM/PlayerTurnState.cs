namespace Modules.Combat.FSM
{
	public class PlayerTurnState: IBattleState
	{
		public void Enter(CombatManager combatManager)
		{
			combatManager.SelectedSkillId = -1;
			combatManager.SelectedTarget = default;
		}

		public void Update(CombatManager combatManager)
		{
			throw new System.NotImplementedException();
		}

		public void Exit()
		{
			throw new System.NotImplementedException();
		}
	}
}