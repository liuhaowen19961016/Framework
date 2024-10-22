using System;

namespace Framework
{
    /// <summary>
    /// 装载器
    /// </summary>
    /// 用于开启Hoxfix工程
    public class Loader
    {
        public static Action FixedUpdate;
        public static Action<float> Update;
        public static Action LateUpdate;
        public static Action<bool> OnApplicationPause;
        public static Action<bool> OnApplicationFocus;
        public static Action OnApplicationQuit;

        public static void StartLoader()
        {
            HotfixBridge.Init();
            HotfixBridge.CallStaticMethod("Hotfix", "GameGlobal", "Start");
        }
    }
}