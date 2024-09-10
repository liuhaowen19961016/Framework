using System;
using UnityEngine;

namespace Framework
{
    [System.Serializable]
    public class GameInitSetting
    {
        public bool logEnable;
        public ELogLevel logLevel;
    }

    /// <summary>
    /// 游戏入口（挂启动场景中）
    /// </summary>
    public class GameInit : MonoBehaviour
    {
        public GameInitSetting gameInitSetting; //游戏启动设置

        private void Start()
        {
            Init();

            // TODO：放在热更结束后调用
            Loader.StartLoader();
        }

        private void Init()
        {
            //初始化log
            // LogService.Init(gameInitSetting.logEnable, gameInitSetting.logLevel);
            // LogService.Add(new UnityLog());
            // LogService.Add(new FileLog());
            // Log.Info("log init success");
        }

        private void FixedUpdate()
        {
            Loader.FixedUpdate?.Invoke();
        }

        private void Update()
        {
            Loader.Update?.Invoke();
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
        }
    }
}