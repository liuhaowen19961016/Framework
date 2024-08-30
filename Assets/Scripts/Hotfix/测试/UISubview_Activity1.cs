using Framework;
using UnityEngine;
using UnityEngine.UI;

public class UISubview_Activity1 : UISubview_Activity1Base
{
    protected override void OnClose()
    {
        base.OnClose();
        Debug.LogError("OnClose UISubview_Activity1");
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        Debug.LogError("OnDestroy UISubview_Activity1");
    }
}