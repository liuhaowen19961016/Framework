using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public abstract class UIBase
    {
        protected GameObject go;
        public GameObject Go => go;

        protected object viewData;

        private Dictionary<string, UISubViewBase> subViews = new Dictionary<string, UISubViewBase>(); //所有子界面
        private HashSet<string> subViewNameList = new HashSet<string>();

        #region 生命周期

        protected virtual void OnInit(object viewData)
        {
            this.viewData = viewData;
            BindComponent();
            RegisterUIEvent();
            RegisterGameEvent();

            foreach (var subViewName in subViewNameList)
            {
                var subView = ReflectUtils.Create(subViewName) as UISubViewBase;
                subViews.Add(subViewName, subView);
                subView.InternalInit(subViewName, viewData);
            }
        }

        protected virtual void OnCreate()
        {
            foreach (var subView in subViews.Values)
            {
                subView.InternalCreate(go);
            }
        }

        protected virtual void OnShow()
        {
            foreach (var subView in subViews.Values)
            {
                subView.OnShow();
            }
        }

        /// <summary>
        /// 界面每次打开时调用
        /// </summary>
        protected virtual void OnRefresh()
        {
            foreach (var subView in subViews.Values)
            {
                subView.OnRefresh();
            }
        }

        /// <summary>
        /// 关闭界面时调用（无论是否销毁）
        /// </summary>
        protected virtual void OnClose()
        {
            foreach (var subView in subViews.Values)
            {
                subView.OnClose();
            }
        }

        protected virtual void OnDestroy()
        {
            foreach (var subView in subViews.Values)
            {
                subView.OnDestroy();
            }
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