using UnityEngine;

/// <summary>
/// 编辑器常量
/// </summary>
public class EditorConst
{
    private const string TOOLBAR_GAMETOOL = "游戏工具/";
    private const string TOOLBAR_UITOOL = "游戏工具/UI工具/";
    private const string ASSETS_UITOOL = "Assets/UI工具/";

    //工具栏
    public const string ClearProgressBar = TOOLBAR_GAMETOOL + "ClearProgressBar";
    public const string GenUIViewName = TOOLBAR_UITOOL + "生成UIViewName";
    public const string OpenGenUIInfoDir = TOOLBAR_UITOOL + "打开生成UI信息文件夹";

    //Assets
    public const string ReplaceButton2GameButton = ASSETS_UITOOL + "替换组件 Button->GameButton";
    
    public const string SUFFIX_CS = ".cs";
}