using Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using Event = Framework.Event;

namespace Hotfix
{
    public class GameGlobal
    {
        private static Timer timer;
        public static Timer Timer => timer;

        private static Event _event;
        public static Event Event => _event;

        public static TestHotfix test;
        public static TestHotfix Test => test;

        private static UIMgr uiMgr;
        public static UIMgr UIMgr => uiMgr;

        private static bool initComplete;

        public static EventSystem EventSystem { get; private set; }

        private static void Start()
        {
            Log.Debug("Hotfix.GameInit Start", ELogColor.Cyan);

            Loader.FixedUpdate += FixedUpdate;
            Loader.Update += Update;
            Loader.LateUpdate += LateUpdate;
            Loader.OnApplicationQuit += OnApplicationQuit;

            CreateEventSystem();

            //
            timer = new Timer();
            _event = new Event();

            //
            test = new TestHotfix();
            test.Register();

            uiMgr = new UIMgr();
            uiMgr.Init();
            uiMgr.OpenSync(1, 1231);

            //
            initComplete = true;
        }

        private static void CreateEventSystem()
        {
            GameObject eventSystemGo = GameUtils.CreateGameObject("EventSystem", null, false,
                typeof(EventSystem), typeof(StandaloneInputModule));
            EventSystem = eventSystemGo.GetComponent<EventSystem>();
            Object.DontDestroyOnLoad(eventSystemGo);
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
            test.Update();
            uiMgr?.Update();
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