using System.Collections.Generic;
using System;

public class Event
{
    private static Dictionary<EEventType, Dictionary<int, List<Delegate>>> events = new();

    #region 添加监听者

    public static bool AddListener(EEventType gameEventType, Action handler, int subId = -1)
    {
        return AddListener(gameEventType, (Delegate)handler, subId);
    }
    public static bool AddListener<T>(EEventType gameEventType, Action<T> handler, int subId = -1)
    {
        return AddListener(gameEventType, (Delegate)handler, subId);
    }
    public static bool AddListener<T, U>(EEventType gameEventType, Action<T, U> handler, int subId = -1)
    {
        return AddListener(gameEventType, (Delegate)handler, subId);
    }
    public static bool AddListener<T, U, V>(EEventType gameEventType, Action<T, U, V> handler, int subId = -1)
    {
        return AddListener(gameEventType, (Delegate)handler, subId);
    }
    public static bool AddListener<T, U, V, W>(EEventType gameEventType, Action<T, U, V, W> handler, int subId = -1)
    {
        return AddListener(gameEventType, (Delegate)handler, subId);
    }
    public static bool AddListener<T, U, V, W, X>(EEventType gameEventType, Action<T, U, V, W, X> handler, int subId = -1)
    {
        return AddListener(gameEventType, (Delegate)handler, subId);
    }
    public static bool AddListener<T, U, V, W, X, Y>(EEventType gameEventType, Action<T, U, V, W, X, Y> handler, int subId = -1)
    {
        return AddListener(gameEventType, (Delegate)handler, subId);
    }
    public static bool AddListener<T, U, V, W, X, Y, Z>(EEventType gameEventType, Action<T, U, V, W, X, Y, Z> handler, int subId = -1)
    {
        return AddListener(gameEventType, (Delegate)handler, subId);
    }

    #endregion 添加监听者

    #region 移除监听者

    public static bool RemoveListener(EEventType gameEventType, Action handler, int subId = -1)
    {
        return RemoveListener(gameEventType, (Delegate)handler, subId);
    }
    public static bool RemoveListener<T>(EEventType gameEventType, Action<T> handler, int subId = -1)
    {
        return RemoveListener(gameEventType, (Delegate)handler, subId);
    }
    public static bool RemoveListener<T, U>(EEventType gameEventType, Action<T, U> handler, int subId = -1)
    {
        return RemoveListener(gameEventType, (Delegate)handler, subId);
    }
    public static bool RemoveListener<T, U, V>(EEventType gameEventType, Action<T, U, V> handler, int subId = -1)
    {
        return RemoveListener(gameEventType, (Delegate)handler, subId);
    }
    public static bool RemoveListener<T, U, V, W>(EEventType gameEventType, Action<T, U, V, W> handler, int subId = -1)
    {
        return RemoveListener(gameEventType, (Delegate)handler, subId);
    }
    public static bool RemoveListener<T, U, V, W, X>(EEventType gameEventType, Action<T, U, V, W, X> handler, int subId = -1)
    {
        return RemoveListener(gameEventType, (Delegate)handler, subId);
    }
    public static bool RemoveListener<T, U, V, W, X, Y>(EEventType gameEventType, Action<T, U, V, W, X, Y> handler, int subId = -1)
    {
        return RemoveListener(gameEventType, (Delegate)handler, subId);
    }
    public static bool RemoveListener<T, U, V, W, X, Y, Z>(EEventType gameEventType, Action<T, U, V, W, X, Y, Z> handler, int subId = -1)
    {
        return RemoveListener(gameEventType, (Delegate)handler, subId);
    }

    public static void RemoveAllListener()
    {
        events.Clear();
    }

    #endregion 移除监听者

    #region 发送事件

