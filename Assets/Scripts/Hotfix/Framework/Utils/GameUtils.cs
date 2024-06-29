using System;
using System.Collections;
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

    private const float DefaultGameTimeScale = 1f; //游戏内默认TimeScale
    /// <summary>
    /// 进入子弹时间
    /// </summary>
    public static IEnumerator EnterBulletTime(float toTimeScale, float duration)
    {
        float curTime = 0;
        var scaleDis = Time.timeScale - toTimeScale;
        while (curTime < duration)
        {
            curTime += Time.unscaledDeltaTime;
            if (Time.timeScale <= toTimeScale)
                Time.timeScale = toTimeScale;
            else
                Time.timeScale -= Time.timeScale * 0.5f;
            yield return null;
        }
        Time.timeScale = DefaultGameTimeScale;
    }

    public static void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
    }
}