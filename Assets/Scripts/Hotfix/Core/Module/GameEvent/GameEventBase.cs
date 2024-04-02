public enum EGameEventType
{
    Invalid,

    #region UI

    AddFlyObjData,//添加飞物体
    FlyObjGroupComplete,//完成本组飞物体
    FlyObjSingleComplete,//完成单个飞物体

    #endregion UI
}

public class GameEventBase : IReferencePoolObject
{
    public EGameEventType gameEventType;

    public virtual void OnCreate()
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