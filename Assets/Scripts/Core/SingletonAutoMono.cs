using UnityEngine;

// 定义命名空间，建议用 "项目名.模块名"
// 以后其他脚本要用单例时，记得在顶部写 "using MazingDays.Utils;"
namespace Utils 
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
        
        // 线程锁
        private static readonly object _lock = new object();
        
        // 应用程序是否正在退出
        private static bool _isShuttingDown = false;

        public static T Instance
        {
            get
            {
                if (_isShuttingDown)
                {
                    return null;
                }

                lock (_lock)
                {
                    if (_instance == null)
                    {
                        // 1. 先去场景找
                        _instance = (T)FindObjectOfType(typeof(T));

                        // 2. 没找到就创建
                        if (_instance == null)
                        {
                            var singletonObject = new GameObject();
                            _instance = singletonObject.AddComponent<T>();
                            singletonObject.name = typeof(T).ToString() + " (Singleton)";
                            DontDestroyOnLoad(singletonObject);
                        }
                        else
                        {
                            // 找到了也设为不销毁
                            DontDestroyOnLoad(_instance.gameObject);
                        }
                    }

                    return _instance;
                }
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

        protected virtual void OnApplicationQuit()
        {
            _isShuttingDown = true;
        }
    }
}