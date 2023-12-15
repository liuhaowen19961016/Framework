using System;

/// <summary>
/// 所有Component的根节点
/// </summary>
public class ComponentRoot : ComponentBase
{
    public ComponentRoot()
    {
        instanceId = IdUtils.GenInstanceId();
        Root = this;
        Parent = null;
    }
}
