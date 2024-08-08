using UnityEngine;

namespace Framework
{
    /// <summary>
    /// UI界面的子界面基类
    /// </summary>
    public abstract class UISubViewBase : UIBase
    {
        private string subViewName; //子界面名字

        public void InternalInit(UIBase parent, string subViewName, object viewData = null)
        {
            this.viewData = viewData;
            this.subViewName = subViewName;
            this.parent = parent;
            uiViewHolder = parent.uiViewHolder;
            parent.SubViews.Add(this);
            OnInit(viewData);
        }

        public void InternalCreate(GameObject go)
        {
            this.go = go;
            OnCreate();
        }

        public void InternalShow()
        {
            OnShow();
        }

        public void InternalDestory()
        {
            OnClose();
            OnDestroy();
            Object.Destroy(go);
        }
    }
}