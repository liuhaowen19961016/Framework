using Main;
using System;
using System.IO;
using UnityEngine;

public enum ELogPlatform
{
    Android,
    IOS,
    Win,
    MACOS,
}

/// <summary>
/// 文件写日志
/// </summary>
public class FileLog : ILog
{
    private const string LogFileDir = "GameLog";
    private const string LogFileSuffix = "log";
    private int MaxLogFileCount = 11;

    private ELogPlatform logPlatform;
    private string logFileName;

    private bool isInit;

    private StreamWriter sw;

    public FileLog(string logFileName = "")
    {
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.WindowsEditor:
                logPlatform = ELogPlatform.Win;
                break;
            case RuntimePlatform.Android:
                logPlatform = ELogPlatform.Android;
                break;
            case RuntimePlatform.IPhonePlayer:
                logPlatform = ELogPlatform.IOS;
                break;
            default:
                break;
        }
        this.logFileName = logFileName;
    }

    public void Init()
    {
        if (isInit)
            return;

        try
        {
            string logFileDir = GetFileDir();
            if (!Directory.Exists(logFileDir))
            {
                Directory.CreateDirectory(logFileDir);
            }

            string[] logFilePaths = GetLogFilePaths(logFileDir);
            if (logFilePaths.Length > MaxLogFileCount)
            {
                foreach (var filePath in logFilePaths)
                {
                    File.Delete(filePath);
                }
            }

            string logFileName = $"{(string.IsNullOrEmpty(this.logFileName) ? "GameLog" : this.logFileName)}" +
                $" {DateTime.Now.Year}_{DateTime.Now.Month}_{DateTime.Now.Day} {DateTime.Now.Hour}_{DateTime.Now.Minute}_{DateTime.Now.Second}.{LogFileSuffix}";
            FileStream fs = new FileStream(Path.Combine(logFileDir, logFileName), FileMode.OpenOrCreate);
            sw = new StreamWriter(fs);
            sw.WriteLine($"LogFileName：{(string.IsNullOrEmpty(this.logFileName) ? "GameLog" : this.logFileName)}");
            sw.WriteLine($"DateTime：{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day} {DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}");
            sw.WriteLine();
            sw.AutoFlush = true;
        }
        catch
        {
            sw = null;
        }
        isInit = true;
    }

    public void Wirte(string message, ELogLevel logLevel, ELogColor logColor = ELogColor.Default)
    {
        if (sw == null)
            return;

        try
        {
            string dtStr = DateTime.Now.ToString();
            sw.WriteLine($"{dtStr} {message}");
        }
        catch
        {

        }
    }

    private string GetFileDir()
    {
        switch (logPlatform)
        {
            case ELogPlatform.Android:
                return Path.Combine(Application.persistentDataPath, LogFileDir);
            case ELogPlatform.IOS:
                return Path.Combine(Application.temporaryCachePath, LogFileDir);
            case ELogPlatform.Win:
            case ELogPlatform.MACOS:
                return Application.dataPath + "/../" + LogFileDir;
            default:
                return Application.dataPath + "/../" + LogFileDir;
        }
    }

    private static string[] GetLogFilePaths(string logFileDir)
    {
        string[] logFilePaths = Directory.GetFiles(logFileDir, $"*.{LogFileSuffix}", SearchOption.TopDirectoryOnly);
        return logFilePaths;
    }
}
