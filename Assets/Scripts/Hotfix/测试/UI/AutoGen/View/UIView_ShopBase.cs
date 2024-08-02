/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-8-2 18:17:16*****/
/*****************************/

using Framework;
using UnityEngine.UI;

public class UIView_ShopBase : UIViewBase
{
	protected Text UITxt_Title;
	protected Button UIBtn_Close;
	protected UISubView_ResourceBar UISubView_ResourceBar;
	protected Text UITxt_Title2;

    protected override void BindComponent()
    {
		UITxt_Title = go.transform.Find("Root/UITxt_Title").GetComponent<Text>();
		UIBtn_Close = go.transform.Find("Root/UIBtn_Close").GetComponent<Button>();
		UISubView_ResourceBar =new UISubView_ResourceBar();
		UISubView_ResourceBar.InternalInit(this, "UISubView_ResourceBar");
		UISubView_ResourceBar.InternalCreate(go.transform.Find("Root/Root2/UIRoot_Node3/UISubView_ResourceBar").gameObject);
		UITxt_Title2 = go.transform.Find("Root/UITxt_Title2").GetComponent<Text>();

    }
}
