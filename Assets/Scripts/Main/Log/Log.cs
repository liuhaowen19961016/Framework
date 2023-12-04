using System;
using System.Globalization;

public enum ELogLevel
{
    None = 0,
    Debug,
    Info,
    Exception,
    Warning,
    Error,
    All,
}

public enum ELogColor
{
    Red,
    Green,
    Blue,
    Cyan,
    Yellow,
}

/// <summary>
/// Logϵͳ
/// </summary>
public class Log
{
    private static ILog log = new UnityLog();

    private static ELogLevel CurLogLevel = ELogLevel.All;

    public static void Init(ELogLevel logLevel)
    {
        CurLogLevel = logLevel;
    }

    #region Debug

    public static void Debug(object message)
    {
        if (CurLogLevel < ELogLevel.Debug)
            return;
        message = GetString(message);
        log.Debug(message);
    }

    public static void Debug(object message, ELogColor logColor)
    {
        if (CurLogLevel < ELogLevel.Debug)
            return;
        message = GetString(message);
        message = string.Format("<color={0}>{1}</color>", GetHexColor(logColor), message);
        log.Debug(message);
    }

    public static void DebugFormat(string message, params object[] args)
    {
        if (CurLogLevel < ELogLevel.Debug)
            return;
        message = GetString(message, args);
        log.Debug(message);
    }

    public static void DebugFormat(string message, ELogColor logColor, params object[] args)
    {
        if (CurLogLevel < ELogLevel.Debug)
            return;
        message = GetString(message, args);
        message = string.Format("<color={0}>{1}</color>", GetHexColor(logColor), message);
        log.Debug(message);
    }

    #endregion Debug

    #region Info

    public static void Info(object message)
    {
        if (CurLogLevel < ELogLevel.Info)
            return;
        message = GetString(message);
        log.Info(message);
    }

    public static void Info(object message, ELogColor logColor)
    {
        if (CurLogLevel < ELogLevel.Info)
            return;
        message = GetString(message);
        message = string.Format("<color={0}>{1}</color>", GetHexColor(logColor), message);
        log.Info(message);
    }

    public static void InfoFormat(string message, params object[] args)
    {
        if (CurLogLevel < ELogLevel.Info)
            return;
        message = GetString(message, args);
        log.Info(message);
    }

    public static void InfoFormat(string message, ELogColor logColor, params object[] args)
    {
        if (CurLogLevel < ELogLevel.Info)
            return;
        message = GetString(message, args);
        message = string.Format("<color={0}>{1}</color>", GetHexColor(logColor), message);
        log.Info(message);
    }

    #endregion Debug

    #region Exception

    public static void Exception(Exception e)
    {
        if (CurLogLevel < ELogLevel.Exception)
            return;
        log.Exception(e);
    }

    #endregion Exception

    #region Warning

    public static void Warning(object message)
    {
        if (CurLogLevel < ELogLevel.Warning)
            return;
        message = GetString(message);
        log.Warning(message);
    }

    public static void Warning(object message, ELogColor logColor)
    {
        if (CurLogLevel < ELogLevel.Warning)
            return;
        message = GetString(message);
        message = string.Format("<color={0}>{1}</color>", GetHexColor(logColor), message);
        log.Warning(message);
    }

    public static void WarningFormat(string message, params object[] args)
    {
        if (CurLogLevel < ELogLevel.Info)
            return;
        message = GetString(message, args);
        log.Warning(message);
    }

    public static void WarningFormat(string message, ELogColor logColor, params object[] args)
    {
        if (CurLogLevel < ELogLevel.Warning)
            return;
        message = GetString(message, args);
        message = string.Format("<color={0}>{1}</color>", GetHexColor(logColor), message);
        log.Warning(message);
    }

    #endregion Warning

    #region Error

    public static void Error(object message)
    {
        if (CurLogLevel < ELogLevel.Error)
            return;
        message = GetString(message);
        log.Error(message);
    }

    public static void Error(object message, ELogColor logColor)
    {
        if (CurLogLevel < ELogLevel.Error)
            return;
        message = GetString(message);
        message = string.Format("<color={0}>{1}</color>", GetHexColor(logColor), message);
        log.Error(message);
    }

    public static void ErrorFormat(string message, params object[] args)
    {
        if (CurLogLevel < ELogLevel.Error)
            return;
        message = GetString(message, args);
        log.Error(message);
    }

    public static void ErrorFormat(string message, ELogColor logColor, params object[] args)
    {
        if (CurLogLevel < ELogLevel.Error)
            return;
        message = GetString(message, args);
        message = string.Format("<color={0}>{1}</color>", GetHexColor(logColor), message);
        log.Error(message);
    }

    #endregion Error

    private static string GetHexColor(ELogColor logColor)
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

    private static string GetString(object message)
    {
        string ret = "Null";
        if (message == null)
        {
            return ret;
        }
        IFormattable formattable = message as IFormattable;
        if (formattable != null)
        {
            ret = formattable.ToString(null, CultureInfo.InvariantCulture);
            return ret;
        }
        ret = message.ToString();
        return ret;
    }

    private static string GetString(string message, params object[] args)
    {
        string ret = string.Format(message, args);
        return ret;
    }
}
