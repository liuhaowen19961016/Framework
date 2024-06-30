using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Framework
{
    public class PointerUpEventTrigger : MonoBehaviour, IPointerUpHandler
    {
        private Action<GameObject, PointerEventData> onPointerUp;

        public static PointerUpEventTrigger Get(GameObject go)
        {
            var component = go.GetComponent<PointerUpEventTrigger>();
            if (component == null)
                component = go.AddComponent<PointerUpEventTrigger>();
            return component;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            onPointerUp?.Invoke(gameObject, eventData);
        }

        public void AddListener(Action<GameObject, PointerEventData> onPointerUp)
        {
            this.onPointerUp += onPointerUp;
        }

        public void RemoveListener(Action<GameObject, PointerEventData> onPointerUp)
        {
            this.onPointerUp -= onPointerUp;
        }

        public void RemoveAllListener()
        {
            this.onPointerUp = null;
        }
    }
}