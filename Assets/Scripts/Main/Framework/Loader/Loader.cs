using System;

namespace Main
{
    /// <summary>
    /// 装载器
    /// </summary>
    /// 用于开启Hoxfix工程
    public class Loader
    {
        public static Action FixedUpdate;
        public static Action Update;
        public static Action LateUpdate;
        public static Action OnApplicationQuit;

        public static void StartLoader()
        {
            HotfixBridge.Init();
            HotfixBridge.CallStaticMethod("Hotfix", "GameGlobal", "Start");
        }
    }
}
