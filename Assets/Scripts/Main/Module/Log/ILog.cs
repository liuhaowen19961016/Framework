using System;

public interface ILog
{
    void Debug(object message);
    void Info(object message);
    void Exception(Exception e);
    void Warning(object message);
    void Error(object message);
}
