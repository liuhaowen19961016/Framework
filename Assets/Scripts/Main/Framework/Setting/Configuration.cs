using Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "Configuration", menuName = "GameSetting/Configuration", order = 1)]
public class Configuration : ScriptableObject
{
    public const string CONFIGURATION_PATH = "Settings/Configuration";

    private static Configuration _ins;
    public static Configuration Ins
    {
        get
        {
            if (_ins == null)
            {
                _ins = Resources.Load<Configuration>(CONFIGURATION_PATH);
            }
            return _ins;
        }
    }

    [Header("-----Log配置-----")]
    public bool logEnable;
    public ELogType logTypeMask;
    public ELogTag logTagMask;
}