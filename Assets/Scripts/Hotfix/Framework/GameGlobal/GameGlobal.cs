using Main;

namespace Hotfix
{
    public class GameGlobal
    {
        private static Timer timer;
        public static Timer Timer => timer;

        private static GameEvent gameEvent;
        public static GameEvent GameEvent => gameEvent;

        public static TestHotfix test;
        public static TestHotfix Test => test;

        private static bool initComplete;

        private static void Start()
        {
            CommonLog.Debug("Hotfix.GameInit Start", ELogColor.Cyan);

            Loader.FixedUpdate += FixedUpdate;
            Loader.Update += Update;
            Loader.LateUpdate += LateUpdate;
            Loader.OnApplicationQuit += OnApplicationQuit;

            //
            timer = new Timer();
            gameEvent = new GameEvent();

            //
            test = new TestHotfix();
            test.Register();

            //
            initComplete = true;
        }

        private void Init()
        {

        }

        public static void FixedUpdate()
        {
            if (!initComplete)
                return;
        }

        public static void Update()
        {
            if (!initComplete)
                return;

            timer?.Update();
        }

        public static void LateUpdate()
        {
            if (!initComplete)
                return;
        }

        public static void OnApplicationQuit()
        {
            if (!initComplete)
                return;

            Loader.FixedUpdate -= FixedUpdate;
            Loader.Update -= Update;
            Loader.LateUpdate -= LateUpdate;
            Loader.OnApplicationQuit -= OnApplicationQuit;
        }
    }
}
