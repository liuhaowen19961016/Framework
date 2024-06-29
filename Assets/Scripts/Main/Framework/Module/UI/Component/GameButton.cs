using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
    public class GameButton : Button
    {
        [SerializeField]
        private bool enableClickSound;

        [SerializeField]
        private int clickSoundId; //点击按钮时的音效

        [SerializeField]
        private float invalidTime; //视为无效的时间

        [SerializeField]
        private float longPressTime = 3; //视为长按的时间

        [SerializeField]
        private float doubleClickTime = 0.5f; //视为连击的时间

        [SerializeField]
        private bool enableCheckInvalid;

        [SerializeField]
        private bool enableCheckLongPress;

        [SerializeField]
        private bool enableCheckDoubleClick;

        private float lastClickTime;
        private float timer_longPress;
        private int doubleClickCount;

        private Action onClick;
        private Action onLongPress;
        private Action<int> onDoubleClick;

        public void AddOnClick(Action onClick)
        {
            this.onClick = onClick;
        }

        public void AddOnLongPress(Action onLongPress)
        {
            this.onLongPress = onLongPress;
        }

        public void AddOnDoubleClick(Action<int> onDoubleClick)
        {
            this.onDoubleClick = onDoubleClick;
        }

        public void SetInvalidTime(float invalidTime)
        {
            this.invalidTime = invalidTime;
        }

        public void SetLongPressTime(float longPressTime)
        {
            this.longPressTime = longPressTime;
        }

        public void SetDoubleClickTime(float doubleClickTime)
        {
            this.doubleClickTime = doubleClickTime;
        }

        public void SetCheckInvalid(bool enable)
        {
            enableCheckInvalid = enable;
        }

        public void SetCheckLongPress(bool enable)
        {
            enableCheckLongPress = enable;
        }

        public void SetCheckDoubleClick(bool enable)
        {
            enableCheckDoubleClick = enable;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (!IsActive() || !IsInteractable())
                return;
            if (enableCheckInvalid && Time.unscaledTime - lastClickTime < invalidTime)
                return;
            //检查单击和连击
            if (enableCheckDoubleClick)
            {
                if (Time.unscaledTime - lastClickTime < doubleClickTime)
                {
                    doubleClickCount++;
                    if (doubleClickCount > 1)
                    {
                        onDoubleClick?.Invoke(doubleClickCount);
                    }
                    else
                    {
                        onClick?.Invoke();
                    }
                }
                else
                {
                    onClick?.Invoke();
                    doubleClickCount = 0;
                }
            }
            else
            {
                onClick?.Invoke();
            }
            if (enableClickSound)
            {
                PlayClickSound();
            }
            lastClickTime = Time.unscaledTime;
            timer_longPress = 0;
        }

        private void Update()
        {
            CheckLongPress();
        }

        /// <summary>
        /// 检查长按
        /// </summary>
        private void CheckLongPress()
        {
            if (enableCheckLongPress && IsPressed())
            {
                timer_longPress += Time.unscaledDeltaTime;
                if (timer_longPress >= longPressTime)
                {
                    onLongPress?.Invoke();
                    timer_longPress = 0;
                }
            }
        }

        private void PlayClickSound()
        {
            //todo logic 播放声音
        }
    }
}