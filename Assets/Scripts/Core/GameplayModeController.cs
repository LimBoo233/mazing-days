using Combat;
using Modules.Combat;
using Modules.Exploration;

namespace Core
{
	public enum GameState
	{
		Exploration,
		Combat
	}
	
	public class GameplayModeController
	{
		public GameState CurrentState { get; private set; }
		
		private CombatManager _combatManager;       
		private ExplorationManager _explorationManager;
		

	}
}