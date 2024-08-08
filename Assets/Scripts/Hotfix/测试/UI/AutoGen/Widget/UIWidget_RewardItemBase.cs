/******************************/
/*****自动生成的UIWidget界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-8-8 22:59:12*****/
/*****************************/

using Framework;
using UnityEngine.UI;

public class UIWidget_RewardItemBase : UIWidgetBase
{
	protected Text UITxt_Title;

    protected override void BindComponent()
    {
		UITxt_Title = go.transform.Find("Root/UITxt_Title").GetComponent<Text>();

    }
}
