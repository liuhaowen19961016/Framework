using System;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace UnityEngine.UI
{
    public class GameButton : Button
    {
        [SerializeField]
        private bool enableClickSound;

        [SerializeField]
        private int clickSoundId; //点击按钮时的音效

        [SerializeField]
        private bool enableClickAni = true;

        private Sequence clickAniSeq;

        protected override void OnEnable()
        {
            base.OnEnable();
            clickAniSeq.Kill(true);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            if (enableClickAni)
            {
                PlayAni();
            }
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

        private void PlayAni()
        {
            clickAniSeq = DOTween.Sequence();
            clickAniSeq.Append(transform.DOScale(Vector3.one * 0.85f, 0.1f));
            clickAniSeq.Append(transform.DOScale(Vector3.one * 1f, 0.1f));
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            clickAniSeq.Kill(true);
        }
    }
}