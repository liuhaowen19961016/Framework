using Framework;
using Hotfix;
using UnityEngine;
using UnityEngine.UI;

public class UISubview_Activity1 : UISubview_Activity1Base
{
    protected override void OnClose()
    {
        base.OnClose();
        Debug.LogError("OnClose UISubview_Activity1 " + ViewData);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        Debug.LogError("OnDestroy UISubview_Activity1 " + ViewData);
    }

    protected override void OnShow()
    {
        base.OnShow();
        Debug.LogError("OnShow UISubview_Activity1 " + ViewData);
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        Debug.LogError("OnOpen UISubview_Activity1 " + ViewData);
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (Input.GetKeyDown(KeyCode.D))
        {
            (Parent as UIView_Activity)?.ClosePage(1);
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            GameGlobal.UIMgr.OpenSync(2,"hh");
        }
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameGlobal.UIMgr.Close(2);
        }
    }
}