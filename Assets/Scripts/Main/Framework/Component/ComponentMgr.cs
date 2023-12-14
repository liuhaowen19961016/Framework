using System.Collections.Generic;

/// <summary>
/// Component管理器
/// </summary>
public class ComponentMgr : Singleton<ComponentMgr>, ISingletonFixedUpdate, ISingletonUpdate, ISingletonLateUpdate
{
    private static ComponentRoot componentRoot;//ComponentRoot，没实际作用，所有的Component都属于它的子Component
    public static ComponentRoot ComponentRoot => componentRoot;

    private Queue<IFixedUpdate> fixedUpdates = new();
    private Queue<IUpdate> updates = new();
    private Queue<ILateUpdate> lateUpdates = new();

    public override void Register()
    {
        base.Register();
        componentRoot = new ComponentRoot();
    }

    public void Register(ComponentBase component)
    {
        if (component is IFixedUpdate fixedUdate)
        {
            fixedUpdates.Enqueue(fixedUdate);
        }
        if (component is IUpdate update)
        {
            updates.Enqueue(update);
        }
        if (component is ILateUpdate lateUpdate)
        {
            lateUpdates.Enqueue(lateUpdate);
        }
    }

    public void FixedUpdate()
    {

    }

    public void Update()
    {
        if (updates.Count <= 0)
            return;
        int count = updates.Count;
        while (count-- > 0)
        {
            var update = updates.Dequeue();
            update?.Update();
            updates.Enqueue(update);
        }
    }

    public void LateUpdate()
    {

    }
}
