using System;

/// <summary>
/// 单例模版
/// </summary>
public abstract class Singleton<T>
    where T : class
{
    private static readonly object syncRoot = new object();
    private static T _Ins;
    public static T Ins
    {
        get
        {
            if (_Ins == null)
            {
                lock (syncRoot)
                {
                    if (_Ins == null)
                    {
                        _Ins = Activator.CreateInstance(typeof(T), true) as T;
                    }
                }
            }
            return _Ins;
        }
    }

    protected Singleton()
    {
        Init();
    }

    public virtual void Init()
    {

    }
}

////使用反射构造对象是为了每个子类都能够将构造函数私有化，防止外界通过new构造，如果在单例模版中通过new构造，那么T必须约束为new()，子类则不能定义私有构造函数
//public class TestClass : Singleton<TestClass>
//{
//    private TestClass()
//    {

//    }
//}