using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHotfix : Singleton<TestHotfix>, ISingletonUpdate
{
    public override void Register()
    {
        base.Register();
        CommonLog.Error("Register");
    }

    public override void UnRegister()
    {
        base.UnRegister();
        CommonLog.Error("UnRegister");
    }

    public void Update()
    {
        CommonLog.Error(" TestHotfix Update");
    }
    
    public void TestHotfixFun()
    {
        CommonLog.Error("TestHotfixFun");
    }
}
