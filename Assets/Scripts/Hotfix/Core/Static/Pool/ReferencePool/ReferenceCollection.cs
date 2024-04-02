using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 引用池
/// </summary>
public class ReferenceCollection
{
    private Queue<object> references = new Queue<object>();
    private Type referenceType;
    private int capacity;//-1表示无限容量

    public int UnusedReferenceCount
    {
        get
        {
            return references.Count;
        }
    }

    private int usingReferenceCount;
    public int UsingReferenceCount
    {
        get
        {
            return usingReferenceCount;
        }
    }

    private int allocateReferenceCount;
    public int AllocateReferenceCount
    {
        get
        {
            return allocateReferenceCount;
        }
    }

    private int recycleReferenceCount;
    public int RecycleReferenceCount
    {
        get
        {
            return recycleReferenceCount;
        }
    }

    private int addReferenceCount;
    public int AddReferenceCount
    {
        get
        {
            return addReferenceCount;
        }
    }

    public ReferenceCollection(Type referenceType, int capacity = ReferencePool.DefaultCapacity)
    {
        this.referenceType = referenceType;
        this.capacity = capacity;
        usingReferenceCount = 0;
        allocateReferenceCount = 0;
        recycleReferenceCount = 0;
        addReferenceCount = 0;
    }

    public object Allocate(Type type)
    {
        if (type != referenceType)
        {
            Debug.LogError($"类型不一致，对象类型：{type}，池子类型：{referenceType}");
            return null;
        }

        usingReferenceCount++;
        allocateReferenceCount++;
        if (references.Count > 0)
        {
            var temp = references.Dequeue();
            return temp;
        }
        var newReference = Create();
        return newReference;
    }

    public T Allocate<T>()
         where T : class
    {
        Type type = typeof(T);
        object obj = Allocate(type);
        return obj as T;
    }

    public bool Recycle(object obj)
    {
        if (obj.GetType() != referenceType)
        {
            Debug.LogError($"类型不一致，对象类型：{obj.GetType()}，池子类型：{referenceType}");
            return false;
        }
        usingReferenceCount--;
        recycleReferenceCount++;
        if (capacity >= 0 && references.Count >= capacity)
        {
            obj = null;
            Debug.LogError($"池子容量已满，无法回收，直接释放，池子类型：{referenceType}");
            return false;
        }

        references.Enqueue(obj);
        if (obj is IReferencePoolObject poolObject)
        {
            poolObject.OnRecycle();
        }
        return true;
    }

    public bool Recycle<T>(T obj)
         where T : class
    {
        return Recycle((object)obj);
    }

    public bool Add(int count)
    {
        bool addComplete = true;
        while (count-- > 0)
        {
            var newReference = Create();
            references.Enqueue(newReference);
            if (capacity >= 0 && references.Count >= capacity)
            {
                addComplete = false;
                break;
            }
        }
        return addComplete;
    }

    public void Remove(int count)
    {
        if (count > references.Count)
        {
            count = references.Count;
        }

        while (count-- > 0)
        {
            references.Dequeue();
        }
    }

    public void Dispose()
    {
        while (references.Count > 0)
        {
            var obj = references.Dequeue();
            if (obj is IReferencePoolObject poolObject)
            {
                poolObject.OnDispose();
            }
        }
        references.Clear();
    }

    public void SetCapacity(int capacity)
    {
        this.capacity = capacity;
    }

    private object Create()
    {
        var newReference = Activator.CreateInstance(referenceType);
        if (newReference is IReferencePoolObject poolObject)
        {
            poolObject.OnCreate();
        }
        addReferenceCount++;
        return newReference;
    }
}