using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Start()
    {
        var v1 = GameUtils.GetRandomEmum<ETestV>();
    }

    void Update()
    {

    }

    public void TestFun()
    {

    }
}

public enum ETestV
{
    T1,
    T2,
    T3,
    T4,
}
