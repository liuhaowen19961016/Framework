using Hotfix;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSingletn : Singleton<TestSingletn>
{
    protected override void OnDispose()
    {
        base.OnDispose();
        CommonLog.Error("TestSingletn OnDispose");
    }
}
