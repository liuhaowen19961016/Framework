using System;
using System.Collections.Generic;
using Framework;
using UnityEngine;

public class UIViewOrUISubViewBase : UIBase
{
    private List<UISubViewBase> SubViewList = new List<UISubViewBase>(); //所有子界面

    #region 子界面

    public void InternalAddToSubViews(UISubViewBase subView)
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
        if (subView == null)
            return false;

        subView.InternalClose(true);
        SubViewList.Remove(subView);
        return true;
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
        foreach (var subView in SubViewList)
        {
            subView.OnOpen();
        }
    }

    protected override void OnRefresh()
    {
        base.OnRefresh();
        foreach (var subView in SubViewList)
        {
            subView.OnRefresh();
        }
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        foreach (var subView in SubViewList)
        {
            subView.OnUpdate();
        }
    }

    protected override void OnClose()
    {
        base.OnClose();
        foreach (var subView in SubViewList)
        {
            subView.OnClose();
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        foreach (var subView in SubViewList)
        {
            subView.OnDestroy();
        }
        SubViewList.Clear();
    }
}