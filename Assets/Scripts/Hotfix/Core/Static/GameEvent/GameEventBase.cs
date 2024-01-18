public enum EGameEventType
{
    Invalid,

    #region UI

    EnqueueFlyRewardData,
    PlayFlyRewardGroupComplete,
    PlayFlyRewardSingleTypeComplete,
    PlayFlyRewardSingleOnceComplete,

    #endregion UI
}

public class GameEventBase : IPoolObject
{
    public EGameEventType gameEventType;

    public virtual void OnInit()
    {
        gameEventType = EGameEventType.Invalid;
    }

    public virtual void OnRecycle()
    {

    }

    public virtual void OnDispose()
    {

    }
}

public class GameEventPool
{
    public static T Allocate<T>()
        where T : GameEventBase, new()
    {
        T data = ReferencePool.Allocate<T>();
        return data;
    }

    public static bool Recycle<T>(T data)
        where T : GameEventBase, new()
    {
        return ReferencePool.Recycle<T>(data);
    }
}