using UnityEngine;
using System;
using System.Collections.Generic;

public static class GameUtils
{
    /// <summary>
    /// 获取一个随机的枚举值
    /// </summary>
    public static T GetRandomEmum<T>()
    {
        Array values = Enum.GetValues(typeof(T));
        int randumNum = UnityEngine.Random.Range(0, values.Length);
        T randomEnum = (T)values.GetValue(randumNum);
        return randomEnum;
    }

    /// <summary>
    /// 获取一个随机的枚举值列表
    /// </summary>
    public static List<T> GetRandomEnumList<T>(int getCount, bool excludeSame = false)
    {
        Array values = Enum.GetValues(typeof(T));
        if (!excludeSame && values.Length < getCount)
        {
            Log.Error("枚举类型数量小于要获取的数量");
            return null;
        }
        List<T> enumList = new();
        while (enumList.Count < getCount)
        {
            var value = GetRandomEmum<T>();
            enumList.Add(value);
        }
        return enumList;
    }
}
