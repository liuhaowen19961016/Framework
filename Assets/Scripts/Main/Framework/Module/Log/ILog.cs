public interface ILog
{
    void Init();

    void Wirte(string message, ELogLevel logLevel, ELogColor logColor = ELogColor.Default);
}