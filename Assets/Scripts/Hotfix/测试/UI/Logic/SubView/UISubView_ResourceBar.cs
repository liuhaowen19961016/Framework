using Framework;
using Hotfix;
using UnityEngine;
using UnityEngine.UI;

public class UISubView_ResourceBar : UISubView_ResourceBarBase
{
    protected override void OnShow()
    {
        base.OnShow();
        Debug.LogError("UISubView_ResourceBar OnShow ");
        Debug.LogError("UISubView_ResourceBar OnShow " + parent);
        Debug.LogError("UISubView_ResourceBar OnShow " + uiViewHolder);

        // AddUISubview<UISubView_ResourceBarItem>(UIImg_Bg.rectTransform, 111111);
    }

    protected override void OnInit(object viewData)
    {
        base.OnInit(viewData);
        Debug.LogError("UISubView_ResourceBar OnInit " + viewData);
    }

    protected override void OnCreate()
    {
        base.OnCreate();
        Debug.LogError("UISubView_ResourceBar OnCreate");
    }

    protected override void OnRefresh()
    {
        base.OnRefresh();
        Debug.LogError("UISubView_ResourceBar OnRefresh");
    }

    protected override void OnClose()
    {
        base.OnClose();
        Debug.LogError("UISubView_ResourceBar OnClose");
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        Debug.LogError("UISubView_ResourceBar OnDestroy");
    }
}