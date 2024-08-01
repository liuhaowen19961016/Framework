using System.Collections;
using System.Collections.Generic;
using Framework;

/// <summary>
/// 临时用的注册UI界面的地方
/// </summary>
public class UIViewTemp
{
    public static Dictionary<int, UIViewConfig> UIViewConfigs = new Dictionary<int, UIViewConfig>()
    {
        { 1, new UIViewConfig() { Id = 1, Path = "UIView_Main", LayerType = (int)EUILayerType.Window, Type = (int)EUIType.Main } },
        { 2, new UIViewConfig() { Id = 2, Path = "UIView_Shop", LayerType = (int)EUILayerType.Window, Type = (int)EUIType.FullScreen } },
    };
}