using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Framework
{
    public class EndDragEventTrigger : MonoBehaviour, IEndDragHandler
    {
        private Action<GameObject, PointerEventData> onEndDrag;

        public static EndDragEventTrigger Get(GameObject go)
        {
            var component = go.GetComponent<EndDragEventTrigger>();
            if (component == null)
                component = go.AddComponent<EndDragEventTrigger>();
            return component;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            onEndDrag?.Invoke(gameObject, eventData);
        }

        public void AddListener(Action<GameObject, PointerEventData> onEndDrag)
        {
            this.onEndDrag += onEndDrag;
        }

        public void RemoveListener(Action<GameObject, PointerEventData> onEndDrag)
        {
            this.onEndDrag -= onEndDrag;
        }

        public void RemoveAllListener()
        {
            this.onEndDrag = null;
        }
    }
}