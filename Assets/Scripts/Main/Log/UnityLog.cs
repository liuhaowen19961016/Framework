using System;

public class UnityLog : ILog
{
    public void Debug(string message, params object[] args)
    {
        if (args.Length == 0)
            UnityEngine.Debug.Log(message);
        else
            UnityEngine.Debug.LogFormat(message, args);
    }

    public void Info(string message, params object[] args)
    {
        if (args.Length == 0)
            UnityEngine.Debug.Log(message);
        else
            UnityEngine.Debug.LogFormat(message, args);
    }

    public void Exception(Exception e)
    {
        UnityEngine.Debug.LogException(e);
    }

    public void Warning(string message, params object[] args)
    {
        if (args.Length == 0)
            UnityEngine.Debug.LogWarning(message);
        else
            UnityEngine.Debug.LogWarningFormat(message, args);
    }

    public void Error(string message, params object[] args)
    {
        if (args.Length == 0)
            UnityEngine.Debug.LogError(message);
        else
            UnityEngine.Debug.LogErrorFormat(message, args);
    }
}
