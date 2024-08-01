/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-8-1 10:58:28*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using UnityEngine;

public class UIView_ShopBase : UIViewBase
{
	protected Text UITxt_Title;
	protected Button UIBtn_Close;
	protected RectTransform UINode_ResourceBar;

    protected override void BindComponent()
    {
		UITxt_Title = go.transform.Find("Root/UITxt_Title").GetComponent<Text>();
		UIBtn_Close = go.transform.Find("Root/UIBtn_Close").GetComponent<Button>();
		UINode_ResourceBar = go.transform.Find("UINode_ResourceBar").GetComponent<RectTransform>();

    }
}
