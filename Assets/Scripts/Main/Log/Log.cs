using System;

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

public class Log
{
    private static ILog log = new UnityLog();

    private static ELogLevel CurLogLevel = ELogLevel.All;

    public static void Init(ELogLevel logLevel)
    {
        CurLogLevel = logLevel;
    }

    public static void Debug(string message, params object[] args)
    {
        if (CurLogLevel < ELogLevel.Debug)
            return;
        log.Debug(message, args);
    }

    public static void Debug(string message, ELogColor logColor, params object[] args)
    {
        if (CurLogLevel < ELogLevel.Debug)
            return;
        message = GetString(message, args);
        message = string.Format("<color={0}>{1}</color>", GetHexColor(logColor), message);
        log.Debug(message);
    }

    public static void Info(string message, params object[] args)
    {
        if (CurLogLevel < ELogLevel.Info)
            return;
        log.Info(message, args);
    }

    public static void Info(string message, ELogColor logColor, params object[] args)
    {
        if (CurLogLevel < ELogLevel.Info)
            return;
        message = GetString(message, args);
        message = string.Format("<color={0}>{1}</color>", GetHexColor(logColor), message);
        log.Info(message, args);
    }

    public static void Exception(Exception e)
    {
        if (CurLogLevel < ELogLevel.Exception)
            return;
        log.Exception(e);
    }

    public static void Warning(string message, params object[] args)
    {
        if (CurLogLevel < ELogLevel.Warning)
            return;
        log.Warning(message, args);
    }

    public static void Warning(string message, ELogColor logColor, params object[] args)
    {
        if (CurLogLevel < ELogLevel.Warning)
            return;
        message = GetString(message, args);
        message = string.Format("<color={0}>{1}</color>", GetHexColor(logColor), message);
        log.Warning(message, args);
    }

    public static void Error(string message, params object[] args)
    {
        if (CurLogLevel < ELogLevel.Error)
            return;
        log.Error(message, args);
    }

    public static void Error(string message, ELogColor logColor, params object[] args)
    {
        if (CurLogLevel < ELogLevel.Error)
            return;
        message = GetString(message, args);
        message = string.Format("<color={0}>{1}</color>", GetHexColor(logColor), message);
        log.Error(message, args);
    }

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

    private static string GetString(string message, params object[] args)
    {
        string ret;
        try
        {
            ret = string.Format(message, args);
        }
        catch
        {
            ret = message;
        }
        return ret;
    }
}
