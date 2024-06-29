using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class UGUIExtension 
{
    public static void SetHorizontalOrVerticalLayoutGroup(this HorizontalOrVerticalLayoutGroup horizontalOrVerticalLayoutGroup, float spacing = 0
        , bool controlChildSizeX = false, bool controlChildSizeY = false, bool useChildScaleX = false, bool useChildScaleY = false, bool childForceExpandX = false, bool childForceExpandY = false)
    {
        horizontalOrVerticalLayoutGroup.childControlWidth = controlChildSizeX;
        horizontalOrVerticalLayoutGroup.childControlHeight = controlChildSizeY;
        horizontalOrVerticalLayoutGroup.childScaleWidth = useChildScaleX;
        horizontalOrVerticalLayoutGroup.childScaleHeight = useChildScaleY;
        horizontalOrVerticalLayoutGroup.childForceExpandWidth = childForceExpandX;
        horizontalOrVerticalLayoutGroup.childForceExpandHeight = childForceExpandY;
        horizontalOrVerticalLayoutGroup.spacing = spacing;
    }
}
