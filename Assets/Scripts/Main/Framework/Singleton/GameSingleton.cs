using System.Collections.Generic;
using System;

/// <summary>
/// 管理Singleton
/// </summary>
public class GameSingleton
{
    private static Dictionary<Type, ISingleton> singletonDict = new();

    private static Queue<ISingletonFixedUpdate> singletonFixedUpdates = new();
    private static Queue<ISingletonUpdate> singletonUpdates = new();
    private static Queue<ISingletonLateUpdate> singletonLateUpdates = new();

    public static T AddSingleton<T>()
        where T : ISingleton, new()
    {
        var singleton = new T();
        AddSingleton(singleton);
        return singleton;
    }

    private static void AddSingleton(ISingleton singleton)
    {
        Type type = singleton.GetType();
        if (singletonDict.ContainsKey(type))
        {
            CommonLog.Error($"已经存在此单例，不能重复添加，单例类型：{type}");
            return;
        }
        singletonDict.Add(type, singleton);
        if (singleton is ISingletonFixedUpdate fixedUpdate)
        {
            singletonFixedUpdates.Enqueue(fixedUpdate);
        }
        if (singleton is ISingletonUpdate update)
        {
            singletonUpdates.Enqueue(update);
        }
        if (singleton is ISingletonLateUpdate lateUpdate)
        {
            singletonLateUpdates.Enqueue(lateUpdate);
        }
        singleton.Register();
    }

    public static void FixedUpdate()
    {
        if (singletonFixedUpdates.Count <= 0)
            return;
        int count = singletonFixedUpdates.Count;
        while (count-- > 0)
        {
            var fixedUpdate = singletonFixedUpdates.Dequeue();
            fixedUpdate?.FixedUpdate();
            singletonFixedUpdates.Enqueue(fixedUpdate);
        }
    }

    public static void Update()
    {
        if (singletonUpdates.Count <= 0)
            return;
        int count = singletonUpdates.Count;
        while (count-- > 0)
        {
            var update = singletonUpdates.Dequeue();
            update?.Update();
            singletonUpdates.Enqueue(update);
        }
    }

    public static void LateUpdate()
    {
        if (singletonLateUpdates.Count <= 0)
            return;
        int count = singletonLateUpdates.Count;
        while (count-- > 0)
        {
            var lateUpdate = singletonLateUpdates.Dequeue();
            lateUpdate?.LateUpdate();
            singletonLateUpdates.Enqueue(lateUpdate);
        }
    }

    public static void Dispose()
    {
        foreach (var kvp in singletonDict)
        {
            kvp.Value.UnRegister();
        }
        singletonDict.Clear();
    }
}
