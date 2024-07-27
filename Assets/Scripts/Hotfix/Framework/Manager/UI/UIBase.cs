using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public class UIBase
    {
        protected GameObject go;
        public GameObject Go => go;

        protected object viewData;

        private List<UIBase> childs = new List<UIBase>();

        #region 生命周期

        /// <summary>
        /// 打开界面时最先调用
        /// </summary>
        protected virtual void OnInit(object viewData)
        {
            BindComponent();
            RegisterUIEvent();
            RegisterGameEvent();
            viewData = this.viewData;
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

        /// <summary>
        /// 界面第一次打开时调用
        /// </summary>
        protected virtual void OnShow()
        {
            go.SetActive(true);
            foreach (var child in childs)
            {
                child.OnShow();
            }
        }

        /// <summary>
        /// 界面每次打开时调用
        /// </summary>
        protected virtual void OnRefresh()
        {
            foreach (var child in childs)
            {
                child.OnRefresh();
            }
        }

        /// <summary>
        /// 关闭界面时调用（无论是否Destory）
        /// </summary>
        protected virtual void OnClose()
        {
            go.SetActive(false);
            foreach (var child in childs)
            {
                child.OnClose();
            }
        }

        protected virtual void OnDestroy()
        {
            foreach (var child in childs)
            {
                child.OnDestroy();
            }
        }

        #endregion 生命周期
    }
}