namespace Framework
{
    public enum EEventType
    {
        Invalid,

        #region UI

        AddFlyObjData, //添加飞物体
        FlyObjGroupComplete, //完成本组飞物体
        FlyObjSingleComplete, //完成单个飞物体

        #endregion UI
    }

    public class EventBase : IReferencePoolObject
    {
        public EEventType EventType;

        public virtual void OnCreate()
        {
            EventType = EEventType.Invalid;
        }

        public virtual void OnRecycle()
        {
        }

        public virtual void OnDispose()
        {
        }
    }

    public class EventPool
    {
        public static T Allocate<T>()
            where T : EventBase, new()
        {
            T data = ReferencePool.Allocate<T>();
            return data;
        }

        public static bool Recycle<T>(T data)
            where T : EventBase, new()
        {
            return ReferencePool.Recycle<T>(data);
        }
    }
}