using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Framework
{
    public class LongPressEventTrigger : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private Action<GameObject> onLongPress;

        private bool isPress;
        private float longPressTime;
        private float longPressTimer;
        private bool isContinueCheck;
        private bool isReleaseOnLongPress;

        public static LongPressEventTrigger Get(GameObject go)
        {
            var component = go.GetComponent<LongPressEventTrigger>();
            if (component == null)
                component = go.AddComponent<LongPressEventTrigger>();
            return component;
        }

        public void SetLongPressTime(float longPressTime = 0)
        {
            this.longPressTime = longPressTime;
        }

        public void SetIsContinueCheck(bool isContinueCheck)
        {
            this.isContinueCheck = isContinueCheck;
        }

        public void AddListener(Action<GameObject> onLongPress)
        {
            this.onLongPress += onLongPress;
        }

        public void RemoveListener(Action<GameObject> onLongPress)
        {
            this.onLongPress -= onLongPress;
        }

        public void RemoveAllListener()
        {
            this.onLongPress = null;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            isPress = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isPress = false;
            longPressTimer = 0;
        }

        private void Update()
        {
            if (isPress)
            {
                longPressTimer += Time.unscaledDeltaTime;
                if (longPressTimer >= longPressTime)
                {
                    if (!isReleaseOnLongPress || isContinueCheck)
                    {
                        onLongPress?.Invoke(gameObject);
                        isReleaseOnLongPress = true;
                    }
                    longPressTimer = 0;
                }
            }
        }
    }
}