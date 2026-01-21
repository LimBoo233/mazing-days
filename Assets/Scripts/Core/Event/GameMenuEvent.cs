using System.Threading;
using Core.Architecture;
using Cysharp.Threading.Tasks;
using Features.UI;
using UnityEngine;

namespace Core.Event
{
	public interface INotifiedEvent : IGameEvent
	{
		public UniTask PlayNotificationAsync(NotificationView notificationView, CancellationToken cancellationToken);
	}

	public readonly struct TextNotifiedEvent : INotifiedEvent
	{
		public TextNotifiedEvent(string text, int duration) => (_text, _duration) = (text, duration);

		private readonly string _text;
		private readonly int _duration;

		public async UniTask PlayNotificationAsync(NotificationView notificationView, CancellationToken ct = default)
		{
			notificationView.Text.text = _text;
			await UniTask.Delay(millisecondsDelay: _duration, cancellationToken: ct);
		}
	}

	public readonly struct PrefabNotifiedEvent : INotifiedEvent
	{
		public PrefabNotifiedEvent(GameObject prefab, int duration) => (_prefab, _duration) = (prefab, duration);

		private readonly GameObject _prefab;
		private readonly int _duration;

		public async UniTask PlayNotificationAsync(NotificationView notificationView, CancellationToken ct = default)
		{
			var instance = Object.Instantiate(_prefab, notificationView.Container);
			await UniTask.Delay(millisecondsDelay: _duration, cancellationToken: ct);
			Object.Destroy(instance);
		}
	}
	
}