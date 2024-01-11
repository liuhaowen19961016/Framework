using UnityEngine;

public class EvtTest : GameEventData
{
    public int v;

    public override void OnInit()
    {
        gameEventType = EGameEventType.Test;
    }

    public override void OnRecycle()
    {
        base.OnRecycle();
        v = 0;
    }
}