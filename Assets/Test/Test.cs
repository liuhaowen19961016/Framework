using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Start()
    {
        var v1 = GameUtils.GetRandomEmum<ETestV>();
        //Debug.LogFormat()

        //Log.Info("info");
        //Log.Warning("Warning");
        //Log.Error("Error");
        //Log.Info(ETestV);
        //Log.Warning(ETestV);
        //Log.Error(ETestV);

        TestTest t = new TestTest();

        //Debug.Log(Log.GetString(t));
        //Debug.Log(Log.GetString(t));

        Log.DebugFormat("ssdd{0} - {1}", 111, 123);
        Log.Error(null);
        Log.Info(t);
    }

    void Update()
    {

    }

    public void TestFun()
    {

    }
}

public class TestTest : IFormattable
{
    public string ToString(string format, IFormatProvider formatProvider)
    {
        return "asdasd{0}";
    }
}

public enum ETestV
{
    T1,
    T2,
    T3,
    T4,
}
