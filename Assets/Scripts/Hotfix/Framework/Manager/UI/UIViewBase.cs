using UnityEngine;
using UnityEngine.UI;

namespace Framework
{
    /// <summary>
    /// UI界面基类
    /// </summary>
    public abstract class UIViewBase : UIBase
    {
        private UIViewConfig uiViewCfg; //UIView表

        private string viewName; //界面名字

        public int ViewId => uiViewCfg.Id; //界面id
        public EUILayerType LayerType => (EUILayerType)uiViewCfg.LayerType; //层级类型
        private EUIType Type => (EUIType)uiViewCfg.Type; //类型

        private Canvas canvas; //当前界面的Canvas
        private Canvas[] childCanvas; //当前界面下的所有子Canvas
        private int[] childCanvasOriginSortingOrder;

        public int OrderInLayer //排序顺序
        {
            get
            {
                if (canvas == null)
                    return 0;
                return canvas.sortingOrder;
            }
            set
            {
                if (canvas != null)
                {
                    if (canvas.sortingOrder == value)
                        return;

                    canvas.sortingOrder = value;
                    int v = value;
                    for (int i = 0; i < childCanvas.Length; i++)
                    {
                        var canvas = childCanvas[i];
                        if (canvas != this.canvas)
                        {
                            canvas.sortingOrder = childCanvasOriginSortingOrder[i] + v;
                        }
                    }
                }
            }
        }

        public void InternalInit(string viewName, UIViewConfig uiViewCfg, object viewData)
        {
            this.viewData = viewData;
            this.uiViewCfg = uiViewCfg;
            this.viewName = viewName;
            uiViewHolder = this;
            parent = this;

            OnInit(viewData);
        }

        public void InternalCreate(GameObject go)
        {
            this.go = go;
            canvas = go.GetComponent<Canvas>(true);
            go.GetComponent<GraphicRaycaster>(true);
            canvas.overrideSorting = true;
            childCanvas = go.GetComponentsInChildren<Canvas>(true);
            childCanvasOriginSortingOrder = new int[childCanvas.Length];
            for (int i = 0; i < childCanvasOriginSortingOrder.Length; i++)
            {
                childCanvasOriginSortingOrder[i] = childCanvas[i].sortingOrder;
            }

            OnCreate();
        }

        public void InternalShow()
        {
            go.SetActive(true);

            PlayAudio(true);
            PlayAni(true);

            OnShow();
        }

        public void InternalClose(bool destory = true)
        {
            go.SetActive(false);

            OnClose();

            if (destory)
            {
                OnDestroy();
            }
        }

        public void InternalRefresh()
        {
            OnRefresh();
        }

        #region Callback

        #endregion Callback

        protected void Close(bool isDestory)
        {
            
        }
        
        private void PlayAni(bool isOpen)
        {
        }

        private void PlayAudio(bool isOpen)
        {
        }
    }
}