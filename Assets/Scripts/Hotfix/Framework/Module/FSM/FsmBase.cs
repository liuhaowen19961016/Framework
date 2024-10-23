using System;
using System.Collections.Generic;
using Framework;

public abstract class FsmBase
{
    private Dictionary<Type, IFsmState> stateDict = new Dictionary<Type, IFsmState>();
    protected FsmDataBase Data { get; private set; }

    protected IFsmState CurFsmState;

    public void InitFsm(FsmDataBase data)
    {
        Data = data;
    }

    public void StartFsm()
    {
        if (CurFsmState == null)
        {
            Log.Error($"至少添加一个状态才能启动状态机");
            return;
        }
        CurFsmState?.OnEnter();
    }

    public T GetCurState<T>()
        where T : class, IFsmState
    {
        if (CurFsmState == null)
        {
            Log.Error($"当前状态为null");
            return null;
        }
        var state = CurFsmState as T;
        return state;
    }

    public void AddState(IFsmState fsmState)
    {
        var type = fsmState.GetType();
        if (stateDict.ContainsKey(type))
        {
            Log.Error($"状态已存在，不能重复添加状态，state：{fsmState}");
            return;
        }

        // 默认第一个添加的状态为初始状态
        if (stateDict.Count <= 0)
            CurFsmState = fsmState;

        stateDict.Add(type, fsmState);
        fsmState.OnInit(this);
    }

    public void Update(float deltaTime)
    {
        CurFsmState?.OnUpdate(deltaTime);
    }

    public void ChangeState<T>(params object[] objs)
        where T : IFsmState
    {
        var type = typeof(T);
        ChangeState(type, objs);
    }

    public void ChangeState(Type type, params object[] objs)
    {
        if (!stateDict.TryGetValue(type, out var _state))
        {
            Log.Error($"找不到此state，先添加state：{type.FullName}");
            return;
        }
        if (CurFsmState.GetType() == type)
        {
            Log.Error($"不能转换到自身状态，state：{type.FullName}");
            return;
        }

        CurFsmState?.OnExit();
        CurFsmState = _state;
        CurFsmState?.OnEnter(objs);
    }
}