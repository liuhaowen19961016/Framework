/******************************/
/*****自动生成的UISubView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-8-1 13:46:8*****/
/*****************************/

using Framework;
using UnityEngine.UI;

public class UISubView_ResourceBarItemBase : UISubViewBase
{
    protected Image UIImg_Bg;
    protected UISubView_ResourceBarItemBase resourceBarItem;

    protected override void BindComponent()
    {
        UIImg_Bg = go.transform.Find("UIImg_Bg").GetComponent<Image>();

        //todo test
        resourceBarItem = new UISubView_ResourceBarItem();
        resourceBarItem.InternalInit(this, "resourceBarItem");
        resourceBarItem.InternalCreate(UIImg_Bg.gameObject);
    }
}