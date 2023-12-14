namespace Hotfix
{
    public static class GameInit
    {
        private static void Start()
        {
            CommonLog.Debug("Hotfix.GameInit Start", ELogColor.Cyan);

            GameComponent.Init();

            //TODO
            GameSingleton.AddSingleton<TestSingletn>();
            GameComponent.ComponentRoot.AddComponent<TestComP1>();
        }
    }
}
