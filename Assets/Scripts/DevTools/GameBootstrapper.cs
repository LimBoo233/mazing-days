using Modules.Combat;
using UnityEngine;

#if UNITY_EDITOR 

// 用于测试逻辑，临时运行一些代码等
namespace DevTools
{
	public static class GameBootstrapper
	{
		
		
		
		
		
		// 这个属性确保它在所有 MonoBehaviour Awake 之前运行
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void InitializeGameFramework()
		{
			// 配置
			var eventLogger = new EventLogger();
			eventLogger.Enable();
			// 游戏逻辑
			var combatManager = new CombatManager();
			UnityEngine.Debug.Log("【开发模式】自动加载 MainManager");
		}
		
		
	}
}

#endif 