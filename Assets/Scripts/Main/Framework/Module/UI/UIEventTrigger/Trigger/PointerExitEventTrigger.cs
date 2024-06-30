using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Framework
{
    public class PointerExitEventTrigger : MonoBehaviour, IPointerExitHandler
    {
        private Action<GameObject, PointerEventData> onPointerExit;

        public static PointerExitEventTrigger Get(GameObject go)
        {
            var component = go.GetComponent<PointerExitEventTrigger>();
            if (component == null)
                component = go.AddComponent<PointerExitEventTrigger>();
            return component;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            onPointerExit?.Invoke(gameObject, eventData);
        }

        public void AddListener(Action<GameObject, PointerEventData> onPointerExit)
        {
            this.onPointerExit += onPointerExit;
        }

        public void RemoveListener(Action<GameObject, PointerEventData> onPointerExit)
        {
            this.onPointerExit -= onPointerExit;
        }

        public void RemoveAllListener()
        {
            this.onPointerExit = null;
        }
    }
}