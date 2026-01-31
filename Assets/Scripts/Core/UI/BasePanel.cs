using UnityEngine;
using UnityEngine.Events;

namespace Core.UI
{
    public abstract class BasePanel: MonoBehaviour
    {
        protected virtual void Awake()
        {
            
        }
        
        /// <summary>
        /// 显示面板
        /// </summary>
        /// <param name="onCompleted"></param>
        /// <param name="args">打开面板时传递的数据</param>
        public virtual void ShowMe(UnityAction onCompleted =null,params object[] args)
        {
            this.gameObject.SetActive(true);
            OnShow(args);
            onCompleted?.Invoke();
        }

        /// <summary>
        /// 隐藏面板
        /// </summary>
        /// <param name="onCompleted">隐藏面板事件回调函数</param>
        public virtual void HideMe(UnityAction onCompleted =null)
        {
            OnHide();
            this.gameObject.SetActive(false);
            onCompleted?.Invoke();
        }
        
        protected virtual void OnShow(params object[] args) { }
        
        protected virtual void OnHide() { }
    }
}