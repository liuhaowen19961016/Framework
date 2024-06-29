using System;
using System.Collections.Generic;
using Framework;
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
    /// while循环安全版
    /// </summary>
    public static void SafeWhile(Func<bool> loopCondition, Action loopBody)
    {
        if (loopCondition == null)
        {
            Log.Error("循环条件不能为null");
            return;
        }

        int countLimit = 11111; //最大的循环次数
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
}