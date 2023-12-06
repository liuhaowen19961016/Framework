public class Singleton<T> : ISingleton
    where T : Singleton<T>
{
    private static T ins;
    public static T Ins => ins;

    private bool isDisposed;

    public void Register()
    {
        if (ins != null)
            return;
        ins = (T)this;
    }

    public void UnRegister()
    {
        if (isDisposed)
        {
            return;
        }
        isDisposed = true;
    }

    public bool IsDisposed()
    {
        return isDisposed;
    }
}
