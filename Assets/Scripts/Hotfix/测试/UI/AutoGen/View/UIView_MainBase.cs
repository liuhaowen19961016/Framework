/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-8-1 10:59:47*****/
/*****************************/

using Framework;
using UnityEngine.UI;

public class UIView_MainBase : UIViewBase
{
	protected Text UITxt_Title;
	protected Button UIBtn_Shop;

    protected override void BindComponent()
    {
		UITxt_Title = go.transform.Find("Root/UITxt_Title").GetComponent<Text>();
		UIBtn_Shop = go.transform.Find("Root/UIBtn_Shop").GetComponent<Button>();

    }
}
