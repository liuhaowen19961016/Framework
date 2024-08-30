using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Framework
{
    /// <summary>
    /// 所有UI基类
    /// </summary>
    public abstract class UIBase
    {
        public UIViewBase UIViewHolder; //属于哪个界面

        protected UIBase Parent; //父对象

        protected GameObject go; //自身GameObject
        public GameObject Go => go;

        protected object ViewData;

        public List<UIWidgetBase> Widgets = new List<UIWidgetBase>(); //所有控件

        #region 控件

        /// <summary>
        /// 添加控件
        /// </summary>
        /// reusable：为true则通过对象池管理
        public T AddUIWidget<T>(Transform trans, bool reusable, object viewData = null)
            where T : UIWidgetBase
        {
            Type type = typeof(T);
            string widgetName = type.Name;
            var classType = Type.GetType(widgetName);
            T widget = Activator.CreateInstance(classType) as T;
            if (widget == null)
                return null;

            widget.InternalInit(this, widgetName, reusable, viewData);
            bool createRet = widget.InternalCreate(trans);
            if (!createRet)
            {
                return null;
            }
            widget.InternalShow();
            return widget;
        }

        /// <summary>
        /// 移除控件
        /// </summary>
        public bool RemoveUIWidget(UIWidgetBase widget)
        {
            for (int i = 0; i < Widgets.Count; i++)
            {
                if (Widgets[i] == widget)
                {
                    widget.OnClose();
                    widget.InternalDestory();
                    Widgets.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        #endregion 控件

        #region 生命周期

        protected virtual void OnInit(object viewData)
        {
            ViewData = viewData;
        }

        protected virtual void OnCreate()
        {
            BindComponent();
            RegisterUIEvent();
            RegisterGameEvent();
        }

        protected virtual void OnOpen()
        {
            for (int i = 0, len = Widgets.Count; i < len; i++)
            {
                var widget = Widgets[i];
                if (widget == null)
                    continue;
                widget.OnOpen();
            }
        }

        protected virtual void OnRefresh()
        {
            for (int i = 0, len = Widgets.Count; i < len; i++)
            {
                var widget = Widgets[i];
                if (widget == null)
                    continue;
                widget.OnRefresh();
            }
        }

        protected virtual void OnUpdate()
        {
            for (int i = 0, len = Widgets.Count; i < len; i++)
            {
                var widget = Widgets[i];
                if (widget == null)
                    continue;
                widget.OnUpdate();
            }
        }

        /// <summary>
        /// 关闭界面时调用（无论是否销毁）
        /// </summary>
        protected virtual void OnClose()
        {
            for (int i = 0, len = Widgets.Count; i < len; i++)
            {
                var widget = Widgets[i];
                if (widget == null)
                    continue;
                widget.OnClose();
            }
        }

        protected virtual void OnDestroy()
        {
            for (int i = 0, len = Widgets.Count; i < len; i++)
            {
                var widget = Widgets[i];
                if (widget == null)
                    continue;
                widget.OnDestroy();
            }
            Widgets.Clear();
        }

        protected virtual void BindComponent()
        {
        }

        protected virtual void RegisterUIEvent()
        {
        }

        protected virtual void RegisterGameEvent()
        {
        }

        #endregion 生命周期
    }
}