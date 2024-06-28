using System;
using System.Collections.Generic;
using System.Reflection;
using Framework;

/// <summary>
/// 主工程调用热更工程
/// </summary>
public static class HotfixBridge
{
    private const string hotfixAssemblyName = "Hotfix";

    public static void CallMethod(string nameSpace, string typeName, string methodName, BindingFlags bindingFlags, object obj = null, params object[] args)
    {
        string fullTypeName = string.IsNullOrEmpty(nameSpace) ? typeName : $"{nameSpace}.{typeName}";
        Type type = GetType(fullTypeName);
        if (type == null)
            return;
        MethodInfo methodInfo = type.GetMethod(methodName, bindingFlags);
        if (methodInfo == null)
        {
            Log.Error($"方法获取失败，nameSpace：{nameSpace}，typeName：{typeName}，methodName：{methodName}");
            return;
        }
        methodInfo.Invoke(obj, args);
    }

    public static void CallStaticMethod(string nameSpace, string typeName, string methodName, params object[] args)
    {
        CallMethod(nameSpace, typeName, methodName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, args);
    }

    public static void CallNoneStaticMethod(string nameSpace, string typeName, string methodName, object obj, params object[] args)
    {
        CallMethod(nameSpace, typeName, methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, obj, args);
    }

    #region 缓存热更类

    private static Dictionary<string, Type> types = new();

    public static void Init()
    {
        var tempTypes = ReflectUtils.GetTypes(hotfixAssemblyName);
        if (tempTypes == null)
            return;

        // 缓存type
        foreach (var type in tempTypes)
        {
            types.Add(type.FullName, type);
        }
    }

    public static Dictionary<string, Type> GetTypes()
    {
        return types;
    }

    public static Type GetType(string typeName)
    {
        if (types.TryGetValue(typeName, out var type))
        {
            return type;
        }
        Log.Error($"类获取失败，typeName：{typeName}");
        return null;
    }

    #endregion 缓存热更类
}
