using System;
using System.Collections.Generic;
using Framework;
using UnityEngine;

public class UIViewOrUISubViewBase : UIBase
{
    private List<UISubViewBase> SubViewList = new List<UISubViewBase>(); //所有子界面
    private List<UISubViewBase> SubViewList_Temp = new List<UISubViewBase>();

    #region 子界面

    public void InternalAddToSubViewList(UISubViewBase subView)
    {
        SubViewList.Add(subView);
    }

    /// <summary>
    /// 添加子界面
    /// </summary>
    public T AddUISubView<T>(Transform trans, object viewData = null)
        where T : UISubViewBase
    {
        Type classType = typeof(T);
        string subViewName = classType.Name;
        T subView = Activator.CreateInstance(classType) as T;
        if (subView == null)
            return null;

        subView.InternalInit(this, subViewName, viewData);
        bool createRet = subView.InternalCreate(trans);
        if (!createRet)
            return null;
        subView.InternalOpen();
        return subView;
    }

    /// <summary>
    /// 移除子界面
    /// </summary>
    public bool RemoveUISubView(UISubViewBase subView)
    {
        foreach (var temp in SubViewList)
        {
            if (temp == subView)
            {
                subView.InternalClose(true);
                SubViewList.Remove(subView);
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 移除所有子界面
    /// </summary>
    public void RemoveAllUISubView()
    {
        foreach (var subView in SubViewList)
        {
            subView.InternalClose(true);
        }
        SubViewList.Clear();
    }

    #endregion 子界面

    protected override void OnOpen()
    {
        base.OnOpen();
        SubViewList.CopyListNonAlloc(SubViewList_Temp);
        foreach (var subView in SubViewList_Temp)
        {
            subView.OnOpen();
        }
    }

    protected override void OnRefresh()
    {
        base.OnRefresh();
        SubViewList.CopyListNonAlloc(SubViewList_Temp);
        foreach (var subView in SubViewList_Temp)
        {
            subView.OnRefresh();
        }
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        SubViewList.CopyListNonAlloc(SubViewList_Temp);
        foreach (var subView in SubViewList_Temp)
        {
            subView.OnUpdate();
        }
    }

    protected override void OnClose()
    {
        base.OnClose();
        SubViewList.CopyListNonAlloc(SubViewList_Temp);
        foreach (var subView in SubViewList_Temp)
        {
            subView.OnClose();
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        SubViewList.CopyListNonAlloc(SubViewList_Temp);
        foreach (var subView in SubViewList_Temp)
        {
            subView.OnDestroy();
        }
        SubViewList.Clear();
    }
}