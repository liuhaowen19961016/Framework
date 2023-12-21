using UnityEngine;

namespace Main
{
    [System.Serializable]
    public class GameInitSetting
    {
        #region Log Setting

        public ELogLevel logLevel;
        public bool enableCommonLog;
        public bool enableNetworkLog;

        #endregion Log Setting
    }

    /// <summary>
    /// 游戏入口（挂启动场景中）
    /// </summary>
    public class GameInit : MonoBehaviour
    {
        public static GameInitSetting GameInitSetting;//外部获取用
        public GameInitSetting gameInitSetting;//游戏启动设置

        private void Start()
        {
            GameInitSetting = gameInitSetting;
            Log.Init(gameInitSetting.logLevel);

            // TODO：放在热更结束后调用
            Loader.StartLoader();
        }

        private void FixedUpdate()
        {

        }

        private void Update()
        {
           
        }

        private void LateUpdate()
        {
        
        }

        private void OnApplicationQuit()
        {
       
        }
    }
}
