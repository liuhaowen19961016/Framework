using System;
using System.Collections.Generic;
using Hotfix;
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
    }

    /// <summary>
    /// 界面数据
    /// </summary>
    public class UIViewInfo
    {
        public string viewName;
        public EUILayerType layerType;
        public EUIType type;

        public UIViewInfo(string viewName, EUILayerType layerType, EUIType type)
        {
            this.viewName = viewName;
            this.layerType = layerType;
            this.type = type;
        }
    }

    public class UIMgr
    {
        //根据项目进行调整
        public static Vector2 referenceResolution = new Vector2(768, 1366);
        public const float SCREENMATCHVALUE = 0;

        public static int LAYERNAME_UI = LayerMask.NameToLayer("UI");
        public const int LAYER_ORDERINLAYER = 1000;
        public const int VIEW_ORDERINLAYER = 100;

        private GameObject uiRootGo;

        private Canvas uiCanvas; //UI画布
        public Canvas UICanvas => uiCanvas;

        private Camera uiCamera; //UI相机
        public Camera UICamera => uiCamera;

        private GraphicRaycaster raycaster; //UI射线检测组件
        public GraphicRaycaster Raycaster => raycaster;

        private Dictionary<string, UIViewInfo> viewInfos = new Dictionary<string, UIViewInfo>(); //所有界面数据（只有viewInfos中存在的界面才可以被UI管理器控制）

        private List<UIViewBase> viewStack = new List<UIViewBase>();
        private Dictionary<EUILayerType, UILayer> layerType2Layer = new Dictionary<EUILayerType, UILayer>();

        private void CollectViewInfo()
        {
            //todo test
            UIViewInfo viewInfo = new UIViewInfo("UITest", EUILayerType.Top, EUIType.Main);
            viewInfos.Add(viewInfo.viewName, viewInfo);
        }

        /// <summary>
        /// 同步打开界面
        /// </summary>
        public void ShowSync(string viewName, object viewData = null)
        {
            var curView = FindView(viewName);
            if (curView != null)
            {
                Pop(curView);
                Push(curView);
                curView.InternalRefresh();
            }
            else
            {
                var viewInfo = FindViewInfo(viewName);
                if (viewInfo == null)
                    return;
                var layer = FindLayer(viewInfo.layerType);
                if (layer == null)
                    return;
                //GameObject viewGo = TMGame.GameGlobal.GetManager<ResMgr>().GetGameObject(viewName).GetInstance();
                GameObject viewGo = Object.Instantiate(Resources.Load<GameObject>(viewName)); //todo test
                if (viewGo == null)
                {
                    Debug.LogError($"{viewName}界面打开失败");
                    return;
                }
                UIViewBase view = new UIViewBase(viewGo, viewInfo);
                //初始化
                view.InternalInit(viewData);
                //入栈
                Push(view);
                //显示
                view.InternalShow();
                view.InternalRefresh();
            }
        }

        public bool Close(string viewName, bool isDestory = true)
        {
            var curView = FindView(viewName);
            if (curView == null)
                return false;
            var layer = FindLayer(curView.LayerType);
            if (layer == null)
                return false;
            curView.InternalClose(isDestory);
            if (isDestory)
            {
                Object.Destroy(curView.Go);
                Pop(curView);
            }
            return true;
        }

        /// <summary>
        /// 设置是否开启射线检测（当前Canvas下的UI能否接收射线检测）
        /// </summary>
        public void SetRaycastEnable(bool enable)
        {
            raycaster.enabled = enable;
        }

        /// <summary>
        /// 查找界面
        /// </summary>
        public UIViewBase FindView(string viewName)
        {
            foreach (var uiView in viewStack)
            {
                if (uiView.ViewName == viewName)
                    return uiView;
            }
            return null;
        }

        /// <summary>
        /// 查找某一层级下的最顶部界面
        /// </summary>
        public UIViewBase FindTopView(EUILayerType layerType)
        {
            var layer = FindLayer(layerType);
            var view = layer.GetTopView();
            return view;
        }

        /// <summary>
        /// 查找层级
        /// </summary>
        public UILayer FindLayer(EUILayerType layerType)
        {
            if (!layerType2Layer.TryGetValue(layerType, out var layer))
            {
                Debug.LogError($"没有找到{layerType}层");
                return null;
            }
            return layer;
        }

        #region private

        /// <summary>
        /// 查找界面数据
        /// </summary>
        private UIViewInfo FindViewInfo(string viewName)
        {
            foreach (var kvp in viewInfos)
            {
                if (kvp.Key == viewName)
                    return kvp.Value;
            }
            Debug.LogError($"请先在配置表中添加 {viewName} 界面数据");
            return null;
        }

        private void Push(UIViewBase view)
        {
            if (FindView(view.ViewName) != null)
                return;
            var layer = FindLayer(view.LayerType);
            layer.AddView(view);
            viewStack.Add(view);
        }

        private void Pop(UIViewBase view)
        {
            if (FindView(view.ViewName) == null)
                return;
            var layer = FindLayer(view.LayerType);
            layer.RemoveView(view);
            viewStack.Remove(view);
        }

        private Camera CreateUICamera()
        {
            GameObject uiCameraGo = GameUtils.CreateGameObject("UICamera", uiRootGo.transform, false, typeof(Camera));
            Camera uiCamera = uiCameraGo.GetComponent<Camera>();
            uiCamera.orthographic = true;
            uiCamera.cullingMask = LayerMask.GetMask("UI");
            return uiCamera;
        }

        private Canvas CreateUICanvas(Camera uiCamera)
        {
            GameObject uiCanvasGo = GameUtils.CreateGameObject("UICanvas", uiRootGo.transform, false,
                typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            uiCanvasGo.layer = LAYERNAME_UI;
            Canvas canvas = uiCanvasGo.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = uiCamera;
            raycaster = uiCanvasGo.GetComponent<GraphicRaycaster>();
            CanvasScaler canvasScaler = uiCanvasGo.GetComponent<CanvasScaler>();
            canvasScaler.matchWidthOrHeight = SCREENMATCHVALUE;
            canvasScaler.referenceResolution = referenceResolution;
            //创建Canvas下的层级结构
            string[] uiLayerNameArray = Enum.GetNames(typeof(EUILayerType));
            foreach (var layerName in uiLayerNameArray)
            {
                EUILayerType uiLayerType = (EUILayerType)Enum.Parse(typeof(EUILayerType), layerName);
                GameObject layerRootGo = GameUtils.CreateGameObject(layerName, uiCanvasGo.transform, false, typeof(RectTransform));
                layerRootGo.layer = LAYERNAME_UI;
                RectTransform rect = layerRootGo.GetComponent<RectTransform>();
                rect.localScale = Vector3.one;
                rect.localPosition = Vector3.zero;
                rect.localRotation = Quaternion.identity;
                rect.anchoredPosition = Vector2.zero;
                rect.anchorMin = Vector2.zero;
                rect.anchorMax = Vector2.one;
                rect.sizeDelta = Vector2.zero;
                UILayer layer = new UILayer();
                layer.Init(layerRootGo, uiLayerType);
                layerType2Layer.Add(uiLayerType, layer);
            }
            CreateEventSystem();
            return canvas;
        }

        private EventSystem CreateEventSystem()
        {
            GameObject eventSystemGo = GameUtils.CreateGameObject("UICanvas", uiRootGo.transform, false,
                typeof(EventSystem), typeof(StandaloneInputModule), typeof(CanvasScaler), typeof(GraphicRaycaster));
            EventSystem eventSystem = eventSystemGo.GetComponent<EventSystem>();
            return eventSystem;
        }

        #endregion Private

        #region 生命周期

        public void Init()
        {
            //根据配表初始化界面数据
            CollectViewInfo();
            //创建UI结构
            uiRootGo = GameUtils.CreateGameObject("UIRoot", null, true);
            Object.DontDestroyOnLoad(uiRootGo);
            uiCamera = CreateUICamera();
            uiCanvas = CreateUICanvas(uiCamera);
        }

        public void Start()
        {
        }

        public void Dispose()
        {
        }

        #endregion 生命周期
    }
}