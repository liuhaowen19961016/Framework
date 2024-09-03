using UnityEngine;

namespace Framework
{
    /// <summary>
    /// UI控件基类
    /// </summary>
    public class UIWidgetBase : UIBase
    {
        public string WidgetName { get; private set; } //控件名字

        private bool reusable;

        public void SetViewData(object viewData = null)
        {
            ViewData = viewData;
        }

        public void InternalInit(UIBase parent, string widgetName, bool reusable, object viewData = null)
        {
            ViewData = viewData;
            WidgetName = widgetName;
            this.reusable = reusable;
            Parent = parent;
            UIViewHolder = parent.UIViewHolder;
            parent.InternalAddToWidgetList(this);
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
                widgetGo = Object.Instantiate(Resources.Load<GameObject>(WidgetName)); //todo 通过资源管理器加载
            }
            if (widgetGo == null)
            {
                Debug.LogError($"{WidgetName}控件资源实例化失败");
                return false;
            }
            widgetGo.transform.SetParent(trans, false);
            GO = widgetGo;
            OnCreate();
            return true;
        }

        public void InternalOpen()
        {
            OnOpen();
        }

        public void InternalClose()
        {
            OnClose();
        }

        public void InternalDestroy()
        {
            if (reusable)
            {
                //todo 通过对象池管理
            }
            else
            {
                Object.Destroy(GO);
                OnDestroy();
            }
        }
    }
}