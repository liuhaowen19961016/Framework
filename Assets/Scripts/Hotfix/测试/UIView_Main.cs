using System;
using Framework;
using Hotfix;
using UnityEngine;
using UnityEngine.UI;

public class UIView_Main : UIView_MainBase
{
    protected override void OnInit(object viewData)
    {
        base.OnInit(viewData);
        viewAniType = EViewAniType.Animator;
        Debug.LogError("UIView_Main OnInit --" + viewData);
    }

    protected override void RegisterUIEvent()
    {
        base.RegisterUIEvent();
        UIBtn_Activity.onClick.AddListener(() => { GameGlobal.UIMgr.OpenSync(2, "2024"); });
    }

    protected override void OnRefresh()
    {
        base.OnRefresh();
        Debug.LogError("UIView_Main OnRefresh --" + ViewData);
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        Debug.LogError("UIView_Main OnShow --" + ViewData);
    }
}