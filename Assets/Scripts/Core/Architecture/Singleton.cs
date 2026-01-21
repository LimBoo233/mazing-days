using System;
using System.Threading;


namespace Core
{	
	/// <summary>
	/// 泛型单例基类，适用于需要单例模式的类。
	/// 通过继承此类，可以轻松实现单例模式，确保在多线程环境下只有一个实例被创建。 
	/// </summary>
	public abstract class Singleton<T> where T : class
	{
		// 使用 Lazy<T> 保证线程安全和延迟加载
		// LazyThreadSafetyMode.ExecutionAndPublication 确保多线程下只创建一个实例
		private static readonly Lazy<T> LazyInstance = new Lazy<T>(CreateInstance, LazyThreadSafetyMode.ExecutionAndPublication);

		public static T Instance => LazyInstance.Value;

		// 这是一个工厂方法，用来处理私有构造函数的问题
		private static T CreateInstance()
		{
			// 获取 T 的类型信息
			var type = typeof(T);

			// 尝试通过反射获取无参构造函数（BindingFlags 包含了 NonPublic）
			// true 表示匹配私有构造函数
			var instance = Activator.CreateInstance(type, true) as T;
        
			if (instance == null)
			{
				throw new Exception($"未能创建 {type.Name} 的实例。请确保它有一个无参构造函数（可以是私有的）。");
			}

			return instance;
		}
	}
}