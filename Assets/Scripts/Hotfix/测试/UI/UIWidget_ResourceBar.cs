using Framework;
using UnityEngine;
using UnityEngine.UI;

public class UIWidget_ResourceBar : UIWidget_ResourceBarBase
{
    protected override void OnShow()
    {
        base.OnShow();
        Debug.LogError("UIWidget_ResourceBar OnShow ");
        Debug.LogError("UIWidget_ResourceBar OnShow " + parent);
        Debug.LogError("UIWidget_ResourceBar OnShow " + uiViewHolder);
    }

    protected override void OnInit(object viewData)
    {
        base.OnInit(viewData);
        Debug.LogError("UIWidget_ResourceBar OnInit " + viewData);
    }
    
    protected override void OnClose()
    {
        base.OnClose();
        Debug.LogError("UIWidget_ResourceBar OnClose");
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        Debug.LogError("UIWidget_ResourceBar OnDestroy");
    }
}
