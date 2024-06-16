using System;
using System.Collections.Generic;
using UnityEngine;

public static class GameUtils
{
    public static void HttpPost()
    {

    }

    /// <summary>
    /// 是否在Unity编辑器环境下
    /// </summary>
    public static bool IsInEditorEnv()
    {
        return Application.isEditor;
    }

    /// <summary>
    /// while循环安全版
    /// </summary>
    public static void SafeWhile(Func<bool> loopCondition, Action loopBody)
    {
        if (loopCondition == null)
        {
            Log.Error("循环条件不能为null");
            return;
        }

        int countLimit = 11111;
        int count = 0;
        while (loopCondition())
        {
            loopBody?.Invoke();
            count++;
            if (count == countLimit)
            {
                Log.Error("出现死循环！！！！！！！！！！");
                break;
            }
        }
    }

    ///// <summary>
    ///// 是否全部相等
    ///// </summary>
    //public static bool AllSame<T>(params T[] array)
    //{
    //    if (array == null || array.Length <= 0)
    //        return true;
    //    T compareValue = array[0];
    //    for (int i = 0; i < array.Length; i++)
    //    {
    //        if (!compareValue.Equals(array[i]))
    //            return false;
    //    }
    //    return true;
    //}
}
