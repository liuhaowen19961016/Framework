using Hotfix;
using UnityEngine;
using UnityEngine.UI;

namespace Framework
{
    /// <summary>
    /// UI界面基类
    /// </summary>
    public class UIViewBase : UIBase
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

        public UIViewBase(GameObject go, UIViewInfo uiViewInfo)
        {
            this.go = go;
            viewName = uiViewInfo.viewName;
            layerType = uiViewInfo.layerType;
            type = uiViewInfo.type;
        }

        public void InternalInit(object viewData)
        {
            this.viewData = viewData;

            go.transform.SetParent(GameGlobal.UIMgr.FindLayer(layerType).LayerGo.transform, false);

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

            OnInit(viewData);
        }

        public void InternalShow()
        {
            PlayAudio(true);
            PlayAni(true);

            OnShow();

            //todo 发送打开界面事件
        }

        public void InternalClose(bool destory)
        {
            OnClose();
            if (destory)
                OnDestroy();
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
            // var openAniName = GetOpenAniName();
            // if (!string.IsNullOrEmpty(openAniName))
            // {
            //     try
            //     {
            //         var openAudio = GetOpenAudioStr();
            //         if (!string.IsNullOrEmpty(openAudio))
            //         {
            //             GameGlobal.GetManager<SoundMgr>().PlaySfx(openAudio);
            //         }
            //     }
            //     catch (Exception e)
            //     {
            //         Log.Error(e.Message);
            //         Log.Error(e.StackTrace);
            //     }
            //   
            //     var animator = gameObject.GetComponent<Animator>();
            //     if (animator != null && animator.HasState(openAniName))
            //     {
            //         await TMUtility.PlayAnimationAsync(animator, openAniName,
            //             gameObject.GetCancellationTokenOnDestroy());
            //         OpenAniEndCallBack();
            //     }
            // }
        }

        private void PlayAudio(bool isOpen)
        {
        }
    }
}