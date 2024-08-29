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
        private EventSystem eventSystem; //EventSystem

        private List<UIViewBase> viewStack = new List<UIViewBase>();
        private Dictionary<EUILayerType, UILayer> layerType2Layer = new Dictionary<EUILayerType, UILayer>();

        private Dictionary<object, bool> setEventSystemStateCache = new Dictionary<object, bool>();

        /// <summary>
        /// 同步打开界面
        /// </summary>
        public UIViewBase ShowSync(int viewId, object viewData = null)
        {
            var curView = FindView(viewId);
            if (curView != null)
            {
                Pop(curView);
                Push(curView);
                if (!curView.Go.activeSelf)
                {
                    curView.InternalShow();
                }
                else
                {
                    curView.InternalRefresh();
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
                var layer = FindLayer((EUILayerType)uiViewCfg.LayerType);
                if (layer == null)
                    return null;
                var classType = Type.GetType(viewName);
                if (classType == null)
                {
                    Debug.LogError($"没有界面类{viewName}，请先生成界面类");
                    return null;
                }
                UIViewBase view = Activator.CreateInstance(classType) as UIViewBase;
                if (view == null)
                    return null;

                view.InternalInit(viewName, uiViewCfg, viewData);
                bool createRet = view.InternalCreate(layer.LayerGo.transform);
                if (!createRet)
                {
                    return null;
                }
                //入栈
                Push(view);
                //显示
                view.InternalShow();
                return view;
            }
        }

        public bool Close(int viewId, bool isDestory = true)
        {
            var curView = FindView(viewId);
            if (curView == null)
                return false;
            var layer = FindLayer(curView.LayerType);
            if (layer == null)
                return false;
            curView.InternalClose(isDestory);
            if (isDestory)
            {
                Pop(curView);
            }
            return true;
        }

        /// <summary>
        /// 设置所有UI是否可交互
        /// </summary>
        public void SetEventSystemEnable(bool enable, object sender)
        {
            if (setEventSystemStateCache.ContainsKey(sender))
            {
                if (enable)
                {
                    setEventSystemStateCache.Remove(sender);
                    eventSystem.enabled = true;
                }
            }
            else
            {
                if (!enable)
                {
                    setEventSystemStateCache.Add(sender, false);
                    eventSystem.enabled = false;
                }
            }
        }

        /// <summary>
        /// 查找界面
        /// </summary>
        public UIViewBase FindView(int viewId)
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
            CreateEventSystem();
        }

        private void CreateEventSystem()
        {
            GameObject eventSystemGo = GameUtils.CreateGameObject("EventSystem", uiRootGo.transform, false,
                typeof(EventSystem), typeof(StandaloneInputModule));
            eventSystem = eventSystemGo.GetComponent<EventSystem>();
        }

        #endregion 创建UI结构

        #endregion Private

        #region 生命周期

        public void Init()
        {
            //创建UI结构
            uiRootGo = GameUtils.CreateGameObject("UIRoot", null);
            Object.DontDestroyOnLoad(uiRootGo);
            CreateUICamera();
            CreateUICanvas();
        }

        public void Start()
        {
        }

        public void Update()
        {
            for (int i = viewStack.Count - 1; i >= 0; i--)
            {
                if (viewStack[i] == null)
                    continue;
                viewStack[i].InternalUpdate();
            }
        }

        public void Dispose()
        {
        }

        #endregion 生命周期
    }
}