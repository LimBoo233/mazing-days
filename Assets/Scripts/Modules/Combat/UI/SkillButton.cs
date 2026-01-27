using System;
using Core.Architecture;
using Core.Event;
using Core.Utils;
using Modules.Combat.Data.SO;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Combat.UI
{
	public class SkillButton : MonoBehaviour
	{
		[field: SerializeField] public SkillDataSo SkillData { get; set; }

		[SerializeField] private Button button;

		private void Awake()
		{
			// 如果没有手动赋值 Button 组件，则在 Awake 时自动获取
			if (button == null)
			{
				button = GetComponent<Button>();
			}
		}

		private void OnEnable()
		{
			button.AddDebouncedClickListener(SkillButtonClicked);
		}


		private void OnDisable()
		{
			button.onClick.RemoveListener(SkillButtonClicked);
		}


		private void SkillButtonClicked() =>
			EventBus.Publish(new SkillSelectedEvent(SkillData));
		
	}
}