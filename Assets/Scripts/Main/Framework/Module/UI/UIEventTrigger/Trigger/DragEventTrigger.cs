using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Framework
{
    public class DragEventTrigger : MonoBehaviour, IDragHandler
    {
        private Action<GameObject, PointerEventData> onDrag;

        public static DragEventTrigger Get(GameObject go)
        {
            var component = go.GetComponent<DragEventTrigger>();
            if (component == null)
                component = go.AddComponent<DragEventTrigger>();
            return component;
        }

        public void OnDrag(PointerEventData eventData)
        {
            onDrag?.Invoke(gameObject, eventData);
        }

        public void AddListener(Action<GameObject, PointerEventData> onDrag)
        {
            this.onDrag += onDrag;
        }

        public void RemoveListener(Action<GameObject, PointerEventData> onDrag)
        {
            this.onDrag -= onDrag;
        }

        public void RemoveAllListener()
        {
            this.onDrag = null;
        }
    }
}