    public static void DispatchGameEvent<T>(T data, int subId = -1)
        where T : EventDataBase, new()
    {
        var handlers = GetHandlers(data.gameEventType, subId);
        if (handlers == null)
            return;

        foreach (var handler in handlers)
        {
            var callback = handler as Action<T>;
            callback?.Invoke(data);
        }

        EventDataPool.Recycle(data);
    }
    public static void Dispatch(EEventType gameEventType, int subId = -1)
    {
        var handlers = GetHandlers(gameEventType, subId);
        if (handlers == null)
            return;

        foreach (var handler in handlers)
        {
            var callback = handler as Action;
            callback?.Invoke();
        }
    }
    public static void Dispatch<T>(EEventType gameEventType, T arg1, int subId = -1)
    {
        var handlers = GetHandlers(gameEventType, subId);
        if (handlers == null)
            return;

        foreach (var handler in handlers)
        {
            var callback = handler as Action<T>;
            callback?.Invoke(arg1);
        }
    }
    public static void Dispatch<T, U>(EEventType gameEventType, T arg1, U arg2, int subId = -1)
    {
        var handlers = GetHandlers(gameEventType, subId);
        if (handlers == null)
            return;

        foreach (var handler in handlers)
        {
            var callback = handler as Action<T, U>;
            callback?.Invoke(arg1, arg2);
        }
    }
    public static void Dispatch<T, U, V>(EEventType gameEventType, T arg1, U arg2, V arg3, int subId = -1)
    {
        var handlers = GetHandlers(gameEventType, subId);
        if (handlers == null)
            return;

        foreach (var handler in handlers)
        {
            var callback = handler as Action<T, U, V>;
            callback?.Invoke(arg1, arg2, arg3);
        }
    }
    public static void Dispatch<T, U, V, W>(EEventType gameEventType, T arg1, U arg2, V arg3, W arg4, int subId = -1)
    {
        var handlers = GetHandlers(gameEventType, subId);
        if (handlers == null)
            return;

        foreach (var handler in handlers)
        {
            var callback = handler as Action<T, U, V, W>;
            callback?.Invoke(arg1, arg2, arg3, arg4);
        }
    }
    public static void Dispatch<T, U, V, W, X>(EEventType gameEventType, T arg1, U arg2, V arg3, W arg4, X arg5, int subId = -1)
    {
        var handlers = GetHandlers(gameEventType, subId);
        if (handlers == null)
            return;

        foreach (var handler in handlers)
        {
            var callback = handler as Action<T, U, V, W, X>;
            callback?.Invoke(arg1, arg2, arg3, arg4, arg5);
        }
    }
    public static void Dispatch<T, U, V, W, X, Y>(EEventType gameEventType, T arg1, U arg2, V arg3, W arg4, X arg5, Y arg6, int subId = -1)
    {
        var handlers = GetHandlers(gameEventType, subId);
        if (handlers == null)
            return;

        foreach (var handler in handlers)
        {
            var callback = handler as Action<T, U, V, W, X, Y>;
            callback?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6);
        }
    }
    public static void Dispatch<T, U, V, W, X, Y, Z>(EEventType gameEventType, T arg1, U arg2, V arg3, W arg4, X arg5, Y arg6, Z arg7, int subId = -1)
    {
        var handlers = GetHandlers(gameEventType, subId);
        if (handlers == null)
            return;

        foreach (var handler in handlers)
        {
            var callback = handler as Action<T, U, V, W, X, Y, Z>;
            callback?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }
    }

    #endregion 发送事件

    private static bool AddListener(EEventType gameEventType, Delegate handler, int subId = -1)
    {
        if (handler == null)
            return false;
        if (!events.TryGetValue(gameEventType, out var handlerDict))
        {
            handlerDict = new Dictionary<int, List<Delegate>>();
            events.Add(gameEventType, handlerDict);
        }
        if (!handlerDict.TryGetValue(subId, out List<Delegate> handlers))
        {
            handlers = new List<Delegate>();
            handlerDict[subId] = handlers;
        }
        handlers.Add(handler);
        return true;
    }

    private static bool RemoveListener(EEventType gameEventType, Delegate handler, int subId = -1)
    {
        if (!events.TryGetValue(gameEventType, out Dictionary<int, List<Delegate>> handlerDict))
            return false;
        if (!handlerDict.TryGetValue(subId, out List<Delegate> handlers))
            return false;

        handlers.Remove(handler);

        if (handlers.Count <= 0)
        {
            handlerDict.Remove(subId);
            if (handlerDict.Count <= 0)
            {
                events.Remove(gameEventType);
            }
        }
        return true;
    }

    private static List<Delegate> GetHandlers(EEventType gameEventType, int subId = -1)
    {
        if (!events.TryGetValue(gameEventType, out Dictionary<int, List<Delegate>> handlerDict))
            return null;
        if (!handlerDict.TryGetValue(subId, out List<Delegate> handlers))
            return null;

        return handlers;
    }
}