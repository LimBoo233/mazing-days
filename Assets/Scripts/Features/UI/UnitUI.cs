using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Features.Units.Core;
using Features.Units.View;

namespace Features.UI
{
    public class UnitUI : MonoBehaviour
    {
        [Header("UI Components")] 
        //方便测试，这里暂时先用Slider代替
        [field:SerializeField] private Slider _hpSlider;

        [field:SerializeField] private Image _fillImage;
        [Header("Settings")]
        [SerializeField] private Color _hpHighColor = Color.green;
        
        [SerializeField] private Color _hpLowColor = Color.red;
        
        private UnitView _targetUnitView;

       

        public void Initialize(UnitView unitView)
        {
            _targetUnitView = unitView;
            _targetUnitView.OnHpChanged += UpdateHealthBar;
            UpdateHealthBar((float)unitView.CurrentHp / unitView.MaxHp);
        }

        /// <summary>
        /// 更新角色血条
        /// </summary>
        /// <param name="hpPercent">Hp值百分比</param>
        private void UpdateHealthBar(float hpPercent)
        {
            if (_hpSlider == null)
                return;
            
            _hpSlider.value = hpPercent;
            
            if (_fillImage != null)
            {
                _fillImage.color = hpPercent > 0.5f ? _hpHighColor : _hpLowColor;
            }
        }
        
        private void Start()
        {
            if (_targetUnitView == null)
            {
                var unit = GetComponentInParent<UnitView>();
                if (unit != null)
                {
                    Initialize(unit);
                }
                else
                {
                    Debug.LogError("UnitUI: No Unit found in parent.");
                }
            }
        }
        
        private void OnDestroy()
        {
            // 取消订阅，防止报错
            if (_targetUnitView != null)
            {
                _targetUnitView.OnHpChanged -= UpdateHealthBar;
            }
        }
    }
}

