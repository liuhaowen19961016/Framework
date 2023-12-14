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

        //dict=dict.OrderBy(k => k).ToDictionary(k=>k,v=>v);
        //CommonLog.Info("OrderBy");

        //foreach (var item in dict)
        //{
        //    CommonLog.Info(item.Key);
        //}

    }
}
