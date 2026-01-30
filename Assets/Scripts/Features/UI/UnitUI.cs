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
		
		public void Initialize(Unit unit)
		{
			_targetUnit = unit;
			_targetUnit.HpChanged += UpdateHealthBar;
		}

		/// <summary>
		/// 更新角色血条
		/// </summary>
		private void UpdateHealthBar(Unit unit, int hpCost)
		{
			if (hpSlider == null) return;

			float hpPercent = (float)unit.CurrentHp / unit.MaxHp;
			hpSlider.value = hpPercent;

			if (fillImage != null)
			{
				fillImage.color = hpPercent > 0.5f ? hpHighColor : hpLowColor;
			}
		}

		private void Start()
		{
			if (_targetUnit != null) return;

			var unit = GetComponentInParent<Unit>();
			if (unit != null)
			{
				Initialize(unit);
			}
			else
			{
				Debug.LogError("UnitUI: No Unit found in parent.");
			}
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