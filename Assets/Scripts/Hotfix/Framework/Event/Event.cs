using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Event
{
    private static Dictionary<string, List<Delegate>> events = new();//存所有事件的字典

    private static bool AddListener(string eventId, Delegate handler)
    {
        if (handler == null)
            return false;
        if (!events.TryGetValue(eventId, out var handlers))
        {
            handlers = new List<Delegate>();
            events.Add(eventId, handlers);
        }
        handlers.Add(handler);
        return true;
    }

    private static bool RemoveListener(string eventId, Delegate handler)
    {
        if (handler == null)
            return false;
        bool ret = false;
        if (events.TryGetValue(eventId, out var handlers))
        {
            ret = handlers.Remove(handler);
        }
        if (handlers.Count == 0)
        {
            events.Remove(eventId);
        }
        return ret;
    }

    #region 添加监听者

    public static bool AddListener(string eventId, Action handler)
    {
        return AddListener(eventId, (Delegate)handler);
    }
    public static bool AddListener<T>(string eventId, Action<T> handler)
    {
        return AddListener(eventId, (Delegate)handler);
    }
    public static bool AddListener<T, U>(string eventId, Action<T, U> handler)
    {
        return AddListener(eventId, (Delegate)handler);
    }
    public static bool AddListener<T, U, V>(string eventId, Action<T, U, V> handler)
    {
        return AddListener(eventId, (Delegate)handler);
    }
    public static bool AddListener<T, U, V, W>(string eventId, Action<T, U, V, W> handler)
    {
        return AddListener(eventId, (Delegate)handler);
    }
    public static bool AddListener<T, U, V, W, X>(string eventId, Action<T, U, V, W, X> handler)
    {
        return AddListener(eventId, (Delegate)handler);
    }
    public static bool AddListener<T, U, V, W, X, Y>(string eventId, Action<T, U, V, W, X, Y> handler)
    {
        return AddListener(eventId, (Delegate)handler);
    }
    public static bool AddListener<T, U, V, W, X, Y, Z>(string eventId, Action<T, U, V, W, X, Y, Z> handler)
    {
        return AddListener(eventId, (Delegate)handler);
    }

    #endregion 添加监听者

    #region 移除监听者

    public static bool RemoveListener(string eventId, Action handler)
    {
        return RemoveListener(eventId, (Delegate)handler);
    }
    public static bool RemoveListener<T>(string eventId, Action<T> handler)
    {
        return RemoveListener(eventId, (Delegate)handler);
    }
    public static bool RemoveListener<T, U>(string eventId, Action<T, U> handler)
    {
        return RemoveListener(eventId, (Delegate)handler);
    }
    public static bool RemoveListener<T, U, V>(string eventId, Action<T, U, V> handler)
    {
        return RemoveListener(eventId, (Delegate)handler);
    }
    public static bool RemoveListener<T, U, V, W>(string eventId, Action<T, U, V, W> handler)
    {
        return RemoveListener(eventId, (Delegate)handler);
    }
    public static bool RemoveListener<T, U, V, W, X>(string eventId, Action<T, U, V, W, X> handler)
    {
        return RemoveListener(eventId, (Delegate)handler);
    }
    public static bool RemoveListener<T, U, V, W, X, Y>(string eventId, Action<T, U, V, W, X, Y> handler)
    {
        return RemoveListener(eventId, (Delegate)handler);
    }
    public static bool RemoveListener<T, U, V, W, X, Y, Z>(string eventId, Action<T, U, V, W, X, Y, Z> handler)
    {
        return RemoveListener(eventId, (Delegate)handler);
    }

    public static void RemoveAllListener()
    {
        events.Clear();
    }

    #endregion 移除监听者

    #region 发送事件

    public static void Dispatch(string eventId)
    {
        List<Delegate> eventList;
        if (events.TryGetValue(eventId, out eventList))
        {
            for (int i = 0; i < eventList.Count; i++)
            {
                Action callBack = eventList[i] as Action;
                callBack?.Invoke();
            }
        }
    }
    public static void Dispatch<T>(string eventId, T arg1)
    {
        List<Delegate> eventList;
        if (events.TryGetValue(eventId, out eventList))
        {
            for (int i = 0; i < eventList.Count; i++)
            {
                var callBack = eventList[i] as Action<T>;
                callBack?.Invoke(arg1);
            }
        }
    }
    public static void Dispatch<T, U>(string eventId, T arg1, U arg2)
    {
        List<Delegate> eventList;
        if (events.TryGetValue(eventId, out eventList))
        {
            for (int i = 0; i < eventList.Count; i++)
            {
                var callBack = eventList[i] as Action<T, U>;
                callBack?.Invoke(arg1, arg2);
            }
        }
    }
    public static void Dispatch<T, U, V>(string eventId, T arg1, U arg2, V arg3)
    {
        List<Delegate> eventList;
        if (events.TryGetValue(eventId, out eventList))
        {
            for (int i = 0; i < eventList.Count; i++)
            {
                var callBack = eventList[i] as Action<T, U, V>;
                callBack?.Invoke(arg1, arg2, arg3);
            }
        }
    }
    public static void Dispatch<T, U, V, W>(string eventId, T arg1, U arg2, V arg3, W arg4)
    {
        List<Delegate> eventList;
        if (events.TryGetValue(eventId, out eventList))
        {
            for (int i = 0; i < eventList.Count; i++)
            {
                var callBack = eventList[i] as Action<T, U, V, W>;
                callBack?.Invoke(arg1, arg2, arg3, arg4);
            }
        }
    }
    public static void Dispatch<T, U, V, W, X>(string eventId, T arg1, U arg2, V arg3, W arg4, X arg5)
    {
        List<Delegate> eventList;
        if (events.TryGetValue(eventId, out eventList))
        {
            for (int i = 0; i < eventList.Count; i++)
            {
                var callBack = eventList[i] as Action<T, U, V, W, X>;
                callBack?.Invoke(arg1, arg2, arg3, arg4, arg5);
            }
        }
    }
    public static void Dispatch<T, U, V, W, X, Y>(string eventId, T arg1, U arg2, V arg3, W arg4, X arg5, Y arg6)
    {
        List<Delegate> eventList;
        if (events.TryGetValue(eventId, out eventList))
        {
            for (int i = 0; i < eventList.Count; i++)
            {
                var callBack = eventList[i] as Action<T, U, V, W, X, Y>;
                callBack?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6);
            }
        }
    }
    public static void Dispatch<T, U, V, W, X, Y, Z>(string eventId, T arg1, U arg2, V arg3, W arg4, X arg5, Y arg6, Z arg7)
    {
        List<Delegate> eventList;
        if (events.TryGetValue(eventId, out eventList))
        {
            for (int i = 0; i < eventList.Count; i++)
            {
                var callBack = eventList[i] as Action<T, U, V, W, X, Y, Z>;
                callBack?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
            }
        }
    }

    #endregion 发送事件

    public static void Log()
    {
        var sb = new StringBuilder();
        sb.Append("----------所有注册事件------------");
        sb.Append("\n");
        foreach (var pair in events)
        {
            sb.AppendFormat($"事件key = {pair.Key}");
            sb.Append("\n");
            sb.AppendFormat($"监听者数量 : {pair.Value.Count}");
            sb.Append("\n");
            foreach (var item in pair.Value)
            {
                sb.AppendFormat($"监听者方法名 : {item.Method.Name}，监听者方法所属类 : {item.Method.ReflectedType}");
                sb.Append("\n");
            }
            sb.Append("\n");
        }
        sb.Append("\n");
        sb.Append("---------------------------------------");
        Debug.Log(sb);
    }
}
