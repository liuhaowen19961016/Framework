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
                    if (buttons == null || buttons.Length <= 0)
                        continue;
                    bool hasModify = false;
                    for (int k = 0; k < buttons.Length; k++)
                    {
                        if (buttons[k].GetType() != typeof(Button))
                            continue;
                        if (EditorUtility.DisplayCancelableProgressBar("Button替换为GameButton", $"正在替换{prefabPath}中的{buttons[i]}", (k + 1) * 1f / buttons.Length))
                        {
                            EditorUtility.ClearProgressBar();
                            return;
                        }
                        hasModify = true;
                        Transform componentTrans = buttons[k].transform;
                        Object.DestroyImmediate(buttons[k]);
                        componentTrans.gameObject.AddComponent<GameButton>();
                    }
                    if (hasModify)
                    {
                        PrefabUtility.SaveAsPrefabAsset(instance, prefabPath);
                    }
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

    private const string UIVIEWNAME_TEMPLATE_PATH = "Assets/Scripts/Editor/UI/GenerateUI/Template/UIViewNameTemplate.txt"; //UIViewName模板路径
    private const string UIVIEWNAME_GENCODE_PATH = "Assets/Scripts/Hotfix/Logic/UI/UIViewName"; //自动生成UIViewName的文件夹
    private const string UIVIEWNAME_DEFINE_TEMPLATE = "\tpublic const int #VIEWNAME# = #VIEWID#;\n"; //界面名称定义模板
    /// <summary>
    /// 生成UIViewName
    /// </summary>
    [MenuItem(EditorConst.GenUIViewName, false, 210)]
    private static void GenUIViewName()
    {
        if (!IOUtils.FileExist(UIVIEWNAME_TEMPLATE_PATH))
        {
            EditorUtils.ShowDialogWindow("生成失败", $"{UIVIEWNAME_TEMPLATE_PATH}中不存在UIViewName模板", "确定");
            return;
        }
        StringBuilder str = new StringBuilder();
        foreach (var uiViewCfg in UIViewTemp.UIViewConfigs.Values) //todo test
        {
            string uiViewNameDefineStr = UIVIEWNAME_DEFINE_TEMPLATE;
            uiViewNameDefineStr = uiViewNameDefineStr.Replace("#VIEWNAME#", uiViewCfg.Path);
            uiViewNameDefineStr = uiViewNameDefineStr.Replace("#VIEWID#", uiViewCfg.Id.ToString());
            str.Append(uiViewNameDefineStr + "\n");
        }
        string uiViewNmaeCode = File.ReadAllText(UIVIEWNAME_TEMPLATE_PATH);
        uiViewNmaeCode = uiViewNmaeCode.Replace("#GENDATETIME#", EditorUtils.GenCurDateTimeStr());
        uiViewNmaeCode = uiViewNmaeCode.Replace("#UIVIEWNAMEDEFINE#", str.ToString());
        string filePath = $"{UIVIEWNAME_GENCODE_PATH}{EditorConst.SUFFIX_CS}";
        IOUtils.WirteToFile(filePath, uiViewNmaeCode);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtils.ShowDialogWindow("生成成功", $"生成路径\n{filePath}", "确定");
    }

    /// <summary>
    /// 打开生成UI信息文件夹
    /// </summary>
    [MenuItem(EditorConst.OpenGenUIInfoDir, false, 200)]
    public static void OpenGenUIInfoDir()
    {
        IOUtils.OpenFolder(Application.dataPath + "/../", GenerateUIEditor.UIGENINFOARCHIVEPATH);
    }
}