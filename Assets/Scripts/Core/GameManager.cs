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
        

        protected override void Awake()
        {
            base.Awake();
            _gameplayModeController = new GameplayModeController();
        }
        
        private void Start()
        {
            
        }
  
    }
}