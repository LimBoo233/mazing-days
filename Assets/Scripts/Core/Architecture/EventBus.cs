using System;
using System.Collections.Generic;

namespace Core.Architecture
{
	public interface IGameEvent { }

	public static class EventBus
	{
		// 存储所有订阅者，key是事件类型，value是回调列表
		private static readonly Dictionary<Type, List<Delegate>> Handlers = new();
			
		
#if UNITY_EDITOR
		// 定义一个全局调试事件，只在编辑器下编译。只用于在测试时，可以监听所有事件的发布
		public static event Action<IGameEvent> OnDebugLogAll; 
#endif

		// 订阅
		public static void Subscribe<T>(Action<T> handler) where T : IGameEvent
		{
			var type = typeof(T);
			if (!Handlers.TryGetValue(type, out var list))
			{
				list = new List<Delegate>();
				Handlers[type] = list;
			}

			// 这里的 Add 只是加引用，很快
			if (!list.Contains(handler))
			{
				list.Add(handler);
			}
		}

		// 取消订阅
		public static void Unsubscribe<T>(Action<T> handler) where T : IGameEvent
		{
			var type = typeof(T);
			if (Handlers.TryGetValue(type, out var list))
			{
				// 列表移除会有少许开销，但在 Unsubscribe 时通常不敏感
				list.Remove(handler);
            
				// 可选：如果列表空了，移除 Key 以节省内存（小项目其实不移除也行）
				if (list.Count == 0)
				{
					Handlers.Remove(type);
				}
			}
		}

		// 发布 
		public static void Publish<T>(T gameEvent) where T : IGameEvent
		{
			// 测试阶段，在分发给具体订阅者之前，先丢给全局监听器
#if UNITY_EDITOR
			OnDebugLogAll?.Invoke(gameEvent);
#endif
			
			var type = typeof(T);
			if (Handlers.TryGetValue(type, out var list))
			{
				// 倒叙遍历允许在处理事件时，订阅者取消订阅自己（Unsubscribe），而不会报错
				for (int i = list.Count - 1; i >= 0; i--)
				{
					((Action<T>) list[i])?.Invoke(gameEvent);
				}
			}
		}
    
		// 清理所有（切换场景时调用，防止野指针）
		public static void ClearAll()
		{
			Handlers.Clear();
		}
	}
}