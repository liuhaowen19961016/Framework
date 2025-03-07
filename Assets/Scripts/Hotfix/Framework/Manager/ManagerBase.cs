/// <summary>
/// Manager基类（职责重的称为Manager）
/// </summary>
public abstract class ManagerBase
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

    public virtual void Update(float deltaTime)
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