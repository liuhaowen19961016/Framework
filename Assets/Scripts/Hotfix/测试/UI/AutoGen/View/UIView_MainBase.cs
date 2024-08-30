/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-08-30 14:22:59*****/
/*****************************/

using Framework;
using UnityEngine.UI;

public class UIView_MainBase : UIViewBase
{
	protected Text UITxt_Title1;
	protected Text UITxt_Title;
	protected Button UIBtn_Activity;

    protected override void BindComponent()
    {
		UITxt_Title1 = go.transform.Find("Root/UITxt_Title/UITxt_Title1").GetComponent<Text>();
		UITxt_Title = go.transform.Find("Root/UITxt_Title").GetComponent<Text>();
		UIBtn_Activity = go.transform.Find("Root/UIBtn_Activity").GetComponent<Button>();

    }
}
