using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentRoot : ComponentBase
{
    public ComponentRoot()
    {
        InstanceId = IdUtils.GenInstanceId();
        Root = this;
        Parent = null;
    }
}
