using System;
using Core;
using Core.Architecture;
using Core.Event;

namespace Modules.Combat
{
	// TODO：文字通知显示应该基于配置，而非此处的硬编码
	/// <summary>
	/// 将战斗系统中所发生的事件，传递给 UI 系统
	/// </summary>
	public class CombatUIBridge : IDisposable
	{
		public CombatUIBridge()
		{
			EventBus.Subscribe<TurnEndedEvent>(OnTurnEnded);
		}

		public void Dispose()
		{
			EventBus.Unsubscribe<TurnEndedEvent>(OnTurnEnded);
		}

		private void OnTurnEnded(TurnEndedEvent _) =>
			EventBus.Publish(new TextNotifiedEvent("回合结束！", 1000));
	}
}