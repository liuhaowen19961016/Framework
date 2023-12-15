using System;
using System.Collections.Generic;

/// <summary>
/// 管理Component
/// </summary>
public class GameComponent
{
    public static ComponentRoot ComponentRoot;//ComponentRoot，没实际作用，所有的Component都属于它的子Component

    private static Queue<IFixedUpdate> fixedUpdates = new();
    private static Queue<IUpdate> updates = new();
    private static Queue<ILateUpdate> lateUpdates = new();

    public static void Init()
    {
        ComponentRoot = new ComponentRoot();
    }

    /// <summary>
    /// 注册（只通过ComponentBase的AddComponent时注册，外界禁止调用！！！！！）
    /// </summary>
    public static void Register(ComponentBase component)
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

    public static void FixedUpdate()
    {
        if (fixedUpdates.Count <= 0)
            return;
        int count = fixedUpdates.Count;
        while (count-- > 0)
        {
            var fixedUpdate = fixedUpdates.Dequeue();
            fixedUpdate?.FixedUpdate();
            fixedUpdates.Enqueue(fixedUpdate);
        }
    }

    public static void Update()
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

    public static void LateUpdate()
    {
        if (lateUpdates.Count <= 0)
            return;
        int count = lateUpdates.Count;
        while (count-- > 0)
        {
            var lateUpdate = lateUpdates.Dequeue();
            lateUpdate?.LateUpdate();
            lateUpdates.Enqueue(lateUpdate);
        }
    }

    public static void Dispose()
    {
        fixedUpdates.Clear();
        updates.Clear();
        lateUpdates.Clear();
        ComponentRoot.Dispose();
    }
}
