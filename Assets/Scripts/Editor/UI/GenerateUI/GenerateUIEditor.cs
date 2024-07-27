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
    private const string UIGENINFOFILEPATH = "../UIGenInfo.json"; //已经生成过逻辑view序列化成json存项目根目录下，防止覆盖生成逻辑View
    private const string UIVIEW_LOGIC_TEMPLATEPATH = "Assets/Scripts/Editor/UI/GenerateUI/Template/UIViewLogicTemplate.txt"; //UIView逻辑模板路径
    private const string UIVIEW_VIEW_TEMPLATEPATH = "Assets/Scripts/Editor/UI/GenerateUI/Template/UIViewViewTemplate.txt"; //UIView界面模板路径
    private const string UIVIEW_VIEW_GENCODEDIR = "Assets/Scripts/Hotfix/UI/AutoGen/"; //自动生成UIView代码的文件夹

    private const string FIELDDEFINE_TEMPLATE = "protected #FieldType# #FieldName#;"; //字段定义模板
    private const string FIELDBIND_TEMPLATE = "#FieldName# = go.transform.Find(#FieldPath#).GetComponent<#ComponentType#>();"; //字段绑定模板
    private const string SUFFIX_CS = ".cs";

    // private static List<UIGenInfo> genInfos;
    // private static StringBuilder fieldStr = new StringBuilder();
    // private static StringBuilder fieldBindStr = new StringBuilder();
    // private static List<string> fieldNameStrList = new List<string>();

    private static GenUIData genUIData;
    private static StringBuilder errorStr = new StringBuilder();

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
        ClearCache();
        genUIData = new GenUIData();

        Transform root = go.transform;
        string className = go.name;

        //收集生成UI的数据
        CollectGenUIData(root, root, false);
        //生成UIView界面代码
        var genUIViewData = genUIData.uiViewData;
        if (!File.Exists(UIVIEW_VIEW_TEMPLATEPATH))
        {
            EditorUtility.DisplayDialog("生成UIView界面脚本失败", $"{UIVIEW_VIEW_TEMPLATEPATH}中不存在UIView界面模板：", "关闭");
            return;
        }
        string viewCode = File.ReadAllText(UIVIEW_VIEW_TEMPLATEPATH);
        viewCode = viewCode.Replace("#GENDATETIME#", DateTime.Now.ToLongDateString());
        viewCode = viewCode.Replace("#CLASSNAME#", className);
        viewCode = viewCode.Replace("#FIELDDEFINECODE#", GenFieldDefineCode(genUIViewData.fieldDataDict.Values.ToList()));
        viewCode = viewCode.Replace("#FIELDBINDCODE#", GenFieldBindCode(genUIViewData.fieldDataDict.Values.ToList()));
        string uiViewViewPath = UIVIEW_VIEW_GENCODEDIR + genUIViewData.className + SUFFIX_CS;
        IOUtils.WirteToFile(uiViewViewPath, viewCode);
        //判断是否已经生成过逻辑view
        bool needGenViewLogic = true;
        // if (!IOUtils.FileExist(UIGENINFOFILEPATH))
        // {
        //     needGenViewLogic = false;
        //     genInfos = new List<UIGenInfo>();
        // }
        // else
        // {
        //     string uiGenInfoConente = File.ReadAllText(UIGENINFOFILEPATH);
        //     genInfos = JsonConvert.DeserializeObject<List<UIGenInfo>>(content);
        //     needGenViewLogic = !genInfos.Any(info => info.name == className);
        // }
        // string uiLogicViewDir = "";
        // uiLogicViewDir = EditorUtility.OpenFolderPanel("选择UIViewLogic的存储路径", uiLogicViewDir, "");
        //
        // genInfos.Add(new UIGenInfo() { name = className, });
        //创建脚本
        // path = EditorUtility.OpenFilePanel("选择文件", path, ".xls");
        // 

        //todo 判断是否存在
       
        // Debug.LogError("filedStr: " + fieldStr);
        // Debug.LogError("fieldBindStr " + fieldBindStr);
    }

    private static void CollectGenUIData(Transform targetTrans, Transform rootTrans, bool isSubView)
    {
        for (int i = 0; i < targetTrans.childCount; i++)
        {
            Transform trans = targetTrans.GetChild(i);
            string transName = trans.name;
            CollectGenUIData(trans, rootTrans, isSubView);
            string namePrefix = transName.Split('_')[0];
            if (namePrefix == "UISubView")
            {
                CollectGenUIData(trans, rootTrans, true);
                continue;
            }
            if (!Name2ComponentType.TryGetValue(namePrefix.ToLower(), out var type))
                continue;
            Component component = trans.GetComponent(type);
            if (component == null)
                continue;

            if (!isSubView)
            {
                if (genUIData.uiViewData != null && genUIData.uiViewData.fieldDataDict.ContainsKey(transName))
                {
                    errorStr.Append($"预制体{Selection.activeObject.name}中存在相同名字{transName}的{type.Name}组件\n");
                    continue;
                }
                ClassData classData;
                if (genUIData.uiViewData == null)
                {
                    classData = new ClassData();
                    genUIData.uiViewData = classData;
                }
                else
                {
                    classData = genUIData.uiViewData;
                }
                FieldData fieldData = new FieldData();
                fieldData.name = transName;
                fieldData.type = type;
                fieldData.path = GameUtils.CalculateTransPath(trans, rootTrans);
                classData.fieldDataDict.Add(transName, fieldData);
            }
        }
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

    private static void ClearCache()
    {
        errorStr.Clear();
    }

    private void SaveGenInfoFile()
    {
        // if (!File.Exists(UIGENINFOFILEPATH))
        // {
        //     genInfos = new List<UIGenInfo>();
        // }
        // else
        // {
        // }
        // IOUtils.
    }

    //**********只有按照以下规范命名的节点才会生成，不区分大小写，可扩展
    private static Dictionary<string, Type> Name2ComponentType = new Dictionary<string, Type>()
    {
        { "uibtn", typeof(Button) },
        { "uitxt", typeof(Text) },
        //{ "uitxt", typeof(TextMeshProUGUI) },
        { "uiimg", typeof(Image) },
        { "uirawimage", typeof(RawImage) },
        { "uitoggle", typeof(Toggle) },
        { "uitg", typeof(ToggleGroup) },
        { "uilayouth", typeof(HorizontalLayoutGroup) },
        { "uilayoutv", typeof(VerticalLayoutGroup) },
        { "uilayouthv", typeof(GridLayoutGroup) },
        { "uiif", typeof(InputField) },
    };
}