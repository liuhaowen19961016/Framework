using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHotfixCom : ComponentBase, IUpdate
{
    public void Update()
    {
        //CommonLog.Error("Update");
    }

    protected override void Awake(object arg)
    {
        base.Awake(arg);
        CommonLog.Error("TestHotfixCom Awake");

        //CommonLog.Error(GameUtils.AllSame(1, 2, 3, 4, 5));
        //CommonLog.Error(GameUtils.AllSame(1, 1, 1, 1, 2));
        //CommonLog.Error(GameUtils.AllSame(1, 1, 1, 1, 1));
        //C1 c1 = new C1();
        //C2 c2 = new C2();
        //CommonLog.Error(GameUtils.AllSame(c1, c1));
        //CommonLog.Error(GameUtils.AllSame(c2, c2));

        //int v= GameUtils.GetRandomEmum<int>(new List<int>() { 1,2,3});
        // CommonLog.Error(v);

        ETest e = GameUtils.GetRandomEmum<ETest>();
        Debug.LogError(e);
    }
}

public class C1
{
    public override bool Equals(object obj)
    {
        return false;
    }
}
public struct C2
{
    public override bool Equals(object obj)
    {
        return false;
    }
}

public enum ETest
{
    Test1 = 2,
    Test2 = 1,
    Test3 = 22,
}