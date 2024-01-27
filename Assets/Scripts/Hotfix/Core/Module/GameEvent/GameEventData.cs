using System;
using System.Collections.Generic;

public class GameEventData
{
    private EGameEventType gameEventType;
    private Dictionary<int, List<Delegate>> handlers = new();
    private List<Delegate> executedHandlers = new();

    public GameEventData(EGameEventType gameEventType)
    {
        this.gameEventType = gameEventType;
    }

    #region 添加监听者

    public void AddListener(Delegate callback, int subId = -1)
    {
        if (!handlers.TryGetValue(subId, out List<Delegate> list))
        {
            list = new List<Delegate>();
            handlers.Add(subId, list);
        }
        list.Add(callback);
    }

    #endregion 添加监听者

    #region 移除监听者

    public void RemoveListener(Delegate callback, int subId = -1)
    {
        if (!handlers.TryGetValue(subId, out List<Delegate> callbacks))
            return;

        callbacks.Remove(callback);
    }

    #endregion 移除监听者

    #region 分发事件

    public void Dispatch(int subId = -1)
    {
        if (!handlers.TryGetValue(subId, out List<Delegate> callbacks))
            return;

        int count = callbacks.Count;
        for (int i = 0; i < callbacks.Count; i++)
        {
            if (callbacks.Count != count)
            {
                i--;
            }
            var callback = callbacks[i] as Action;
            callback?.Invoke();
        }
    }
    public void Dispatch<T>(T arg1, int subId = -1)
    {
        if (!handlers.TryGetValue(subId, out List<Delegate> callbacks))
            return;

        for (int i = 0; i < callbacks.Count; i++)
        {
            executedHandlers.Add(callbacks[i]);
            var callback = callbacks[i] as Action<T>;
            callback?.Invoke(arg1);
        }

        for (int i = 0; i < callbacks.Count; i++)
        {
            if (executedHandlers.Contains(callbacks[i]))
                continue;
            var callback = callbacks[i] as Action<T>;
            callback?.Invoke(arg1);
        }
        executedHandlers.Clear();
    }
    public void Dispatch<T, U>(T arg1, U arg2, int subId = -1)
    {
        if (!handlers.TryGetValue(subId, out List<Delegate> callbacks))
            return;

        for (int i = 0; i < callbacks.Count; i++)
        {
            executedHandlers.Add(callbacks[i]);
            var callback = callbacks[i] as Action<T, U>;
            callback?.Invoke(arg1, arg2);
        }

        for (int i = 0; i < callbacks.Count; i++)
        {
            if (executedHandlers.Contains(callbacks[i]))
                continue;
            var callback = callbacks[i] as Action<T, U>;
            callback?.Invoke(arg1, arg2);
        }
        executedHandlers.Clear();
    }
    public void Dispatch<T, U, V>(T arg1, U arg2, V arg3, int subId = -1)
    {
        if (!handlers.TryGetValue(subId, out List<Delegate> callbacks))
            return;

        for (int i = 0; i < callbacks.Count; i++)
        {
            executedHandlers.Add(callbacks[i]);
            var callback = callbacks[i] as Action<T, U, V>;
            callback?.Invoke(arg1, arg2, arg3);
        }

        for (int i = 0; i < callbacks.Count; i++)
        {
            if (executedHandlers.Contains(callbacks[i]))
                continue;
            var callback = callbacks[i] as Action<T, U, V>;
            callback?.Invoke(arg1, arg2, arg3);
        }
        executedHandlers.Clear();
    }
    public void Dispatch<T, U, V, W>(T arg1, U arg2, V arg3, W arg4, int subId = -1)
    {
        if (!handlers.TryGetValue(subId, out List<Delegate> callbacks))
            return;

        for (int i = 0; i < callbacks.Count; i++)
        {
            executedHandlers.Add(callbacks[i]);
            var callback = callbacks[i] as Action<T, U, V, W>;
            callback?.Invoke(arg1, arg2, arg3, arg4);
        }

        for (int i = 0; i < callbacks.Count; i++)
        {
            if (executedHandlers.Contains(callbacks[i]))
                continue;
            var callback = callbacks[i] as Action<T, U, V, W>;
            callback?.Invoke(arg1, arg2, arg3, arg4);
        }
        executedHandlers.Clear();
    }
    public void Dispatch<T, U, V, W, X>(T arg1, U arg2, V arg3, W arg4, X arg5, int subId = -1)
    {
        if (!handlers.TryGetValue(subId, out List<Delegate> callbacks))
            return;

        for (int i = 0; i < callbacks.Count; i++)
        {
            executedHandlers.Add(callbacks[i]);
            var callback = callbacks[i] as Action<T, U, V, W, X>;
            callback?.Invoke(arg1, arg2, arg3, arg4, arg5);
        }

        for (int i = 0; i < callbacks.Count; i++)
        {
            if (executedHandlers.Contains(callbacks[i]))
                continue;
            var callback = callbacks[i] as Action<T, U, V, W, X>;
            callback?.Invoke(arg1, arg2, arg3, arg4, arg5);
        }
        executedHandlers.Clear();
    }
    public void Dispatch<T, U, V, W, X, Y>(T arg1, U arg2, V arg3, W arg4, X arg5, Y arg6, int subId = -1)
    {
        if (!handlers.TryGetValue(subId, out List<Delegate> callbacks))
            return;

        for (int i = 0; i < callbacks.Count; i++)
        {
            executedHandlers.Add(callbacks[i]);
            var callback = callbacks[i] as Action<T, U, V, W, X, Y>;
            callback?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6);
        }

        for (int i = 0; i < callbacks.Count; i++)
        {
            if (executedHandlers.Contains(callbacks[i]))
                continue;
            var callback = callbacks[i] as Action<T, U, V, W, X, Y>;
            callback?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6);
        }
        executedHandlers.Clear();
    }
    public void Dispatch<T, U, V, W, X, Y, Z>(T arg1, U arg2, V arg3, W arg4, X arg5, Y arg6, Z arg7, int subId = -1)
    {
        if (!handlers.TryGetValue(subId, out List<Delegate> callbacks))
            return;

        for (int i = 0; i < callbacks.Count; i++)
        {
            executedHandlers.Add(callbacks[i]);
            var callback = callbacks[i] as Action<T, U, V, W, X, Y, Z>;
            callback?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }

        for (int i = 0; i < callbacks.Count; i++)
        {
            if (executedHandlers.Contains(callbacks[i]))
                continue;
            var callback = callbacks[i] as Action<T, U, V, W, X, Y, Z>;
            callback?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }
        executedHandlers.Clear();
    }

    #endregion 分发事件
}
