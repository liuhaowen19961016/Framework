using UnityEngine;

namespace Framework
{
    /// <summary>
    /// UI控件基类
    /// </summary>
    public class UIWidgetBase : UIBase
    {
        private string widgetName; //控件名字

        private bool reusable;

        public void InternalInit(UIBase parent, string widgetName, bool reusable, object viewData = null)
        {
            this.viewData = viewData;
            this.widgetName = widgetName;
            this.reusable = reusable;
            this.parent = parent;
            uiViewHolder = parent.uiViewHolder;
            parent.Widgets.Add(this);
            OnInit(viewData);
        }

        public bool InternalCreate(Transform trans)
        {
            GameObject widgetGo = null;
            if (reusable)
            {
                //todo 通过对象池管理
            }
            else
            {
                widgetGo = Object.Instantiate(Resources.Load<GameObject>(widgetName)); //todo 通过资源管理器加载
            }
            if (widgetGo == null)
            {
                Debug.LogError($"{widgetName}控件资源实例化失败");
                return false;
            }
            widgetGo.transform.SetParent(trans, false);
            widgetGo.ResetLocal();
            go = widgetGo;
            OnCreate();
            return true;
        }

        public void InternalShow()
        {
            OnShow();
        }

        public void InternalDestory()
        {
            if (reusable)
            {
                //todo 通过对象池管理
            }
            else
            {
                OnDestroy();
                Object.Destroy(go);
            }
        }
    }
}