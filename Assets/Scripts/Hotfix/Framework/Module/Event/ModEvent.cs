using System;
using System.Collections.Generic;

/// <summary>
/// 事件模块
/// </summary>
public class ModEvent : ModuleBase
{
    public Dictionary<Type, EventData> eventDict; //所有事件

    public override void Init()
    {
        base.Init();
        eventDict = new Dictionary<Type, EventData>();
    }

    public void RegisterEvent<T>(Action<T> callback, int subId = -1)
        where T : IEvent
    {
        var type = typeof(T);
        if (!eventDict.TryGetValue(type, out var eventData))
        {
            eventData = new EventData();
            eventDict.Add(type, eventData);
        }
        eventData.AddEvent<T>(callback, subId);
    }

    public bool UnRegisterEvent<T>(Action<T> callback, int subId = -1)
        where T : IEvent
    {
        var type = typeof(T);
        if (!eventDict.TryGetValue(type, out var eventData))
            return false;
        bool ret = eventData.RemoveEvent<T>(callback, subId);
        return ret;
    }

    public void Disptch<T>(T evt, int subId = -1)
        where T : IEvent
    {
        var type = typeof(T);
        if (!eventDict.TryGetValue(type, out var eventData))
            return;
        eventData.Dispatch(evt, subId);
    }
}