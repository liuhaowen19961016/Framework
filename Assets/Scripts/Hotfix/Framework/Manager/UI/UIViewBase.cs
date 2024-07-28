using UnityEngine;
using UnityEngine.UI;

namespace Framework
{
    /// <summary>
    /// UI界面基类
    /// </summary>
    public abstract class UIViewBase : UIBase
    {
        private EUILayerType layerType;
        public EUILayerType LayerType => layerType;

        private EUIType type;
        private EUIType Type => type;

        private string viewName;
        public string ViewName => viewName;

        private Canvas canvas; //当前界面的Canvas
        private Canvas[] childCanvas; //当前界面下的所有子Canvas
        private int[] childCanvasOriginSortingOrder;
        private GraphicRaycaster raycaster; //当前界面的UI射线检测组件
        private GraphicRaycaster[] childRaycaster; //当前界面下的所有子UI射线检测组件

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

        public bool Interactable //可交互性
        {
            get
            {
                if (raycaster == null)
                    return false;
                return raycaster.enabled;
            }
            set
            {
                if (raycaster == null)
                    return;
                raycaster.enabled = value;
                for (int i = 0; i < childRaycaster.Length; i++)
                {
                    childRaycaster[i].enabled = value;
                }
                OnSetInteractable(value);
            }
        }

        public void InternalInit(UIViewInfo uiViewInfo, object viewData)
        {
            viewName = uiViewInfo.viewName;
            layerType = uiViewInfo.layerType;
            type = uiViewInfo.type;
            this.viewData = viewData;
            OnInit(viewData);
        }

        public void InternalCreate(GameObject go)
        {
            this.go = go;
            canvas = go.GetComponent<Canvas>(true);
            canvas.overrideSorting = true;
            childCanvas = go.GetComponentsInChildren<Canvas>(true);
            childCanvasOriginSortingOrder = new int[childCanvas.Length];
            for (int i = 0; i < childCanvasOriginSortingOrder.Length; i++)
            {
                childCanvasOriginSortingOrder[i] = childCanvas[i].sortingOrder;
            }

            raycaster = go.GetComponent<GraphicRaycaster>(true);
            childRaycaster = go.GetComponentsInChildren<GraphicRaycaster>(true);
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

        /// <summary>
        /// 设置界面的可交互性回调
        /// </summary>
        public virtual void OnSetInteractable(bool b)
        {
        }

        #endregion Callback

        private void PlayAni(bool isOpen)
        {
        }

        private void PlayAudio(bool isOpen)
        {
        }
    }
}