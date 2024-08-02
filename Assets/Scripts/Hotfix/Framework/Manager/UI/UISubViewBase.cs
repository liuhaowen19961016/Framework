using UnityEngine;

namespace Framework
{
    /// <summary>
    /// UI界面的子界面基类
    /// </summary>
    public abstract class UISubViewBase : UIBase
    {
        private string subViewName; //子界面名字

        public void InternalInit(UIBase parent, string subViewName, object viewData)
        {
            this.viewData = viewData;
            this.subViewName = subViewName;
            this.parent = parent;
            uiViewHolder = parent.uiViewHolder;
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
    }
}