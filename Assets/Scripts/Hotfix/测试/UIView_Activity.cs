using Framework;
using Hotfix;
using UnityEngine;
using UnityEngine.UI;

public class UIView_Activity : UIView_ActivityBase
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
UIBtn_Activity1.onClick.AddListener(() =>
{
    //AddUISubview<>()
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
