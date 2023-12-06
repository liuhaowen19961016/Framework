using System.Collections.Generic;
using UnityEngine;

public abstract class ComponentBase
{
    protected ComponentBase Root;
    protected ComponentBase Parent;
    protected List<ComponentBase> Childs = new();

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

    public void AddComponent<T>(T component, object arg = null)
        where T : ComponentBase, new()
    {
        if (HasComponent(component))
        {
            Debug.LogError($"[ObjectBase] 已经添加了此Component：{component}");
            return;
        }
        Parent = this;
        T objectBase = Create<T>();
        objectBase.Awake(arg);
        objectBase.Start();
    }

    private T Create<T>()
        where T : ComponentBase, new()
    {
        T objectBase = new T();
        return objectBase;
    }

    private bool HasComponent(ComponentBase objectBase)
    {
        return Childs.Contains(objectBase);
    }

    protected virtual void Awake(object arg)
    {

    }

    protected virtual void Start()
    {

    }
}
