using System;
using UnityEngine;

namespace Framework
{
    /// <summary>
    /// 游戏入口（挂启动场景中）
    /// </summary>
    public class GameInit : MonoBehaviour
    {
        private void Start()
        {
            Init();

            // TODO：放在热更结束后调用
            Loader.StartLoader();
        }

        private void Init()
        {
            // 初始化log
            InitLog();
        }

        /// <summary>
        /// 初始化log
        /// </summary>
        private void InitLog()
        {
            bool logEnable;
            ELogType logTypeMask;
            ELogTag logTagMask;
#if UNITY_EDITOR
            logEnable = Configuration.Ins.logEnable;
            logTypeMask = Configuration.Ins.logTypeMask;
            logTagMask = Configuration.Ins.logTagMask;
#else
            if (Debug.isDebugBuild)
            {
                logEnable = true;
                logTypeMask = (ELogType)~0;
                logTagMask = (ELogTag)~0;
            }
            else
            {
                logEnable = true;
                logTypeMask = (ELogType)0;
                logTagMask = (ELogTag)0;
            }
#endif
            LogService.Init(logEnable, logTypeMask, logTagMask);
            LogService.Add(new FileLog());
            Log.Info("log init success");
        }

        private void FixedUpdate()
        {
            Loader.FixedUpdate?.Invoke();
        }

        private void Update()
        {
            Loader.Update?.Invoke(Time.deltaTime);
        }

        private void LateUpdate()
        {
            Loader.LateUpdate?.Invoke();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            Loader.OnApplicationFocus?.Invoke(hasFocus);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            Loader.OnApplicationPause?.Invoke(pauseStatus);
        }

        private void OnApplicationQuit()
        {
            Loader.OnApplicationQuit?.Invoke();

            LogService.Dispose();
        }
    }
}