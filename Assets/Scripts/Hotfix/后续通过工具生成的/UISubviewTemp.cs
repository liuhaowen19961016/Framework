using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISubviewTemp
{
    public static Dictionary<int, UISubViewConfig> UISubViewConfigs = new Dictionary<int, UISubViewConfig>()
    {
        { 1, new UISubViewConfig() { Id = 1, Path = "UISubView_ResourceBar" } },
        { 2, new UISubViewConfig() { Id = 2, Path = "UISubView_ResourceBarItem" } },
    };
}