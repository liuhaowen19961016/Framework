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
        for (int i = 0, len = SubViewList.Count; i < len; i++)
        {
            if (SubViewList[i] == subView)
            {
                SubViewList[i].InternalClose(true);
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
        for (int i = 0, len = SubViewList.Count; i < len; i++)
        {
            var subView = SubViewList[i];
            if (subView == null)
                continue;
            subView.InternalClose(true);
        }
        SubViewList.Clear();
    }

    #endregion 子界面

    protected override void OnOpen()
    {
        base.OnOpen();
        for (int i = 0, len = SubViewList.Count; i < len; i++)
        {
            var subView = SubViewList[i];
            if (subView == null)
                continue;
            subView.OnOpen();
        }
    }

    protected override void OnRefresh()
    {
        base.OnRefresh();
        for (int i = 0, len = SubViewList.Count; i < len; i++)
        {
            var subView = SubViewList[i];
            if (subView == null)
                continue;
            subView.OnRefresh();
        }
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        for (int i = 0, len = SubViewList.Count; i < len; i++)
        {
            var subView = SubViewList[i];
            if (subView == null)
                continue;
            subView.OnUpdate();
        }
    }

    protected override void OnClose()
    {
        base.OnClose();
        for (int i = 0, len = SubViewList.Count; i < len; i++)
        {
            var subView = SubViewList[i];
            if (subView == null)
                continue;
            subView.OnClose();
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        for (int i = 0, len = SubViewList.Count; i < len; i++)
        {
            var subView = SubViewList[i];
            if (subView == null)
                continue;
            subView.OnDestroy();
        }
        SubViewList.Clear();
    }
}