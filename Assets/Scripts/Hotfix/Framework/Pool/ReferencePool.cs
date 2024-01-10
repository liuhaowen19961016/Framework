using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 引用池管理器
/// </summary>
public class ReferencePool
{
    private static Dictionary<Type, ReferenceCollection> referencelCollections = new();

    public static void Set<T>(int count, int capacity = -1)
        where T : class, new()
    {
        var pool = GetReferenceCollection(typeof(T));
        pool.SetCapacity(capacity);
        pool.Add(count);
    }

    public static T Allocate<T>()
        where T : class, new()
    {
        var pool = GetReferenceCollection(typeof(T));
        return pool.Allocate<T>();
    }

    public static bool Recycle<T>(T obj)
       where T : class, new()
    {
        var pool = GetReferenceCollection(typeof(T));
        return pool.Recycle<T>(obj);
    }

    public static bool Add<T>(int count)
       where T : class, new()
    {
        var pool = GetReferenceCollection(typeof(T));
        return pool.Add(count);
    }

    public static void Remove<T>(int count)
      where T : class, new()
    {
        var pool = GetReferenceCollection(typeof(T));
        pool.Remove(count);
    }

    public static void Dispose<T>()
    {
        var pool = GetReferenceCollection(typeof(T));
        pool.Dispose();
    }

    public static void DisposeAll()
    {
        foreach (var referencelCollection in referencelCollections.Values)
        {
            referencelCollection.Dispose();
        }
        referencelCollections.Clear();
    }

    public static ReferenceCollection GetReferenceCollection(Type referenceType)
    {
        if (referenceType == null)
        {
            Debug.LogError($"类型错误，不能为null");
            return null;
        }

        if (!referencelCollections.TryGetValue(referenceType, out ReferenceCollection referenceCollection))
        {
            referenceCollection = new ReferenceCollection(referenceType);
            referencelCollections.Add(referenceType, referenceCollection);
        }
        return referenceCollection;
    }
}
