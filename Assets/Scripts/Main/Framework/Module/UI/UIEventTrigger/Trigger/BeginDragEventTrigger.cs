using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Framework
{
    public class BeginDragEventTrigger : MonoBehaviour, IBeginDragHandler
    {
        private Action<GameObject, PointerEventData> onBeginDrag;

        public static BeginDragEventTrigger Get(GameObject go)
        {
            var component = go.GetComponent<BeginDragEventTrigger>();
            if (component == null)
                component = go.AddComponent<BeginDragEventTrigger>();
            return component;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            onBeginDrag?.Invoke(gameObject, eventData);
        }

        public void AddListener(Action<GameObject, PointerEventData> onBeginDrag)
        {
            this.onBeginDrag += onBeginDrag;
        }

        public void RemoveListener(Action<GameObject, PointerEventData> onBeginDrag)
        {
            this.onBeginDrag -= onBeginDrag;
        }

        public void RemoveAllListener()
        {
            this.onBeginDrag = null;
        }
    }
}