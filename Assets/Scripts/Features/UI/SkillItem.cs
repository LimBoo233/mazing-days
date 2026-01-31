using System;
using System.Text;
using Modules.Combat.Data.SO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Features.UI
{
    public class SkillItem:MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
    {
        [Header("UI Components")]
         
        [SerializeField] private Button skillButton;
        [SerializeField] private TextMeshProUGUI skillName;
        [SerializeField] private TextMeshProUGUI skillCost;
        [SerializeField] private Image skillIcon;
        
        private RectTransform _rectTransform;
        private Action<int> _onClickCallBack;
        // 拖拽完成回调
        private Action _onDragCompleteCallBack;
        private bool _isDragging = false;
        private CanvasGroup _canvasGroup;
        public SkillDataSo Data { get;private set; }

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _rectTransform = GetComponent<RectTransform>();
        }

        public void SetUp(SkillDataSo data,Action<int> callBack)
        {
            if (data == null)
                return;
            
            Data = data;
            _onClickCallBack = callBack;
            
            if (skillName != null)
                skillName.text = data.SkillName;
            

            if (skillIcon != null)
                skillIcon.sprite = data.SkillIcon;
            
            
            
            if (skillCost != null)
            {
                if (data.ApCost == 0 && data.RpCost == 0 && data.BpCost == 0)
                {
                    skillCost.text = ""; // 无消耗则不显示
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    // 这里可以根据需要加颜色，例如: $"<color=yellow>{data.ApCost}AP</color> "
                    if (data.ApCost > 0) sb.Append($"{data.ApCost}AP ");
                    if (data.RpCost > 0) sb.Append($"{data.RpCost}RP ");
                    if (data.BpCost > 0) sb.Append($"{data.BpCost}BP");
                    skillCost.text = sb.ToString();
                }
            }

            if (skillButton != null)
            {
                skillButton.onClick.RemoveAllListeners();
                skillButton.onClick.AddListener(() => _onClickCallBack(Data.SkillID));
            }
        }

        /// <summary>
        /// 鼠标进入时，应该显示技能的详细信息
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            //TODO 在TooltipManager中处理
        }
        
        

        public void OnBeginDrag(PointerEventData eventData)
        {
           
        }

        public void OnDrag(PointerEventData eventData)
        {
            
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            
        }
    }
}