using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

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
    /// 获取指定路径中预制体下的某个组件列表
    /// </summary>
    public static List<T> GetComponents<T>(string[] paths)
        where T : Component
    {
        List<T> components = new List<T>();
        foreach (var path in paths)
        {
            string[] assetGUIDs = AssetDatabase.FindAssets("t:prefab", new[] { path });
            foreach (var assetGUID in assetGUIDs)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(assetGUID);
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
                var coms = prefab.GetComponentsInChildren<T>();
                components.AddRange(coms);
            }
        }
        return components;
    }
}