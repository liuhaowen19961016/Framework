using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI工具
/// </summary>
public class UIToolEditor
{
    public const string UIVIEW_TEMPLATE_PATH = "Assets/Scripts/"; //UIView模板路径

    private static StringBuilder fieldStr = new StringBuilder();
    private static StringBuilder fieldBindStr = new StringBuilder();
    private static List<string> fieldNameStrList = new List<string>();

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

        CollectGenInfo(root, root);

        Debug.LogError("filedStr: " + fieldStr);
        Debug.LogError("fieldBindStr " + fieldBindStr);
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
            if (fieldNameStrList.Contains(transName))
            {
                Debug.LogError($"预制体{Selection.activeObject.name}中存在相同名字的组件：{transName}");
                continue;
            }
            Component component = trans.GetComponent(type);
            if (component == null)
                continue;
            fieldNameStrList.Add(transName);
            fieldStr.Append($"  protected {type.Name} {transName}\n");
            string fieldBindPath = GameUtils.CalculateTransPath(trans, rootTrans);
            string fieldBindPathStr = $" {transName} = go.transform.Find(\"{fieldBindPath}\").GetComponent<{component.name}>();\n";
            fieldBindStr.Append(fieldBindPathStr);
        }
    }

    private static void ClearCache()
    {
        fieldStr.Clear();
        fieldBindStr.Clear();
        fieldNameStrList.Clear();
    }

    //**********只有按照以下规范命名的节点才会生成，可扩展
    private static Dictionary<string, Type> Name2ComponentType = new Dictionary<string, Type>()
    {
        { "uibtn", typeof(Button) },
    };
}