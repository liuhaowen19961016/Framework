using Framework;
using Hotfix;
using UnityEngine;
using UnityEngine.UI;

public class UISubView_ResourceBar : UISubView_ResourceBarBase
{
    protected override void OnShow()
    {
        base.OnShow();
        Debug.LogError("ResourceBar OnShow");
        Debug.LogError("ResourceBar OnShow " + parent);
        Debug.LogError("ResourceBar OnShow " + uiViewHolder);
        AddUISubview(2, UIImg_Bg.transform, "hh");
    }

    protected override void OnInit(object viewData)
    {
        base.OnInit(viewData);
        Debug.LogError("ResourceBar OnInit " + viewData);
    }

    protected override void OnCreate()
    {
        base.OnCreate();
        Debug.LogError("ResourceBar OnCreate");
    }

    protected override void OnRefresh()
    {
        base.OnRefresh();
        Debug.LogError("ResourceBar OnRefresh");
    }

    protected override void OnClose()
    {
        base.OnClose();
        Debug.LogError("ResourceBar OnClose");
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        Debug.LogError("ResourceBar OnDestroy");
    }
}