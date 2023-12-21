using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using System.Linq;

public class TestMono : MonoBehaviour
{
    Dictionary<int, List<int>> dict = new Dictionary<int, List<int>>();

    public void Start()
    {
        //dict.Add(2, new List<int>() { 1, 2, 3 });
        //dict.Add(1, new List<int>() { 555 });
        //dict.Add(5, new List<int>() { 222 });
        //dict.Add(3, new List<int>() { 111 });
        //foreach (var item in dict)
        //{
        //    CommonLog.Info(item.Key);
        //}

        //dict = dict.OrderBy(k => k.Key).ToDictionary(a => a.Key, a => a.Value);
        //CommonLog.Info("OrderBy");

        //foreach (var item in dict)
        //{
        //    CommonLog.Info(item.Key);
        //}


        List<TestC> list = new List<TestC>();
        list.Add(new TestC() { index = 1, v = 2 });
        list.Add(new TestC() { index = 2, v = 3 });
        list.Add(new TestC() { index = 5, v = 11 });

        var dicts = list.ToDictionary(k => k.index, k => k.v);
        foreach (var item in dicts)
        {
            CommonLog.Error(item.Key + " " + item.Value);
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {

        }
    }
}

public class TestC
{
    public int index;
    public int v;
}
