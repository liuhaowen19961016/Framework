/******************************/
/*****自动生成的UISubView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-8-8 23:3:15*****/
/*****************************/

using Framework;
using UnityEngine.UI;

public class UISubView_ResourceBarItemBase : UISubViewBase
{
	protected Image UIImg_Bg;
	protected HorizontalLayoutGroup UILayoutH_CC;

    protected override void BindComponent()
    {
		UIImg_Bg = go.transform.Find("UIImg_Bg").GetComponent<Image>();
		UILayoutH_CC = go.transform.Find("UILayoutH_CC").GetComponent<HorizontalLayoutGroup>();

    }
}
