/******************************/
/*****自动生成的UIView界面代码，禁止手动修改*****/
/*****界面逻辑写在子类中*****/
/*****生成时间：2024-09-05 11:47:59*****/
/*****************************/

using Framework;
using UnityEngine.UI;
using UnityEngine;

public class UIView_ActivityBase : UIViewBase
{
	protected Text UITxt_Title2;
	protected Button UIBtn_Activity1;
	protected Button UIBtn_Activity2;
	protected Button UIBtn_Activity3;
	protected Button UIBtn_Close;
	protected UISubView_Test UISubView_Test;
	protected RectTransform UINode_ActivityPage;

    protected override void BindComponent()
    {
		UITxt_Title2 = GO.transform.Find("Root/UITxt_Title2").GetComponent<Text>();
		UIBtn_Activity1 = GO.transform.Find("Root/UIBtn_Activity1").GetComponent<Button>();
		UIBtn_Activity2 = GO.transform.Find("Root/UIBtn_Activity2").GetComponent<Button>();
		UIBtn_Activity3 = GO.transform.Find("Root/UIBtn_Activity3").GetComponent<Button>();
		UIBtn_Close = GO.transform.Find("Root/UIBtn_Close").GetComponent<Button>();
		UISubView_Test =new UISubView_Test();
		UISubView_Test.InternalInit(this, "UISubView_Test");
		UISubView_Test.InternalCreateWithoutInstantiate(GO.transform.Find("Root/UINode_ActivityPage/UISubView_Test").gameObject);
		UINode_ActivityPage = GO.transform.Find("Root/UINode_ActivityPage").GetComponent<RectTransform>();

    }
}
