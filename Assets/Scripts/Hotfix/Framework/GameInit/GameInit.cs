namespace Hotfix
{
    public static class GameInit
    {
        private static void Start()
        {
            CommonLog.Debug("Hotfix.GameInit Start", ELogColor.Cyan);

            //Game.AddSingleton<Root>();
            //Game.AddSingleton<TestHotfix>();
            TestHotfix.Ins.TestHotfixFun();
        }
    }
}
