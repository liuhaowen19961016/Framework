using System;
using System.Collections.Generic;

public abstract class ComponentBase
{
    protected ComponentBase Root;
    protected ComponentBase Parent;

    protected Dictionary<Type, ComponentBase> ComponentDict = new();
    protected Dictionary<long, ComponentBase> ChildDict = new();

    protected long instanceId;
    public long InstanceId => instanceId;

    #region 组件（一个Component上只能有一个）

    public T GetComponent<T>()
        where T : ComponentBase
    {
        Type type = typeof(T);
        if (ComponentDict.TryGetValue(type, out ComponentBase outComponent))
        {
            return (T)outComponent;
        }
        return null;
    }

    public void AddComponent<T>(object arg = null)
        where T : ComponentBase, new()
    {
        Type type = typeof(T);
        if (HasComponent(type))
        {
            CommonLog.Error($"{GetType().Name}不能重复添加组件，{type.Name}");
            return;
        }
        Parent = this;
        T objectBase = Create<T>();
        objectBase.instanceId = IdUtils.GenInstanceId();
        objectBase.Awake(arg);
        objectBase.Start();
        GameComponent.Register(objectBase);
        ComponentDict.Add(type, objectBase);
    }

    public bool RemoveComponent(ComponentBase component)
    {
        return RemoveComponent(component.instanceId);
    }

    public bool RemoveComponent(long instanceId)
    {
        foreach (var v in ComponentDict.Values)
        {
            if (v.instanceId == instanceId)
            {
                Type type = v.GetType();
                return ComponentDict.Remove(type);
            }
        }
        return false;
    }

    public bool RemoveComponent<T>()
    {
        Type type = typeof(T);
        return ComponentDict.Remove(type);
    }

    public bool HasComponent(Type type)
    {
        bool hasComponent = ComponentDict.ContainsKey(type);
        return hasComponent;
    }

    #endregion 组件（一个Component上只能有一个）

    #region Child

    public T GetChild<T>(int instanceId)
        where T : ComponentBase
    {
        if (ChildDict.TryGetValue(instanceId, out ComponentBase outComponent))
        {
            return (T)outComponent;
        }
        return null;
    }

    public void AddChild<T>(object arg = null)
        where T : ComponentBase, new()
    {
        Parent = this;
        T objectBase = Create<T>();
        objectBase.instanceId = IdUtils.GenInstanceId();
        objectBase.Awake(arg);
        objectBase.Start();
        GameComponent.Register(objectBase);
        ChildDict.Add(objectBase.instanceId, objectBase);
    }

    public bool RemoveChild(ComponentBase component)
    {
        return RemoveChild(component.instanceId);
    }

    public bool RemoveChild(long instanceId)
    {
        return ChildDict.Remove(instanceId);
    }

    #endregion Child

    private T Create<T>()
        where T : ComponentBase, new()
    {
        T objectBase = new T();
        return objectBase;
    }

    public void Dispose()
    {
        OnDispose();

        foreach (var kvp in ComponentDict)
        {
            kvp.Value.Dispose();
        }
        foreach (var kvp in ChildDict)
        {
            kvp.Value.Dispose();
        }
        ComponentDict.Clear();
        ChildDict.Clear();
        Root = null;
        Parent = null;
        instanceId = -1;
    }

    #region 生命周期

    protected virtual void Awake(object arg)
    {

    }

    protected virtual void Start()
    {

    }

    protected virtual void OnDispose()
    {

    }

    #endregion 生命周期
}
