using System;

/// <summary>
/// Unity Log
/// </summary>
public class UnityLog : ILog
{
    public void Debug(object message)
    {
        UnityEngine.Debug.Log(message);
    }

    public void Info(object message)
    {
        UnityEngine.Debug.Log(message);
    }

    public void Exception(Exception e)
    {
        UnityEngine.Debug.LogException(e);
    }

    public void Warning(object message)
    {
        UnityEngine.Debug.LogWarning(message);
    }

    public void Error(object message)
    {
        UnityEngine.Debug.LogError(message);
    }
}
