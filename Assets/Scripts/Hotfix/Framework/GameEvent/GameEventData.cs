public enum EGameEventType
{
    Invalid,

    Test,
}

public class GameEventData : IPoolObject
{
    public EGameEventType gameEventType;

    public virtual void OnInit()
    {
        gameEventType = EGameEventType.Invalid;
    }

    public virtual void OnRecycle()
    {

    }
}

public class GameEventDataPool
{
    public static T Allocate<T>()
        where T : GameEventData, new()
    {
        T data = ReferencePool.Allocate<T>();
        return data;
    }

    public static bool Recycle<T>(GameEventData data)
        where T : GameEventData, new()
    {
        return ReferencePool.Recycle<T>(data as T);
    }
}