/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-08-11 22:15:53*****/
/*****************************/

using Framework;
using UnityEngine.UI;

public class UIView_ShopBase : UIViewBase
{
	protected Button UIBtn_Close;
	protected UISubView_ResourceBar UISubView_ResourceBar;

    protected override void BindComponent()
    {
		UIBtn_Close = go.transform.Find("Root/UIBtn_Close").GetComponent<Button>();
		UISubView_ResourceBar =new UISubView_ResourceBar();
		UISubView_ResourceBar.InternalInit(this, "UISubView_ResourceBar");
		UISubView_ResourceBar.InternalCreateWithoutInstantiate(go.transform.Find("Root/Root2/UIRoot_Node3/UISubView_ResourceBar").gameObject);

    }
}
