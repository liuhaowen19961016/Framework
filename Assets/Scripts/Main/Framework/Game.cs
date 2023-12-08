using System;
using System.Collections.Generic;

//整个游戏通过Component（继承ComponentBase）和Singleton（继承Singelton）两大模块控制
//之所以有Component和Singleton两个，是因为Component和Singleton的概念不一致
//Game类是注册全局Component和Singleton的入口
public static class Game
{
    #region 组件（Component）

    public static ComponentRoot ComponentRoot => Root.Ins.ComponentRoot;//ComponentRoot，没实际作用，所有的Component都属于它的子Component

    #endregion 组件（Component）

    #region 单例（Singleton）

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

    #endregion 单例（Singleton）
}
