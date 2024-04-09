using System.Reflection;
using System;

/// <summary>
/// 反射工具类
/// </summary>
public static class ReflectUtils
{
    public static Assembly LoadAssembly(string assemblyName)
    {
        Assembly assembly = Assembly.Load(assemblyName);
        if (assembly == null)
        {
            Log.Error($"程序集获取失败，assemblyName：{assemblyName}");
            return null;
        }
        return assembly;
    }

    public static T Create<T>()
    {
        T type = Activator.CreateInstance<T>();
        return type;
    }

    public static object Create(Type type)
    {
        object obj = Activator.CreateInstance(type);
        return obj;
    }

    public static Type[] GetTypes(string assemblyName)
    {
        Assembly assembly = LoadAssembly(assemblyName);
        var types = assembly?.GetTypes();
        return types;
    }

    public static Type GetType(string assemblyName, string nameSpace, string typeName)
    {
        Assembly assembly = LoadAssembly(assemblyName);
        if (assembly == null)
            return null;
        Type type = assembly.GetType($"{nameSpace}.{typeName}");
        if (type == null)
        {
            Log.Error($"类获取失败，assemblyName：{assemblyName}，nameSpace：{nameSpace}，typeName：{typeName}");
            return null;
        }
        return type;
    }

    public static void CallMethod(string assemblyName, string nameSpace, string typeName, string methodName, BindingFlags bindingFlags, object obj = null, params object[] args)
    {
        Type type = GetType(assemblyName, nameSpace, typeName);
        if (type == null)
            return;
        MethodInfo methodInfo = type.GetMethod(methodName, bindingFlags);
        if (methodInfo == null)
        {
            Log.Error($"方法获取失败，assemblyName：{assemblyName}，nameSpace：{nameSpace}，typeName：{typeName}，methodName：{methodName}");
            return;
        }
        methodInfo.Invoke(obj, args);
    }

    public static void CallStaticMethod(string assemblyName, string nameSpace, string typeName, string methodName, params object[] args)
    {
        CallMethod(assemblyName, nameSpace, typeName, methodName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, args);
    }

    public static void CallNoneStaticMethod(string assemblyName, string nameSpace, string typeName, string methodName, object obj, params object[] args)
    {
        CallMethod(assemblyName, nameSpace, typeName, methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, obj, args);
    }
}