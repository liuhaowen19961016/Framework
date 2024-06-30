using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Framework
{
    public class PointerDownEventTrigger : MonoBehaviour, IPointerDownHandler
    {
        private Action<GameObject, PointerEventData> onPointerDown;

        public static PointerDownEventTrigger Get(GameObject go)
        {
            var component = go.GetComponent<PointerDownEventTrigger>();
            if (component == null)
                component = go.AddComponent<PointerDownEventTrigger>();
            return component;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            onPointerDown?.Invoke(gameObject, eventData);
        }

        public void AddListener(Action<GameObject, PointerEventData> onPointerDown)
        {
            this.onPointerDown += onPointerDown;
        }

        public void RemoveListener(Action<GameObject, PointerEventData> onPointerDown)
        {
            this.onPointerDown -= onPointerDown;
        }

        public void RemoveAllListener()
        {
            this.onPointerDown = null;
        }
    }
}