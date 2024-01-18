using System.Collections.Generic;
using System;

public class GameEvent
{
    private Dictionary<EGameEventType, GameEventData> gameEventDatas = new();

    #region 添加监听者

    public void AddListener(EGameEventType gameEventType, Action handler, int subId = -1)
    {
        AddListener(gameEventType, (Delegate)handler, subId);
    }
    public void AddListener<T>(EGameEventType gameEventType, Action<T> handler, int subId = -1)
    {
        AddListener(gameEventType, (Delegate)handler, subId);
    }
    public void AddListener<T, U>(EGameEventType gameEventType, Action<T, U> handler, int subId = -1)
    {
        AddListener(gameEventType, (Delegate)handler, subId);
    }
    public void AddListener<T, U, V>(EGameEventType gameEventType, Action<T, U, V> handler, int subId = -1)
    {
        AddListener(gameEventType, (Delegate)handler, subId);
    }
    public void AddListener<T, U, V, W>(EGameEventType gameEventType, Action<T, U, V, W> handler, int subId = -1)
    {
        AddListener(gameEventType, (Delegate)handler, subId);
    }
    public void AddListener<T, U, V, W, X>(EGameEventType gameEventType, Action<T, U, V, W, X> handler, int subId = -1)
    {
        AddListener(gameEventType, (Delegate)handler, subId);
    }
    public void AddListener<T, U, V, W, X, Y>(EGameEventType gameEventType, Action<T, U, V, W, X, Y> handler, int subId = -1)
    {
        AddListener(gameEventType, (Delegate)handler, subId);
    }
    public void AddListener<T, U, V, W, X, Y, Z>(EGameEventType gameEventType, Action<T, U, V, W, X, Y, Z> handler, int subId = -1)
    {
        AddListener(gameEventType, (Delegate)handler, subId);
    }

    #endregion 添加监听者

    #region 移除监听者

    public void RemoveListener(EGameEventType gameEventType, Action handler, int subId = -1)
    {
        RemoveListener(gameEventType, (Delegate)handler, subId);
    }
    public void RemoveListener<T>(EGameEventType gameEventType, Action<T> handler, int subId = -1)
    {
        RemoveListener(gameEventType, (Delegate)handler, subId);
    }
    public void RemoveListener<T, U>(EGameEventType gameEventType, Action<T, U> handler, int subId = -1)
    {
        RemoveListener(gameEventType, (Delegate)handler, subId);
    }
    public void RemoveListener<T, U, V>(EGameEventType gameEventType, Action<T, U, V> handler, int subId = -1)
    {
        RemoveListener(gameEventType, (Delegate)handler, subId);
    }
    public void RemoveListener<T, U, V, W>(EGameEventType gameEventType, Action<T, U, V, W> handler, int subId = -1)
    {
        RemoveListener(gameEventType, (Delegate)handler, subId);
    }
    public void RemoveListener<T, U, V, W, X>(EGameEventType gameEventType, Action<T, U, V, W, X> handler, int subId = -1)
    {
        RemoveListener(gameEventType, (Delegate)handler, subId);
    }
    public void RemoveListener<T, U, V, W, X, Y>(EGameEventType gameEventType, Action<T, U, V, W, X, Y> handler, int subId = -1)
    {
        RemoveListener(gameEventType, (Delegate)handler, subId);
    }
    public void RemoveListener<T, U, V, W, X, Y, Z>(EGameEventType gameEventType, Action<T, U, V, W, X, Y, Z> handler, int subId = -1)
    {
        RemoveListener(gameEventType, (Delegate)handler, subId);
    }

    public void RemoveAllListener()
    {
        gameEventDatas.Clear();
    }

    #endregion 移除监听者

    #region 分发事件

    public void DispatchGameEvent<T>(T data, int subId = -1)
        where T : GameEventBase, new()
    {
        if (!gameEventDatas.TryGetValue(data.gameEventType, out GameEventData gameEventData))
            return;

        gameEventData.Dispatch<T>(data, subId);

        GameEventPool.Recycle(data);
    }
    public void Dispatch(EGameEventType gameEventType, int subId = -1)
    {
        if (!gameEventDatas.TryGetValue(gameEventType, out GameEventData gameEventData))
            return;

        gameEventData.Dispatch(subId);
    }
    public void Dispatch<T>(EGameEventType gameEventType, T arg1, int subId = -1)
    {
        if (!gameEventDatas.TryGetValue(gameEventType, out GameEventData gameEventData))
            return;

        gameEventData.Dispatch<T>(arg1, subId);
    }
    public void Dispatch<T, U>(EGameEventType gameEventType, T arg1, U arg2, int subId = -1)
    {
        if (!gameEventDatas.TryGetValue(gameEventType, out GameEventData gameEventData))
            return;

        gameEventData.Dispatch<T, U>(arg1, arg2, subId);
    }
    public void Dispatch<T, U, V>(EGameEventType gameEventType, T arg1, U arg2, V arg3, int subId = -1)
    {
        if (!gameEventDatas.TryGetValue(gameEventType, out GameEventData gameEventData))
            return;

        gameEventData.Dispatch<T, U, V>(arg1, arg2, arg3, subId);
    }
    public void Dispatch<T, U, V, W>(EGameEventType gameEventType, T arg1, U arg2, V arg3, W arg4, int subId = -1)
    {
        if (!gameEventDatas.TryGetValue(gameEventType, out GameEventData gameEventData))
            return;

        gameEventData.Dispatch<T, U, V, W>(arg1, arg2, arg3, arg4, subId);
    }
    public void Dispatch<T, U, V, W, X>(EGameEventType gameEventType, T arg1, U arg2, V arg3, W arg4, X arg5, int subId = -1)
    {
        if (!gameEventDatas.TryGetValue(gameEventType, out GameEventData gameEventData))
            return;

        gameEventData.Dispatch<T, U, V, W, X>(arg1, arg2, arg3, arg4, arg5, subId);
    }
    public void Dispatch<T, U, V, W, X, Y>(EGameEventType gameEventType, T arg1, U arg2, V arg3, W arg4, X arg5, Y arg6, int subId = -1)
    {
        if (!gameEventDatas.TryGetValue(gameEventType, out GameEventData gameEventData))
            return;

        gameEventData.Dispatch<T, U, V, W, X, Y>(arg1, arg2, arg3, arg4, arg5, arg6, subId);
    }
    public void Dispatch<T, U, V, W, X, Y, Z>(EGameEventType gameEventType, T arg1, U arg2, V arg3, W arg4, X arg5, Y arg6, Z arg7, int subId = -1)
    {
        if (!gameEventDatas.TryGetValue(gameEventType, out GameEventData gameEventData))
            return;

        gameEventData.Dispatch<T, U, V, W, X, Y, Z>(arg1, arg2, arg3, arg4, arg5, arg6, arg7, subId);
    }

    #endregion 分发事件

    private void AddListener(EGameEventType gameEventType, Delegate handler, int subId = -1)
    {
        if (handler == null)
            return;

        if (!gameEventDatas.TryGetValue(gameEventType, out GameEventData gameEventData))
        {
            gameEventData = new GameEventData(gameEventType);
            gameEventDatas.Add(gameEventType, gameEventData);
        }
        gameEventData.AddListener(handler, subId);
    }

    private void RemoveListener(EGameEventType gameEventType, Delegate handler, int subId = -1)
    {
        if (handler == null)
            return;
        if (!gameEventDatas.TryGetValue(gameEventType, out GameEventData gameEventData))
            return;

        gameEventData.RemoveListener(handler, subId);
    }
}