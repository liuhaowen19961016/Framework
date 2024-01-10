
namespace Hotfix
{
    public class GameGlobal
    {
        private static GameGlobal ins;
        public static GameGlobal Get()
        {
            if (ins == null)
            {
                ins = new GameGlobal();
            }
            return ins;
        }

        private static TimerManager timerMgr;
        public TimerManager TimerMgr => timerMgr;

        private static bool initComplete;

        private static void Start()
        {
            CommonLog.Debug("Hotfix.GameInit Start", ELogColor.Cyan);

            timerMgr = new TimerManager();

            initComplete = true;
        }

        private void Init()
        {

        }

        public void FixedUpdate()
        {
            if (!initComplete)
                return;
        }

        public void Update()
        {
            if (!initComplete)
                return;
        }

        public void LateUpdate()
        {
            if (!initComplete)
                return;
        }
    }
}
