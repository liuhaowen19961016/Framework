/// <summary>
/// 模块基类
/// </summary>
public abstract class ModuleBase
{
    public virtual void Init()
    {
    }

    public virtual void Start()
    {
    }

    public virtual void FixedUpdate()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void LateUpdate()
    {
    }

    public virtual void OnApplicationPause(bool pauseStatus)
    {
    }

    public virtual void OnApplicationFocus(bool hasFocus)
    {
    }

    public virtual void Dispose()
    {
    }
}