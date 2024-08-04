using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class UIEditorTool
{
    [MenuItem("Assets/UI工具/预制体中的Button替换为GameButton", false, 100)]
    private static void ReplaceButton2GameButton()
    {
        try
        {
            string[] selectPathArray = EditorUtils.GetSelectPathArray();
            var components = EditorUtils.GetComponents<Button>(selectPathArray);
            for (int i = 0; i < components.Count; i++)
            {
                if (EditorUtility.DisplayCancelableProgressBar("替换Button -> GameButton", $"正在替换{components[i].name}", (i + 1) * 1f / components.Count))
                {
                    EditorUtility.ClearProgressBar();
                    return;
                }
                Transform componentTrans = components[i].transform;
                Object.Destroy(components[i]);
                componentTrans.gameObject.AddComponent<GameButton>();
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        catch (Exception e)
        {
            EditorUtility.ClearProgressBar();
        }
        finally
        {
            EditorUtility.ClearProgressBar();
        }
    }

    [MenuItem("Assets/UI工具/选择的路径下所有预制体中的Button替换为GameButton", true, 100)]
    private static bool ValidateReplaceButton2GameButton()
    {
        string[] assetGUIDs = Selection.assetGUIDs;
        if (assetGUIDs == null || assetGUIDs.Length <= 0)
            return false;
        return true;
    }
}

// [MenuItem("Assets/UI工具/当前文件夹下的所有预制体中的Button替换为GameButton", true, 20)]
// private static bool ValidateReplaceButton2GameButton()
// {
//     GameObject selectedGo = Selection.activeGameObject;
//     if (selectedGo == null || selectedGo.GetComponent<RectTransform>() == null)
//         return false;
//     return PrefabUtility.GetPrefabAssetType(Selection.activeObject) != PrefabAssetType.NotAPrefab;
// }