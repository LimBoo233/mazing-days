using System;
using System.Collections.Generic;
using Core.UI;
using UnityEngine;
using UnityEngine.Events;


namespace Core
{
	public enum E_UILayer
	{
		Bottom,
		Middle,
		Top,
		System
	}
	public class UIManager: SingletonAutoMono<UIManager>
	{
		private Dictionary<string, BasePanel> _panelDic = new Dictionary<string, BasePanel>();
		[SerializeField] private Transform bottomLayer;
		[SerializeField] private Transform middleLayer;
		[SerializeField] private Transform topLayer;
		[SerializeField] private Transform systemLayer;

		protected override void Awake()
		{
			base.Awake();
			Initialize();
		}

		/// <summary>
		/// 初始化内容，但我现在想不到
		/// </summary>
		private void Initialize()
		{
		
			var panels = GetComponentsInChildren<BasePanel>(true);
			foreach (var panel in panels)
			{
				RegisterPanel(panel);
			}
			string loadedKeys = string.Join(", ", _panelDic.Keys);
			Debug.Log($"[UIManager] 初始化完毕，已注册面板: {loadedKeys}");
		}

		public T ShowPanel<T>(E_UILayer layer = E_UILayer.Middle,UnityAction onCompleted =null ,params object[] args) where T : BasePanel
		{
			string panelName = typeof(T).Name;
			print(panelName);
			if (_panelDic.TryGetValue(panelName, out BasePanel panel))
			{
				Transform targetLayer = GetLayerNode(layer);
				if (panel.transform.parent != targetLayer)
				{
					panel.transform.SetParent(targetLayer);
				}
				panel.ShowMe(onCompleted, args);
			
				return (T)panel;
			}
			Debug.LogError($"{panelName} 不存在！");
			return null;
		}

		public void HidePanel<T>() where T:BasePanel
		{
			string panelName = typeof(T).Name;
			if (_panelDic.TryGetValue(panelName, out BasePanel panel))
			{
				panel.HideMe();
			}
			else
			{
				Debug.LogError($"{panelName} 不存在！");
			}
		}
		
		/// <summary>
		/// 获取面板实例 (不显示)
		/// </summary>
		public T GetPanel<T>() where T : BasePanel
		{
			string panelName = typeof(T).Name;
			if (_panelDic.TryGetValue(panelName, out BasePanel panel))
			{
				return panel as T;
			}
			return null;
		}

		private void RegisterPanel(BasePanel panel)
		{
			string panelName = panel.GetType().Name;
			if (!_panelDic.ContainsKey(panelName))
			{
				_panelDic.Add(panelName, panel);
				panel.HideMe();
			}
		}

		private Transform GetLayerNode(E_UILayer layer)
		{
			switch (layer)
			{
				case E_UILayer.Bottom:
					return bottomLayer;
				
				case E_UILayer.Middle:
					return middleLayer;
			
				case E_UILayer.Top:
					return topLayer;
				
				case E_UILayer.System:
					return systemLayer;
			
				default:
					return middleLayer;
			}
		}
	}
	
	
}