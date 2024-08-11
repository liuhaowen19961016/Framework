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

    #region 自动生成UI工具

    public static string UIGENINFOARCHIVEPATH = Application.dataPath + "/../UIGenInfo.json"; //已经生成过的逻辑代码序列化成json存项目根目录下，防止覆盖生成逻辑代码

    //模板路径
    public const string UIVIEW_LOGIC_TEMPLATE_PATH = "Assets/Scripts/Editor/UI/GenerateUI/Template/UIViewLogicTemplate.txt"; //UIView逻辑模板路径
    public const string UIVIEW_VIEW_TEMPLATE_PATH = "Assets/Scripts/Editor/UI/GenerateUI/Template/UIViewViewTemplate.txt"; //UIView界面模板路径
    public const string UISUBVIEW_LOGIC_TEMPLATE_PATH = "Assets/Scripts/Editor/UI/GenerateUI/Template/UISubViewLogicTemplate.txt"; //UISubView逻辑模板路径
    public const string UISUBVIEW_VIEW_TEMPLATE_PATH = "Assets/Scripts/Editor/UI/GenerateUI/Template/UISubViewViewTemplate.txt"; //UISubView界面模板路径
    public const string UIWIDGET_LOGIC_TEMPLATE_PATH = "Assets/Scripts/Editor/UI/GenerateUI/Template/UIWidgetLogicTemplate.txt"; //UIWidget逻辑模板路径
    public const string UIWIDGET_VIEW_TEMPLATE_PATH = "Assets/Scripts/Editor/UI/GenerateUI/Template/UIWidgetViewTemplate.txt"; //UIWidget界面模板路径
    public const string UIVIEWNAME_TEMPLATE_PATH = "Assets/Scripts/Editor/UI/GenerateUI/Template/UIViewNameTemplate.txt"; //UIViewName模板路径

    //生成代码文件夹
    public const string UIVIEW_VIEW_GENCODE_DIR = "Assets/Scripts/Hotfix/测试/UI/AutoGen/View/"; //自动生成UIView界面代码的文件夹
    public const string UISUBVIEW_VIEW_GENCODE_DIR = "Assets/Scripts/Hotfix/测试/UI/AutoGen/SubView/"; //自动生成UISubView界面代码的文件夹
    public const string UIWIDGET_VIEW_GENCODE_DIR = "Assets/Scripts/Hotfix/测试/UI/AutoGen/Widget/"; //自动生成UIWidget界面代码的文件夹
    public const string UIVIEWNAME_GENCODE_PATH = "Assets/Scripts/Hotfix/Logic/UI/UIViewName"; //自动生成UIViewName的文件夹

    public const string SUFFIX_CS = ".cs";
    public const string EXTRANAME_AUTOGEN = "Base";
    public const string PREFIX_UISUBVIEW = "UISubView";
    public const string PREFIX_UICONTAINER = "UIContainer";
    public const string NAMESPACE_DEFINE_TEMPLATE = "using #Namespace#;"; //命名空间定义模板
    public const string COMMON_FIELD_DEFINE_TEMPLATE = "\tprotected #FieldType# #FieldName#;"; //通用字段定义模板
    public const string SUBVIEW_FIELD_DEFINE_TEMPLATE = "\tprotected #FieldType# #FieldName#;"; //子界面字段定义模板
    public const string CONTAINER_FIELD_DEFINE_TEMPLATE = "\tprotected UIContainer #FieldName#;"; //容器字段定义模板
    public const string COMMON_FIELD_BIND_TEMPLATE = "\t\t#FieldName# = go.transform.Find(\"#FieldPath#\").GetComponent<#ComponentType#>();"; //通用字段绑定模板
    public const string SUBVIEW_FIELD_BIND_TEMPLATE = "\t\t#FieldName# =new #FieldName#();\n" +
                                                      "\t\t#FieldName#.InternalInit(this, \"#FieldName#\");\n" +
                                                      "\t\t#FieldName#.InternalCreateWithoutInstantiate(go.transform.Find(\"#FieldPath#\").gameObject);"; //子界面字段绑定模板
    public const string CONTAINER_FIELD_BIND_TEMPLATE = "\t\t#FieldName# =new UIContainer();\n" +
                                                        "\t\t#FieldName#.InternalInit(go.transform.Find(\"#FieldPath#\").gameObject);"; //容器字段绑定模版
    public const string UIVIEWNAME_DEFINE_TEMPLATE = "\tpublic const int #VIEWNAME# = #VIEWID#;\n"; //界面名称定义模板

    #endregion 自动生成UI工具
}