using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIMenuOption
{
    private const string UI_LAYER = "UI";

    [MenuItem("GameObject/Framework/UI/GameImage", priority = 0)]
    public static void AddGameImage()
    {
        GameObject componentGo = CreateUIComponent("GameImage");
        Image gameImage = componentGo.AddComponent<Image>();
        gameImage.raycastTarget = false;
    }

    [MenuItem("GameObject/Framework/UI/GameButton", priority = 1)]
    public static void AddGameButton()
    {
        GameObject componentGo = CreateUIComponent("GameButton");
        componentGo.AddComponent<GameButton>();
        componentGo.AddComponent<Image>();
    }

    [MenuItem("GameObject/Framework/UI/EmptyGraphics", priority = 2)]
    public static void AddEmptyGraphic()
    {
        GameObject componentGo = CreateUIComponent("EmptyGraphics");
        componentGo.AddComponent<EmptyGraphics>();
        componentGo.AddComponent<CanvasRenderer>();
    }

    /// <summary>
    /// 创建UI组件
    /// </summary>
    private static GameObject CreateUIComponent(string componentName)
    {
        GameObject componentGo = new GameObject(componentName);
        componentGo.layer = LayerMask.NameToLayer(UI_LAYER);
        componentGo.AddComponent<RectTransform>();

        Canvas canvas;
        if (Selection.activeGameObject != null
            && LayerMask.LayerToName(Selection.activeGameObject.layer) != UI_LAYER)
        {
            canvas = CreateCanvas();
            canvas.transform.SetParent(Selection.activeGameObject.transform, false);
        }
        else
        {
            canvas = Object.FindObjectOfType<Canvas>();
            if (canvas == null)
                canvas = CreateCanvas();
        }
        EventSystem eventSystem = Object.FindObjectOfType<EventSystem>();
        if (eventSystem == null)
            CreateEventSystem();
        componentGo.transform.SetParent(canvas.transform, false);
        componentGo.transform.localPosition = Vector3.zero;
        componentGo.transform.localScale = Vector3.one;
        return componentGo;
    }

    /// <summary>
    /// 创建UI画布
    /// </summary>
    private static Canvas CreateCanvas()
    {
        GameObject canvasGo = new GameObject("Canvas");
        canvasGo.layer = LayerMask.NameToLayer(UI_LAYER);
        canvasGo.AddComponent<RectTransform>();
        Canvas canvas = canvasGo.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasGo.AddComponent<CanvasScaler>();
        canvasGo.AddComponent<GraphicRaycaster>();
        return canvas;
    }

    private static EventSystem CreateEventSystem()
    {
        GameObject eventSystemGo = new GameObject("EventSystem");
        EventSystem eventSystem = eventSystemGo.AddComponent<EventSystem>();
        eventSystemGo.AddComponent<StandaloneInputModule>();
        return eventSystem;
    }
}