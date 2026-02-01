using Core;
using Features.Units.Data;
using Modules.Exploration;
using Modules.Exploration.View;
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
			Debug.Log("【开发模式】游戏框架初始化完成");
		}
		
		// 这个属性确保它在场景加载后运行
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		private static void TestAfterSceneLoad()
		{
			Test4ExplorationModule();
		}
		
		private static void Test4ExplorationModule()
		{
			if (GameManager.Instance == null)
			{
				Debug.LogError("【开发模式】GameManager 实例未初始化，无法测试 ExplorationModule");
				return;
			}
			Debug.Log("【开发模式】开始测试 ExplorationModule");
			
			var playerPref = GameObject.Find("Player");
			if (playerPref == null)
			{
				Debug.LogError("【开发模式】场景中未找到名为 'Player' 的游戏对象，无法测试 ExplorationModule");
				return;
			}

			// 初始化组件
			var characterData = new CharacterData
			{
				LogicalPosition = Vector3.zero
			};
			var explorationModule = new ExplorationModule();
			explorationModule.Bind(characterData);
			
			Debug.Log("【开发模式】完成 explorationModule 的创建");

			var playerOverworldView = playerPref.GetComponent<PlayerOverworldView>();
			playerOverworldView.Bind(explorationModule);
			
			Debug.Log("【开发模式】完成 PlayerOverworldView 的绑定");
		}
		
		
	}
}

#endif 