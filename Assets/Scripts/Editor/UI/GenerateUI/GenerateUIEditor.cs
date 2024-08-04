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
    //**********只有按照以下规范命名的节点才会生成，可扩展
    private static Dictionary<string, Type> Name2ComponentType = new Dictionary<string, Type>()
    {
        { "UIBtn", typeof(Button) },
        { "UITxt", typeof(Text) },
        //{ "UITxt", typeof(TextMeshProUGUI) },
        { "UIImg", typeof(Image) },
        { "UIRawImg", typeof(RawImage) },
        { "UIToggle", typeof(Toggle) },
        { "UITG", typeof(ToggleGroup) },
        { "UILayoutH", typeof(HorizontalLayoutGroup) },
        { "UILayooutV", typeof(VerticalLayoutGroup) },
        { "UILayoutHV", typeof(GridLayoutGroup) },
        { "UIIF", typeof(InputField) },
        { "UISlider", typeof(Slider) },
        { "UINode", typeof(RectTransform) },
    };

    private static GenUIData genUIData;
    private static List<string> genSuccessClassNameList = new List<string>();
    private static List<string> genFailClassNameList = new List<string>();

    [MenuItem("Assets/UI工具/生成UIView", false, 11)]
    private static void GenUIViewCode()
    {
        GameObject selectedGo = Selection.activeObject as GameObject;
        GenUIViewCode(selectedGo);
    }

    [MenuItem("Assets/UI工具/生成UIView", true, 11)]
    private static bool ValidateGenUIViewCode()
    {
        GameObject selectedGo = Selection.activeGameObject;
        if (selectedGo == null || selectedGo.GetComponent<RectTransform>() == null)
            return false;
        return PrefabUtility.GetPrefabAssetType(Selection.activeObject) != PrefabAssetType.NotAPrefab;
    }

    [MenuItem("Assets/UI工具/生成UISubView", false, 12)]
    private static void GenUISubViewCode()
    {
        GameObject selectedGo = Selection.activeObject as GameObject;
        GenUISubViewCode(selectedGo);
    }

    [MenuItem("Assets/UI工具/生成UISubView", true, 12)]
    private static bool ValidateGenUISubViewCode()
    {
        GameObject selectedGo = Selection.activeGameObject;
        if (selectedGo == null || selectedGo.GetComponent<RectTransform>() == null)
            return false;
        return PrefabUtility.GetPrefabAssetType(Selection.activeObject) != PrefabAssetType.NotAPrefab;
    }

    /// <summary>
    /// 生成UIView
    /// </summary>
    private static void GenUIViewCode(GameObject go)
    {
        genSuccessClassNameList.Clear();
        genFailClassNameList.Clear();

        genUIData = new GenUIData();
        genUIData.prefab = go;
        ClassData uiViewData = new ClassData();
        uiViewData.className = go.name;
        genUIData.uiViewData = uiViewData;

        //收集生成UI的数据
        Transform root = go.transform;
        CollectGenUIData(genUIData, root, root, EGenUIType.View);
        //生成UIView代码
        GenUIViewCode_Logic(genUIData.uiViewData);
        GenUIViewCode_View(genUIData.uiViewData);
        //生成UISubView代码
        foreach (var uiSubViewData in genUIData.uiSubViewDataDict.Values)
        {
            GenUISubViewCode_Logic(uiSubViewData);
            GenUISubViewCode_View(uiSubViewData);
        }

        AssetDatabase.Refresh();

        EditorUtils.ShowDialogWindow("生成UIView完成", GenResultStr(), "确定");
    }

    /// <summary>
    /// 生成UISubView
    /// </summary>
    private static void GenUISubViewCode(GameObject go)
    {
        genSuccessClassNameList.Clear();
        genFailClassNameList.Clear();

        genUIData = new GenUIData();
        genUIData.prefab = go;
        genUIData.uiViewData = null;

        //收集生成UI的数据
        Transform root = go.transform;
        CollectGenUIData(genUIData, root, root, EGenUIType.SubView);
        //生成UISubView代码
        foreach (var uiSubViewData in genUIData.uiSubViewDataDict.Values)
        {
            GenUISubViewCode_Logic(uiSubViewData);
            GenUISubViewCode_View(uiSubViewData);
        }

        AssetDatabase.Refresh();

        EditorUtils.ShowDialogWindow("生成UISubView完成", GenResultStr(), "确定");
    }

    private static void CollectGenUIData(GenUIData genUIData, Transform targetTrans, Transform rootTrans, EGenUIType genUIType)
    {
        for (int i = 0; i < targetTrans.childCount; i++)
        {
            Transform trans = targetTrans.GetChild(i);
            string transName = trans.name;
            string namePrefix = transName.Split('_')[0];
            if (namePrefix == EditorConst.PREFIX_UICONTAINER)
            {
                //todo logic 后续加上，还有滚动列表相关的
                continue;
            }
            if (namePrefix == EditorConst.PREFIX_UISUBVIEW)
            {
                AddGenUIData(genUIData, genUIType, EGenUIFieldType.SubView, trans, null, trans.name, rootTrans);
                CollectGenUIData(genUIData, trans, trans, EGenUIType.SubView); //UISubView的子物体只能是UISubView，UIView只能有一个
                continue;
            }

            CollectGenUIData(genUIData, trans, rootTrans, genUIType);
            if (!Name2ComponentType.TryGetValue(namePrefix, out var type))
                continue;
            Component component = trans.GetComponent(type);
            if (component == null)
            {
                genUIData.errorStr.Append($"预制体{genUIData.prefab.name}中的{transName}节点找不到{type.Name}组件\n");
                continue;
            }
            AddGenUIData(genUIData, genUIType, EGenUIFieldType.Common, trans, type, type.Name, rootTrans);
        }
    }

    /// <summary>
    /// 添加生成UI数据
    /// </summary>
    private static void AddGenUIData(GenUIData genUIData, EGenUIType genUIType, EGenUIFieldType genUIFieldType, Transform trans, Type type, string typeName, Transform rootTrans)
    {
        string transName = trans.name;
        if (!RegexUtils.IsCSValidName(transName))
        {
            genUIData.errorStr.Append($"预制体{genUIData.prefab.name}中的{transName}节点命名不符合C#规则\n");
            return;
        }
        if (genUIType == EGenUIType.View)
        {
            AddFieldData(genUIData.uiViewData, genUIFieldType, trans, type, typeName, rootTrans);
        }
        else if (genUIType == EGenUIType.SubView)
        {
            string uiSubViewName = rootTrans.name;
            if (!genUIData.uiSubViewDataDict.TryGetValue(uiSubViewName, out var uiSubViewData))
            {
                uiSubViewData = new ClassData();
                uiSubViewData.className = uiSubViewName;
                genUIData.uiSubViewDataDict.Add(uiSubViewName, uiSubViewData);
            }
            AddFieldData(uiSubViewData, genUIFieldType, trans, type, typeName, rootTrans);
        }
    }

    /// <summary>
    /// 添加字段数据
    /// </summary>
    private static void AddFieldData(ClassData classData, EGenUIFieldType genUIFieldType, Transform trans, Type type, string typeName, Transform rootTrans)
    {
        string transName = trans.name;
        if (classData.fieldDataDict.ContainsKey(transName))
        {
            genUIData.errorStr.Append($"预制体{genUIData.prefab.name}中存在相同{transName}名字的{typeName}组件\n");
            return;
        }
        if (type != null)
        {
            classData.namespaceList.Add(type.Namespace);
        }
        FieldData fieldData = new FieldData();
        fieldData.GenUIFieldType = genUIFieldType;
        fieldData.fieldName = transName;
        fieldData.fieldTypeName = typeName;
        fieldData.fieldPath = GameUtils.CalculateTransPath(trans, rootTrans);
        classData.fieldDataDict.Add(transName, fieldData);
    }

    /// <summary>
    /// 生成UIView逻辑代码
    /// </summary>
    private static void GenUIViewCode_Logic(ClassData classData)
    {
        if (!IOUtils.FileExist(EditorConst.UIVIEW_LOGIC_TEMPLATE_PATH))
        {
            genUIData.errorStr.Append($"{EditorConst.UIVIEW_LOGIC_TEMPLATE_PATH}中不存在UIView逻辑模板");
            genFailClassNameList.Add(classData.className);
            return;
        }
        if (HasGenLogicCode(classData.className))
            return;
        string logicCode = File.ReadAllText(EditorConst.UIVIEW_LOGIC_TEMPLATE_PATH);
        logicCode = logicCode.Replace("#CLASSNAME#", classData.className);
        logicCode = logicCode.Replace("#BASECLASSNAME#", $"{classData.className}{EditorConst.EXTRANAME_AUTOGEN}");
        string filePath = $"{EditorConst.UIVIEW_LOGIC_GENCODE_DIR}{classData.className}{EditorConst.SUFFIX_CS}";
        IOUtils.WirteToFile(filePath, logicCode);
        SaveToGenUIInfoArchive(classData, filePath);
        genSuccessClassNameList.Add(classData.className);
    }

    /// <summary>
    /// 生成UIView界面代码
    /// </summary>
    private static void GenUIViewCode_View(ClassData classData)
    {
        string className = $"{classData.className}{EditorConst.EXTRANAME_AUTOGEN}";
        if (!IOUtils.FileExist(EditorConst.UIVIEW_VIEW_TEMPLATE_PATH))
        {
            genUIData.errorStr.Append($"{EditorConst.UIVIEW_VIEW_TEMPLATE_PATH}中不存在UIView界面模板");
            genFailClassNameList.Add(className);
            return;
        }
        string viewCode = File.ReadAllText(EditorConst.UIVIEW_VIEW_TEMPLATE_PATH);
        viewCode = viewCode.Replace("#GENDATETIME#", GenCurDateTimeStr());
        viewCode = viewCode.Replace("#NAMESPACE#", GenNamespaceCode(classData));
        viewCode = viewCode.Replace("#CLASSNAME#", className);
        viewCode = viewCode.Replace("#FIELDDEFINECODE#", GenFieldDefineCode(classData.fieldDataDict.Values.ToList()));
        viewCode = viewCode.Replace("#FIELDBINDCODE#", GenFieldBindCode(classData.fieldDataDict.Values.ToList()));
        string filePath = $"{EditorConst.UIVIEW_VIEW_GENCODE_DIR}{className}{EditorConst.SUFFIX_CS}";
        IOUtils.WirteToFile(filePath, viewCode);
        genSuccessClassNameList.Add(className);
    }

    /// <summary>
    /// 生成UISubView逻辑代码
    /// </summary>
    private static void GenUISubViewCode_Logic(ClassData classData)
    {
        if (!IOUtils.FileExist(EditorConst.UISUBVIEW_LOGIC_TEMPLATE_PATH))
        {
            genUIData.errorStr.Append($"{EditorConst.UISUBVIEW_LOGIC_TEMPLATE_PATH}中不存在UISubView逻辑模板");
            genFailClassNameList.Add(classData.className);
            return;
        }
        if (HasGenLogicCode(classData.className))
            return;
        string logicCode = File.ReadAllText(EditorConst.UISUBVIEW_LOGIC_TEMPLATE_PATH);
        logicCode = logicCode.Replace("#CLASSNAME#", classData.className);
        logicCode = logicCode.Replace("#BASECLASSNAME#", $"{classData.className}{EditorConst.EXTRANAME_AUTOGEN}");
        string filePath = $"{EditorConst.UISUBVIEW_LOGIC_GENCODE_DIR}{classData.className}{EditorConst.SUFFIX_CS}";
        IOUtils.WirteToFile(filePath, logicCode);
        SaveToGenUIInfoArchive(classData, filePath);
        genSuccessClassNameList.Add(classData.className);
    }

    /// <summary>
    /// 生成UISubView界面代码
    /// </summary>
    private static void GenUISubViewCode_View(ClassData classData)
    {
        string className = $"{classData.className}{EditorConst.EXTRANAME_AUTOGEN}";
        if (!IOUtils.FileExist(EditorConst.UISUBVIEW_VIEW_TEMPLATE_PATH))
        {
            genUIData.errorStr.Append($"{EditorConst.UISUBVIEW_VIEW_TEMPLATE_PATH}中不存在UISubView界面模板");
            genFailClassNameList.Add(className);
            return;
        }
        string viewCode = File.ReadAllText(EditorConst.UISUBVIEW_VIEW_TEMPLATE_PATH);
        viewCode = viewCode.Replace("#GENDATETIME#", GenCurDateTimeStr());
        viewCode = viewCode.Replace("#NAMESPACE#", GenNamespaceCode(classData));
        viewCode = viewCode.Replace("#CLASSNAME#", className);
        viewCode = viewCode.Replace("#FIELDDEFINECODE#", GenFieldDefineCode(classData.fieldDataDict.Values.ToList()));
        viewCode = viewCode.Replace("#FIELDBINDCODE#", GenFieldBindCode(classData.fieldDataDict.Values.ToList()));
        string filePath = $"{EditorConst.UISUBVIEW_VIEW_GENCODE_DIR}{className}{EditorConst.SUFFIX_CS}";
        IOUtils.WirteToFile(filePath, viewCode);
        genSuccessClassNameList.Add(className);
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
            string namespaceDefineStr = EditorConst.NAMESPACE_DEFINE_TEMPLATE;
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
            string fieldDefineStr = EditorConst.FIELD_DEFINE_TEMPLATE;
            fieldDefineStr = fieldDefineStr.Replace("#FieldType#", fieldData.fieldTypeName);
            fieldDefineStr = fieldDefineStr.Replace("#FieldName#", fieldData.fieldName);
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
            string fieldBindStr = string.Empty;
            switch (fieldData.GenUIFieldType)
            {
                case EGenUIFieldType.Common:
                    fieldBindStr = EditorConst.COMMON_FIELD_BIND_TEMPLATE;
                    fieldBindStr = fieldBindStr.Replace("#FieldName#", fieldData.fieldName);
                    fieldBindStr = fieldBindStr.Replace("#FieldPath#", fieldData.fieldPath);
                    fieldBindStr = fieldBindStr.Replace("#ComponentType#", fieldData.fieldTypeName);
                    str.Append(fieldBindStr + "\n");
                    break;

                case EGenUIFieldType.SubView:
                    fieldBindStr = EditorConst.SUBVIEW_FIELD_BIND_TEMPLATE;
                    fieldBindStr = fieldBindStr.Replace("#FieldName#", fieldData.fieldName);
                    fieldBindStr = fieldBindStr.Replace("#FieldPath#", fieldData.fieldPath);
                    str.Append(fieldBindStr + "\n");
                    break;
            }
        }
        return str.ToString();
    }

    private static string GenCurDateTimeStr()
    {
        string dateTimeStr = DateTime.Now.ToString("yyyy-M-d H:m:s");
        return dateTimeStr;
    }

    private static string GenResultStr()
    {
        string genResultStr = string.Empty;
        genResultStr += "生成成功：\n";
        foreach (var className in genSuccessClassNameList)
        {
            genResultStr += className + "\n";
        }
        genResultStr += "\n生成失败：\n";
        foreach (var className in genFailClassNameList)
        {
            genResultStr += className + "\n";
        }
        genResultStr += "\n错误信息：\n";
        genResultStr += genUIData.errorStr;
        return genResultStr;
    }

    private static bool HasGenLogicCode(string className)
    {
        if (!IOUtils.FileExist(EditorConst.UIGENINFOARCHIVEPATH))
            return false;
        string content = File.ReadAllText(EditorConst.UIGENINFOARCHIVEPATH);
        List<GenUIArchive> genUIArchives = JsonConvert.DeserializeObject<List<GenUIArchive>>(content);
        if (genUIArchives == null)
            return false;
        var hasGen = genUIArchives.Exists(v => v.className == className);
        return hasGen;
    }

    private static void SaveToGenUIInfoArchive(ClassData classData, string logicCodePath)
    {
        List<GenUIArchive> genUIArchives;
        if (!IOUtils.FileExist(EditorConst.UIGENINFOARCHIVEPATH))
        {
            genUIArchives = new List<GenUIArchive>();
        }
        else
        {
            string str = File.ReadAllText(EditorConst.UIGENINFOARCHIVEPATH);
            genUIArchives = JsonConvert.DeserializeObject<List<GenUIArchive>>(str);
        }
        if (genUIArchives == null)
            return;
        GenUIArchive genUIArchive = new GenUIArchive();
        genUIArchive.className = classData.className;
        genUIArchive.filePath = logicCodePath;
        genUIArchives.Add(genUIArchive);
        string content = JsonConvert.SerializeObject(genUIArchives);
        IOUtils.WirteToFile(EditorConst.UIGENINFOARCHIVEPATH, content);
    }
}