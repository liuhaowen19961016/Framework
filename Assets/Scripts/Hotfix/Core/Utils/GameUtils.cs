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
    /// 获取一个随机值
    /// </summary>
    public static T GetRandomValue<T>(List<T> list, List<T> ignoreList = null)
    {
        int randomNum = UnityEngine.Random.Range(0, list.Count);
        T randomValue = list[randomNum];
        SafeWhile(
            () =>
            {
                return ignoreList != null && ignoreList.Contains(randomValue);
            },
            () =>
            {
                int randomNum = UnityEngine.Random.Range(0, list.Count);
                randomValue = list[randomNum];
            });
        return randomValue;
    }

    /// <summary>
    /// 获取一个随机值
    /// </summary>
    public static T GetRandomValue<T>(T[] list, List<T> ignoreList = null)
    {
        int randomNum = UnityEngine.Random.Range(0, list.Length);
        T randomValue = list[randomNum];
        SafeWhile(
            () =>
            {
                return ignoreList != null && ignoreList.Contains(randomValue);
            },
            () =>
            {
                int randomNum = UnityEngine.Random.Range(0, list.Length);
                randomValue = list[randomNum];
            });
        return randomValue;
    }

    /// <summary>
    /// 获取一个随机值
    /// </summary>
    public static T GetRandomValue<T>(Array list, List<T> ignoreList = null)
    {
        int randomNum = UnityEngine.Random.Range(0, list.Length);
        T randomValue = (T)list.GetValue(randomNum);
        SafeWhile(
            () =>
            {
                return ignoreList != null && ignoreList.Contains(randomValue);
            },
            () =>
            {
                int randomNum = UnityEngine.Random.Range(0, list.Length);
                randomValue = (T)list.GetValue(randomNum);
            });
        return randomValue;
    }

    /// <summary>
    /// 获取一个随机的枚举值
    /// </summary>
    public static T GetRandomEmum<T>(List<T> ignoreList = null)
        where T : Enum
    {
        Array values = Enum.GetValues(typeof(T));
        T randomEnum = GetRandomValue(values, ignoreList);
        return randomEnum;
    }

    /// <summary>
    /// 获取一个随机值列表
    /// </summary>
    public static List<T> GetRandomValueList<T>(List<T> list, int getCount, bool excludeSame = false, List<T> ignoreList = null)
    {
        int ignoreCount = ignoreList == null ? 0 : ignoreList.Count;
        int canRandomCount = list.Count - ignoreCount;
        if (!excludeSame && canRandomCount < getCount)
        {
            CommonLog.Error("可随机的数量小于要获取的数量");
            return null;
        }
        List<T> randomList = new();
        SafeWhile(
            () =>
            {
                return randomList.Count < getCount;
            },
            () =>
            {
                var value = GetRandomValue<T>(list, ignoreList);
                if (excludeSame || !randomList.Contains(value))
                {
                    randomList.Add(value);
                }
            });
        return randomList;
    }

    /// <summary>
    /// 获取一个随机值列表
    /// </summary>
    public static List<T> GetRandomValueList<T>(T[] list, int getCount, bool excludeSame = false, List<T> ignoreList = null)
    {
        int ignoreCount = ignoreList == null ? 0 : ignoreList.Count;
        int canRandomCount = list.Length - ignoreCount;
        if (!excludeSame && canRandomCount < getCount)
        {
            CommonLog.Error("可随机的数量小于要获取的数量");
            return null;
        }
        List<T> randomList = new();
        SafeWhile(
            () =>
            {
                return randomList.Count < getCount;
            },
            () =>
            {
                var value = GetRandomValue<T>(list, ignoreList);
                if (excludeSame || !randomList.Contains(value))
                {
                    randomList.Add(value);
                }
            });
        return randomList;
    }

    /// <summary>
    /// 获取一个随机值列表
    /// </summary>
    public static List<T> GetRandomValueList<T>(Array list, int getCount, bool excludeSame = false, List<T> ignoreList = null)
    {
        int ignoreCount = ignoreList == null ? 0 : ignoreList.Count;
        int canRandomCount = list.Length - ignoreCount;
        if (!excludeSame && canRandomCount < getCount)
        {
            CommonLog.Error("可随机的数量小于要获取的数量");
            return null;
        }
        List<T> randomList = new();
        SafeWhile(
            () =>
            {
                return randomList.Count < getCount;
            },
            () =>
            {
                var value = GetRandomValue<T>(list, ignoreList);
                if (excludeSame || !randomList.Contains(value))
                {
                    randomList.Add(value);
                }
            });
        return randomList;
    }

    /// <summary>
    /// 获取一个随机的枚举值列表
    /// </summary>
    public static List<T> GetRandomEnumList<T>(int getCount, bool excludeSame = false, List<T> ignoreList = null)
            where T : Enum
    {
        Array values = Enum.GetValues(typeof(T));
        List<T> randomEnumList = GetRandomValueList<T>(values, getCount, excludeSame, ignoreList);
        return randomEnumList;
    }

    /// <summary>
    /// while循环安全版
    /// </summary>
    public static void SafeWhile(Func<bool> loopCondition, Action loopBody)
    {
        if (loopCondition == null)
        {
            CommonLog.Error("循环条件不能为null");
            return;
        }

        int countLimit = 999;
        int count = 0;
        while (loopCondition())
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
