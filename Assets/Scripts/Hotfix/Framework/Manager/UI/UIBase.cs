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
        public UIViewBase uiViewHolder; //属于哪个界面

        protected UIBase parent; //父对象

        protected GameObject go; //自身GameObject
        public GameObject Go => go;

        protected object viewData;

        public Dictionary<string, UISubViewBase> SubViews = new Dictionary<string, UISubViewBase>(); //所有子界面
        public List<UIWidgetBase> Widgets = new List<UIWidgetBase>(); //所有控件

        #region 子界面

        /// <summary>
        /// 添加子界面
        /// </summary>
        protected T AddUISubview<T>(Transform trans, object viewData = null)
            where T : UISubViewBase
        {
            Type type = typeof(T);
            string subViewName = type.Name;
            var classType = Type.GetType(subViewName);
            T subView = Activator.CreateInstance(classType) as T;
            if (subView == null)
                return null;

            subView.InternalInit(this, subViewName, viewData);
            bool createRet = subView.InternalCreate(trans);
            if (!createRet)
            {
                return null;
            }
            subView.InternalShow();
            return subView;
        }

        /// <summary>
        /// 移除子界面
        /// </summary>
        public bool RemoveUISubView<T>()
            where T : UISubViewBase
        {
            string subViewName = typeof(T).Name;
            if (SubViews.TryGetValue(subViewName, out var subView))
            {
                subView.InternalDestory();
                SubViews.Remove(subViewName);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 查找子界面
        /// </summary>
        public T FindUISubView<T>()
            where T : UISubViewBase
        {
            string subViewName = typeof(T).Name;
            if (SubViews.TryGetValue(subViewName, out var subView))
            {
                return subView as T;
            }
            return null;
        }

        public void RemoveAllUISubView()
        {
            foreach (var subView in SubViews.Values)
            {
                subView.InternalDestory();
            }
            SubViews.Clear();
        }

        #endregion 子界面

        #region 控件

        /// <summary>
        /// 添加控件
        /// </summary>
        /// reusable：为true则通过对象池管理
        protected T AddUIWidget<T>(Transform trans, bool reusable, object viewData = null)
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
            this.viewData = viewData;
        }

        protected virtual void OnCreate()
        {
            BindComponent();
            RegisterUIEvent();
            RegisterGameEvent();
        }

        protected virtual void OnShow()
        {
            foreach (var widget in Widgets)
            {
                widget.OnShow();
            }
            foreach (var subView in SubViews.Values)
            {
                subView.OnShow();
            }
        }

        protected virtual void OnRefresh()
        {
            foreach (var widget in Widgets)
            {
                widget.OnRefresh();
            }
            foreach (var subView in SubViews.Values)
            {
                subView.OnRefresh();
            }
        }

        protected virtual void OnUpdate()
        {
            foreach (var widget in Widgets)
            {
                widget.OnUpdate();
            }
            foreach (var subView in SubViews.Values)
            {
                subView.OnUpdate();
            }
        }

        /// <summary>
        /// 关闭界面时调用（无论是否销毁）
        /// </summary>
        protected virtual void OnClose()
        {
            foreach (var widget in Widgets)
            {
                widget.OnClose();
            }
            foreach (var subView in SubViews.Values)
            {
                subView.OnClose();
            }
        }

        protected virtual void OnDestroy()
        {
            foreach (var widget in Widgets)
            {
                widget.InternalDestory();
            }
            Widgets.Clear();

            foreach (var subView in SubViews.Values)
            {
                subView.OnDestroy();
            }
            SubViews.Clear();
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