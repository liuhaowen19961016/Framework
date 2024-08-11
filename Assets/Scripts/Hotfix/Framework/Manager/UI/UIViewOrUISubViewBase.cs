using System;
using System.Collections.Generic;
using Framework;
using UnityEngine;

public class UIViewOrUISubViewBase : UIBase
{
    public Dictionary<string, UISubViewBase> SubViews = new Dictionary<string, UISubViewBase>(); //所有子界面

    #region 子界面

    /// <summary>
    /// 添加子界面
    /// </summary>
    protected T AddUISubview<T>(Transform trans, object viewData = null)
        where T : UISubViewBase
    {
        Type type = typeof(T);
        string subViewName = type.Name;
        var classType = Type.GetType(subViewName);
        T subView = Activator.CreateInstance(classType) as T;
        if (subView == null)
            return null;

        subView.InternalInit(this, subViewName, viewData);
        bool createRet = subView.InternalCreate(trans);
        if (!createRet)
        {
            return null;
        }
        subView.InternalShow();
        return subView;
    }

    /// <summary>
    /// 移除子界面
    /// </summary>
    public bool RemoveUISubView<T>()
        where T : UISubViewBase
    {
        string subViewName = typeof(T).Name;
        if (SubViews.TryGetValue(subViewName, out var subView))
        {
            subView.InternalDestory();
            SubViews.Remove(subViewName);
            return true;
        }
        return false;
    }

    /// <summary>
    /// 查找子界面
    /// </summary>
    public T FindUISubView<T>()
        where T : UISubViewBase
    {
        string subViewName = typeof(T).Name;
        if (SubViews.TryGetValue(subViewName, out var subView))
        {
            return subView as T;
        }
        return null;
    }

    public void RemoveAllUISubView()
    {
        foreach (var subView in SubViews.Values)
        {
            subView.InternalDestory();
        }
        SubViews.Clear();
    }

    #endregion 子界面

    protected override void OnShow()
    {
        base.OnShow();
        foreach (var subView in SubViews.Values)
        {
            subView.OnShow();
        }
    }

    protected override void OnRefresh()
    {
        base.OnRefresh();
        foreach (var subView in SubViews.Values)
        {
            subView.OnRefresh();
        }
    }

    protected override void OnClose()
    {
        base.OnClose();
        foreach (var subView in SubViews.Values)
        {
            subView.OnClose();
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        foreach (var subView in SubViews.Values)
        {
            subView.OnDestroy();
        }
        SubViews.Clear();
    }
}