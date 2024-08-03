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
            GameObject subViewGo = Object.Instantiate(Resources.Load<GameObject>(subViewName)); //todo 通过资源管理器加载
            if (subViewGo == null)
            {
                Debug.LogError($"{subViewName}子界面资源实例化失败");
                return null;
            }
            subViewGo.transform.SetParent(trans, false);
            subViewGo.ResetLocal();
            subView.InternalCreate(subViewGo);
            subView.InternalShow();
            return subView;
        }

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
            foreach (var subView in SubViews.Values)
            {
                subView.OnShow();
            }
        }

        /// <summary>
        /// 界面每次打开时调用
        /// </summary>
        protected virtual void OnRefresh()
        {
            foreach (var subView in SubViews.Values)
            {
                subView.OnRefresh();
            }
        }

        protected virtual void OnUpdate()
        {
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
            foreach (var subView in SubViews.Values)
            {
                subView.OnClose();
            }
        }

        protected virtual void OnDestroy()
        {
            foreach (var subView in SubViews.Values)
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