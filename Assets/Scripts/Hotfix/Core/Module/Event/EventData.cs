public enum EEventType
{
    Invalid,

    #region UI

    EnqueueFlyRewardData,
    PlayFlyRewardGroupComplete,
    PlayFlyRewardSingleTypeComplete,
    PlayFlyRewardSingleOnceComplete,

    #endregion UI
}

public class EventDataBase : IPoolObject
{
    public EEventType gameEventType;

    public virtual void OnInit()
    {
        gameEventType = EEventType.Invalid;
    }

    public virtual void OnRecycle()
    {

    }
}

public class EventDataPool
{
    public static T Allocate<T>()
        where T : EventDataBase, new()
    {
        T data = ReferencePool.Allocate<T>();
        return data;
    }

    public static bool Recycle<T>(T data)
        where T : EventDataBase, new()
    {
        return ReferencePool.Recycle<T>(data);
    }
}