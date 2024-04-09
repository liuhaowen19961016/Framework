using System.Collections.Generic;

/// <summary>
/// log等级
/// </summary>
/// 可自定义添加
public enum ELogLevel
{
    None = 0,

    Error = 100,
    Warning = 200,
    Exception = 300,
    Info = 400,
    Debug = 500,

    Net = 501,
    UI = 502,

    LHW = 600,

    All = 11111,
}

public enum ELogColor
{
    Default,

    Red,
    Green,
    Blue,
    Cyan,
    Yellow,
}

public class LogService
{
    private LogService() { }

    private static ELogLevel mCurLogLevel;
    private static bool mEnable;

    private static List<ILog> logs = new List<ILog>();

    public static void Init(bool enable, ELogLevel logLevel)
    {
        mCurLogLevel = logLevel;
        mEnable = enable;
    }

    public static void SetLogLevel(ELogLevel logLevel)
    {
        mCurLogLevel = logLevel;
    }

    public static void SetEnable(bool enable)
    {
        mEnable = enable;
    }

    public static void Add(ILog log)
    {
        logs.Add(log);
        log.Init();
    }

    public static void Remove(ILog log)
    {
        logs.Remove(log);
    }

    public static void Wirte(string message, ELogLevel logLevel, ELogColor logColor)
    {
        if (!mEnable || mCurLogLevel < logLevel)
            return;

        foreach (var log in logs)
        {
            log.Wirte($"[{logLevel}] {message}", logLevel, logColor);
        }
    }
}
