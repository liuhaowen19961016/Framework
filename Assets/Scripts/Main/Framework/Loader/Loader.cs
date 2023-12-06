using System;
using System.Reflection;

namespace Main
{
    /// <summary>
    /// 装载器
    /// </summary>
    /// 用于开启Hoxfix工程
    public class Loader : MonoSingleton<Loader>
    {
        public void StartLoader()
        {
            Assembly assembly = Assembly.Load("Hotfix");
            Type type = assembly.GetType("Hotfix.GameInit");
            type.GetMethod("Start", BindingFlags.Static | BindingFlags.NonPublic)?.Invoke(null, null);
        }
    }
}
