using System;
using System.Collections.Generic;

/// <summary>
/// 管理每个事件类型中的事件
/// </summary>
public class EventData
{
    private Dictionary<int, List<Delegate>> handleDict = new Dictionary<int, List<Delegate>>();

    public bool AddEvent<T>(Action<T> callback, int subId = -1)
        where T : IEvent
    {
        if (!handleDict.TryGetValue(subId, out var handleList))
        {
            handleList = new List<Delegate>();
            handleDict.Add(subId, handleList);
        }
        handleList.Add(callback);
        return true;
    }

    public bool RemoveEvent<T>(Action<T> callback, int subId = -1)
        where T : IEvent
    {
        if (!handleDict.TryGetValue(subId, out var handleList))
            return false;

        bool ret = handleList.Remove(callback);
        return ret;
    }

    public void Dispatch<T>(T evt, int subId = -1)
    {
        if (!handleDict.TryGetValue(subId, out var handleList))
            return;

        foreach (var handle in handleList)
        {
            var callback = handle as Action<T>;
            callback?.Invoke(evt);
        }
    }
}