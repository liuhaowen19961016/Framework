using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;

public class UIContainer
{
    private GameObject go;
    private UIBase holder;

    private List<UIWidgetBase> widgets = new List<UIWidgetBase>();

    public void InternalInit(GameObject go, UIBase holder)
    {
        this.go = go;
        this.holder = holder;
    }

    public void Refresh<T>(object[] viewDatas, bool reusable)
        where T : UIWidgetBase
    {
        for (int i = 0; i < viewDatas.Length; i++)
        {
            UIWidgetBase widget;
            if (widgets.Count > i)
            {
                widget = widgets[i];
            }
            else
            {
                widget = holder.AddUIWidget<T>(go.transform, reusable, viewDatas[i]);
                widgets.Add(widget);
            }
        }
        for (int i = viewDatas.Length; i < widgets.Count; i++)
        {
            widgets[i].Go.SetActive(false);
        }
    }
}