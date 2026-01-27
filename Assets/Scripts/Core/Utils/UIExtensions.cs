using System;
using Cysharp.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Core.Utils
{
	public static class UIExtensions
	{
		/// <summary>
		/// 添加带简单冷却/防抖的点击监听
		/// </summary>
		public static void AddDebouncedClickListener(this Button button, UnityAction action, int debounceMs = 100) =>
			button.onClick.AddListener(() => DebouncedInvokeAsync(button, action, debounceMs).Forget());

		/// <summary>
		/// 添加等待异步完成的点击监听（期间禁用按钮）
		/// </summary>
		public static void AddAwaitableClickListener(this Button button, Func<UniTask> asyncAction) =>
			button.onClick.AddListener(() => InvokeAsyncWithInteractableLock(button, asyncAction).Forget());

		/// <summary>
		/// AddDebouncedClickListener 的辅助函数
		/// </summary>
		private static async UniTaskVoid DebouncedInvokeAsync(Button button, UnityAction action, int debounceMs)
		{
			button.interactable = false;

			action?.Invoke();
			await UniTask.Delay(debounceMs, ignoreTimeScale: true);

			button.interactable = true;
		}

		/// <summary>
		/// AddAwaitableClickListener 的辅助函数
		/// </summary>
		private static async UniTaskVoid InvokeAsyncWithInteractableLock(Button button, Func<UniTask> asyncAction)
		{
			button.interactable = false;
			try
			{
				if (asyncAction != null)
					await asyncAction();
			}
			finally
			{
				button.interactable = true;
			}
		}

	 
	}
}