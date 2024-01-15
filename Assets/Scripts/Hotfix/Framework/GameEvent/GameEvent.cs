using System.Collections.Generic;
using System;

public class GameEvent
{
    private static Dictionary<EGameEventType, Dictionary<int, List<Delegate>>> gameEvents = new();

    #region 添加监听者

    public static bool AddListener(EGameEventType gameEventType, Action handler, int subId = -1)
    {
        return AddListener(gameEventType, (Delegate)handler, subId);
    }
    public static bool AddListener<T>(EGameEventType gameEventType, Action<T> handler, int subId = -1)
    {
        return AddListener(gameEventType, (Delegate)handler, subId);
    }
    public static bool AddListener<T, U>(EGameEventType gameEventType, Action<T, U> handler, int subId = -1)
    {
        return AddListener(gameEventType, (Delegate)handler, subId);
    }
    public static bool AddListener<T, U, V>(EGameEventType gameEventType, Action<T, U, V> handler, int subId = -1)
    {
        return AddListener(gameEventType, (Delegate)handler, subId);
    }
    public static bool AddListener<T, U, V, W>(EGameEventType gameEventType, Action<T, U, V, W> handler, int subId = -1)
    {
        return AddListener(gameEventType, (Delegate)handler, subId);
    }
    public static bool AddListener<T, U, V, W, X>(EGameEventType gameEventType, Action<T, U, V, W, X> handler, int subId = -1)
    {
        return AddListener(gameEventType, (Delegate)handler, subId);
    }
    public static bool AddListener<T, U, V, W, X, Y>(EGameEventType gameEventType, Action<T, U, V, W, X, Y> handler, int subId = -1)
    {
        return AddListener(gameEventType, (Delegate)handler, subId);
    }
    public static bool AddListener<T, U, V, W, X, Y, Z>(EGameEventType gameEventType, Action<T, U, V, W, X, Y, Z> handler, int subId = -1)
    {
        return AddListener(gameEventType, (Delegate)handler, subId);
    }

    #endregion 添加监听者

    #region 移除监听者

    public static bool RemoveListener(EGameEventType gameEventType, Action handler, int subId = -1)
    {
        return RemoveListener(gameEventType, (Delegate)handler, subId);
    }
    public static bool RemoveListener<T>(EGameEventType gameEventType, Action<T> handler, int subId = -1)
    {
        return RemoveListener(gameEventType, (Delegate)handler, subId);
    }
    public static bool RemoveListener<T, U>(EGameEventType gameEventType, Action<T, U> handler, int subId = -1)
    {
        return RemoveListener(gameEventType, (Delegate)handler, subId);
    }
    public static bool RemoveListener<T, U, V>(EGameEventType gameEventType, Action<T, U, V> handler, int subId = -1)
    {
        return RemoveListener(gameEventType, (Delegate)handler, subId);
    }
    public static bool RemoveListener<T, U, V, W>(EGameEventType gameEventType, Action<T, U, V, W> handler, int subId = -1)
    {
        return RemoveListener(gameEventType, (Delegate)handler, subId);
    }
    public static bool RemoveListener<T, U, V, W, X>(EGameEventType gameEventType, Action<T, U, V, W, X> handler, int subId = -1)
    {
        return RemoveListener(gameEventType, (Delegate)handler, subId);
    }
    public static bool RemoveListener<T, U, V, W, X, Y>(EGameEventType gameEventType, Action<T, U, V, W, X, Y> handler, int subId = -1)
    {
        return RemoveListener(gameEventType, (Delegate)handler, subId);
    }
    public static bool RemoveListener<T, U, V, W, X, Y, Z>(EGameEventType gameEventType, Action<T, U, V, W, X, Y, Z> handler, int subId = -1)
    {
        return RemoveListener(gameEventType, (Delegate)handler, subId);
    }

    public static void RemoveAllListener()
    {
        gameEvents.Clear();
    }

    #endregion 移除监听者

    #region 发送事件

    public static void DispatchGameEvent<T>(EGameEventType gameEventType, T arg1, int subId = -1)
        where T : GameEventDataBase, new()
    {
        var handlers = GetHandlers(gameEventType, subId);
        if (handlers == null)
            return;

        foreach (var handler in handlers)
        {
            var callback = handler as Action<T>;
            callback?.Invoke(arg1);
        }

        GameEventDataPool.Recycle(arg1);
    }
    public static void Dispatch(EGameEventType gameEventType, int subId = -1)
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
    public static void Dispatch<T>(EGameEventType gameEventType, T arg1, int subId = -1)
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
    public static void Dispatch<T, U>(EGameEventType gameEventType, T arg1, U arg2, int subId = -1)
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
    public static void Dispatch<T, U, V>(EGameEventType gameEventType, T arg1, U arg2, V arg3, int subId = -1)
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
    public static void Dispatch<T, U, V, W>(EGameEventType gameEventType, T arg1, U arg2, V arg3, W arg4, int subId = -1)
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
    public static void Dispatch<T, U, V, W, X>(EGameEventType gameEventType, T arg1, U arg2, V arg3, W arg4, X arg5, int subId = -1)
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
    public static void Dispatch<T, U, V, W, X, Y>(EGameEventType gameEventType, T arg1, U arg2, V arg3, W arg4, X arg5, Y arg6, int subId = -1)
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
    public static void Dispatch<T, U, V, W, X, Y, Z>(EGameEventType gameEventType, T arg1, U arg2, V arg3, W arg4, X arg5, Y arg6, Z arg7, int subId = -1)
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

    private static bool AddListener(EGameEventType gameEventType, Delegate handler, int subId = -1)
    {
        if (handler == null)
            return false;
        if (!gameEvents.TryGetValue(gameEventType, out var handlerDict))
        {
            handlerDict = new Dictionary<int, List<Delegate>>();
            gameEvents.Add(gameEventType, handlerDict);
        }
        if (!handlerDict.TryGetValue(subId, out List<Delegate> handlers))
        {
            handlers = new List<Delegate>();
            handlerDict[subId] = handlers;
        }
        handlers.Add(handler);
        return true;
    }

    private static bool RemoveListener(EGameEventType gameEventType, Delegate handler, int subId = -1)
    {
        if (!gameEvents.TryGetValue(gameEventType, out Dictionary<int, List<Delegate>> handlerDict))
            return false;
        if (!handlerDict.TryGetValue(subId, out List<Delegate> handlers))
            return false;

        handlers.Remove(handler);

        if (handlers.Count <= 0)
        {
            handlerDict.Remove(subId);
            if (handlerDict.Count <= 0)
            {
                gameEvents.Remove(gameEventType);
            }
        }
        return true;
    }

    private static List<Delegate> GetHandlers(EGameEventType gameEventType, int subId = -1)
    {
        if (!gameEvents.TryGetValue(gameEventType, out Dictionary<int, List<Delegate>> handlerDict))
            return null;
        if (!handlerDict.TryGetValue(subId, out List<Delegate> handlers))
            return null;

        return handlers;
    }
}