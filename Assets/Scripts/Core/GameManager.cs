using Modules.Combat;
using Modules.Exploration;
using UnityEngine;


namespace Core
{
	public class GameManager : SingletonAutoMono<GameManager>
	{
		/// <summary>
		/// 游戏模式控制器（探索/战斗）
		/// </summary>
		public static GameplayModeController GameplayModeController => Instance._gameplayModeController;

		private GameplayModeController _gameplayModeController;

		public static UIManager UIManager => Instance._uiManager;
		private UIManager _uiManager;

		public CombatManager CombatManager => _combatManager;
		private CombatManager _combatManager;

		public static ExplorationManager ExplorationManager => Instance._explorationManager;
		private ExplorationManager _explorationManager;

		/// <summary>
		/// 输入管理类
		/// </summary>
		public static InputManager InputManager => Instance._inputManager;

		private InputManager _inputManager;


		protected override void Awake()
		{
			base.Awake();
			_gameplayModeController = new GameplayModeController();
			_uiManager = new UIManager();
			_combatManager = new CombatManager();
			_inputManager = new InputManager();
		}

		protected void Update()
		{
			_combatManager.Update();
		}
	}
}