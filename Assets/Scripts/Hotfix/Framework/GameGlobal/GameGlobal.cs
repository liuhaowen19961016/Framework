
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

        #region 

        private static TimerManager timerMgr;
        public TimerManager TimerMgr => timerMgr;

        #endregion

        private static void Start()
        {
            CommonLog.Debug("Hotfix.GameInit Start", ELogColor.Cyan);
       
            timerMgr = new TimerManager();
        }

        private void Init()
        {

        }
    }
}
