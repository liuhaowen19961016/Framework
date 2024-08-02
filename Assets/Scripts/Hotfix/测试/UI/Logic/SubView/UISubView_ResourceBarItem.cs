using Framework;
using UnityEngine;
using UnityEngine.UI;

public class UISubView_ResourceBarItem : UISubView_ResourceBarItemBase
{
    protected override void OnShow()
    {
        base.OnShow();
        Debug.LogError("UISubView_ResourceBarItem OnShow ");
        Debug.LogError("UISubView_ResourceBarItem OnShow " + parent);
        Debug.LogError("UISubView_ResourceBarItem OnShow " + uiViewHolder);
    }

    protected override void OnClose()
    {
        base.OnClose();
        Debug.LogError("UISubView_ResourceBarItem OnClose");
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        Debug.LogError("UISubView_ResourceBarItem OnDestroy");
    }
}
