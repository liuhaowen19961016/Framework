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

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            if (enableClickSound)
            {
                PlayClickSound();
            }
        }

        private void PlayClickSound()
        {
            //todo logic 播放声音
            Debug.LogError("播放声音");
        }
    }
}