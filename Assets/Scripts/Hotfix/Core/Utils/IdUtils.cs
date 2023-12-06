using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IdUtils
{
    private static long instanceId;

    public static long GenInstanceId()
    {
        instanceId++;
        return instanceId;
    }
}
