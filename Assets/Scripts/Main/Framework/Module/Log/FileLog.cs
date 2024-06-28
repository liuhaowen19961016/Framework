using System;
using System.IO;
using UnityEngine;

namespace Framework
{
    public enum ELogPlatform
    {
        WinEditor,
        OSXEditor,
        WinPlayer,
        OSXPlayer,
        Android,
        IOS,
    }

    /// <summary>
    /// 文件写日志
    /// </summary>
    public class FileLog : ILog
    {
        private const string LogFileDir = "GameLog";
        private const string LogFileSuffix = "log";
        private const int MaxLogFileCount = 11;

        private ELogPlatform logPlatform;
        private string logFileName;

        private bool isInit;

        private StreamWriter sw;

        public FileLog(string logFileName = "")
        {
            switch (Application.platform)
            {
                case RuntimePlatform.WindowsEditor:
                    logPlatform = ELogPlatform.WinEditor;
                    break;

                case RuntimePlatform.OSXEditor:
                    logPlatform = ELogPlatform.OSXEditor;
                    break;

                case RuntimePlatform.WindowsPlayer:
                    logPlatform = ELogPlatform.WinPlayer;
                    break;

                case RuntimePlatform.OSXPlayer:
                    logPlatform = ELogPlatform.OSXPlayer;
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
                case ELogPlatform.WinEditor:
                case ELogPlatform.OSXEditor:
                    return Application.dataPath + "/../" + LogFileDir;

                case ELogPlatform.Android:
                case ELogPlatform.IOS:
                case ELogPlatform.WinPlayer:
                case ELogPlatform.OSXPlayer:
                    return Path.Combine(Application.persistentDataPath, LogFileDir);

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
}