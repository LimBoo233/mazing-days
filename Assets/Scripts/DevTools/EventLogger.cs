using System;
using Core.Architecture;
using UnityEngine;

namespace DevTools
{
	public class EventLogger
	{
		// 是否开启日志
		private bool _enableLogging = true;

		// 只看特定事件(留空则看全部)
		// public string FilterKeyword = "";

		public void Enable()
		{
#if UNITY_EDITOR
			Debug.Log("EventLogger 已启用");
			_enableLogging = true;
			EventBus.OnDebugLogAll += LogEvent;
#endif
		}

		public void Disable()
		{
#if UNITY_EDITOR
			Debug.Log("EventLogger 已禁用");
			_enableLogging = false;
			EventBus.OnDebugLogAll -= LogEvent;
#endif
		}

		private void LogEvent(IGameEvent e)
		{
			if (!_enableLogging) return;
			
			string eventName = e.GetType().Name;

			// 简单的过滤器功能
			// if (!string.IsNullOrEmpty(FilterKeyword) && !eventName.Contains(FilterKeyword)) return;
			
			// 技巧：利用 JsonUtility 把类里面的字段打印出来，方便看数值
			string json = Newtonsoft.Json.JsonConvert.SerializeObject(e);
			Debug.Log($"<color=#00FF00>[EventBus]</color> <color=#00FFFF>{eventName}</color>: {json}");
		}


	}
}