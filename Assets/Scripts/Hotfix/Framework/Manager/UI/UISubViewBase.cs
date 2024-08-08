using UnityEngine;

namespace Framework
{
    /// <summary>
    /// UI界面的子界面基类
    /// </summary>
    public abstract class UISubViewBase : UIBase
    {
        private string subViewName; //子界面名字

        public void InternalInit(UIBase parent, string subViewName, object viewData = null)
        {
            this.viewData = viewData;
            this.subViewName = subViewName;
            this.parent = parent;
            uiViewHolder = parent.uiViewHolder;
            parent.SubViews.Add(this.subViewName, this);
            OnInit(viewData);
        }

        public bool InternalCreate(Transform trans)
        {
            GameObject subViewGo = Object.Instantiate(Resources.Load<GameObject>(subViewName)); //todo 通过资源管理器加载
            if (subViewGo == null)
            {
                Debug.LogError($"{subViewName}子界面资源实例化失败");
                return false;
            }
            subViewGo.transform.SetParent(trans, false);
            subViewGo.ResetLocal();
            go = subViewGo;
            OnCreate();
            return true;
        }

        public void InternalCreateWithoutInstantiate(GameObject go)
        {
            this.go = go;
            OnCreate();
        }

        public void InternalShow()
        {
            OnShow();
        }

        public void InternalDestory()
        {
            OnClose();
            OnDestroy();
            Object.Destroy(go);
        }
    }
}