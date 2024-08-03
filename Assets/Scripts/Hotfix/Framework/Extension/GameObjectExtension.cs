using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtension
{
    /// <summary>
    /// 设置X坐标
    /// </summary>
    public static void SetPosX(this GameObject go, float x)
    {
        var tempPos = go.transform.position;
        tempPos.x = x;
        go.transform.position = tempPos;
    }

    /// <summary>
    /// 设置Y坐标
    /// </summary>
    public static void SetPosY(this GameObject go, float y)
    {
        var tempPos = go.transform.position;
        tempPos.y = y;
        go.transform.position = tempPos;
    }

    /// <summary>
    /// 设置Z坐标
    /// </summary>
    public static void SetPosZ(this GameObject go, float z)
    {
        var tempPos = go.transform.position;
        tempPos.z = z;
        go.transform.position = tempPos;
    }

    /// <summary>
    /// 设置局部X坐标
    /// </summary>
    public static void SetLocalPosX(this GameObject go, float x)
    {
        var tempLocalPos = go.transform.localPosition;
        tempLocalPos.x = x;
        go.transform.localPosition = tempLocalPos;
    }

    /// <summary>
    /// 设置局部Y坐标
    /// </summary>
    public static void SetLocalPosY(this GameObject go, float y)
    {
        var tempLocalPos = go.transform.localPosition;
        tempLocalPos.y = y;
        go.transform.localPosition = tempLocalPos;
    }

    /// <summary>
    /// 设置局部Z坐标
    /// </summary>
    public static void SetLocalPosZ(this GameObject go, float z)
    {
        var tempLocalPos = go.transform.localPosition;
        tempLocalPos.z = z;
        go.transform.localPosition = tempLocalPos;
    }

    /// <summary>
    /// 设置X旋转
    /// </summary>
    public static void SetRotX(this GameObject go, float x)
    {
        var tempRot = go.transform.eulerAngles;
        tempRot.x = x;
        go.transform.eulerAngles = tempRot;
    }

    /// <summary>
    /// 设置Y旋转
    /// </summary>
    public static void SetRotY(this GameObject go, float y)
    {
        var tempRot = go.transform.eulerAngles;
        tempRot.y = y;
        go.transform.eulerAngles = tempRot;
    }

    /// <summary>
    /// 设置Z旋转
    /// </summary>
    public static void SetRotZ(this GameObject go, float z)
    {
        var tempRot = go.transform.eulerAngles;
        tempRot.z = z;
        go.transform.eulerAngles = tempRot;
    }

    /// <summary>
    /// 设置局部X旋转
    /// </summary>
    public static void SetLocalRotX(this GameObject go, float x)
    {
        var tempLocalRot = go.transform.localEulerAngles;
        tempLocalRot.x = x;
        go.transform.localEulerAngles = tempLocalRot;
    }

    /// <summary>
    /// 设置局部Y旋转
    /// </summary>
    public static void SetLocalRotY(this GameObject go, float y)
    {
        var tempLocalRot = go.transform.localEulerAngles;
        tempLocalRot.y = y;
        go.transform.localEulerAngles = tempLocalRot;
    }

    /// <summary>
    /// 设置局部Z旋转
    /// </summary>
    public static void SetLocalRotZ(this GameObject go, float z)
    {
        var tempLocalRot = go.transform.localEulerAngles;
        tempLocalRot.z = z;
        go.transform.localEulerAngles = tempLocalRot;
    }

    /// <summary>
    /// 设置X缩放
    /// </summary>
    public static void SetScaleX(this GameObject go, float x)
    {
        var tempScale = go.transform.localScale;
        tempScale.x = x;
        go.transform.localScale = tempScale;
    }

    /// <summary>
    /// 设置Y缩放
    /// </summary>
    public static void SetScaleY(this GameObject go, float y)
    {
        var tempScale = go.transform.localScale;
        tempScale.y = y;
        go.transform.localScale = tempScale;
    }

    /// <summary>
    /// 设置Z缩放
    /// </summary>
    public static void SetScaleZ(this GameObject go, float z)
    {
        var tempScale = go.transform.localScale;
        tempScale.z = z;
        go.transform.localScale = tempScale;
    }

    /// <summary>
    /// 设置父物体
    /// </summary>
    public static void SetParent(this GameObject go, GameObject parentGo, bool worldPositionStays = true)
    {
        go.transform.SetParent(parentGo.transform, worldPositionStays);
    }

    /// <summary>
    /// 获取组件
    /// </summary>
    public static T GetComponent<T>(this GameObject go, bool forceGet = false)
        where T : Component
    {
        if (go == null)
            return null;
        T component = go.GetComponent<T>();
        if (component == null && forceGet)
            component = go.AddComponent<T>();
        return component;
    }

    /// <summary>
    /// 添加组件
    /// </summary>
    public static T AddComponent<T>(this GameObject go, bool canRepeat = false)
        where T : Component
    {
        if (go == null)
            return null;
        T component = go.GetComponent<T>();
        if (component != null && !canRepeat)
            return null;
        component = go.AddComponent<T>();
        return component;
    }

    /// <summary>
    /// 销毁游戏物体
    /// </summary>
    public static bool Destroy(this GameObject go)
    {
        if (go == null)
            return false;
        Object.Destroy(go);
        return true;
    }

    public static void Reset(this GameObject go)
    {
        go.transform.position = Vector3.zero;
        go.transform.rotation = Quaternion.identity;
        go.transform.localScale = Vector3.one;
    }

    public static void ResetLocal(this GameObject go)
    {
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = Vector3.one;
    }

    /// <summary>
    /// 获取根父节点
    /// </summary>
    public static GameObject GetParentRoot(this GameObject go)
    {
        Transform parent = go.transform;
        while (parent.parent != null)
        {
            parent = parent.parent;
        }
        return parent.gameObject;
    }
}