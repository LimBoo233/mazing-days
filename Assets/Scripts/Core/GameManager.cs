using System;
using Modules.Combat;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core
{
	public class GameManager : SingletonAutoMono<GameManager>
	{
		/// <summary>
		/// 游戏模式控制器（探索/战斗）
		/// </summary>
		public GameplayModeController GameplayModeController => _gameplayModeController;
		private GameplayModeController _gameplayModeController;
		
		public UIManager UIManager => _uiManager;
		private UIManager _uiManager;
		
		public CombatManager CombatManager => _combatManager;
		private CombatManager _combatManager;


		protected override void Awake()
		{
			base.Awake();
			_gameplayModeController = new GameplayModeController();
			_uiManager = new UIManager();
			_combatManager = new CombatManager();
		}

		protected void Update()
		{
			_combatManager.Update();
		}
	}
}