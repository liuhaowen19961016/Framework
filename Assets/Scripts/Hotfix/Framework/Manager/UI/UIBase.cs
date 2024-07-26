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
        public virtual void OnInit(object viewData)
        {
        }

        public virtual void RegisterUIEvent()
        {
        }

        public virtual void RegisterGameEvent()
        {
        }

        /// <summary>
        /// 界面第一次打开时调用
        /// </summary>
        public virtual void OnShow()
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
        public virtual void OnRefresh()
        {
            foreach (var child in childs)
            {
                child.OnRefresh();
            }
        }

        /// <summary>
        /// 关闭界面时调用（无论是否Destory）
        /// </summary>
        public virtual void OnClose()
        {
            go.SetActive(false);
            foreach (var child in childs)
            {
                child.OnClose();
            }
        }

        public virtual void OnDestroy()
        {
            foreach (var child in childs)
            {
                child.OnDestroy();
            }
        }

        #endregion 生命周期
    }
}