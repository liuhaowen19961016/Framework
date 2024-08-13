using System;
using System.Collections.Generic;
using System.Linq;
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
    /// 获取一个随机的枚举值
    /// </summary>
    public static T GetRandomEmum<T>(List<T> ignoreList = null)
        where T : Enum
    {
        Array valueArray = Enum.GetValues(typeof(T));
        T randomEnum = valueArray.GetRandomValue(ignoreList);
        return randomEnum;
    }

    /// <summary>
    /// 获取一个随机的枚举值列表
    /// </summary>
    public static List<T> GetRandomEnumList<T>(int getCount, bool excludeSame = false, List<T> ignoreList = null)
        where T : Enum
    {
        Array valueArray = Enum.GetValues(typeof(T));
        List<T> randomEnumList = valueArray.GetRandomValueList<T>(getCount, excludeSame, ignoreList);
        return randomEnumList;
    }
    
    /// <summary>
    /// 获取随机权重index
    /// </summary>
    public static int GetRandomWeightResultIndex(List<int> weight)
    {
        var totalWight = weight.Sum();
            
        var randomInt = UnityEngine.Random.Range(0, totalWight);
           
        for (var i = 0; i < weight.Count; i++)
        {
            if (randomInt < weight[i])
            {
                return i;
            }
            else
            {
                randomInt -= weight[i];
            }
        }

        return weight.Count;
    }
}