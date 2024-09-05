using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Framework
{
    /// <summary>
    /// UI层级类型
    /// </summary>
    public enum EUILayerType
    {
        Hud = 0,
        Window = 1,
        Pop = 2,
        Guide = 3,
        Top = 4,
        OverTop = 5,
    }

    /// <summary>
    /// UI类型
    /// </summary>
    public enum EUIType
    {
        Main = 1, //主界面
        FullScreen, //全屏界面
        NotFullScreen, //非全屏界面
        Popup, //弹窗
        System, //系统
    }

    public class UIMgr
    {
        //根据项目进行调整
        public static Vector2 ReferenceResolution = new Vector2(768, 1366);
        public const float ScreenMatchValue = 0;

        public const int LAYERSTEP_ORDERINLAYER = 2000;
        public const int VIEWSTEP_ORDERINLAYER = 100;

        private GameObject uiRootGo; //UIRoot物体
        public Canvas UICanvas { get; private set; } //UI画布
        public Camera UICamera { get; private set; } //UI相机
        public RectTransform UIRect => UICanvas.GetComponent<RectTransform>(); //UIRect

        private List<UIViewBase> viewStack = new List<UIViewBase>();
        private List<UIViewBase> viewStack_Temp = new List<UIViewBase>();
        private Dictionary<EUILayerType, UILayer> layerType2Layer = new Dictionary<EUILayerType, UILayer>();

        /// <summary>
        /// 同步打开界面
        /// </summary>
        public UIViewBase OpenSync(int viewId, object viewData = null)
        {
            var curView = FindViewFirst(viewId);
            if (curView != null && !curView.UIViewCfg.DisplayMultiple)
            {
                Pop(curView);
                Push(curView);
                if (!curView.Visible)
                {
                    curView.InternalOpen();
                }
                else
                {
                    curView.InternalShow();
                }
                return curView;
            }
            else
            {
                //todo 通过读表获取UIViewCfg
                if (!UIViewTemp.UIViewConfigs.TryGetValue(viewId, out UIViewConfig uiViewCfg))
                {
                    Debug.LogError($"UIView表中没有配置Id为{viewId}的界面");
                    return null;
                }
                string viewName = Path.GetFileName(uiViewCfg.Path);
                var uiLayer = FindLayer((EUILayerType)uiViewCfg.LayerType);
                if (uiLayer == null)
                    return null;
                var classType = Type.GetType(viewName);
                if (classType == null)
                {
                    Debug.LogError($"没有界面类{viewName}，请先生成界面类");
                    return null;
                }
                UIViewBase view = Activator.CreateInstance(classType) as UIViewBase;
                if (view == null)
                {
                    Debug.LogError($"类{viewName}不是继承UIViewBase的界面类");
                    return null;
                }

                view.InternalInit(viewName, uiViewCfg, uiLayer, viewData);
                bool createRet = view.InternalCreate(uiLayer.LayerGo.transform);
                if (!createRet)
                    return null;
                Push(view);
                view.InternalOpen();
                return view;
            }
        }

        public void Close(int viewId, bool isDestroy = true, Action onComplete = null)
        {
            var viewList = FindView(viewId);
            foreach (var view in viewList)
            {
                Close(view, isDestroy, onComplete);
            }
        }

        public bool Close(UIViewBase view, bool isDestroy = true, Action onComplete = null)
        {
            if (view == null)
                return false;
            view.InternalClose(isDestroy, onComplete);
            if (isDestroy)
            {
                Pop(view);
            }
            return true;
        }

        /// <summary>
        /// 关闭所有界面
        /// </summary>
        public void CloseAll(bool isDestroy, List<int> ignoreViewList = null)
        {
            List<UIViewBase> copyList = new List<UIViewBase>();
            viewStack.CopyListNonAlloc(copyList);
            foreach (var view in copyList)
            {
                if (ignoreViewList != null && ignoreViewList.Contains(view.ViewId))
                    continue;
                Close(view, isDestroy);
            }
        }

        /// <summary>
        /// 查找界面（同一个id的界面可以打开多个，所以返回值是一个列表）
        /// </summary>
        public List<UIViewBase> FindView(int viewId)
        {
            List<UIViewBase> viewList = new List<UIViewBase>();
            foreach (var uiView in viewStack)
            {
                if (uiView.ViewId == viewId)
                    viewList.Add(uiView);
            }
            return viewList;
        }

        /// <summary>
        /// 查找界面（最先打开的）
        /// </summary>
        public UIViewBase FindViewFirst(int viewId)
        {
            foreach (var uiView in viewStack)
            {
                if (uiView.ViewId == viewId)
                    return uiView;
            }
            return null;
        }

        /// <summary>
        /// 查找某一层级下的最顶部界面
        /// </summary>
        public UIViewBase FindTopView(EUILayerType layerType, List<int> ignoreViewList = null)
        {
            var layer = FindLayer(layerType);
            var view = layer.GetTopView(ignoreViewList);
            return view;
        }

        public UIViewBase FindTopView(List<int> ignoreViewList = null)
        {
            if (viewStack.Count <= 0)
                return null;

            for (int i = viewStack.Count - 1; i >= 0; i--)
            {
                var view = viewStack[i];
                if (!view.Visible ||
                    (ignoreViewList != null && ignoreViewList.Contains(view.ViewId)))
                    continue;
                return view;
            }
            return null;
        }

        #region private

        /// <summary>
        /// 查找层级
        /// </summary>
        private UILayer FindLayer(EUILayerType layerType)
        {
            if (!layerType2Layer.TryGetValue(layerType, out var layer))
            {
                Debug.LogError($"没有找到{layerType}层");
                return null;
            }
            return layer;
        }

        private void Push(UIViewBase view)
        {
            var layer = FindLayer(view.LayerType);
            layer.AddView(view);
            viewStack.Add(view);
        }

        private void Pop(UIViewBase view)
        {
            var layer = FindLayer(view.LayerType);
            layer.RemoveView(view);
            viewStack.Remove(view);
        }

        #region 创建UI结构

        private void CreateUIStructure()
        {
            uiRootGo = GameUtils.CreateGameObject("UIRoot", null);
            Object.DontDestroyOnLoad(uiRootGo);
            CreateUICamera();
            CreateUICanvas();
        }

        private void CreateUICamera()
        {
            GameObject uiCameraGo = GameUtils.CreateGameObject("UICamera", uiRootGo.transform, false, typeof(Camera));
            UICamera = uiCameraGo.GetComponent<Camera>();
            UICamera.orthographic = true;
            UICamera.cullingMask = LayerMask.GetMask("UI");
        }

        private void CreateUICanvas()
        {
            GameObject uiCanvasGo = GameUtils.CreateGameObject("UICanvas", uiRootGo.transform, false,
                typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            uiCanvasGo.GetComponent<GraphicRaycaster>();
            uiCanvasGo.layer = LayerMask.NameToLayer("UI");
            UICanvas = uiCanvasGo.GetComponent<Canvas>();
            UICanvas.renderMode = RenderMode.ScreenSpaceCamera;
            UICanvas.worldCamera = UICamera;
            CanvasScaler canvasScaler = uiCanvasGo.GetComponent<CanvasScaler>();
            canvasScaler.matchWidthOrHeight = ScreenMatchValue;
            canvasScaler.referenceResolution = ReferenceResolution;
            //创建Canvas下的层级结构
            string[] uiLayerNameArray = Enum.GetNames(typeof(EUILayerType));
            foreach (var layerName in uiLayerNameArray)
            {
                EUILayerType uiLayerType = (EUILayerType)Enum.Parse(typeof(EUILayerType), layerName);
                GameObject layerRootGo = GameUtils.CreateGameObject(layerName, uiCanvasGo.transform, false, typeof(RectTransform));
                RectTransform rect = layerRootGo.GetComponent<RectTransform>();
                rect.anchoredPosition = Vector2.zero;
                rect.anchorMin = Vector2.zero;
                rect.anchorMax = Vector2.one;
                rect.sizeDelta = Vector2.zero;
                UILayer layer = new UILayer();
                layer.Init(layerRootGo, uiLayerType);
                layerType2Layer.Add(uiLayerType, layer);
            }
        }

        #endregion 创建UI结构

        #endregion Private

        #region 生命周期

        public void Init()
        {
            //创建UI结构
            CreateUIStructure();
        }

        public void Start()
        {
        }

        public void Update()
        {
            viewStack.CopyListNonAlloc(viewStack_Temp);
            foreach (var view in viewStack_Temp)
            {
                view.InternalUpdate();
            }
        }

        public void Dispose()
        {
        }

        #endregion 生命周期
    }
}