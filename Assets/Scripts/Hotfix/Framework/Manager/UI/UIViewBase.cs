using System;
using Hotfix;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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

        private RectTransform rootRect; //界面Root根节点，控制动画，适配，可以没有

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

        public void InternalInit(string viewName, UIViewConfig uiViewCfg, object viewData = null)
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
            rootRect = go.transform.Find("Root")?.GetComponent<RectTransform>();
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
            showAniSeq?.Kill(true);
            closeAniSeq?.Kill(true);

            go.SetActive(true);

            PlayAudio(true);
            PlayAni(true, () => { OnAniComplete(true); });

            OnShow();
        }

        public void InternalRefresh()
        {
            go.SetActive(true);

            OnRefresh();
        }

        public void InternalUpdate()
        {
            OnUpdate();
        }

        public void InternalClose(bool destory = true)
        {
            OnClose();
            PlayAudio(false);
            PlayAni(false, () =>
            {
                go.SetActive(false);
                OnAniComplete(false);

                showAniSeq?.Kill(true);
                closeAniSeq?.Kill(true);

                if (destory)
                {
                    OnDestroy();
                }
            });
        }

        #region Callback

        protected virtual void OnAniComplete(bool isShow)
        {
        }

        #endregion Callback

        protected void Close(bool isDestory = true)
        {
            GameGlobal.UIMgr.Close(ViewId, isDestory);
        }

        private Sequence showAniSeq;
        private Sequence closeAniSeq;
        private void PlayAni(bool isShow, Action onComplete = null)
        {
            if (rootRect == null)
            {
                onComplete?.Invoke();
                return;
            }
            if (isShow)
            {
                showAniSeq = DOTween.Sequence();
                rootRect.localScale = Vector3.zero;
                Tween tween1 = rootRect.DOScale(Vector3.one * 1.1f, 0.16f);
                Tween tween2 = rootRect.DOScale(Vector3.one * 1f, 0.08f);
                showAniSeq.Append(tween1);
                showAniSeq.Append(tween2);
                showAniSeq.OnComplete(() => { onComplete?.Invoke(); });
            }
            else
            {
                closeAniSeq = DOTween.Sequence();
                Tween tween1 = rootRect.DOScale(Vector3.one * 1.1f, 0.08f);
                Tween tween2 = rootRect.DOScale(Vector3.one * 0, 0.08f);
                closeAniSeq.Append(tween1);
                closeAniSeq.Append(tween2);
                closeAniSeq.OnComplete(() => { onComplete?.Invoke(); });
            }
        }

        private void PlayAudio(bool isShow)
        {
        }
    }
}