using UnityEngine;

/// <summary>
/// Unity写日志
/// </summary>
public class UnityLog : ILog
{
    public void Init()
    {
    }

    public void Wirte(string message, ELogLevel logLevel, ELogColor logColor = ELogColor.Default)
    {
        message = logColor == ELogColor.Default ? message : string.Format("<color={0}>{1}</color>", GetHexColor(logColor), message);
        switch (logLevel)
        {
            case ELogLevel.Debug:
                Debug.Log(message);
                break;

            case ELogLevel.Warning:
                Debug.LogWarning(message);
                break;

            case ELogLevel.Error:
                Debug.LogError(message);
                break;

            default:
                Debug.Log(message);
                break;
        }
    }

    private string GetHexColor(ELogColor logColor)
    {
        switch (logColor)
        {
            case ELogColor.Red:
                return "#FF0000";

            case ELogColor.Green:
                return "#00FF00";

            case ELogColor.Blue:
                return "#0000FF";

            case ELogColor.Cyan:
                return "#00FFFF";

            case ELogColor.Yellow:
                return "#FFFF00";

            default:
                return "#FFFFFF";
        }
    }
}