using Framework;
using UnityEngine;
using UnityEngine.UI;

public class UIView_Main : UIView_MainBase
{
    protected override void OnInit(object viewData)
    {
        base.OnInit(viewData);
        Debug.LogError("UIView_Main OnInit --" + viewData);
    }

    protected override void OnRefresh()
    {
        base.OnRefresh();
        Debug.LogError("UIView_Main OnRefresh --" + viewData);
    }

    protected override void OnShow()
    {
        base.OnShow();
        Debug.LogError("UIView_Main OnShow --" + viewData);
    }
}