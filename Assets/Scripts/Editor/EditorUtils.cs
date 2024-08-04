using System;
using UnityEditor;

public static class EditorUtils
{
    /// <summary>
    /// 显示对话框
    /// </summary>
    public static void ShowDialogWindow(string title, string content, string btn1Name,
        string btn2Name = "", string btn3Name = "", Action onBtn1 = null, Action onBtn2 = null, Action onBtn3 = null)
    {
        CommonDialogWindow.CreateWindow(title, content, btn1Name, btn2Name, btn3Name, onBtn1, onBtn2, onBtn3);
    }

    /// <summary>
    /// 获取选择的所有路径
    /// </summary>
    public static string[] GetSelectPathArray()
    {
        string[] assetGUIDs = Selection.assetGUIDs;
        string[] assetPaths = new string[assetGUIDs.Length];
        for (int i = 0; i < assetGUIDs.Length; i++)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(assetGUIDs[i]);
            assetPaths[i] = assetPath;
        }
        return assetPaths;
    }
    
    /// <summary>
    /// 获取当前时间
    /// </summary>
    /// <returns></returns>
    public static string GenCurDateTimeStr()
    {
        string dateTimeStr = DateTime.Now.ToString("yyyy-M-d H:m:s");
        return dateTimeStr;
    }
}