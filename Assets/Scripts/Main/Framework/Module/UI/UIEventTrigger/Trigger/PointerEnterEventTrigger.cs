using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Framework
{
    public class PointerEnterEventTrigger : MonoBehaviour, IPointerEnterHandler
    {
        private Action<GameObject, PointerEventData> onPointerEnter;

        public static PointerEnterEventTrigger Get(GameObject go)
        {
            var component = go.GetComponent<PointerEnterEventTrigger>();
            if (component == null)
                component = go.AddComponent<PointerEnterEventTrigger>();
            return component;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            onPointerEnter?.Invoke(gameObject, eventData);
        }

        public void AddListener(Action<GameObject, PointerEventData> onPointerEnter)
        {
            this.onPointerEnter += onPointerEnter;
        }

        public void RemoveListener(Action<GameObject, PointerEventData> onPointerEnter)
        {
            this.onPointerEnter -= onPointerEnter;
        }

        public void RemoveAllListener()
        {
            this.onPointerEnter = null;
        }
    }
}