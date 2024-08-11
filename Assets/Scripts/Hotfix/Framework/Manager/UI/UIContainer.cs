using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;

public class UIContainer
{
    private GameObject go;
    private UIViewOrUISubViewBase holder;

    private List<UIWidgetBase> widgets = new List<UIWidgetBase>();

    public void InternalInit(GameObject go, UIViewOrUISubViewBase holder)
    {
        this.go = go;
        this.holder = holder;
    }

    public void Refresh<T>(object[] viewDatas, bool reusable, List<Vector3> localPos = null)
        where T : UIWidgetBase
    {
        if (viewDatas == null)
            viewDatas = new object[0];

        for (int i = 0; i < viewDatas.Length; i++)
        {
            UIWidgetBase widget;
            if (widgets.Count > i)
            {
                widget = widgets[i];
                widget.SetViewData(viewDatas[i]);
                widget.InternalShow();
            }
            else
            {
                widget = holder.AddUIWidget<T>(go.transform, reusable, viewDatas[i]);
                widgets.Add(widget);
            }
            if (localPos != null && i >= localPos.Count - 1)
            {
                widget.Go.transform.localPosition = localPos[i];
            }
        }
        for (int i = viewDatas.Length; i < widgets.Count; i++)
        {
            widgets[i].Go.SetActive(false);
        }
    }

    public void ClearAll()
    {
        foreach (var widget in widgets)
        {
            holder.RemoveUIWidget(widget);
        }
        widgets.Clear();
    }
}