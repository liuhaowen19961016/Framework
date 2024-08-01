using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Framework
{
    public abstract class UIBase
    {
        public UIViewBase uiViewHolder; //属于哪个界面

        protected UIBase parent; //父对象

        protected GameObject go; //自身GameObject
        public GameObject Go => go;

        protected object viewData;

        private Dictionary<string, UISubViewBase> subViews = new Dictionary<string, UISubViewBase>(); //所有子界面

        /// <summary>
        /// 添加子界面
        /// </summary>
        protected UISubViewBase AddUISubview(int uiSubViewId, Transform trans, object viewData = null)
        {
            //todo 通过读表获取UIViewCfg
            if (!UISubviewTemp.UISubViewConfigs.TryGetValue(uiSubViewId, out UISubViewConfig uiSubViewCfg))
            {
                Debug.LogError($"UISubView表中没有配置Id为{uiSubViewId}的界面");
                return null;
            }
            string subViewName = Path.GetFileName(uiSubViewCfg.Path);
            var classType = Type.GetType(subViewName);
            if (classType == null)
            {
                Debug.LogError($"脚本绑定{subViewName}子界面失败，请先生成子界面脚本");
                return null;
            }
            UISubViewBase subView = Activator.CreateInstance(classType) as UISubViewBase;
            if (subView == null)
                return null;

            subView.InternalInit(this, subViewName, uiSubViewCfg, viewData);
            subViews.Add(subViewName, subView);
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