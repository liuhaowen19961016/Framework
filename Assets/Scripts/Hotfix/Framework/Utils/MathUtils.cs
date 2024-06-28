using System;
using System.Collections.Generic;
using Framework;
using UnityEngine;

public static class MathUtils
{
    /// <summary>
    /// 四舍五入
    /// </summary>
    /// digits：保留几位小数
    public static float Round(float value, int digits = 1)
    {
        if (value == 0)
            return 0;
        float sign = Mathf.Sign(value);
        value = Mathf.Abs(value);
        float multiple = Mathf.Pow(10, digits);
        float tempValue = value * multiple + 0.5f;
        tempValue = Mathf.FloorToInt(tempValue);
        return tempValue / multiple * sign;
    }

    /// <summary>
    /// 获取一个随机值
    /// </summary>
    public static T GetRandomValue<T>(List<T> list, List<T> ignoreList = null)
    {
        int randomNum = UnityEngine.Random.Range(0, list.Count);
        T randomValue = list[randomNum];
        GameUtils.SafeWhile(
            () => { return ignoreList != null && ignoreList.Contains(randomValue); },
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
        GameUtils.SafeWhile(
            () => { return ignoreList != null && ignoreList.Contains(randomValue); },
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
        GameUtils.SafeWhile(
            () => { return ignoreList != null && ignoreList.Contains(randomValue); },
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
            Log.Error("可随机的数量小于要获取的数量");
            return null;
        }
        List<T> randomList = new();
        GameUtils.SafeWhile(
            () => { return randomList.Count < getCount; },
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
            Log.Error("可随机的数量小于要获取的数量");
            return null;
        }
        List<T> randomList = new();
        GameUtils.SafeWhile(
            () => { return randomList.Count < getCount; },
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
            Log.Error("可随机的数量小于要获取的数量");
            return null;
        }
        List<T> randomList = new();
        GameUtils.SafeWhile(
            () => { return randomList.Count < getCount; },
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
    /// 列表元素乱序
    /// </summary>
    public static void DisruptOfOrder<T>(List<T> list)
    {
        int index;
        T temp;
        for (int i = 0; i < list.Count; i++)
        {
            index = UnityEngine.Random.Range(0, list.Count);
            if (index != i)
            {
                temp = list[i];
                list[i] = list[index];
                list[index] = temp;
            }
        }
    }
}