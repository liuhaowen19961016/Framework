using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

/// <summary>
/// 生成UI工具
/// </summary>
public class GenerateUIEditor
{
    //**********只有按照以下规范命名的节点才会生成，不区分大小写，可扩展
    private static Dictionary<string, Type> Name2ComponentType = new Dictionary<string, Type>()
    {
        { "uibtn", typeof(Button) },
        { "uitxt", typeof(Text) },
        //{ "uitxt", typeof(TextMeshProUGUI) },
        { "uiimg", typeof(Image) },
        { "uirawimg", typeof(RawImage) },
        { "uitoggle", typeof(Toggle) },
        { "uitg", typeof(ToggleGroup) },
        { "uilayouth", typeof(HorizontalLayoutGroup) },
        { "uilayoutv", typeof(VerticalLayoutGroup) },
        { "uilayouthv", typeof(GridLayoutGroup) },
        { "uiif", typeof(InputField) },
        { "uislider", typeof(Slider) },
        { "uinode", typeof(RectTransform) },
    };

    private const string UIGENINFOFILEPATH = "../UIGenInfo.json"; //已经生成过逻辑view序列化成json存项目根目录下，防止覆盖生成逻辑View
    private const string UIVIEW_LOGIC_TEMPLATEPATH = "Assets/Scripts/Editor/UI/GenerateUI/Template/UIViewLogicTemplate.txt"; //UIView逻辑模板路径
    private const string UIVIEW_VIEW_TEMPLATEPATH = "Assets/Scripts/Editor/UI/GenerateUI/Template/UIViewViewTemplate.txt"; //UIView界面模板路径
    private const string UIVIEW_LOGIC_GENCODEDIR = "Assets/TempTest/UI/Logic/"; //自动生成UIView逻辑代码的文件夹
    private const string UIVIEW_VIEW_GENCODEDIR = "Assets/TempTest/UI/AutoGen/UIView/"; //自动生成UIView界面代码的文件夹

    private const string UISUBVIEW_VIEW_GENCODEDIR = "Assets/TempTest/UI/AutoGen/UISubView/"; //自动生成UISubView界面代码的文件夹

    private const string SUFFIX_CS = ".cs";
    private const string EXTRANAME_AUTOGEN = "Base";
    private const string PREFIX_UISUBVIEW = "UISubView";
    private const string PREFIX_UICONTAINER = "UIContainer";
    private const string FIELDDEFINE_TEMPLATE = "protected #FieldType# #FieldName#;"; //字段定义模板
    private const string FIELDBIND_TEMPLATE = "#FieldName# = go.transform.Find(\"#FieldPath#\").GetComponent<#ComponentType#>();"; //字段绑定模板

    private static GenUIData genUIData;

    [MenuItem("Assets/UI工具/生成UIView", false, 11)]
    private static void AutoGenUIViewCode()
    {
        GameObject selectedGo = Selection.activeObject as GameObject;
        GenUIViewCode(selectedGo);
    }

    [MenuItem("Assets/UI工具/生成UIView", true, 11)]
    private static bool ValidateAutoGenUIViewCode()
    {
        if (Selection.activeObject == null)
            return false;
        return PrefabUtility.GetPrefabAssetType(Selection.activeObject) != PrefabAssetType.NotAPrefab;
    }

    /// <summary>
    /// 生成UIView
    /// </summary>
    private static void GenUIViewCode(GameObject go)
    {
        genUIData = new GenUIData();
        genUIData.prefab = go;
        ClassData uiViewData = new ClassData();
        uiViewData.className = go.name;
        genUIData.uiViewData = uiViewData;

        //收集生成UI的数据
        Transform root = go.transform;
        CollectGenUIData(root, root, false);
        //生成UIView代码
        bool genSuccess_Logic = GenUIViewCode_Logic(genUIData.uiViewData);
        bool genSuccess_View = GenUIViewCode_View(genUIData.uiViewData);
        //生成UISubView代码
        foreach (var uiSubViewData in genUIData.uiSubViewDataDict.Values)
        {
        }

        AssetDatabase.Refresh();

        EditorUtility.DisplayDialog("生成UIView完成", $"预制体：{genUIData.prefab.name}\n保存在:xxx", "确认");
    }

