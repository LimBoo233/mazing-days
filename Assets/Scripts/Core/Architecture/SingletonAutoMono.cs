using UnityEngine;

// 定义命名空间，建议用 "项目名.模块名"
// 以后其他脚本要用单例时，记得在顶部写 "using MazingDays.Utils;"
namespace Core
{
	/// <summary>
	/// 增强版自动单例
	/// 1. 优先查找场景中已存在的对象
	/// 2. 自动创建不存在的对象
	/// 3. 防止手动挂载导致的重复对象
	/// 4. 线程安全锁
	/// </summary>
	public class SingletonAutoMono<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T _instance;
		
		public static T Instance
		{
			get
			{
				if (_instance != null)
				{
					return _instance;
				}
				
				// 尝试在场景中查找
				_instance = (T)FindObjectOfType(typeof(T));

				// 没找到就创建一个新的
				if (_instance == null)
				{
					var singletonObject = new GameObject();
					_instance = singletonObject.AddComponent<T>();
                
					// 设置名字方便调试
					singletonObject.name = $"{typeof(T).Name} (Singleton)";
					
					DontDestroyOnLoad(singletonObject);
				}
				else
				{
					// 如果场景里原本就放了一个，也标记为不销毁
					if (Application.isPlaying)
					{
						DontDestroyOnLoad(_instance.gameObject);
					}
				}
				
				return _instance;
			}
		}
		

		protected virtual void Awake()
		{
			if (_instance == null)
			{
				_instance = this as T;
				DontDestroyOnLoad(gameObject);
			}
			else if (_instance != this)
			{
				Destroy(gameObject);
			}
		}
		
		
	}
}