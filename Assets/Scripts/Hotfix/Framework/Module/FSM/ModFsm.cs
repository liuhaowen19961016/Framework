using System;
using System.Collections.Generic;
using Framework;

public class ModFsm : ModuleBase
{
    private Dictionary<Type, FsmBase> fsmDict;

    public override void Init()
    {
        base.Init();
        fsmDict = new Dictionary<Type, FsmBase>();
    }

    public void AddFsm<T>(FsmDataBase data)
        where T : FsmBase, new()
    {
        var type = typeof(T);
        if (fsmDict.ContainsKey(type))
        {
            Log.Error($"状态机已存在，不能重复添加状态机，fsm：{type}");
            return;
        }
        var newClass = new T();
        newClass.InitFsm(data);
        fsmDict.Add(type, newClass);
    }

    public T GetFsm<T>()
        where T : FsmBase
    {
        var type = typeof(T);
        if (!fsmDict.TryGetValue(type, out var _fsm))
        {
            Log.Error($"状态机不存在，先添加状态机，fsm：{type}");
            return null;
        }
        var fsm = _fsm as T;
        return fsm;
    }

    public bool RemoveFsm<T>()
        where T : FsmBase
    {
        var type = typeof(T);
        if (fsmDict.TryGetValue(type, out var _fsm))
        {
            _fsm.Dispose();
            fsmDict.Remove(type);
            return true;
        }
        return false;
    }

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        foreach (var fsm in fsmDict.Values)
        {
            fsm?.Update(deltaTime);
        }
    }
}