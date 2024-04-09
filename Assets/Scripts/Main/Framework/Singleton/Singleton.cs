using System;
/// <summary>
/// 单例模版
/// </summary>
[Obsolete]
public class Singleton<T> : ISingleton
    where T : Singleton<T>
{
    private static T ins;
    public static T Ins
    {
        get
        {
            if (ins == null)
            {
                Log.Error($"请先注册此单例，{typeof(T).FullName}");
            }
            return ins;
        }
    }

    private bool isDisposed;
    public bool IsDisposed => isDisposed;

    public virtual void Register()
    {
        if (ins != null)
            return;
        ins = (T)this;
    }

    public virtual void UnRegister()
    {
        if (isDisposed)
        {
            return;
        }
        OnDispose();
        isDisposed = true;
        ins = null;
    }

    protected virtual void OnDispose()
    {

    }
}
