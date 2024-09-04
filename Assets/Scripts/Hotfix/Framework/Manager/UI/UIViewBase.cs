using System;
using Hotfix;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Object = UnityEngine.Object;

namespace Framework
{
    /// <summary>
    /// 界面动画类型
    /// </summary>
    public enum EViewAniType
    {
        NoAni = 1, //不播放动画
        Tween, //通过Tween控制
        Animator, //通过Animator控制
    }

    /// <summary>
    /// UI界面基类
    /// </summary>
    public abstract class UIViewBase : UIViewOrUISubViewBase
    {
        public UIViewConfig UIViewCfg { get; private set; } //UIView表
        public string ViewName { get; private set; } //界面名字
        public UILayer UILayer { get; private set; } //UILayer
        public int ViewId => UIViewCfg.Id; //界面id
        public EUILayerType LayerType => (EUILayerType)UIViewCfg.LayerType; //层级类型
        public EUIType Type => (EUIType)UIViewCfg.Type; //界面类型

        private Canvas canvas; //当前界面的Canvas
        private Canvas[] childCanvas; //当前界面下的所有子Canvas
        private int[] childCanvasOriginSortingOrder;

        private RectTransform viewRootRect; //界面Root根节点，可以没有（控制动画，适配）

        protected EViewAniType viewAniType = EViewAniType.Tween; //界面动画类型（之后可以改成读表配置）
        public const string OPEN_ANI_NAME = "Open";
        public const string CLOSE_ANI_NAME = "Close";

        public int OrderInLayer //层级排序
        {
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

        #region Callback

        protected virtual void OnOpenAniComplete()
        {
        }

        protected virtual void OnCloseAniComplete()
        {
        }

        #endregion Callback

        protected void Close(bool isDestroy = true, Action onComplete = null)
        {
            GameGlobal.UIMgr.Close(ViewId, isDestroy, onComplete);
        }

        private Sequence openAniSeq;
        private Sequence closeAniSeq;
        private void PlayAni(bool isOpen, Action onComplete = null)
        {
            if (viewAniType == EViewAniType.NoAni
                || (viewAniType == EViewAniType.Animator && viewRootRect.GetComponent<Animator>() == null)
                || viewRootRect == null)
            {
                onComplete?.Invoke();
                return;
            }

            switch (viewAniType)
            {
                case EViewAniType.Tween:
                    if (isOpen)
                    {
                        openAniSeq = DOTween.Sequence();
                        viewRootRect.localScale = Vector3.zero;
                        Tween tween1 = viewRootRect.DOScale(Vector3.one * 1.1f, 0.16f);
                        Tween tween2 = viewRootRect.DOScale(Vector3.one * 1f, 0.08f);
                        openAniSeq.Append(tween1);
                        openAniSeq.Append(tween2);
                        openAniSeq.SetUpdate(true);
                        openAniSeq.OnComplete(() => { onComplete?.Invoke(); });
                    }
                    else
                    {
                        closeAniSeq = DOTween.Sequence();
                        Tween tween1 = viewRootRect.DOScale(Vector3.one * 1.1f, 0.08f);
                        Tween tween2 = viewRootRect.DOScale(Vector3.one * 0, 0.08f);
                        closeAniSeq.Append(tween1);
                        closeAniSeq.Append(tween2);
                        closeAniSeq.SetUpdate(true);
                        closeAniSeq.OnComplete(() => { onComplete?.Invoke(); });
                    }
                    break;

                case EViewAniType.Animator:
                    var ani = viewRootRect.GetComponent<Animator>();
                    //todo 
                    // ani.Play(isOpen ? OPEN_ANI_NAME : CLOSE_ANI_NAME);
                    break;
            }
        }

        private void PlayAudio(bool isOpen)
        {
        }

        public void InternalInit(string viewName, UIViewConfig uiViewCfg, UILayer uiLayer, object viewData = null)
        {
            ViewData = viewData;
            UIViewCfg = uiViewCfg;
            UILayer = uiLayer;
            ViewName = viewName;
            UIViewHolder = this;
            Parent = null;

            OnInit(viewData);
        }

        public bool InternalCreate(Transform trans)
        {
            GameObject viewGo = Object.Instantiate(Resources.Load<GameObject>(ViewName)); //todo 通过资源管理器加载
            if (viewGo == null)
            {
                Debug.LogError($"{ViewName}界面资源实例化失败");
                return false;
            }
            viewGo.transform.SetParent(trans, false);

            GO = viewGo;
            viewRootRect = GO.transform.Find("Root")?.GetComponent<RectTransform>();
            canvas = GO.GetComponent<Canvas>(true);
            canvas.overrideSorting = true;
            childCanvas = GO.GetComponentsInChildren<Canvas>(true);
            childCanvasOriginSortingOrder = new int[childCanvas.Length];
            for (int i = 0; i < childCanvasOriginSortingOrder.Length; i++)
            {
                childCanvasOriginSortingOrder[i] = childCanvas[i].sortingOrder;
            }
            GO.GetComponent<GraphicRaycaster>(true);

            OnCreate();
            return true;
        }

        public void InternalOpen()
        {
            openAniSeq?.Kill(true);
            closeAniSeq?.Kill(true);

            PlayAudio(true);
            PlayAni(true, () => { OnOpenAniComplete(); });

            OnOpen();
        }

        public void InternalShow()
        {
            OnShow();
        }

        public void InternalUpdate()
        {
            OnUpdate();
        }

        public void InternalClose(bool isDestroy = true, Action onComplete = null)
        {
            PlayAudio(false);
            PlayAni(false, () =>
            {
                openAniSeq?.Kill(true);
                closeAniSeq?.Kill(true);

                OnCloseAniComplete();
                OnClose();

                if (isDestroy)
                {
                    Object.Destroy(GO);
                    OnDestroy();
                }

                onComplete?.Invoke();
            });
        }
    }
}