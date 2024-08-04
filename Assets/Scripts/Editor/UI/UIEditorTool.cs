using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class UIEditorTool
{
    /// <summary>
    ///  替换组件 Button->GameButton
    /// </summary>
    [MenuItem(EditorConst.ReplaceButton2GameButton, false, 100)]
    private static void ReplaceButton2GameButton()
    {
        try
        {
            string[] selectPathArray = EditorUtils.GetSelectPathArray();
            for (int i = 0; i < selectPathArray.Length; i++)
            {
                string[] assetGUIDs = AssetDatabase.FindAssets("t:prefab", new[] { selectPathArray[i] });
                for (int j = 0; j < assetGUIDs.Length; j++)
                {
                    string prefabPath = AssetDatabase.GUIDToAssetPath(assetGUIDs[j]);
                    GameObject instance = PrefabUtility.LoadPrefabContents(prefabPath);
                    Button[] buttons = instance.GetComponentsInChildren<Button>(true);
                    for (int k = 0; k < buttons.Length; k++)
                    {
                        if (EditorUtility.DisplayCancelableProgressBar("Button替换为GameButton", $"正在替换{prefabPath}中的{buttons[i]}", (k + 1) * 1f / buttons.Length))
                        {
                            EditorUtility.ClearProgressBar();
                            return;
                        }
                        Transform componentTrans = buttons[k].transform;
                        Object.DestroyImmediate(buttons[k]);
                        componentTrans.gameObject.AddComponent<GameButton>();
                    }
                    PrefabUtility.SaveAsPrefabAsset(instance, prefabPath);
                    PrefabUtility.UnloadPrefabContents(instance);
                }
            }
            AssetDatabase.Refresh();
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            EditorUtility.ClearProgressBar();
        }
        finally
        {
            EditorUtility.ClearProgressBar();
        }
    }
    [MenuItem(EditorConst.ReplaceButton2GameButton, true, 100)]
    private static bool ValidateReplaceButton2GameButton()
    {
        string[] assetGUIDs = Selection.assetGUIDs;
        if (assetGUIDs == null || assetGUIDs.Length <= 0)
            return false;
        return true;
    }

    /// <summary>
    /// 生成UIViewName
    /// </summary>
    [MenuItem(EditorConst.GenUIViewName, false, 200)]
    private static void GenUIViewName()
    {
        if (!IOUtils.FileExist(EditorConst.UIVIEWNAME_TEMPLATE_PATH))
        {
            EditorUtils.ShowDialogWindow("生成失败", $"{EditorConst.UIVIEWNAME_TEMPLATE_PATH}中不存在UIViewName模板", "确定");
            return;
        }
        StringBuilder str = new StringBuilder();
        foreach (var uiViewCfg in UIViewTemp.UIViewConfigs.Values) //todo test
        {
            string uiViewNameDefineStr = EditorConst.UIVIEWNAME_DEFINE_TEMPLATE;
            uiViewNameDefineStr = uiViewNameDefineStr.Replace("#VIEWNAME#", uiViewCfg.Path);
            uiViewNameDefineStr = uiViewNameDefineStr.Replace("#VIEWID#", uiViewCfg.Id.ToString());
            str.Append(uiViewNameDefineStr + "\n");
        }
        string uiViewNmaeCode = File.ReadAllText(EditorConst.UIVIEWNAME_TEMPLATE_PATH);
        uiViewNmaeCode = uiViewNmaeCode.Replace("#GENDATETIME#", EditorUtils.GenCurDateTimeStr());
        uiViewNmaeCode = uiViewNmaeCode.Replace("#UIVIEWNAMEDEFINE#", str.ToString());
        string filePath = $"{EditorConst.UIVIEWNAME_GENCODE_PATH}{EditorConst.SUFFIX_CS}";
        IOUtils.WirteToFile(filePath, uiViewNmaeCode);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtils.ShowDialogWindow("生成成功", $"生成路径\n{filePath}", "确定");
    }
}