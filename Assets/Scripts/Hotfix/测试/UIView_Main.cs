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
        UIBtn_Shop.onClick.AddListener(() =>
        {
            Debug.LogError("show shop");
        });
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