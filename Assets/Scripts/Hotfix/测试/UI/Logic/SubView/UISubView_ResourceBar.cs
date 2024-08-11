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

        object[] objs = new object[3] { 11, 22, 33 };
        UIContainer_ResourceBar.Refresh<UIWidget_ResourceBar>(objs, false);
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (Input.GetKeyDown(KeyCode.S))
        {
            UIContainer_ResourceBar.Refresh<UIWidget_ResourceBar>(null, false);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            object[] objs = new object[2] { 111, 222 };
            UIContainer_ResourceBar.Refresh<UIWidget_ResourceBar>(objs, false);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            object[] objs = new object[5] { 1, 2, 3, 4, 5 };
            UIContainer_ResourceBar.Refresh<UIWidget_ResourceBar>(objs, false);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            UIContainer_ResourceBar.ClearAll();
        }
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