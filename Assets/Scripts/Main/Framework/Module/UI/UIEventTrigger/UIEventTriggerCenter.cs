using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Framework
{
    public class UIEventTriggerCenter
    {
        #region Click

        public static void RegisterOnClick(GameObject go, Action<GameObject> onClick, float invalidTime = 0)
        {
            var trigger = ClickEventTrigger.Get(go);
            trigger.SetInvalidTime(invalidTime);
            trigger.AddListener(onClick);
        }

        public static void UnRegisterOnClick(GameObject go, Action<GameObject> onClick)
        {
            var trigger = ClickEventTrigger.Get(go);
            trigger.RemoveListener(onClick);
        }

        public static void UnRegisterAllOnClick(GameObject go)
        {
            var trigger = ClickEventTrigger.Get(go);
            trigger.RemoveAllListener();
        }

        #endregion Click

        #region DoubleClick

        public static void RegisterOnDoubleClick(GameObject go, Action<GameObject, int> onDoubleClick, float doubleClickTime = 0.3f)
        {
            var trigger = DoubleClickEventTrigger.Get(go);
            trigger.SetDoubleClickTime(doubleClickTime);
            trigger.AddListener(onDoubleClick);
        }

        public static void UnRegisterOnDoubleClick(GameObject go, Action<GameObject, int> onDoubleClick)
        {
            var trigger = DoubleClickEventTrigger.Get(go);
            trigger.RemoveListener(onDoubleClick);
        }

        public static void UnRegisterAllOnDoubleClick(GameObject go)
        {
            var trigger = DoubleClickEventTrigger.Get(go);
            trigger.RemoveAllListener();
        }

        #endregion DoubleClick

        #region LongPress

        public static void RegisterOnLongPress(GameObject go, Action<GameObject> onClick, float longPressTime, bool isContinueCheck)
        {
            var trigger = LongPressEventTrigger.Get(go);
            trigger.SetLongPressTime(longPressTime);
            trigger.SetIsContinueCheck(isContinueCheck);
            trigger.AddListener(onClick);
        }

        public static void UnRegisterOnLongPress(GameObject go, Action<GameObject> onClick)
        {
            var trigger = LongPressEventTrigger.Get(go);
            trigger.RemoveListener(onClick);
        }

        public static void UnRegisterAllOnLongPress(GameObject go)
        {
            var trigger = LongPressEventTrigger.Get(go);
            trigger.RemoveAllListener();
        }

        #endregion LongPress

        #region BeginDrag

        public static void RegisterOnBeginDrag(GameObject go, Action<GameObject, PointerEventData> onBeginDrag)
        {
            var trigger = BeginDragEventTrigger.Get(go);
            trigger.AddListener(onBeginDrag);
        }

        public static void UnRegisterOnBeginDrag(GameObject go, Action<GameObject, PointerEventData> onBeginDrag)
        {
            var trigger = BeginDragEventTrigger.Get(go);
            trigger.RemoveListener(onBeginDrag);
        }

        public static void UnRegisterAllOnBeginDrag(GameObject go)
        {
            var trigger = BeginDragEventTrigger.Get(go);
            trigger.RemoveAllListener();
        }

        #endregion BeginDrag

        #region Drag

        public static void RegisterOnDrag(GameObject go, Action<GameObject, PointerEventData> onDrag)
        {
            var trigger = DragEventTrigger.Get(go);
            trigger.AddListener(onDrag);
        }

        public static void UnRegisterOnDrag(GameObject go, Action<GameObject, PointerEventData> onDrag)
        {
            var trigger = DragEventTrigger.Get(go);
            trigger.RemoveListener(onDrag);
        }

        public static void UnRegisterAllOnDrag(GameObject go)
        {
            var trigger = DragEventTrigger.Get(go);
            trigger.RemoveAllListener();
        }

        #endregion Drag

        #region EndDrag

        public static void RegisterOnEndDrag(GameObject go, Action<GameObject, PointerEventData> onEndDrag)
        {
            var trigger = EndDragEventTrigger.Get(go);
            trigger.AddListener(onEndDrag);
        }

        public static void UnRegisterOnEndDrag(GameObject go, Action<GameObject, PointerEventData> onEndDrag)
        {
            var trigger = EndDragEventTrigger.Get(go);
            trigger.RemoveListener(onEndDrag);
        }

        public static void UnRegisterAllOnEndDrag(GameObject go)
        {
            var trigger = EndDragEventTrigger.Get(go);
            trigger.RemoveAllListener();
        }

        #endregion EndDrag

        #region PointerEnter

        public static void RegisterOnPointerEnter(GameObject go, Action<GameObject, PointerEventData> onPointerEnter)
        {
            var trigger = PointerEnterEventTrigger.Get(go);
            trigger.AddListener(onPointerEnter);
        }

        public static void UnRegisterOnPointerEnter(GameObject go, Action<GameObject, PointerEventData> onPointerEnter)
        {
            var trigger = PointerEnterEventTrigger.Get(go);
            trigger.RemoveListener(onPointerEnter);
        }

        public static void UnRegisterAllOnPointerEnter(GameObject go)
        {
            var trigger = PointerEnterEventTrigger.Get(go);
            trigger.RemoveAllListener();
        }

        #endregion PointerEnter

        #region PointerExit

        public static void RegisterOnPointerExit(GameObject go, Action<GameObject, PointerEventData> onPointerExit)
        {
            var trigger = PointerExitEventTrigger.Get(go);
            trigger.AddListener(onPointerExit);
        }

        public static void UnRegisterOnPointerExit(GameObject go, Action<GameObject, PointerEventData> onPointerExit)
        {
            var trigger = PointerExitEventTrigger.Get(go);
            trigger.RemoveListener(onPointerExit);
        }

        public static void UnRegisterAllOnPointerExit(GameObject go)
        {
            var trigger = PointerExitEventTrigger.Get(go);
            trigger.RemoveAllListener();
        }

        #endregion PointerExit

        #region PointerDown

        public static void RegisterOnPointerDown(GameObject go, Action<GameObject, PointerEventData> onPointerDown)
        {
            var trigger = PointerDownEventTrigger.Get(go);
            trigger.AddListener(onPointerDown);
        }

        public static void UnRegisterOnPointerDown(GameObject go, Action<GameObject, PointerEventData> onPointerDown)
        {
            var trigger = PointerDownEventTrigger.Get(go);
            trigger.RemoveListener(onPointerDown);
        }

        public static void UnRegisterAllOnPointerDown(GameObject go)
        {
            var trigger = PointerDownEventTrigger.Get(go);
            trigger.RemoveAllListener();
        }

        #endregion PointerDown

        #region PointerUp

        public static void RegisterOnPointerUp(GameObject go, Action<GameObject, PointerEventData> onPointerUp)
        {
            var trigger = PointerUpEventTrigger.Get(go);
            trigger.AddListener(onPointerUp);
        }

        public static void UnRegisterOnPointerUp(GameObject go, Action<GameObject, PointerEventData> onPointerUp)
        {
            var trigger = PointerUpEventTrigger.Get(go);
            trigger.RemoveListener(onPointerUp);
        }

        public static void UnRegisterAllOnPointerUp(GameObject go)
        {
            var trigger = PointerUpEventTrigger.Get(go);
            trigger.RemoveAllListener();
        }

        #endregion PointerUp
    }
}