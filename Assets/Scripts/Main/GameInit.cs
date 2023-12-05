using UnityEngine;

[System.Serializable]
public class GameInitSetting
{
    #region Log Setting

    public ELogLevel logLevel;
    public bool enableCommonLog;
    public bool enableNetworkLog;

    #endregion Log Setting
}

public class GameInit : MonoBehaviour
{
    public static GameInitSetting GameInitSetting;//外部获取用
    public GameInitSetting gameInitSetting;//游戏启动设置

    private void Start()
    {
        GameInitSetting = gameInitSetting;
        Log.Init(gameInitSetting.logLevel);
    }
}
