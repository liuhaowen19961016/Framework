using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public abstract class ComponentBase
{
    protected ComponentBase Root;
    protected ComponentBase Parent;
    protected Dictionary<Type, ComponentBase> Childs = new();

    private long instanceId;
    public long InstanceId
    {
        get
        {
            return instanceId;
        }
        protected set
        {
            instanceId = value;
        }
    }

    public void AddComponent<T>(object arg = null)
        where T : ComponentBase, new()
    {
        Type t = typeof(T);
        if (HasComponent(t))
        {
            CommonLog.Error($"不能重复添加Component：{t.FullName}");
            return;
        }
        Parent = this;
        T objectBase = Create<T>();
        objectBase.Awake(arg);
        objectBase.Start();
        ComponentMgr.Ins.Register(objectBase);
    }

    private T Create<T>()
        where T : ComponentBase, new()
    {
        T objectBase = new T();
        return objectBase;
    }

    private bool HasComponent(Type type)
    {
        bool hasComponent = Childs.ContainsKey(type);
        return hasComponent;
    }

    #region 生命周期

    protected virtual void Awake(object arg)
    {

    }

    protected virtual void Start()
    {

    }

    #endregion 生命周期
}
