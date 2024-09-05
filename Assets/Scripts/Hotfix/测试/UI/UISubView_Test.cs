using Framework;
using UnityEngine;
using UnityEngine.UI;

public class UISubView_Test : UISubView_TestBase
{
    protected override void OnShow()
    {
        base.OnShow();
        Debug.LogError("OnShow UISubView_Test");
    }
    
    protected override void OnOpen()
    {
        base.OnOpen();
        Debug.LogError("OnOpen UISubView_Test");
    }
}
