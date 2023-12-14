using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestComP1 : ComponentBase
{
    protected override void Awake(object arg)
    {
        base.Awake(arg);

        CommonLog.Error("TestComP1 Awake");
    }
}
