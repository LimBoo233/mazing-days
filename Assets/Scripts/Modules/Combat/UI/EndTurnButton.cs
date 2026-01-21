using System;
using Core;
using Core.Architecture;
using Core.Event;
using UnityEngine;
using UnityEngine.UI;

namespace Combat.UI
{
	public class EndTurnButton : MonoBehaviour
	{
		[SerializeField] private Button endTurnButton;

		private void OnEnable()
		{
			endTurnButton.onClick.AddListener(EndTurnButtonClicked);
		}
		
		private void OnDisable()
		{
			endTurnButton.onClick.RemoveAllListeners();
		}
		
		private void EndTurnButtonClicked() => EventBus.Publish(new TurnEndedEvent());

	
		
	}
}