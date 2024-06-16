using System;
using System.Globalization;

public class Log
{
    private Log() { }

    #region Log方法

    public static void Line(object message, ELogLevel logLevel, ELogColor logColor = ELogColor.Default)
    {
        string str = GetString(message);
        LogService.Wirte(str, logLevel, logColor);
    }

    public static void Debug(object message, ELogColor logColor = ELogColor.Default)
    {
        Line(message, ELogLevel.Debug, logColor);
    }

    public static void Info(object message, ELogColor logColor = ELogColor.Default)
    {
        Line(message, ELogLevel.Info, logColor);
    }

    public static void Expection(Exception exception, ELogColor logColor = ELogColor.Default)
    {
        Line(exception.ToString(), ELogLevel.Exception, logColor);
    }

    public static void Warning(object message, ELogColor logColor = ELogColor.Default)
    {
        Line(message, ELogLevel.Warning, logColor);
    }

    public static void Error(object message, ELogColor logColor = ELogColor.Default)
    {
        Line(message, ELogLevel.Error, logColor);
    }

    #endregion Log方法

    #region 工具方法

    private static string GetString(object message)
    {
        string ret = "Null";
        if (message == null)
            return ret;
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

    #endregion 工具方法
}
