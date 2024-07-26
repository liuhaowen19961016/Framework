using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

/// <summary>
/// UI工具
/// </summary>
public class UIToolEditor
{
    private const string UIVIEW_TEMPLATE_PATH = "Assets/Scripts/Editor/UI/Template/UIViewTemplate.txt"; //UIView模板路径

    private const string UIVIEW_GENSCRIPT_DIR = "Assets/Scripts/Hotfix/UI/AutoGen"; //自动生成UIView脚本的路径

    private const string FIELDSTR_TEMPLATE = "protected {0} {1};"; //字段模板
    private const string FIELDBINDPATHSTR_TEMPLATE = " {0} = go.transform.Find(\"{1}\").GetComponent<{2}>();"; //字段绑定模板

    private static StringBuilder fieldStr = new StringBuilder();
    private static StringBuilder fieldBindStr = new StringBuilder();
    private static List<string> fieldNameStrList = new List<string>();
    private static StringBuilder errorStr = new StringBuilder();

    [MenuItem("Assets/UI工具/自动生成UIView脚本", false, 11)]
    private static void AutoGenUIViewScript()
    {
        GameObject selectedGo = Selection.activeObject as GameObject;
        GenUIViewScript(selectedGo);
    }

    [MenuItem("Assets/UI工具/自动生成UIView脚本", true, 11)]
    private static bool ValidateAutoGenUIViewScript()
    {
        if (Selection.activeObject == null)
            return false;
        return PrefabUtility.GetPrefabAssetType(Selection.activeObject) != PrefabAssetType.NotAPrefab;
    }

    /// <summary>
    /// 生成UIView脚本
    /// </summary>
    private static void GenUIViewScript(GameObject go)
    {
        ClearCache();

        Transform root = go.transform;

        //收集数据
        CollectGenInfo(root, root);
        //编辑脚本内容
        if (!File.Exists(UIVIEW_TEMPLATE_PATH))
        {
            EditorUtility.DisplayDialog("生成UIView脚本失败", $"UIView模板路径错误：{UIVIEW_TEMPLATE_PATH}", "关闭");
            return;
        }
        string content = File.ReadAllText(UIVIEW_TEMPLATE_PATH);
        content = content.Replace("#GENDATETIME#", DateTime.Now.ToString());
        content = content.Replace("#CLASSNAME#", go.name);
        content = content.Replace("#FIELDSTR#", fieldStr.ToString());
        content = content.Replace("#FIELDBINDSTR#", fieldBindStr.ToString());
        //创建脚本
        //todo 判断是否存在
        if (!Directory.Exists(UIVIEW_GENSCRIPT_DIR))
        {
            Directory.CreateDirectory(UIVIEW_GENSCRIPT_DIR);
        }
        // Debug.LogError("filedStr: " + fieldStr);
        // Debug.LogError("fieldBindStr " + fieldBindStr);
    }

    private static void CollectGenInfo(Transform targetTrans, Transform rootTrans)
    {
        for (int i = 0; i < targetTrans.childCount; i++)
        {
            Transform trans = targetTrans.GetChild(i);
            CollectGenInfo(trans, rootTrans);
            string transName = trans.name;
            string transPreSuffix = transName.Split('_')[0];
            if (transPreSuffix == "UISubView")
            {
                //todo 生成子界面脚本
                break;
            }
            if (!Name2ComponentType.TryGetValue(transPreSuffix.ToLower(), out var type))
                continue;
            Component component = trans.GetComponent(type);
            if (component == null)
                continue;
            if (fieldNameStrList.Contains(transName))
            {
                errorStr.Append($"预制体{Selection.activeObject.name}中存在相同名字{transName}的{type.Name}组件\n");
                continue;
            }
            fieldNameStrList.Add(transName);
            fieldStr.Append(string.Format(FIELDSTR_TEMPLATE, type.Name, transName) + "\n");
            string fieldBindPath = GameUtils.CalculateTransPath(trans, rootTrans);
            string fieldBindPathStr = string.Format(FIELDBINDPATHSTR_TEMPLATE, transName, fieldBindPath, type.Name) + "\n";
            // Debug.LogError(fieldBindPathStr);
            fieldBindStr.Append(fieldBindPathStr);
        }
    }

    private static void ClearCache()
    {
        fieldStr.Clear();
        fieldBindStr.Clear();
        fieldNameStrList.Clear();
        errorStr.Clear();
    }

    //**********只有按照以下规范命名的节点才会生成，可扩展
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