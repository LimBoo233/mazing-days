using System;
using System.Threading;
using Core;
using Core.Architecture;
using Core.Event;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Features.UI
{
	/// <summary>
	/// 屏幕中上方的通知视图，用来显示各种通知信息（ex：大成功）
	/// </summary>
	public class NotificationView : MonoBehaviour
	{
		/// <summary>
		/// 用于外部设置文本内容，带有特效的预制体，和淡入淡出效果
		/// </summary>
		public TextMeshProUGUI Text => defaultText;
		public Transform Container => dynamicContainer;
		public CanvasGroup CanvasGroup => canvasGroup;

		[SerializeField] private CanvasGroup canvasGroup;
		[SerializeField] private TextMeshProUGUI defaultText;
		[SerializeField] private Transform dynamicContainer;

		private CancellationTokenSource _cts;

		#region 注册事件
		
		private void OnEnable()
		{
			EventBus.Subscribe<TextNotifiedEvent>(OnTextNotified);
			EventBus.Subscribe<PrefabNotifiedEvent>(OnPrefabNotified);
		}

		private void OnDisable()
		{
			EventBus.Unsubscribe<TextNotifiedEvent>(OnTextNotified);
			EventBus.Unsubscribe<PrefabNotifiedEvent>(OnPrefabNotified);
		}
		
		private void OnTextNotified(TextNotifiedEvent textNotifiedEvent) => NotifyAsync(textNotifiedEvent).Forget();

		private void OnPrefabNotified(PrefabNotifiedEvent prefabNotifiedEvent) => NotifyAsync(prefabNotifiedEvent).Forget();

		# endregion
		
		/// <summary>
		/// 异步显示通知，同一时间只显示一个通知，新的通知会取消当前的
		/// </summary>
		private async UniTaskVoid NotifyAsync<T>(T textNotifyEvent) where T : INotifiedEvent
		{
			// 取消当前的通知
			_cts?.Cancel();
			_cts = new CancellationTokenSource();
			ResetView();
			await textNotifyEvent.PlayNotificationAsync(this, _cts.Token);
		}
		
		// 重置状态（每次显示前清理一下）
		private void ResetView()
		{
			defaultText.text = "";
			// 清理动态生成的子物体
			for (int i = transform.childCount - 1; i >= 0; i--)
			{
				var child = transform.GetChild(i);
				if (child == null)
				{
					Destroy(child);
				}
			}
		}
	}
}