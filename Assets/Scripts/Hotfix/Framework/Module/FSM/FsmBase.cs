using System;
using System.Collections.Generic;
using Framework;

/// <summary>
/// 状态机基类
/// </summary>
public abstract class FsmBase
{
    private Dictionary<Type, FsmStateBase> stateDict = new Dictionary<Type, FsmStateBase>();
    public FsmDataBase Data { get; private set; }

    protected FsmStateBase defaultState; //默认状态
    public FsmStateBase CurState { get; private set; } //当前状态
    public FsmStateBase PreState { get; private set; } //上一个状态

    protected bool isBoot;
    protected bool isPause;

    public void InitFsm(FsmDataBase data)
    {
        Data = data;
    }

    public void SetDefaultState<T>()
        where T : FsmStateBase, new()
    {
        var type = typeof(T);
        if (!stateDict.TryGetValue(type, out var _state))
        {
            Log.Error($"找不到此state，先添加state：{type.FullName}");
            return;
        }
        defaultState = _state;
        CurState = _state;
    }

    public void StartFsm()
    {
        if (isBoot)
            return;
        if (CurState == null)
        {
            Log.Error($"至少添加一个状态才能启动状态机");
            return;
        }
        CurState?.OnEnter();
        isBoot = true;
    }

    /// <summary>
    /// 添加状态
    /// </summary>
    public void AddState<T>()
        where T : FsmStateBase, new()
    {
        var type = typeof(T);
        if (stateDict.ContainsKey(type))
        {
            Log.Error($"状态已存在，不能重复添加状态，state：{type}");
            return;
        }

        var state = new T();

        // 默认第一个添加的状态为初始状态
        if (stateDict.Count <= 0)
        {
            defaultState = state;
            CurState = state;
        }

        stateDict.Add(type, state);
        CurState.OnInit(this);
    }

    /// <summary>
    /// 切换状态
    /// </summary>
    public void ChangeState<T>(params object[] objs)
        where T : FsmStateBase
    {
        var type = typeof(T);
        ChangeState(type, objs);
    }

    /// <summary>
    /// 切换状态
    /// </summary>
    public void ChangeState(Type type, params object[] objs)
    {
        if (!isBoot)
        {
            Log.Error($"状态机未启动，先启动状态机");
            return;
        }
        if (!stateDict.TryGetValue(type, out var _state))
        {
            Log.Error($"找不到此state，先添加state：{type.FullName}");
            return;
        }
        if (CurState.GetType() == type)
        {
            Log.Error($"不能切换到自身状态，state：{type.FullName}");
            return;
        }

        PreState = CurState;
        CurState?.OnExit();
        CurState = _state;
        CurState?.OnEnter(objs);
    }

    public bool CheckState<T>()
        where T : FsmStateBase
    {
        var type1 = CurState.GetType();
        var type2 = typeof(T);
        var ret = type1 == type2;
        return ret;
    }

    public void Update(float deltaTime)
    {
        if (!isBoot || isPause)
            return;
        CurState?.OnUpdate(deltaTime);
    }

    public void Pause()
    {
        if (!isBoot)
            return;
        if (isPause)
            return;
        CurState?.OnPause();
    }

    public void Resume()
    {
        if (!isBoot)
            return;
        if (!isPause)
            return;
        CurState?.OnResume();
    }

    public virtual void Dispose()
    {
        foreach (var state in stateDict.Values)
        {
            state?.Dispose();
        }
        Data?.Dispose();
    }
}