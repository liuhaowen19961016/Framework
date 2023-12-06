using System;
using System.Collections.Generic;
using UnityEngine;

public static class GameUtils
{
    /// <summary>
    /// 是否在Unity编辑器环境下
    /// </summary>
    public static bool IsInEditorEnv()
    {
        return Application.isEditor;
    }

    /// <summary>
    /// 获取一个随机的枚举值
    /// </summary>
    public static T GetRandomEmum<T>(List<T> ignoreList = null)
        where T : struct
    {
        Array values = Enum.GetValues(typeof(T));
        int randumNum = UnityEngine.Random.Range(0, values.Length);
        T randomEnum = (T)values.GetValue(randumNum);
        while (ignoreList != null && ignoreList.Contains(randomEnum))
        {
            randumNum = UnityEngine.Random.Range(0, values.Length);
            randomEnum = (T)values.GetValue(randumNum);
        }
        return randomEnum;
    }

    /// <summary>
    /// 获取一个随机的枚举值列表
    /// </summary>
    public static List<T> GetRandomEnumList<T>(int getCount, bool excludeSame = false, List<T> ignoreList = null)
            where T : struct
    {
        int ignoreCount = ignoreList == null ? 0 : ignoreList.Count;
        Array values = Enum.GetValues(typeof(T));
        int enumCount = values.Length - ignoreCount;
        if (!excludeSame && enumCount < getCount)
        {
            CommonLog.Error("枚举类型数量小于要获取的数量");
            return null;
        }
        List<T> enumList = new();
        SafeWhile(
            () =>
            {
                return enumList.Count < getCount;
            },
            () =>
            {
                var value = GetRandomEmum<T>(ignoreList);
                if (excludeSame || !enumList.Contains(value))
                {
                    enumList.Add(value);
                }
            });
        return enumList;
    }

    /// <summary>
    /// while循环安全版
    /// </summary>
    public static void SafeWhile(Func<bool> loopCheck, Action loopBody)
    {
        if (loopCheck == null)
        {
            CommonLog.Error("循环判断不能为null");
            return;
        }

        int countLimit = 999;
        int count = 0;
        while (loopCheck())
        {
            loopBody?.Invoke();
            count++;
            if (count == countLimit)
            {
                CommonLog.Error("出现死循环！！！！！！！！！！");
                break;
            }
        }
    }
}
