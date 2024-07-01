using System;
using System.Collections.Generic;
using Framework;

public static class CollectionExtension
{
    /// <summary>
    /// 打乱顺序
    /// </summary>
    public static void Shuffle<T>(this List<T> list)
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
    
    /// <summary>
    /// 从一个列表中获取一个随机元素
    /// </summary>
    public static T GetRandomValue<T>(this T[] list, List<T> ignoreList = null)
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
    /// 从一个列表中获取一个随机元素
    /// </summary>
    public static T GetRandomValue<T>(this List<T> list, List<T> ignoreList = null)
    {
        var randomValue = GetRandomValue(list.ToArray(), ignoreList);
        return randomValue;
    }
    
    /// <summary>
    /// 从一个列表中获取一个随机元素
    /// </summary>
    public static T GetRandomValue<T>(this Array list, List<T> ignoreList = null)
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
    /// 从一个列表中获取一个随机元素列表
    /// </summary>
    public static List<T> GetRandomValueList<T>(this T[] list, int getCount, bool excludeSame = false, List<T> ignoreList = null)
    {
        int ignoreCount = ignoreList == null ? 0 : ignoreList.Count;
        int canRandomCount = list.Length - ignoreCount;
        if (!excludeSame && canRandomCount < getCount)
        {
            Log.Error("可随机的数量小于要获取的数量");
            return null;
        }
        List<T> randomList = new List<T>();
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
    /// 从一个列表中获取一个随机元素列表
    /// </summary>
    public static List<T> GetRandomValueList<T>(this List<T> list, int getCount, bool excludeSame = false, List<T> ignoreList = null)
    {
        var randomList = GetRandomValueList(list.ToArray(), getCount, excludeSame, ignoreList);
        return randomList;
    }
    
    /// <summary>
    /// 从一个列表中获取一个随机元素列表
    /// </summary>
    public static List<T> GetRandomValueList<T>(this Array list, int getCount, bool excludeSame = false, List<T> ignoreList = null)
    {
        int ignoreCount = ignoreList == null ? 0 : ignoreList.Count;
        int canRandomCount = list.Length - ignoreCount;
        if (!excludeSame && canRandomCount < getCount)
        {
            Log.Error("可随机的数量小于要获取的数量");
            return null;
        }
        List<T> randomList = new List<T>();
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
}