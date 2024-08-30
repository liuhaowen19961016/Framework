/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-08-30 10:44:53*****/
/*****************************/

using Framework;
using UnityEngine.UI;

public class UIView_MainBase : UIViewBase
{
	protected Button UIBtn_Activity;

    protected override void BindComponent()
    {
		UIBtn_Activity = go.transform.Find("Root/UIBtn_Activity").GetComponent<Button>();

    }
}
