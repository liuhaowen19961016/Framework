using System;
using System.Reflection;

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
            Assembly assembly = Assembly.Load("Hotfix");
            Type type = assembly.GetType("Hotfix.GameGlobal");
            type.GetMethod("Start", BindingFlags.Static | BindingFlags.NonPublic)?.Invoke(null, null);
        }
    }
}
