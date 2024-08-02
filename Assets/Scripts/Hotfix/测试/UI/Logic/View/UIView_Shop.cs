using Framework;
using Hotfix;
using UnityEngine;
using UnityEngine.UI;

public class UIView_Shop : UIView_ShopBase
{
    protected override void OnShow()
    {
        base.OnShow();
        Debug.LogError("UIView_Shop OnShow ");
        Debug.LogError("UIView_Shop OnShow " + parent);
        Debug.LogError("UIView_Shop OnShow " + uiViewHolder);
        AddUISubview<UISubView_ResourceBar>(UINode_ResourceBar, "611");
    }

    protected override void OnInit(object viewData)
    {
        base.OnInit(viewData);
        Debug.LogError("UIView_Shop OnInit " + viewData);
    }

    protected override void OnCreate()
    {
        base.OnCreate();
        Debug.LogError("UIView_Shop OnCreate");
    }

    protected override void OnRefresh()
    {
        base.OnRefresh();
        Debug.LogError("UIView_Shop OnRefresh");
    }

    protected override void OnClose()
    {
        base.OnClose();
        Debug.LogError("UIView_Shop OnClose");
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        Debug.LogError("UIView_Shop OnDestroy");
    }

    protected override void RegisterUIEvent()
    {
        base.RegisterUIEvent();

        UIBtn_Close.onClick.AddListener(() => GameGlobal.UIMgr.Close(2));
    }
}