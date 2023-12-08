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
                CommonLog.Error($"请先注册此单例，{typeof(T).Name}");
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
        isDisposed = true;
        ins = null;
    }
}
