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

    /// <summary>
    /// 设置时间缩放
    /// </summary>
    public static void SetTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
    }

    /// <summary>
    /// 创建GameObject
    /// </summary>
    public static GameObject CreateGameObject(string name, Transform parent, bool canRepeat = false, params Type[] components)
    {
        GameObject go = new GameObject();
        go.name = name;
        go.transform.SetParent(parent, false);
        go.transform.localPosition = Vector2.zero;
        if (!canRepeat)
        {
            HashSet<Type> hashtable = new HashSet<Type>();
            foreach (var temp in components)
            {
                hashtable.Add(temp);
            }
            foreach (var component in hashtable)
            {
                go.AddComponent(component);
            }
        }
        else
        {
            foreach (var component in components)
            {
                go.AddComponent(component);
            }
        }
        return go;
    }

    /// <summary>
    /// 计算从fromTrans节点到toTrans节点的路径
    /// </summary>
    /// formTrans：AA toTrans：BB，return：AA/XX/XX/BB
    public static string CalculateTransPath(Transform fromTrans, Transform toTrans)
    {
        if (!toTrans.IsChildOf(fromTrans))
            return;
        
        string transPath = targetTrans.name;
        Transform parent = targetTrans.parent;
        SafeWhile(() =>
        {
            return parent != null
                   && parent != rootTrans;
        }, () =>
        {
            transPath += parent.name;
            parent = parent.parent;
        });
        return transPath;
    }
}