
namespace Hotfix
{
    public class GameGlobal
    {
        private static Timer timer;
        public static Timer Timer => timer;

        private static bool initComplete;

        private static void Start()
        {
            CommonLog.Debug("Hotfix.GameInit Start", ELogColor.Cyan);

            timer = new Timer();

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

            timer?.Update();
        }

        public void LateUpdate()
        {
            if (!initComplete)
                return;
        }
    }
}
