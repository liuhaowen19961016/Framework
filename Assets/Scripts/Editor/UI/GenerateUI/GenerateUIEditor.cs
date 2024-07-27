using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using Hotfix;
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

    private static string UIGENINFOARCHIVEPATH = Application.dataPath + "/../UIGenInfo.json"; //已经生成过的逻辑代码序列化成json存项目根目录下，防止覆盖生成逻辑代码

    //模板路径
    private const string UIVIEW_LOGIC_TEMPLATEPATH = "Assets/Scripts/Editor/UI/GenerateUI/Template/UIViewLogicTemplate.txt"; //UIView逻辑模板路径
    private const string UIVIEW_VIEW_TEMPLATEPATH = "Assets/Scripts/Editor/UI/GenerateUI/Template/UIViewViewTemplate.txt"; //UIView界面模板路径
    private const string UISUBVIEW_LOGIC_TEMPLATEPATH = "Assets/Scripts/Editor/UI/GenerateUI/Template/UISubViewLogicTemplate.txt"; //UISubView逻辑模板路径
    private const string UISUBVIEW_VIEW_TEMPLATEPATH = "Assets/Scripts/Editor/UI/GenerateUI/Template/UISubViewViewTemplate.txt"; //UISubView界面模板路径

    //生成代码文件夹
    private const string UIVIEW_LOGIC_GENCODEDIR = "Assets/TempTest/UI/Logic/View/"; //自动生成UIView逻辑代码的文件夹
    private const string UIVIEW_VIEW_GENCODEDIR = "Assets/TempTest/UI/AutoGen/View/"; //自动生成UIView界面代码的文件夹
    private const string UISUBVIEW_LOGIC_GENCODEDIR = "Assets/TempTest/UI/Logic/SubView/"; //自动生成UISubView逻辑代码的文件夹
    private const string UISUBVIEW_VIEW_GENCODEDIR = "Assets/TempTest/UI/AutoGen/SubView/"; //自动生成UISubView界面代码的文件夹

    private const string SUFFIX_CS = ".cs";
    private const string EXTRANAME_AUTOGEN = "Base";
    private const string PREFIX_UISUBVIEW = "UISubView";
    private const string PREFIX_UICONTAINER = "UIContainer";
    private const string NAMESPACEDEFINE_TEMPLATE = "using #Namespace#;"; //命名空间定义模板
    private const string FIELDDEFINE_TEMPLATE = "\tprotected #FieldType# #FieldName#;"; //字段定义模板
    private const string FIELDBIND_TEMPLATE = "\t\t#FieldName# = go.transform.Find(\"#FieldPath#\").GetComponent<#ComponentType#>();"; //字段绑定模板

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
        if (!IOUtils.FileExist(UIVIEW_VIEW_TEMPLATEPATH))
        {
            EditorUtility.DisplayDialog("生成UIView界面脚本失败", $"{UIVIEW_VIEW_TEMPLATEPATH}中不存在UIView界面模板：", "关闭");
            return;
        }
        GenUIViewCode_Logic(genUIData.uiViewData);
        GenUIViewCode_View(genUIData.uiViewData);
        //生成UISubView代码
        if (!IOUtils.FileExist(UISUBVIEW_VIEW_TEMPLATEPATH))
        {
            EditorUtility.DisplayDialog("生成UISubView界面脚本失败", $"{UISUBVIEW_VIEW_TEMPLATEPATH}中不存在UISubView界面模板：", "关闭");
            return;
        }
        foreach (var uiSubViewData in genUIData.uiSubViewDataDict.Values)
        {
            GenUISubViewCode_Logic(uiSubViewData);
            GenUISubViewCode_View(uiSubViewData);
        }

        AssetDatabase.Refresh();

        string genResultStr = string.Empty;
        genResultStr += $"UIView：{genUIData.uiViewData.className}\n";
        foreach (var uiSubViewData in genUIData.uiSubViewDataDict.Values)
        {
            genResultStr += $"UISubView：{uiSubViewData.className}\n";
        }
        genResultStr += "\n";
        genResultStr += genUIData.errorStr;
        EditorUtility.DisplayDialog("生成UIView完成", genResultStr, "确认");
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
                //UISubView的子物体只能是UISubView，UIView只能有一个
                CollectGenUIData(trans, trans, true);
                continue;
            }
            CollectGenUIData(trans, rootTrans, isSubView);
            if (!Name2ComponentType.TryGetValue(namePrefix.ToLower(), out var type))
                continue;
            Component component = trans.GetComponent(type);
            if (component == null)
            {
                genUIData.errorStr.Append($"预制体{genUIData.prefab.name}中的{transName}节点找不到{type.Name}组件\n");
                continue;
            }

            if (!isSubView)
            {
                if (genUIData.uiViewData.fieldDataDict.ContainsKey(transName))
                {
                    genUIData.errorStr.Append($"预制体{genUIData.prefab.name}中存在相同{transName}名字的{type.Name}组件\n");
                    continue;
                }
                genUIData.uiViewData.namespaceList.Add(type.Namespace);
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
                uiSubViewData.namespaceList.Add(type.Namespace);
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
    private static void GenUIViewCode_Logic(ClassData classData)
    {
        if (HasGenLogicCode(classData.className))
            return;
        string logicCode = File.ReadAllText(UIVIEW_LOGIC_TEMPLATEPATH);
        logicCode = logicCode.Replace("#CLASSNAME#", classData.className);
        logicCode = logicCode.Replace("#BASECLASSNAME#", $"{classData.className}{EXTRANAME_AUTOGEN}");
        string filePath = $"{UIVIEW_LOGIC_GENCODEDIR}{classData.className}{SUFFIX_CS}";
        IOUtils.WirteToFile(filePath, logicCode);
        SaveToGenUIInfoArchive(classData, filePath);
    }

    /// <summary>
    /// 生成UIView界面代码
    /// </summary>
    private static void GenUIViewCode_View(ClassData classData)
    {
        string viewCode = File.ReadAllText(UIVIEW_VIEW_TEMPLATEPATH);
        string className = $"{classData.className}{EXTRANAME_AUTOGEN}";
        viewCode = viewCode.Replace("#GENDATETIME#", GenCurDateTimeStr());
        //viewCode = viewCode.Replace("#NAMESPACE#", GenNamespaceCode(classData));
        viewCode = viewCode.Replace("#CLASSNAME#", className);
        viewCode = viewCode.Replace("#FIELDDEFINECODE#", GenFieldDefineCode(classData.fieldDataDict.Values.ToList()));
        viewCode = viewCode.Replace("#FIELDBINDCODE#", GenFieldBindCode(classData.fieldDataDict.Values.ToList()));
        string filePath = $"{UIVIEW_VIEW_GENCODEDIR}{className}{SUFFIX_CS}";
        IOUtils.WirteToFile(filePath, viewCode);
    }

    /// <summary>
    /// 生成UISubView逻辑代码
    /// </summary>
    private static void GenUISubViewCode_Logic(ClassData classData)
    {
        if (HasGenLogicCode(classData.className))
            return;
        string logicCode = File.ReadAllText(UISUBVIEW_LOGIC_TEMPLATEPATH);
        logicCode = logicCode.Replace("#CLASSNAME#", classData.className);
        logicCode = logicCode.Replace("#BASECLASSNAME#", $"{classData.className}{EXTRANAME_AUTOGEN}");
        string filePath = $"{UISUBVIEW_LOGIC_GENCODEDIR}{classData.className}{SUFFIX_CS}";
        IOUtils.WirteToFile(filePath, logicCode);
        SaveToGenUIInfoArchive(classData, filePath);
    }

    /// <summary>
    /// 生成UISubView界面代码
    /// </summary>
    private static void GenUISubViewCode_View(ClassData classData)
    {
        string viewCode = File.ReadAllText(UISUBVIEW_VIEW_TEMPLATEPATH);
        string className = $"{classData.className}{EXTRANAME_AUTOGEN}";
        viewCode = viewCode.Replace("#GENDATETIME#", GenCurDateTimeStr());
        viewCode = viewCode.Replace("#NAMESPACE#", GenNamespaceCode(classData));
        viewCode = viewCode.Replace("#CLASSNAME#", className);
        viewCode = viewCode.Replace("#FIELDDEFINECODE#", GenFieldDefineCode(classData.fieldDataDict.Values.ToList()));
        viewCode = viewCode.Replace("#FIELDBINDCODE#", GenFieldBindCode(classData.fieldDataDict.Values.ToList()));
        string filePath = $"{UISUBVIEW_VIEW_GENCODEDIR}{className}{SUFFIX_CS}";
        IOUtils.WirteToFile(filePath, viewCode);
    }

    /// <summary>
    /// 生成命名空间定义代码
    /// </summary>
    private static string GenNamespaceCode(ClassData classData)
    {
        StringBuilder str = new StringBuilder();
        HashSet<string> hashSet = new HashSet<string>(classData.namespaceList);
        foreach (var nameSpace in hashSet)
        {
            string namespaceDefineStr = NAMESPACEDEFINE_TEMPLATE;
            namespaceDefineStr = namespaceDefineStr.Replace("#Namespace#", nameSpace);
            str.Append(namespaceDefineStr + "\n");
        }
        return str.ToString();
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

    private static bool HasGenLogicCode(string className)
    {
        if (!IOUtils.FileExist(UIGENINFOARCHIVEPATH))
            return false;
        string content = File.ReadAllText(UIGENINFOARCHIVEPATH);
        List<GenUIArchive> genUIArchives = JsonConvert.DeserializeObject<List<GenUIArchive>>(content);
        if (genUIArchives == null)
            return false;
        var hasGen = genUIArchives.Exists(v => v.className == className);
        return hasGen;
    }

    private static void SaveToGenUIInfoArchive(ClassData classData, string logicCodePath)
    {
        List<GenUIArchive> genUIArchives;
        if (!IOUtils.FileExist(UIGENINFOARCHIVEPATH))
        {
            genUIArchives = new List<GenUIArchive>();
        }
        else
        {
            string str = File.ReadAllText(UIGENINFOARCHIVEPATH);
            genUIArchives = JsonConvert.DeserializeObject<List<GenUIArchive>>(str);
        }
        if (genUIArchives == null)
            return;
        GenUIArchive genUIArchive = new GenUIArchive();
        genUIArchive.className = classData.className;
        genUIArchive.filePath = logicCodePath;
        genUIArchives.Add(genUIArchive);
        string content = JsonConvert.SerializeObject(genUIArchives);
        IOUtils.WirteToFile(UIGENINFOARCHIVEPATH, content);
    }
}