using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class RectTransformExtension 
{
    public static void SetRectSize(this RectTransform transform, Vector2 size)
    {
        transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
        transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);
    }
}
