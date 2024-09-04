using System.Collections.Generic;
using Framework;
using Hotfix;
using UnityEngine;
using UnityEngine.UI;

public class UIView_Activity : UIView_ActivityBase
{
    private Dictionary<int, UISubViewBase> pageDict = new Dictionary<int, UISubViewBase>();
    private int lastActivityId;
    private int curActivityId;

    protected override void OnInit(object viewData)
    {
        base.OnInit(viewData);
        Debug.LogError("UIView_Activity OnInit --" + viewData);
    }

    protected override void RegisterUIEvent()
    {
        base.RegisterUIEvent();
        UIBtn_Activity1.onClick.AddListener(() => OnActivityBtn(1));
        UIBtn_Activity2.onClick.AddListener(() => OnActivityBtn(2));
        UIBtn_Activity3.onClick.AddListener(() => OnActivityBtn(3));
        UIBtn_Close.onClick.AddListener(() => Close());
    }

    private void OnActivityBtn(int activityId)
    {
        if (lastActivityId == activityId)
            return;

        if (pageDict.TryGetValue(lastActivityId, out var lastSubView))
        {
            ClosePage(lastActivityId);
        }
        curActivityId = activityId;
        lastActivityId = curActivityId;
        if (!pageDict.TryGetValue(curActivityId, out var subView))
        {
            UISubViewBase subViewPage = null;
            if (activityId == 1)
            {
                subViewPage = OpenUISubView<UISubview_Activity1>(UINode_ActivityPage, "1");
            }
            else if (activityId == 2)
            {
                subViewPage = OpenUISubView<UISubview_Activity2>(UINode_ActivityPage, "2");
            }
            else if (activityId == 3)
            {
                subViewPage = OpenUISubView<UISubview_Activity3>(UINode_ActivityPage, "3");
            }
            pageDict.Add(curActivityId, subViewPage);
        }
        else
        {
            UISubViewBase subViewPage = null;
            if (activityId == 1)
            {
                subViewPage = subView as UISubview_Activity1;
            }
            else if (activityId == 2)
            {
                subViewPage = subView as UISubview_Activity2;
            }
            else if (activityId == 3)
            {
                subViewPage = subView as UISubview_Activity3;
            }
            ShowUISubView(subViewPage);
        }
    }

    public void ClosePage(int activityId)
    {
        CloseUISubView(pageDict[activityId], true);
        pageDict.Remove(activityId);
        lastActivityId = -1;
    }

    protected override void OnShow()
    {
        base.OnShow();
        Debug.LogError("UIView_Activity OnShow --" + ViewData);
    }

    protected override void OnOpen()
    {
        base.OnOpen();
        Debug.LogError("UIView_Activity OnOpen --" + ViewData);
    }
}