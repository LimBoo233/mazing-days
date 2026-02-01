using System.Collections.Generic;
using Core;
using Core.UI;
using Features.Units.Core;
using Features.UI;
using Modules.Combat.Data.SO;
using UnityEngine;

namespace Features.UI.Panel
{
    public class CombatActionPanel:BasePanel
    {
        [SerializeField] private SkillItem skillItemPrefab;
        [SerializeField] private Transform contentRoot;

        private List<SkillItem> _activeItems = new List<SkillItem>();
        private Stack<SkillItem> _itemPool = new Stack<SkillItem>();
        private Unit _currentUnit;
        
        protected override void OnShow(params object[] args)
        {
            if (args.Length > 0 && args[0] is Unit unit)
            {
                Debug.Log("调用刷新技能面板");
                _currentUnit = unit;
                RefreshSkills();
            }
        }
       
        /// <summary>
        /// 切换角色时，刷新面板中的技能
        /// </summary>
        private void RefreshSkills()
        {
            if (_currentUnit == null)
                return;
            //1.将所有图标回收到对象池中
            ReturnAllItemToPool();
            //2.遍历角色的技能列表 如果有该技能就直接生成
            foreach (SkillDataSo skillData in _currentUnit.Data.Skills)
            {
                SkillItem item = GetItemFromPool();
                item.SetUp(skillData, OnSkillClicked);
                _activeItems.Add(item);
            }
        }

        private void OnSkillClicked(int skilledId)
        {
            GameManager.CombatManager.SelectedSkillId = skilledId;
            HideMe();
        }

        private void ReturnAllItemToPool()
        {
            foreach (var item in _activeItems)
            {
                item.gameObject.SetActive(false);
                _itemPool.Push(item);
            }
            _activeItems.Clear();
        }
        
        private SkillItem GetItemFromPool()
        {
            SkillItem item;
            if (_itemPool.Count > 0)
            {
                item = _itemPool.Pop();
                item.gameObject.SetActive(true);
            }
            else
            {
                item = Instantiate(skillItemPrefab, contentRoot);
            }
            return item;
        }
    }
}