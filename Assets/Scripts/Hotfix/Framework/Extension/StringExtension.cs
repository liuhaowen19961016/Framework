using System.Collections.Generic;
using UnityEngine;

public static class StringExtension
{
    /// <summary>
    /// 字符串中是否包含字符串列表中的某个字符串
    /// </summary>
    public static bool Contains(this string str, List<string> compareStrList)
    {
        if (compareStrList == null || compareStrList.Count <= 0)
            return false;
        foreach (var compareStr in compareStrList)
        {
            if (str.Contains(compareStr))
                return true;
        }
        return false;
    }

    /// <summary>
    /// 标准化路径
    /// </summary>
    public static string NormalizePath(this string str)
    {
        string ret = str.Replace("\\", "/");
        return ret;
    }

    /// <summary>
    /// 截取字符串（通过指定字符串截取）
    /// </summary>
    /// reverseFind：是否反向查找
    public static string Substring(this string str, string splitStr, bool reverseFind = false)
    {
        int index = !reverseFind ? str.IndexOf(splitStr) : str.LastIndexOf(splitStr);
        if (index == -1)
            return str;

        string ret = string.Empty;
        if (!reverseFind)
        {
            ret = str.Substring(index + splitStr.Length);
        }
        else
        {
            ret = str.Substring(0, index);
        }
        return ret;
    }
}