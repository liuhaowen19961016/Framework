using System;

public interface ILog
{
    void Debug(string message, params object[] args);
    void Info(string message, params object[] args);
    void Exception(Exception e);
    void Warning(string message, params object[] args);
    void Error(string message, params object[] args);
}
