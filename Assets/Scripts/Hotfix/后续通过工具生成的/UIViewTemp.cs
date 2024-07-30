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
        { 1, new UIViewConfig() { Id = 1, Path = "xxx", LayerType = (int)EUILayerType.Window, Type = (int)EUIType.FullScreen } },
    };
}