using UnityEngine;

namespace Framework
{
    /// <summary>
    /// UI界面的子界面基类
    /// </summary>
    public abstract class UISubViewBase : UIViewOrUISubViewBase
    {
        public string SubViewName { get; private set; } //子界面名字

        public void InternalInit(UIViewOrUISubViewBase parent, string subViewName, object viewData = null)
        {
            ViewData = viewData;
            SubViewName = subViewName;
            Parent = parent;
            UIViewHolder = parent.UIViewHolder;
            parent.InternalAddToSubViewList(this);
            OnInit(viewData);
        }

        public bool InternalCreate(Transform trans)
        {
            GameObject subViewGo = Object.Instantiate(Resources.Load<GameObject>(SubViewName)); //todo 通过资源管理器加载
            if (subViewGo == null)
            {
                Debug.LogError($"{SubViewName}子界面资源实例化失败");
                return false;
            }
            subViewGo.transform.SetParent(trans, false);
            subViewGo.ResetLocal();
            GameObject = subViewGo;
            OnCreate();
            return true;
        }

        public void InternalCreateWithoutInstantiate(GameObject go)
        {
            GameObject = go;
            OnCreate();
        }

        public void InternalOpen()
        {
            OnOpen();
        }

        public void InternalClose(bool isDestroy)
        {
            OnClose();
            if (isDestroy)
            {
                Object.Destroy(GameObject);
                OnDestroy();
            }
        }
    }
}