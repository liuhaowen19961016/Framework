using System.Collections.Generic;
using UnityEngine;
using System;

namespace Framework
{
    public class TimerTask
    {
        private float duration;
        private bool ignoreTimeScale;
        private int loopCount = 1; //-1代表无限循环
        private Action onComplete;
        private Action onSet;
        private Action<float> onUpdate;

        private bool isPause;
        private bool isDispose;
        private bool isDone;
        public bool IsComplete
        {
            get { return isDispose || isDone; }
        }

        private float leftTime;

        public void Set(float duration, bool ignoreTimeScale, int loopCount, Action onSet, Action<float> onUpdate, Action onComplete)
        {
            this.duration = duration;
            this.ignoreTimeScale = ignoreTimeScale;
            this.loopCount = loopCount;
            this.onSet = onSet;
            this.onUpdate = onUpdate;
            this.onComplete = onComplete;

            SetDuration(duration);
            this.onSet?.Invoke();
        }

        public void Dispose()
        {
            isDispose = true;
        }

        public void Pause()
        {
            isPause = true;
        }

        public void Resume()
        {
            isPause = false;
        }

        public void Update()
        {
            try
            {
                if (IsComplete)
                    return;
                if (isPause)
                    return;

                leftTime -= GetDeltaTime();
                onUpdate?.Invoke(leftTime);
                if (leftTime <= 0)
                {
                    onComplete?.Invoke();

                    if (loopCount == -1)
                    {
                        Reset();
                    }
                    else
                    {
                        loopCount--;
                        if (loopCount <= 0)
                        {
                            isDone = true;
                        }
                        else
                        {
                            Reset();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        private float GetDeltaTime()
        {
            float deltaTime = ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime;
            return deltaTime;
        }

        public void SetDuration(float duration)
        {
            this.duration = duration;
            leftTime = duration;
        }

        public void Reset()
        {
            leftTime = duration;
        }
    }

    public class Timer
    {
        private List<TimerTask> timerTaskList_ToAdd = new();
        private List<TimerTask> timerTaskList = new();

        public TimerTask Register(float duration, bool ignoreTimeScale = false, int loopCount = 1, Action onSet = null, Action<float> onUpdate = null, Action onComplete = null)
        {
            TimerTask timerTask = new TimerTask();
            timerTask.Set(duration, ignoreTimeScale, loopCount, onSet, onUpdate, onComplete);
            timerTaskList_ToAdd.Add(timerTask);
            return timerTask;
        }

        public void PauseAll()
        {
            foreach (var timerTask in timerTaskList)
            {
                timerTask.Pause();
            }
        }

        public void ResumeAll()
        {
            foreach (var timerTask in timerTaskList)
            {
                timerTask.Resume();
            }
        }

        public void DisposeAll()
        {
            foreach (var timerTask in timerTaskList)
            {
                timerTask.Dispose();
            }
            timerTaskList_ToAdd.Clear();
            timerTaskList.Clear();
        }

        private void UpdateAll()
        {
            foreach (var timerTask in timerTaskList_ToAdd)
            {
                timerTaskList.Add(timerTask);
            }
            timerTaskList_ToAdd.Clear();
            foreach (var timerTask in timerTaskList)
            {
                timerTask.Update();
            }

            for (int i = timerTaskList.Count - 1; i >= 0; i--)
            {
                if (timerTaskList[i].IsComplete)
                {
                    timerTaskList[i] = null;
                    timerTaskList.RemoveAt(i);
                }
            }
        }

        public void Update()
        {
            UpdateAll();
        }
    }
}