    private static void CollectGenUIData(Transform targetTrans, Transform rootTrans, bool isSubView)
    {
        for (int i = 0; i < targetTrans.childCount; i++)
        {
            Transform trans = targetTrans.GetChild(i);
            string transName = trans.name;
            string namePrefix = transName.Split('_')[0];
            if (namePrefix == PREFIX_UICONTAINER)
            {
                //todo logic 后续加上，还有滚动列表相关的
                continue;
            }
            if (namePrefix == PREFIX_UISUBVIEW)
            {
                //UISubView的子物体只能是UISubView，UIView只有一个
                CollectGenUIData(trans, trans, true);
                continue;
            }
            CollectGenUIData(trans, rootTrans, isSubView);
            if (!Name2ComponentType.TryGetValue(namePrefix.ToLower(), out var type))
                continue;
            Component component = trans.GetComponent(type);
            if (component == null)
                continue;

            if (!isSubView)
            {
                if (genUIData.uiViewData.fieldDataDict.ContainsKey(transName))
                {
                    genUIData.errorStr.Append($"预制体{genUIData.prefab.name}中存在相同名字{transName}的{type.Name}组件\n");
                    continue;
                }
                FieldData fieldData = new FieldData();
                fieldData.name = transName;
                fieldData.type = type;
                fieldData.path = GameUtils.CalculateTransPath(trans, rootTrans);
                genUIData.uiViewData.fieldDataDict.Add(transName, fieldData);
            }
            else
            {
                string uiSubViewName = rootTrans.name;
                if (!genUIData.uiSubViewDataDict.TryGetValue(uiSubViewName, out var uiSubViewData))
                {
                    uiSubViewData = new ClassData();
                    uiSubViewData.className = uiSubViewName;
                    genUIData.uiSubViewDataDict.Add(uiSubViewName, uiSubViewData);
                }
                FieldData fieldData = new FieldData();
                fieldData.name = transName;
                fieldData.type = type;
                fieldData.path = GameUtils.CalculateTransPath(trans, rootTrans);
                uiSubViewData.fieldDataDict.Add(transName, fieldData);
            }
        }
    }

    /// <summary>
    /// 生成UIView逻辑代码
    /// </summary>
    private static bool GenUIViewCode_Logic(ClassData classData)
    {
        if (!IOUtils.FileExist(UIVIEW_LOGIC_TEMPLATEPATH))
        {
            EditorUtility.DisplayDialog("生成UIView逻辑脚本失败", $"{UIVIEW_LOGIC_TEMPLATEPATH}中不存在UIView逻辑模板：", "关闭");
            return false;
        }
        string logicCode = File.ReadAllText(UIVIEW_LOGIC_TEMPLATEPATH);
        logicCode = logicCode.Replace("#CLASSNAME#", classData.className);
        logicCode = logicCode.Replace("#BASECLASSNAME#", $"{classData.className}{EXTRANAME_AUTOGEN}");
        string csFilePath = $"{UIVIEW_LOGIC_GENCODEDIR}{classData.className}{SUFFIX_CS}";
        IOUtils.WirteToFile(csFilePath, logicCode);
        return true;
    }

    /// <summary>
    /// 生成UIView界面代码
    /// </summary>
    private static bool GenUIViewCode_View(ClassData classData)
    {
        if (!IOUtils.FileExist(UIVIEW_VIEW_TEMPLATEPATH))
        {
            EditorUtility.DisplayDialog("生成UIView界面脚本失败", $"{UIVIEW_VIEW_TEMPLATEPATH}中不存在UIView界面模板：", "关闭");
            return false;
        }
        string viewCode = File.ReadAllText(UIVIEW_VIEW_TEMPLATEPATH);
        viewCode = viewCode.Replace("#GENDATETIME#", GenCurDateTimeStr());
        viewCode = viewCode.Replace("#CLASSNAME#", classData.className);
        viewCode = viewCode.Replace("#FIELDDEFINECODE#", GenFieldDefineCode(classData.fieldDataDict.Values.ToList()));
        viewCode = viewCode.Replace("#FIELDBINDCODE#", GenFieldBindCode(classData.fieldDataDict.Values.ToList()));
        string csFilePath = $"{UIVIEW_VIEW_GENCODEDIR}{classData.className}{EXTRANAME_AUTOGEN}{SUFFIX_CS}";
        IOUtils.WirteToFile(csFilePath, viewCode);
        return true;
    }

    /// <summary>
    /// 生成字段定义代码
    /// </summary>
    private static string GenFieldDefineCode(List<FieldData> fieldDatas)
    {
        StringBuilder str = new StringBuilder();
        foreach (var fieldData in fieldDatas)
        {
            string fieldDefineStr = FIELDDEFINE_TEMPLATE;
            fieldDefineStr = fieldDefineStr.Replace("#FieldType#", fieldData.type.Name);
            fieldDefineStr = fieldDefineStr.Replace("#FieldName#", fieldData.name);
            str.Append(fieldDefineStr + "\n");
        }
        return str.ToString();
    }

    /// <summary>
    /// 生成字段绑定代码
    /// </summary>
    private static string GenFieldBindCode(List<FieldData> fieldDatas)
    {
        StringBuilder str = new StringBuilder();
        foreach (var fieldData in fieldDatas)
        {
            string fieldBindStr = FIELDBIND_TEMPLATE;
            fieldBindStr = fieldBindStr.Replace("#FieldName#", fieldData.name);
            fieldBindStr = fieldBindStr.Replace("#FieldPath#", fieldData.path);
            fieldBindStr = fieldBindStr.Replace("#ComponentType#", fieldData.type.Name);
            str.Append(fieldBindStr + "\n");
        }
        return str.ToString();
    }

    private static string GenCurDateTimeStr()
    {
        string dateTimeStr = DateTime.Now.ToString("yyyy-M-d H:m:s");
        return dateTimeStr;
    }
}