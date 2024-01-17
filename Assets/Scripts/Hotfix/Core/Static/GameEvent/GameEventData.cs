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

public class GameEventDataBase : IPoolObject
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
        where T : GameEventDataBase, new()
    {
        T data = ReferencePool.Allocate<T>();
        return data;
    }

    public static bool Recycle<T>(T data)
        where T : GameEventDataBase, new()
    {
        return ReferencePool.Recycle<T>(data);
    }
}