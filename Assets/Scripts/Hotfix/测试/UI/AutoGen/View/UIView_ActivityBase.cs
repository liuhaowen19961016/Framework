/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-08-30 10:46:58*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using UnityEngine;

public class UIView_ActivityBase : UIViewBase
{
	protected Button UIBtn_Close;
	protected Button UIBtn_Activity1;
	protected Button UIBtn_Activity2;
	protected Button UIBtn_Activity3;
	protected RectTransform UINode_ActivityPage;

    protected override void BindComponent()
    {
		UIBtn_Close = go.transform.Find("Root/UIBtn_Close").GetComponent<Button>();
		UIBtn_Activity1 = go.transform.Find("Root/UIBtn_Activity1").GetComponent<Button>();
		UIBtn_Activity2 = go.transform.Find("Root/UIBtn_Activity2").GetComponent<Button>();
		UIBtn_Activity3 = go.transform.Find("Root/UIBtn_Activity3").GetComponent<Button>();
		UINode_ActivityPage = go.transform.Find("Root/UINode_ActivityPage").GetComponent<RectTransform>();

    }
}
