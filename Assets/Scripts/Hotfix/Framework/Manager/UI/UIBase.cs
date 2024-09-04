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

        public GameObject GO { protected set; get; } //自身GameObject

        protected object ViewData;

        public bool Visible //是否可见
        {
            get
            {
                return GO.activeSelf;
            }
            set
            {
                GO.SetActive(value);
            }
        }

        private List<UIWidgetBase> WidgetList = new List<UIWidgetBase>(); //所有控件
        private List<UIWidgetBase> WidgetList_Temp = new List<UIWidgetBase>();

        #region 控件

        public void InternalAddToWidgetList(UIWidgetBase widget)
        {
            WidgetList.Add(widget);
        }

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
            widget.InternalOpen();
            return widget;
        }

        /// <summary>
        /// 移除控件
        /// </summary>
        public bool RemoveUIWidget(UIWidgetBase widget)
        {
            foreach (var temp in WidgetList)
            {
                if (temp == widget)
                {
                    widget.InternalClose();
                    widget.InternalDestroy();
                    WidgetList.Remove(widget);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 移除所有控件
        /// </summary>
        public void RemoveAllUIWidget()
        {
            foreach (var widget in WidgetList)
            {
                widget.InternalClose();
                widget.InternalDestroy();
            }
            WidgetList.Clear();
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
            Visible = true;
            WidgetList.CopyListNonAlloc(WidgetList_Temp);
            foreach (var widget in WidgetList_Temp)
            {
                widget.OnOpen();
            }
        }

        protected virtual void OnShow()
        {
            Visible = true;
            WidgetList.CopyListNonAlloc(WidgetList_Temp);
            foreach (var widget in WidgetList_Temp)
            {
                widget.OnShow();
            }
        }

        protected virtual void OnUpdate()
        {
            WidgetList.CopyListNonAlloc(WidgetList_Temp);
            foreach (var widget in WidgetList_Temp)
            {
                widget.OnUpdate();
            }
        }

        /// <summary>
        /// 关闭界面时调用（无论是否销毁）
        /// </summary>
        protected virtual void OnClose()
        {
            Visible = false;
            WidgetList.CopyListNonAlloc(WidgetList_Temp);
            foreach (var widget in WidgetList_Temp)
            {
                widget.OnClose();
            }
        }

        protected virtual void OnDestroy()
        {
            WidgetList.CopyListNonAlloc(WidgetList_Temp);
            foreach (var widget in WidgetList_Temp)
            {
                widget.InternalDestroy();
            }
            WidgetList.Clear();
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