using UnityEngine;
using UnityEngine.UI;
using Features.Units.Core;
using UnityEngine.Serialization;


namespace Features.UI
{
	public class UnitUI : MonoBehaviour
	{
		[Header("UI Components")]
		//方便测试，这里暂时先用Slider代替
		[SerializeField] private Slider hpSlider;

		[SerializeField] private Image fillImage;

		[Header("Settings")]
		[SerializeField] private Color hpHighColor = Color.green;

		[SerializeField] private Color hpLowColor = Color.red;

		private Unit _targetUnit;
		
		/// <summary>
		/// UnitUI 的初始化方法订阅事件，与数据传递
		/// </summary>
		/// <param name="unitModel"></param>
		public void Initialize(Unit unitModel)
		{
			
			if (_targetUnit != null)
			{
				_targetUnit.HpChanged -= UpdateHealthBar;
			}
			_targetUnit = unitModel;
			if (_targetUnit != null)
			{
				// 绑定事件
				_targetUnit.HpChanged += UpdateHealthBar;
				// 绑定后立即刷新一次
				RefreshHp();
			}
			
		}

		private void RefreshHp()
		{
			if (hpSlider == null)
				return;
			float hpPercent = (float)_targetUnit.Data.CurrentHp / _targetUnit.Data.MaxHp;
			hpSlider.value = hpPercent;
			
		}

		/// <summary>
		/// 更新角色血条
		/// </summary>
		private void UpdateHealthBar(Unit unit, int hpCost)
		{
			RefreshHp();
		}

		private void OnDestroy()
		{
			if (_targetUnit != null)
			{
				_targetUnit.HpChanged -= UpdateHealthBar;
			}
		}
		
	}
}