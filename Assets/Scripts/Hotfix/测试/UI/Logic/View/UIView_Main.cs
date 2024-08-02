using Framework;
using Hotfix;
using UnityEngine;
using UnityEngine.UI;

public class UIView_Main : UIView_MainBase
{
    protected override void OnShow()
    {
        base.OnShow();
        Debug.LogError("UIView_Main OnShow");
        Debug.LogError("UIView_Main OnShow " + parent);
        Debug.LogError("UIView_Main OnShow " + uiViewHolder);
    }

    protected override void OnInit(object viewData)
    {
        base.OnInit(viewData);
        Debug.LogError("UIView_Main OnInit "+viewData);
    }

    protected override void OnCreate()
    {
        base.OnCreate();
        Debug.LogError("UIView_Main OnCreate");
    }

    protected override void OnRefresh()
    {
        base.OnRefresh();
        Debug.LogError("UIView_Main OnRefresh");
    }

    protected override void OnClose()
    {
        base.OnClose();
        Debug.LogError("UIView_Main OnClose");
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        Debug.LogError("UIView_Main OnDestroy");
    }

    protected override void RegisterUIEvent()
    {
        base.RegisterUIEvent();

        UIBtn_Shop.onClick.AddListener(() =>
        {
            GameGlobal.UIMgr.ShowSync(2,"shop111");
        });
    }
}