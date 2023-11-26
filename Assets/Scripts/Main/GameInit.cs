using UnityEngine;

public class GameInit : MonoBehaviour
{
    #region Log Setting

    public ELogLevel logLevel;

    #endregion Log Setting

    private void Start()
    {
        Log.Init(logLevel);
    }
}
