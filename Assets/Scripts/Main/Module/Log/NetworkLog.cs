using Main;
using System;

/// <summary>
/// 网络Log
/// </summary>
public class NetworkLog
{
    #region Debug

    public static void Debug(object message)
    {
        if (!GameInit.GameInitSetting.enableNetworkLog)
            return;
        Log.Debug(message);
    }

    public static void Debug(object message, ELogColor logColor)
    {
        if (!GameInit.GameInitSetting.enableNetworkLog)
            return;
        Log.Debug(message, logColor);
    }

    public static void DebugFormat(string message, params object[] args)
    {
        if (!GameInit.GameInitSetting.enableNetworkLog)
            return;
        Log.DebugFormat(message, args);
    }

    public static void DebugFormat(string message, ELogColor logColor, params object[] args)
    {
        if (!GameInit.GameInitSetting.enableNetworkLog)
            return;
        Log.DebugFormat(message, logColor, args);
    }

    #endregion Debug

    #region Info

    public static void Info(object message)
    {
        if (!GameInit.GameInitSetting.enableNetworkLog)
            return;
        Log.Info(message);
    }

    public static void Info(object message, ELogColor logColor)
    {
        if (!GameInit.GameInitSetting.enableNetworkLog)
            return;
        Log.Info(message, logColor);
    }

    public static void InfoFormat(string message, params object[] args)
    {
        if (!GameInit.GameInitSetting.enableNetworkLog)
            return;
        Log.InfoFormat(message, args);
    }

    public static void InfoFormat(string message, ELogColor logColor, params object[] args)
    {
        if (!GameInit.GameInitSetting.enableNetworkLog)
            return;
        Log.InfoFormat(message, logColor, args);
    }

    #endregion Debug

    #region Exception

    public static void Exception(Exception e)
    {
        if (!GameInit.GameInitSetting.enableNetworkLog)
            return;
        Log.Exception(e);
    }

    #endregion Exception

    #region Warning

    public static void Warning(object message)
    {
        if (!GameInit.GameInitSetting.enableNetworkLog)
            return;
        Log.Warning(message);
    }

    public static void Warning(object message, ELogColor logColor)
    {
        if (!GameInit.GameInitSetting.enableNetworkLog)
            return;
        Log.Warning(message, logColor);
    }

    public static void WarningFormat(string message, params object[] args)
    {
        if (!GameInit.GameInitSetting.enableNetworkLog)
            return;
        Log.WarningFormat(message, args);
    }

    public static void WarningFormat(string message, ELogColor logColor, params object[] args)
    {
        if (!GameInit.GameInitSetting.enableNetworkLog)
            return;
        Log.WarningFormat(message, logColor, args);
    }

    #endregion Warning

    #region Error

    public static void Error(object message)
    {
        if (!GameInit.GameInitSetting.enableNetworkLog)
            return;
        Log.Error(message);
    }

    public static void Error(object message, ELogColor logColor)
    {
        if (!GameInit.GameInitSetting.enableNetworkLog)
            return;
        Log.Error(message, logColor);
    }

    public static void ErrorFormat(string message, params object[] args)
    {
        if (!GameInit.GameInitSetting.enableNetworkLog)
            return;
        Log.ErrorFormat(message, args);
    }

    public static void ErrorFormat(string message, ELogColor logColor, params object[] args)
    {
        if (!GameInit.GameInitSetting.enableNetworkLog)
            return;
        Log.ErrorFormat(message, logColor, args);
    }

    #endregion Error
}
